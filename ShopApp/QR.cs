using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.LinkLabel;

public class QR
{
    // Matricea care conține numărul de biți pentru diferitele moduri și versiuni
    private static readonly int[][] LENGTH_BITS = new int[][]
    {
        new int[] { 10, 12, 14 }, // Mode: Numeric
        new int[] { 9, 11, 13 }, // Mode: Alphanumeric
        new int[] { 8, 16, 16 }, // Mode: Byte
        new int[] { 8, 10, 12 }  // Mode: Kanji
    };
    // Funcțiile masca pentru matricea QR
    public static readonly Func<decimal, decimal, bool>[] MASK_FNS = new Func<decimal,decimal, bool>[]
    {
            (row, column) => ((row + column) %2) == 0,
            (row, column) => (row % 2) == 0,
            (row, column) => (column % 3) == 0,
            (row, column) => (row + column) % 3 == 0,
            (row, column) => (Math.Floor(row/2) + Math.Floor(column/2)) == 0,
            (row, column) => (row*column%2 + row*column%3) == 0,
            (row, column) => (((row*column)%2 + row*column%3)%2) == 0,
            (row, column) => (((row+column)%2 + row*column%3)%2) == 0,
    };
    // Tabelul de corecție a erorilor pentru codul de corecție a erorilor (EC)
    public static readonly List<Dictionary<char, int[]>> EC_TABLE = new List<Dictionary<char, int[]>>
    {
        new Dictionary<char, int[]> { { 'L', new int[] { 7, 1 } },   { 'M', new int[] { 10, 1 } },  { 'Q', new int[] { 13, 1 } },  { 'H', new int[] { 17, 1 } } },
        new Dictionary<char, int[]> { { 'L', new int[] { 10, 1 } },  { 'M', new int[] { 16, 1 } },  { 'Q', new int[] { 22, 1 } },  { 'H', new int[] { 28, 1 } } },
        new Dictionary<char, int[]> { { 'L', new int[] { 15, 1 } },  { 'M', new int[] { 26, 1 } },  { 'Q', new int[] { 18, 2 } },  { 'H', new int[] { 22, 2 } } },
        new Dictionary<char, int[]> { { 'L', new int[] { 20, 1 } },  { 'M', new int[] { 18, 2 } },  { 'Q', new int[] { 26, 2 } },  { 'H', new int[] { 16, 4 } } },
        new Dictionary<char, int[]> { { 'L', new int[] { 26, 1 } },  { 'M', new int[] { 24, 2 } },  { 'Q', new int[] { 18, 4 } },  { 'H', new int[] { 22, 4 } } },
        new Dictionary<char, int[]> { { 'L', new int[] { 18, 2 } },  { 'M', new int[] { 16, 4 } },  { 'Q', new int[] { 24, 4 } },  { 'H', new int[] { 28, 4 } } },
        new Dictionary<char, int[]> { { 'L', new int[] { 20, 2 } },  { 'M', new int[] { 18, 4 } },  { 'Q', new int[] { 18, 6 } },  { 'H', new int[] { 26, 5 } } },
        new Dictionary<char, int[]> { { 'L', new int[] { 24, 2 } },  { 'M', new int[] { 22, 4 } },  { 'Q', new int[] { 22, 6 } },  { 'H', new int[] { 26, 6 } } },
        new Dictionary<char, int[]> { { 'L', new int[] { 30, 2 } },  { 'M', new int[] { 22, 5 } },  { 'Q', new int[] { 20, 8 } },  { 'H', new int[] { 24, 8 } } },
        new Dictionary<char, int[]> { { 'L', new int[] { 18, 4 } },  { 'M', new int[] { 26, 5 } },  { 'Q', new int[] { 24, 8 } },  { 'H', new int[] { 28, 8 } } },
        new Dictionary<char, int[]> { { 'L', new int[] { 20, 4 } },  { 'M', new int[] { 30, 5 } },  { 'Q', new int[] { 28, 8 } },  { 'H', new int[] { 24, 11 } } },
        new Dictionary<char, int[]> { { 'L', new int[] { 24, 4 } },  { 'M', new int[] { 22, 8 } },  { 'Q', new int[] { 26, 10 } }, { 'H', new int[] { 28, 11 } } },
        new Dictionary<char, int[]> { { 'L', new int[] { 26, 4 } },  { 'M', new int[] { 22, 9 } },  { 'Q', new int[] { 24, 12 } }, { 'H', new int[] { 22, 16 } } },
        new Dictionary<char, int[]> { { 'L', new int[] { 30, 4 } },  { 'M', new int[] { 24, 9 } },  { 'Q', new int[] { 20, 16 } }, { 'H', new int[] { 24, 16 } } },
        new Dictionary<char, int[]> { { 'L', new int[] { 22, 6 } },  { 'M', new int[] { 24, 10 } }, { 'Q', new int[] { 30, 12 } }, { 'H', new int[] { 24, 18 } } },
        new Dictionary<char, int[]> { { 'L', new int[] { 24, 6 } },  { 'M', new int[] { 28, 10 } }, { 'Q', new int[] { 24, 17 } }, { 'H', new int[] { 30, 16 } } },
        new Dictionary<char, int[]> { { 'L', new int[] { 28, 6 } },  { 'M', new int[] { 28, 11 } }, { 'Q', new int[] { 28, 16 } }, { 'H', new int[] { 28, 19 } } },
        new Dictionary<char, int[]> { { 'L', new int[] { 30, 6 } },  { 'M', new int[] { 26, 13 } }, { 'Q', new int[] { 28, 18 } }, { 'H', new int[] { 28, 21 } } },
        new Dictionary<char, int[]> { { 'L', new int[] { 28, 7 } },  { 'M', new int[] { 26, 14 } }, { 'Q', new int[] { 26, 21 } }, { 'H', new int[] { 26, 25 } } },
        new Dictionary<char, int[]> { { 'L', new int[] { 28, 8 } },  { 'M', new int[] { 26, 16 } }, { 'Q', new int[] { 30, 20 } }, { 'H', new int[] { 28, 25 } } },
        new Dictionary<char, int[]> { { 'L', new int[] { 28, 8 } },  { 'M', new int[] { 26, 17 } }, { 'Q', new int[] { 28, 23 } }, { 'H', new int[] { 30, 25 } } },
        new Dictionary<char, int[]> { { 'L', new int[] { 28, 9 } },  { 'M', new int[] { 28, 17 } }, { 'Q', new int[] { 30, 23 } }, { 'H', new int[] { 24, 34 } } },
        new Dictionary<char, int[]> { { 'L', new int[] { 30, 9 } },  { 'M', new int[] { 28, 18 } }, { 'Q', new int[] { 30, 25 } }, { 'H', new int[] { 30, 30 } } },
        new Dictionary<char, int[]> { { 'L', new int[] { 30, 10 } }, { 'M', new int[] { 28, 20 } }, { 'Q', new int[] { 30, 27 } }, { 'H', new int[] { 30, 32 } } },
        new Dictionary<char, int[]> { { 'L', new int[] { 26, 12 } }, { 'M', new int[] { 28, 21 } }, { 'Q', new int[] { 30, 29 } }, { 'H', new int[] { 30, 35 } } },
        new Dictionary<char, int[]> { { 'L', new int[] { 28, 12 } }, { 'M', new int[] { 28, 23 } }, { 'Q', new int[] { 28, 34 } }, { 'H', new int[] { 30, 37 } } },
        new Dictionary<char, int[]> { { 'L', new int[] { 30, 12 } }, { 'M', new int[] { 28, 25 } }, { 'Q', new int[] { 30, 34 } }, { 'H', new int[] { 30, 40 } } },
        new Dictionary<char, int[]> { { 'L', new int[] { 30, 13 } }, { 'M', new int[] { 28, 26 } }, { 'Q', new int[] { 30, 35 } }, { 'H', new int[] { 30, 42 } } },
        new Dictionary<char, int[]> { { 'L', new int[] { 30, 14 } }, { 'M', new int[] { 28, 28 } }, { 'Q', new int[] { 30, 38 } }, { 'H', new int[] { 30, 45 } } },
        new Dictionary<char, int[]> { { 'L', new int[] { 30, 15 } }, { 'M', new int[] { 28, 29 } }, { 'Q', new int[] { 30, 40 } }, { 'H', new int[] { 30, 48 } } },
        new Dictionary<char, int[]> { { 'L', new int[] { 30, 16 } }, { 'M', new int[] { 28, 31 } }, { 'Q', new int[] { 30, 43 } }, { 'H', new int[] { 30, 51 } } },
        new Dictionary<char, int[]> { { 'L', new int[] { 30, 17 } }, { 'M', new int[] { 28, 33 } }, { 'Q', new int[] { 30, 45 } }, { 'H', new int[] { 30, 54 } } },
        new Dictionary<char, int[]> { { 'L', new int[] { 30, 18 } }, { 'M', new int[] { 28, 35 } }, { 'Q', new int[] { 30, 48 } }, { 'H', new int[] { 30, 57 } } },
        new Dictionary<char, int[]> { { 'L', new int[] { 30, 19 } }, { 'M', new int[] { 28, 37 } }, { 'Q', new int[] { 30, 51 } }, { 'H', new int[] { 30, 60 } } },
        new Dictionary<char, int[]> { { 'L', new int[] { 30, 19 } }, { 'M', new int[] { 28, 38 } }, { 'Q', new int[] { 30, 53 } }, { 'H', new int[] { 30, 63 } } },
        new Dictionary<char, int[]> { { 'L', new int[] { 30, 20 } }, { 'M', new int[] { 28, 40 } }, { 'Q', new int[] { 30, 56 } }, { 'H', new int[] { 30, 66 } } },
        new Dictionary<char, int[]> { { 'L', new int[] { 30, 21 } }, { 'M', new int[] { 28, 43 } }, { 'Q', new int[] { 30, 59 } }, { 'H', new int[] { 30, 70 } } },
        new Dictionary<char, int[]> { { 'L', new int[] { 30, 22 } }, { 'M', new int[] { 28, 45 } }, { 'Q', new int[] { 30, 62 } }, { 'H', new int[] { 30, 74 } } },
        new Dictionary<char, int[]> { { 'L', new int[] { 30, 24 } }, { 'M', new int[] { 28, 47 } }, { 'Q', new int[] { 30, 65 } }, { 'H', new int[] { 30, 77 } } },
        new Dictionary<char, int[]> { { 'L', new int[] { 30, 25 } }, { 'M', new int[] { 28, 49 } }, { 'Q', new int[] { 30, 68 } }, { 'H', new int[] { 30, 81 } } }
    };

