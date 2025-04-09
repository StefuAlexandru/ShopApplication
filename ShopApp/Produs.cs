using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Xml.Linq;

public abstract class Produs
{
    public string CodIdentificare { get; set; }
    public string Denumire { get; set; }
    public string Descriere { get; set; }
    public string Categorie { get; set; }
    public float Pret { get; set; }
    public int Stoc { get; set; }

    protected Produs(string codIdentificare, string denumire, string descriere, string categorie, float pret, int stoc)
    {
        CodIdentificare = codIdentificare;
        Denumire = denumire;
        Descriere = descriere;
        Categorie = categorie;
        Pret = pret;
        Stoc = stoc;
    }

    public abstract string DisplayInfo();
}
class ProdusAlimentar : Produs
{
    public DateTime DataDeExpirare { get; set; }

    public ProdusAlimentar(string codIdentificare, string denumire, string descriere, string categorie, float pret, int stoc, DateTime dataDeExpirare)
        : base(codIdentificare, denumire, descriere, categorie, pret, stoc)
    {
        DataDeExpirare = dataDeExpirare;
    }

    public override string DisplayInfo()
    {
        return $"Produs Alimentar - Cod: {CodIdentificare}\nDenumire: {Denumire}\nDescriere: {Descriere}\nCategorie: {Categorie}\nPret: {Pret}\nStoc: {Stoc}\nData de Expirare: {DataDeExpirare.ToShortDateString()}";
    }
}
class ProdusNealimentar : Produs
{
    public int Garantie { get; set; }

    public ProdusNealimentar(string codIdentificare, string denumire, string descriere, string categorie, float pret, int stoc, int garantie)
        : base(codIdentificare, denumire, descriere, categorie, pret, stoc)
    {
        Garantie = garantie;
    }

    public override string DisplayInfo()
    {
        return $"Produs Nealimentar - Cod: {CodIdentificare}\nDenumire: {Denumire}\nDescriere: {Descriere}\nCategorie: {Categorie}\nPret: {Pret}\nStoc: {Stoc}\nGarantie: {Garantie}";
    }
}
