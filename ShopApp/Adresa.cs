using Examen;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

public class Adresa
{
    private string tara;
    private string judet;
    private string oras;
    private string strada;
    private string codPostal;
    private string bloc;
    private int? numar;
    private string scara;
    private string numarDeTelefon;
    public string Tara
    {
        get { return tara; }
        set { tara = string.IsNullOrWhiteSpace(value) ? null : value; }
    }

    public string Judet
    {
        get { return judet; }
        set { judet = string.IsNullOrWhiteSpace(value) ? null : value; }
    }

    public string Oras
    {
        get { return oras; }
        set { oras = string.IsNullOrWhiteSpace(value) ? null : value; }
    }

    public string Strada
    {
        get { return strada; }
        set { strada = string.IsNullOrWhiteSpace(value) ? null : value; }
    }

    public string CodPostal
    {
        get { return codPostal; }
        set { codPostal = string.IsNullOrWhiteSpace(value) ? null : value; }
    }

    public string Bloc
    {
        get { return bloc; }
        set { bloc = string.IsNullOrWhiteSpace(value) ? null : value; }
    }

    public int? Numar
    {
        get { return numar; }
        set { numar = value > 0 ? value : null; }
    }

    public string Scara
    {
        get { return scara; }
        set { scara = string.IsNullOrWhiteSpace(value) ? null : value; }
    }
    
    public string NumarDeTelefon
    {
        get { return numarDeTelefon; }
        set { numarDeTelefon = string.IsNullOrWhiteSpace(value) ? null : value; }
    }
    public Adresa() { }
    public Adresa(string tara, string judet, string oras, string strada, string codPostal, string bloc, int? numar, string scara,string numarDeTelefon)
    {
        Tara = tara;
        Judet = judet;
        Oras = oras;
        Strada = strada;
        CodPostal = codPostal;
        Bloc = bloc;
        Numar = numar;
        Scara = scara;
        NumarDeTelefon = numarDeTelefon;
    }
    public bool InformationsAreCompleted()
    {
        return tara != null && judet != null && oras != null && strada != null && codPostal != null && bloc != null && scara != null && numarDeTelefon != null && numar != null;
    }
    public override string ToString()
    {
        return $"{Tara}, {Judet}, {Oras}, {Strada}, {CodPostal}, {Bloc}, {Numar}, {Scara} \nNumar de Telefon : {NumarDeTelefon}";
    }
    public string SaveAddressInDatabase(int clientID)
    {
        try
        {
            using (SqlConnection con = new SqlConnection("Data Source=WIN10\\MYDATABASE;Initial Catalog=Users;User ID=root;Password=toor;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False"))
            {
                con.Open();
                string sql = "UPDATE Clients SET Tara = @Tara, Judet = @Judet, Oras = @Oras, Strada = @Strada, CodPostal = @CodPostal, Bloc = @Bloc, Numar = @Numar, Scara = @Scara, NumarDeTelefon = @NumarDeTelefon WHERE ID = @ID";
                using (SqlCommand cmd = new SqlCommand(sql, con))
                {
                    cmd.Parameters.AddWithValue("@Tara", (object)this.Tara ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@Judet", (object)this.Judet ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@Oras", (object)this.Oras ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@Strada", (object)this.Strada ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@CodPostal", (object)this.CodPostal ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@Bloc", (object)this.Bloc ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@Numar", (object)this.Numar ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@Scara", (object)this.Scara ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@NumarDeTelefon", (object)this.NumarDeTelefon ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@ID", clientID);

                    cmd.ExecuteNonQuery();
                }
            }
            return "Adresa adaugata cu succes!";
        }
        catch (Exception ex)
        {
            return $"Hopa: {ex.Message}";
        }
    }
    
}