    // Tabele pentru logaritm și exponențiala Galois în corecția erorilor
    private byte[] LOG = new byte[256];
    private byte[] EXP = new byte[256];

    // Indicatorii pentru versiune și format în codul QR
    private static byte[] VERSION_DIVISOR = new byte[] { 1, 1, 1, 1, 1, 0, 0, 1, 0, 0, 1, 0, 1 };
    private static readonly string EDC_ORDER = "MLHQ";

    // Divizorii pentru formatul și mascarea în formatul QR
    private static readonly byte[] FORMAT_DIVISOR = new byte[] { 1, 0, 1, 0, 0, 1, 1, 0, 1, 1, 1 };
    private static readonly byte[] FORMAT_MASK = new byte[] { 1, 0, 1, 0, 1, 0, 0, 0, 0, 0, 1, 0, 0, 1, 0 };

    // Regulile și sabloanele pentru mascarea formatului
    static readonly byte[] RULE_3_PATTERN = new byte[] { 1, 0, 1, 1, 1, 0, 1, 0, 0, 0, 0 };
    static readonly byte[] RULE_3_REVERSED_PATTERN = RULE_3_PATTERN.Reverse().ToArray();

    // Inițializarea LOG și EXP pentru calculul rapid
    public QR()
    {
        int exponent = 1;
        int value = 1;
        // Calculul logaritmilor și exponențialei Galois pentru toți cei 256 de octeți
        for (; exponent < 256; exponent++)
        {
            value = value > 127 ? ((value << 1) ^ 285) : value << 1;
            LOG[value] = (byte)(exponent % 255);
            EXP[exponent % 255] = (byte)value;

        }
    }
    // Returnează modul Byte implicit pentru QR
    private int GetEncodingMode(string str)
    {
        return 0b0100;
    }
    // Funcție pentru determinarea numărului de biți în funcție de mod și versiune
    private int GetLengthBits(int mode, int version)
    {
        int modeIndex = 31 - CountLeadingZeros(mode);
        int bitsIndex = version > 26 ? 2 : version > 9 ? 1 : 0;
        return LENGTH_BITS[modeIndex][bitsIndex];
    }
    // alculează numărul de biți zero din partea superioară a unui număr întreg
    private int CountLeadingZeros(int value)
    {
        if (value == 0)
        {
            return 32;
        }

        int count = 0;
        while ((value & 0x80000000) == 0)
        {
            count++;
            value <<= 1;
        }

        return count;
    }
    // // Obține dimensiunea matricei QR în funcție de versiune
    private int GetSize(int version)
    {
        return version * 4 + 17;
    }
    // Inițializează o matrice nouă pentru matricea QR în funcție de versiune
    private byte[][] GetNewMatrix(int version)
    {
        int length = GetSize(version);
        byte[][] matrix = new byte[length][];
        for (int i = 0; i < length; i++)
        {
            matrix[i] = new byte[length];
        }
        return matrix;
    }
    //Generează polinomul de corecție a erorilor(EDC) pentru datele specificate
    private byte[] GetEDC(byte[] data, int codewords)
    {
        int degree = codewords - data.Length;
        byte[] messagePoly = new byte[codewords];
        Array.Copy(data, messagePoly, data.Length);
        byte[] generatorPoly = GetGeneratorPoly(degree);
        return PolyRest(messagePoly, generatorPoly);
    }
    // Funcție pentru a umple o zonă a matricei cu un anumit tip de byte (1 sau 0)
    private void FillArea(byte[][] matrix, int row, int column, int width, int height, byte fill = 1)
    {
        byte[] fillRow = new byte[width];
        for (int i = 0; i < width; i++)
        {
            fillRow[i] = fill;
        }

        for (int i = row; i < row + height; i++)
        {
            Array.Copy(fillRow, 0, matrix[i], column, width);
        }
    }
    // Funcție pentru înmulțirea a două valori în corpul Galois (GF(256))
    public byte Mul(byte a, byte b)
    {
        return (byte)(a != 0 && b != 0 ? EXP[(LOG[a] + LOG[b]) % 255] : 0);
    }
    // Funcție pentru a împărți două valori în corpul Galois (GF(256))
    public byte Div(byte a, byte b)
    {
        return EXP[(LOG[a] + LOG[b] * 254) % 255];
    }
    // Înmulțește două polinoame date
    private byte[] PolyMul(byte[] poly1, byte[] poly2)
    {
        int length = poly1.Length + poly2.Length - 1;
        byte[] coeffs = new byte[length];

        for (int index = 0; index < length; index++)
        {
            byte coeff = 0;
            for (int p1index = 0; p1index <= index; p1index++)
            {
                int p2index = index - p1index;
                if (p1index < poly1.Length && p2index < poly2.Length)
                {
                    coeff ^= Mul(poly1[p1index], poly2[p2index]);
                }
            }
            coeffs[index] = coeff;
        }

        return coeffs;
    }
    // Efectuează operațiile de rest polinomial pentru corecția erorilor (EDC)
    private byte[] PolyRest(byte[] dividend, byte[] divisor)
    {
        int quotientLength = dividend.Length - divisor.Length + 1;
        byte[] rest = new byte[dividend.Length];
        Array.Copy(dividend, rest, dividend.Length);
        for (int count = 0; count < quotientLength; count++)
        {
            if (rest[0] != 0)
            {
                byte factor = Div(rest[0], divisor[0]);
                byte[] subtr = PolyMul(divisor, new byte[] { factor });
                byte[] tempRest = new byte[rest.Length];
                for (int i = 0; i < rest.Length; i++)
                {
                    if (i < subtr.Length)
                        tempRest[i] = (byte)(rest[i] ^ subtr[i]);
                    else
                        tempRest[i] = rest[i];
                }
                rest = tempRest.Skip(1).ToArray();
            }
            else
            {
                rest = rest.Skip(1).ToArray();
            }
        }
        return rest;
    }
    //Generează polinomul generator pentru codurile corectoare de erori bazat pe gradul specificat.
    private byte[] GetGeneratorPoly(int degree)
    {
        byte[] lastPoly = new byte[] { 1 };
        for (int index = 0; index < degree; index++)
        {
            lastPoly = PolyMul(lastPoly, new byte[] { 1, EXP[index] });
        }
        return lastPoly;
    }
    // Obține modulele de format pentru codul QR, în funcție de nivelul de eroare și indexul măștii.
    private byte[] GetFormatModules(char errorLevel, int maskIndex)
    {
        byte[] formatPoly = new byte[15];
        int errorLevelIndex = EDC_ORDER.IndexOf(errorLevel);

        // Setează modulele de format bazate pe nivelul de eroare și indexul măștii
        formatPoly[0] = (byte)(errorLevelIndex >> 1);
        formatPoly[1] = (byte)(errorLevelIndex & 1);
        formatPoly[2] = (byte)(maskIndex >> 2);
        formatPoly[3] = (byte)((maskIndex >> 1) & 1);
        formatPoly[4] = (byte)(maskIndex & 1);

        byte[] rest = PolyRest(formatPoly, FORMAT_DIVISOR);
        Array.Copy(rest, 0, formatPoly, 5, rest.Length);

        // Aplică masca la modulele de format

        byte[] maskedFormatPoly = new byte[formatPoly.Length];
        for (int i = 0; i < formatPoly.Length; i++)
        {
            maskedFormatPoly[i] = (byte)(formatPoly[i] ^ FORMAT_MASK[i]);
        }

        return maskedFormatPoly;
    }
    // Plasează modulele de format în matricea QR specificată, în funcție de nivelul de eroare și indexul măștii.
    private void PlaceFormatModules(byte[][] matrix, char errorLevel, int maskIndex)
    {
        byte[] formatModules = GetFormatModules(errorLevel, maskIndex);
        int size = matrix.Length;

        // Informația de format din colțul stânga-sus
        Array.Copy(formatModules, 0, matrix[8], 0, 6);
        Array.Copy(formatModules, 6, matrix[8], 7, 2);
        Array.Copy(formatModules, 7, matrix[8], size - 8, formatModules.Length - 7);

        matrix[7][8] = formatModules[8];

        // Informația de format din colțul stânga-jos
        for (int i = 0; i < 7; i++)
        {
            matrix[size - i - 1][8] = formatModules[i];
        }

        // Informația de format din colțul dreapta-sus
        for (int i = 9; i < formatModules.Length; i++)
        {
            matrix[5 - (i - 9)][8] = formatModules[i];
        }
    }
    // Obține matricea QR mascată
    private byte[][] GetMaskedMatrix(int version, byte[] codewords, int maskIndex)
    {
        List<Tuple<int, int>> sequence = GetModuleSequence(version);
        byte[][] matrix = GetNewMatrix(version);
        int index = 0;
        // Construiește matricea QR mascată
        foreach (byte codeword in codewords)
        {
            for (int shift = 7; shift >= 0; shift--)
            {

                int bit = (byte)(codeword >> shift) & 1;
                Tuple<int, int> coords = sequence[index];
                matrix[coords.Item1][coords.Item2] = (byte)(bit ^ (MASK_FNS[maskIndex](coords.Item1, coords.Item2) ? 1 : 0));
                index++;
            }
        }

        return matrix;
    }
    //Calculează penalizarea pentru o linie dată din matricea QR
    private int GetLinePenalty(byte[] line)
    {
        int count = 0;
        byte counting = 0;
        int penalty = 0;

        foreach (var cell in line)
        {
            if (cell != counting)
            {
                counting = cell;
                count = 1;
            }
            else
            {
                count++;
                if (count == 5)
                {
                    penalty += 3;
                }
                else if (count > 5)
                {
                    penalty++;
                }
            }
        }
        return penalty;
    }
    // Calculează scorul total de penalizare pentru matricea QR dată
    private int GetPenaltyScore(byte[][] matrix)
    {
        int totalPenalty = 0;

        // Regula 1: Penalizarea liniilor
        int rowPenalty = matrix.Sum(row => GetLinePenalty(row));
        totalPenalty += rowPenalty;

        int columnPenalty = Enumerable.Range(0, matrix[0].Length)
            .Select(columnIndex =>
            {
                var column = Enumerable.Range(0, matrix.Length)
                    .Select(row => matrix[row][columnIndex])
                    .ToArray();
                return GetLinePenalty(column);
            }).Sum();
        totalPenalty += columnPenalty;

        // Regula 2: Penalizarea coloanelor
        int blocks = 0;
        int size = matrix.Length;
        for (int row = 0; row < size - 1; row++)
        {
            for (int column = 0; column < size - 1; column++)
            {
                byte module = matrix[row][column];
                if (matrix[row][column + 1] == module &&
                    matrix[row + 1][column] == module &&
                    matrix[row + 1][column + 1] == module)
                {
                    blocks++;
                }
            }
        }
        totalPenalty += blocks * 3;

        // Regula 3: Penalizarea pentru modulele identice în linie sau coloană
        int patterns = 0;
        for (int index = 0; index < size; index++)
        {
            var row = matrix[index];
            for (int columnIndex = 0; columnIndex < size - 11; columnIndex++)
            {
                if (new[] { RULE_3_PATTERN, RULE_3_REVERSED_PATTERN }.Any(pattern =>
                    pattern.Select((cell, ptr) => cell == row[columnIndex + ptr]).All(x => x)))
                {
                    patterns++;
                }
            }
            for (int rowIndex = 0; rowIndex < size - 11; rowIndex++)
            {
                if (new[] { RULE_3_PATTERN, RULE_3_REVERSED_PATTERN }.Any(pattern =>
                    pattern.Select((cell, ptr) => cell == matrix[rowIndex + ptr][index]).All(x => x)))
                {
                    patterns++;
                }
            }
        }
        totalPenalty += patterns * 40;

        // Regula 4: Penalizarea pentru procentul de module negre
        int totalModules = size * size;
        int darkModules = matrix.Sum(line => line.Sum(cell => cell));
        double percentage = (double)darkModules * 100 / totalModules;
        int mixPenalty = Math.Abs((int)(Math.Truncate(percentage / 5) - 10)) * 10;

        return totalPenalty + mixPenalty;
    }
    // Obține masca optimă pentru matricea QR dată,
    private Tuple<byte[][], int> GetOptimalMask(int version, byte[] codewords, char errorLevel)
    {
        byte[][] bestMatrix = null;
        int bestScore = int.MaxValue;
        int bestMask = -1;

        for (int index = 0; index < 8; index++)
        {
            byte[][] matrix = GetMaskedQRCode(version, codewords, errorLevel, index);
            int penaltyScore = GetPenaltyScore(matrix);

            if (penaltyScore < bestScore)
            {
                bestScore = penaltyScore;
                bestMatrix = matrix;
                bestMask = index;
            }

        }

        return Tuple.Create(bestMatrix, bestMask);
    }
    // Returnează o secvență de module pentru o anumită versiune de QR Code.
    private List<Tuple<int, int>> GetModuleSequence(int version)
    {
        byte[][] matrix = GetNewMatrix(version);
        int size = GetSize(version);

        // Modelele de căutare + divizori
        FillArea(matrix, 0, 0, 9, 9);  // Modelul de căutare principal
        FillArea(matrix, 0, size - 8, 8, 9); // Divizor
        FillArea(matrix, size - 8, 0, 9, 8); // Divizor

        // Modele de aliniere
        List<int> alignmentTracks = GetAlignmentTracks(version); // Obține traseele de aliniere pentru versiunea specificată
        int lastTrack = alignmentTracks.Count - 1;

        foreach (int r in alignmentTracks)
        {
            foreach (int c in alignmentTracks)
            {
                // Se sare peste modelele de aliniere aproape de modelele de căutare
                if ((r == alignmentTracks[0] && (c == alignmentTracks[0] || c == alignmentTracks[lastTrack]))
                    || (c == alignmentTracks[0] && r == alignmentTracks[lastTrack]))
                {
                    continue;
                }
                FillArea(matrix, r - 2, c - 2, 5, 5);  // Umple zona cu model de aliniere
            }
        }

        // Modele de temporizare
        FillArea(matrix, 6, 9, version * 4, 1);
        FillArea(matrix, 9, 6, 1, version * 4);

        // Modul negru
        matrix[size - 8][8] = 1;

        // Informații despre versiune
        if (version > 6)
        {
            FillArea(matrix, 0, size - 11, 3, 6);
            FillArea(matrix, size - 11, 0, 6, 3);
        }

        // Generarea secvenței de module
        int rowStep = -1;
        int row = size - 1;
        int column = size - 1;
        List<Tuple<int, int>> sequence = new List<Tuple<int, int>>();
        int index = 0;

        while (column >= 0)
        {
            if (matrix[row][column] == 0)
            {
                sequence.Add(Tuple.Create(row, column));
            }

            // Checking the parity of the index of the current module
            if ((index & 1) == 1)
            {
                row += rowStep;

                if (row == -1 || row == size)
                {
                    rowStep = -rowStep;
                    row += rowStep;
                    column -= (column == 7) ? 2 : 1;
                }
                else
                {
                    column++;
                }
            }
            else
            {
                column--;
            }

            index++;
        }

        return sequence;
    }
    // Plasează modelele fixe în matricea QR Code pentru o anumită versiune
    public void PlaceFixedPatterns(byte[][] matrix, int version)
    {
        int size = matrix.GetLength(0);

        // Modele de căutare
        int[][] finderPositions = new int[][] { new int[] { 0, 0 }, new int[] { size - 7, 0 }, new int[] { 0, size - 7 } };
        foreach (var pos in finderPositions)
        {
            FillArea(matrix, pos[0], pos[1], 7, 7);
            FillArea(matrix, pos[0] + 1, pos[1] + 1, 5, 5, 0);
            FillArea(matrix, pos[0] + 2, pos[1] + 2, 3, 3);
        }

        // Separatoare
        FillArea(matrix, 7, 0, 8, 1, 0);
        FillArea(matrix, 0, 7, 1, 7, 0);
        FillArea(matrix, size - 8, 0, 8, 1, 0);
        FillArea(matrix, 0, size - 8, 1, 7, 0);
        FillArea(matrix, 7, size - 8, 8, 1, 0);
        FillArea(matrix, size - 7, 7, 1, 7, 0);

        // Modele de aliniere
        List<int> alignmentTracks = GetAlignmentTracks(version);
        int lastTrack = alignmentTracks.Count - 1;
        foreach (int r in alignmentTracks)
        {
            foreach (int c in alignmentTracks)
            {
                // Se sare peste modelele de aliniere aproape de modelele de căutare
                if ((r == alignmentTracks[0] && (c == alignmentTracks[0] || c == alignmentTracks[lastTrack]))
                    || (c == alignmentTracks[0] && r == alignmentTracks[lastTrack]))
                {
                    continue;
                }
                FillArea(matrix, r - 2, c - 2, 5, 5);
                FillArea(matrix, r - 1, c - 1, 3, 3, 0);
                matrix[r][c] = 1;
            }
        }

        // Modele de temporizare
        for (int pos = 8; pos < size - 9; pos += 2)
        {
            matrix[6][pos] = 1;
            matrix[6][pos + 1] = 0;
            matrix[pos][6] = 1;
            matrix[pos + 1][6] = 0;
        }
        matrix[6][size - 7] = 1;
        matrix[size - 7][6] = 1;

        // Modul negru
        matrix[size - 8][8] = 1;
    }
    // Obține matricea QR Code mascată
    public byte[][] GetMaskedQRCode(int version, byte[] codewords, char errorLevel, int maskIndex)
    {
        byte[][] matrix = GetMaskedMatrix(version, codewords, maskIndex);
        PlaceFormatModules(matrix, errorLevel, maskIndex);
        PlaceFixedPatterns(matrix, version);
        PlaceVersionModules(matrix);
        return matrix;
    }
    
