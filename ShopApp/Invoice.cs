using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Printing;



public class Invoice
{
    public int NumarFactura { get; set; }
    public DateTime DataFactura { get; set; }
    public Client client { get; set; }
    public List<ArticolFactura> Articole { get; set; }
    public float SumaTotala { get; set; }
    public float SumaTotalaFaraTVA { get; set; }
    public string MetodaLivrare { get; set; }
    public float CostLivrare { get; set; }
    public string MetodaPlata { get; set; }
    public Invoice()
    {
        Articole = new List<ArticolFactura>();
    }

}

public class ArticolFactura
{
    public string DenumireProdus { get; set; }
    public int Cantitate { get; set; }
    public float PretUnitar { get; set; }
    public float PretTotal => Cantitate * PretUnitar;
}

public class FacturaBuilder
{
    private Invoice _factura;

    public FacturaBuilder()
    {
        _factura = new Invoice();
    }

    public FacturaBuilder SetNumarFactura(int numarFactura)
    {
        _factura.NumarFactura = numarFactura;
        return this;
    }

    public FacturaBuilder SetDataFactura(DateTime dataFactura)
    {
        _factura.DataFactura = dataFactura;
        return this;
    }

    public FacturaBuilder SetClient(Client c)
    {
        _factura.client = c;
        return this;
    }

    public FacturaBuilder AdaugaArticol(string denumireProdus, int cantitate, float pretUnitar)
    {
        var articol = new ArticolFactura
        {
            DenumireProdus = denumireProdus,
            Cantitate = cantitate,
            PretUnitar = pretUnitar
        };
        _factura.Articole.Add(articol);
        return this;
    }

    public FacturaBuilder SetMetodaLivrare(string metodaLivrare)
    {
        switch (metodaLivrare)
        {
            case "Fan Courier":
                _factura.MetodaLivrare = "Fan Courier";
                _factura.CostLivrare = 19.99f;
                break;
            case "DPD":
                _factura.MetodaLivrare = "DPD";
                _factura.CostLivrare = 24.99f;
                break;
            case "SameDay":
                _factura.MetodaLivrare = "SameDay";
                _factura.CostLivrare = 19.99f;
                break;
            default:
                throw new ArgumentException("Metodă de livrare invalidă");
        }
        return this;
    }
    public FacturaBuilder SetMetodaPlata(string metodaPlata)
    {
        _factura.MetodaPlata = metodaPlata;
        return this;
    }
    public Invoice Build()
    {
        float totalPretArticole = _factura.Articole.Sum(item => item.PretTotal);
        _factura.SumaTotalaFaraTVA = totalPretArticole - (0.19f * totalPretArticole);
        _factura.SumaTotala = totalPretArticole;
        return _factura;
    }
}