    // Returnează numărul de module disponibile pentru o anumită versiune de QR Code
    private int GetAvailableModules(int version)
    {
        if (version == 1)
        {
            return 21 * 21 - 3 * 8 * 8 - 2 * 15 - 1 - 2 * 5;
        }

        int alignmentCount = (int)Math.Floor(version / 7.0) + 2;
        int totalModules = (int)Math.Pow(version * 4 + 17, 2);
        int alignmentDeduction = (int)Math.Pow(alignmentCount, 2) - 3;
        int additionalModules = (alignmentCount - 2) * 5 * 2;
        int extraVersionDeduction = version > 6 ? 2 * 3 * 6 : 0;

        int availableModules = totalModules
                               - 3 * 8 * 8
                               - alignmentDeduction * 5 * 5
                               - 2 * (version * 4 + 1)
                               + additionalModules
                               - 2 * 15
                               - 1
                               - extraVersionDeduction;

        return availableModules;
    }
    // Returneaza numarul de module cu informatie
    private int GetDataCodewords(int version, char errorLevel)
    {
        int totalCodewords = GetAvailableModules(version) >> 3;
        int[] ecTableEntry = EC_TABLE[version - 1][errorLevel];
        int blocks = ecTableEntry[1];
        int ecBlockSize = ecTableEntry[0];
        return totalCodewords - blocks * ecBlockSize;
    }
    // Returneaza numarul liniilor unde se pattern-urile de aliniere
    private List<int> GetAlignmentTracks(int version)
    {
        if (version == 1)
        {
            return new List<int>();
        }

        int intervals = (int)Math.Floor(version / 7.0) + 1;
        int distance = 4 * version + 4;
        int step = (int)Math.Ceiling((double)distance / intervals / 2) * 2;

        List<int> alignmentTracks = new List<int> { 6 };
        for (int index = 0; index < intervals; index++)
        {
            alignmentTracks.Add(distance + 6 - (intervals - 1 - index) * step);
        }

        return alignmentTracks;
    }
    // Returneaza mult chinuitul codQR :)
    public void GetQRCode(string content, char minErrorLevel,out byte[] qrCode, out int version, out char errorLevel,out int encodingMode, out int codewordsLength, out int maskIndex)
    {
        List<object> codewordsData = GetCodewords(content, minErrorLevel);

        qrCode = (byte[])codewordsData[0];
        version = (int)codewordsData[1];
        errorLevel = (char)codewordsData[2];
        encodingMode = (int)codewordsData[3];
        codewordsLength = qrCode.Length;
        maskIndex = GetOptimalMask(version, qrCode, errorLevel).Item2;
    }
    int GetByteCapacity(int availableBits)
    {
        return availableBits >> 3;
    }
    // Calculează capacitatea codului QR 
    private int GetCapacity(int version, char errorLevel, int encodingMode)
    {
        int dataCodewords = GetDataCodewords(version, errorLevel);
        int lengthBits = GetLengthBits(encodingMode, version);
        int availableBits = (dataCodewords << 3) - lengthBits - 4;
        return GetByteCapacity(availableBits);
    }
    // Returneaza un obiect iterabil care contine caracterul si dimensiunea in biti ocupata pentru modul latin
    private IEnumerable<(int value, int bitLength)> GetByteValues(string content)
    {
        foreach (char c in content)
        {
            yield return (c, 8);
        }
    }
    // Introduce bitii in buffer la un anumit offset
    private void PutBits(byte[] buffer, int value, int bitLength, int offset)
    {
        int byteStart = offset >> 3;
        int byteEnd = (offset + bitLength - 1) >> 3;
        int remainingBits = bitLength;

        for (int index = byteStart; index <= byteEnd; index++)
        {
            int availableBits = index == byteStart ? 8 - (offset & 7) : 8;
            int bitMask = (1 << availableBits) - 1;
            int rightShift = Math.Max(0, remainingBits - availableBits);
            int leftShift = Math.Max(0, availableBits - remainingBits);

            // Calculează segmentul de biți de inserat
            int chunk = ((value >> rightShift) & bitMask) << leftShift;

            // Introduce segmentul în buffer
            buffer[index] |= (byte)chunk;

            // Actualizează biții rămași de introdus
            remainingBits -= availableBits;
        }
    }
    // Generează datele codului QR pe baza conținutului
    private byte[] GetData(string content, int lengthBits, int dataCodewords)
    {
        int encodingMode = GetEncodingMode(content);
        int offset = 4 + lengthBits;
        byte[] data = new byte[dataCodewords];

        PutBits(data, encodingMode, 4, 0);
        PutBits(data, content.Length, lengthBits, 4);

        foreach (var item in GetByteValues(content))
        {
            int value = item.value;
            int bitLength = item.bitLength;
            PutBits(data, value, bitLength, offset);
            offset += bitLength;
        }

        int remainderBits = 8 - (offset & 7);
        int fillerStart = (offset >> 3) + (remainderBits < 4 ? 2 : 1);

        for (int index = 0; index < dataCodewords - fillerStart; index++)
        {
            byte b = (byte)((index & 1) != 0 ? 17 : 236);
            data[fillerStart + index] = b;
        }

        return data;
    }
    // Returnează versiunea și nivelul de eroare adecvate pe baza modului de codare ( in cazul nostru Latin (4))
    private (int, char)? GetVersionAndErrorLevel(int encodingMode, int contentLength, char minErrorLevel)
    {
        string errorLevels = "HQML";
        int minErrorIndex = errorLevels.IndexOf(minErrorLevel);

        errorLevels = errorLevels.Substring(0, minErrorIndex + 1);

        for (int version = 1; version <= 40; version++)
        {
            foreach (char errorLevel in errorLevels)
            {
                int capacity = GetCapacity(version, errorLevel, encodingMode);
                if (capacity >= contentLength)
                {
                    return (version,errorLevel);
                }
            }
        }

        return null;
    }
    // reordonează datele
    public static byte[] ReorderData(byte[] data, int blocks)
    {
        // Cuvintele de cod în blocuri de date (în grupul 1)
        int blockSize = data.Length / blocks;
        // Blocuri în grupul 1
        int group1 = blocks - data.Length % blocks;
        // Indexul de start al fiecărui bloc în `data`
        int[] blockStartIndexes = Enumerable.Range(0, blocks)
                                            .Select(index => index < group1
                                                            ? blockSize * index
                                                            : (blockSize + 1) * index - group1)
                                            .ToArray();

        return Enumerable.Range(0, data.Length)
                         .Select(index =>
                         {
                             // Indexul cuvântului de cod din bloc
                             int blockOffset = index / blocks;
                             // Indexul blocului din care se ia cuvântul de cod
                             // Dacă suntem la final (`blockOffset == blockSize`), atunci luăm
                             // doar din blocurile grupului 2
                             int blockIndex = (index % blocks) + (blockOffset == blockSize ? group1 : 0);
                             // Indexul cuvântului de cod din `data`
                             int codewordIndex = blockStartIndexes[blockIndex] + blockOffset;
                             return data[codewordIndex];
                         })
                         .ToArray();
    }
    // generează datele de corecție a erorilor pentru codul QR
    private byte[] GetECData(byte[] data, int blocks, int ecBlockSize)
    {
        int dataBlockSize = data.Length / blocks;
        int group1 = blocks - data.Length % blocks;
        byte[] ecData = new byte[ecBlockSize * blocks];

        for (int offset = 0; offset < blocks; offset++)
        {
            int start = offset < group1 ? dataBlockSize * offset: (dataBlockSize + 1) * offset - group1;
            int end = start + dataBlockSize + (offset < group1 ? 0 : 1);

            byte[] dataBlock = new byte[end - start];
            Array.Copy(data, start, dataBlock, 0, end - start);

            byte[] ecCodewords = GetEDC(dataBlock, dataBlock.Length + ecBlockSize);

            for (int index = 0; index < ecCodewords.Length; index++)
            {
                ecData[index * blocks + offset] = ecCodewords[index];
            }
        }

        return ecData;
    }
    // Obține informațiile despre versiune ale codului QR
    private byte[] GetVersionInformation(int version)
    {
        // Convertește versiunea în reprezentare binară
        string versionBinary = Convert.ToString(version, 2).PadLeft(6, '0');

        // Creează un array de bytes inițial din șirul binar
        byte[] poly = new byte[18];

        for (int i = 0; i < 6; i++)
        {
            poly[i] = (byte)(versionBinary[i] == '1' ? 1 : 0);
        }

        // Efectuează impartirea cu rest a polinoamelor
        byte[] remainder = PolyRest(poly, VERSION_DIVISOR);

        // Copiază restul la poziția corectă în array
        Array.Copy(remainder, 0, poly, 6, remainder.Length);

        return poly;
    }
    // Plasează modulele de versiune în matricea codului QR
    private void PlaceVersionModules(byte[][] matrix)
    {
        int size = matrix.Length;
        int version = (size - 17) >> 2;

        if (version < 7)
        {
            return;
        }

        byte[] versionInfo = GetVersionInformation(version);

        for (int index = 0; index < versionInfo.Length; index++)
        {
            int row = index / 3;
            int col = index % 3;

            matrix[5 - row][size - 9 - col] = versionInfo[index];
            matrix[size - 11 + col][row] = versionInfo[index];
        }
    }
    // genereaza informatia pentru QR
    private List<object> GetCodewords(string content, char minErrorLevel = 'L')
    {
        int encodingMode = GetEncodingMode(content);
        var versionAndErrorLevel = GetVersionAndErrorLevel(encodingMode, content.Length, minErrorLevel);

        int version = versionAndErrorLevel.Value.Item1;
        char errorLevel = versionAndErrorLevel.Value.Item2;

        int lengthBits = GetLengthBits(encodingMode, version);
        int dataCodewords = GetDataCodewords(version, errorLevel);

      
        int[] blockInfo = EC_TABLE[version - 1][errorLevel];

        byte[] rawData = GetData(content, lengthBits, dataCodewords);
        byte[] data = ReorderData(rawData, blockInfo[1]);
        byte[] ecData = GetECData(rawData, blockInfo[1], blockInfo[0]);

        byte[] codewords = new byte[data.Length + ecData.Length];
        Array.Copy(data, 0, codewords, 0, data.Length);
        Array.Copy(ecData, 0, codewords, data.Length, ecData.Length);

        return new List<object>
        {
            codewords,
            version,
            errorLevel,
            encodingMode
        };
    }
    //convertește matricea codului QR într-un bitmap pentru afișare
    public static Bitmap ConvertToBitmap(byte[][] qrCodeArray)
    {
        int size = qrCodeArray.Length;
        int scale = 5;
        Bitmap bitmap = new Bitmap(size * scale, size * scale);

        for (int y = 0; y < size; y++)
        {
            for (int x = 0; x < size; x++)
            {
                Color color = qrCodeArray[y][x] == 1 ? Color.Black : Color.White;
                for (int dy = 0; dy < scale; dy++)
                {
                    for (int dx = 0; dx < scale; dx++)
                    {
                        bitmap.SetPixel(x * scale + dx, y * scale + dy, color);
                    }
                }
            }
        }

        return bitmap;
    }
    // afișează un pop-up cu codul QR generat
    public static void ShowQRCodePopup(Form parentForm, Bitmap qrCodeBitmap)
    {
        PictureBox picBox = new PictureBox
        {
            Image = qrCodeBitmap,
            SizeMode = PictureBoxSizeMode.AutoSize,
            BackColor = Color.White
        };

        Button closeButton = new Button
        {
            Text = "X",
            BackColor = Color.Red,
            ForeColor = Color.White,
            FlatStyle = FlatStyle.Flat,
            Size = new Size(30, 30)
        };
        closeButton.FlatAppearance.BorderSize = 0;

        Panel panel = new Panel
        {
            Size = new Size(qrCodeBitmap.Width + 80, qrCodeBitmap.Height + 80),
            BackColor = Color.White,
            BorderStyle = BorderStyle.FixedSingle
        };

        panel.Location = new Point((parentForm.ClientSize.Width - panel.Width) / 2, (parentForm.ClientSize.Height - panel.Height) / 2);

        panel.Controls.Add(picBox);
        panel.Controls.Add(closeButton);

        picBox.Location = new Point(40, 40);

        closeButton.Location = new Point(panel.Width - closeButton.Width - 5, 5);

        parentForm.Controls.Add(panel);

        panel.BringToFront();

        closeButton.Click += (s, e) =>
        {
            parentForm.Controls.Remove(panel);
            panel.Dispose();
        };
    }























}

