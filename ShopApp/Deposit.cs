using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.IO;
using System.Windows.Forms;

public class Depozit
{
    private static Depozit current;
    public static List<Produs> AllProducts { get; private set; } = new List<Produs>();
    public  List<Produs> produse;

    private Depozit()
    {
        produse = new List<Produs>();
    }
    public static Depozit Current
    {
        get
        {
            if (current == null)
            {
                current = new Depozit();
            }
            return current;
        }
    }
    public Produs getProdusFromAllProducts(string id)
    {
        return AllProducts.Find(p => p.CodIdentificare == id);
    }
    public void AddProdus(Produs produs)
    {
        produse.Add(produs);
        if (!AllProducts.Any(p => p.CodIdentificare == produs.CodIdentificare))
        {
            AllProducts.Add(produs);
        }
    }

    public int CheckStoc(string codProdus)
    {
        var produs = produse.FirstOrDefault(p => p.CodIdentificare == codProdus);
        return produs != null ? produs.Stoc : 0;
    }

    public void UpdateStoc(string codProdus, int cantitate)
    {
        var produs = produse.FirstOrDefault(p => p.CodIdentificare == codProdus);
        if (produs != null)
        {
            produs.Stoc -= cantitate;
        }
    }

    public async Task LoadProduseFromXmlAsync(string filePath, string categorie)
    {
        try
        {

            string xmlContent = await Task.Run(() => File.ReadAllText(filePath));
     
    
            XDocument xmlDoc = XDocument.Parse(xmlContent);

            foreach (var element in xmlDoc.Descendants("produs"))
            {
                string codIdentificare = element.Element("cod")?.Value;
                string denumire = element.Element("denumire")?.Value;
                string descriere = element.Element("descriere")?.Value;
                float pret = float.Parse(element.Element("pret")?.Value);
                int stoc = int.Parse(element.Element("stoc")?.Value);

                Produs product = null;

                if (categorie == "Alimentar")
                {
                    DateTime dataDeExpirare = DateTime.Parse(element.Element("data_valabilitate").Value);
                    ProductFactory factory = new FoodProductFactory(dataDeExpirare);
                    product = factory.CreateProduct(codIdentificare, denumire, descriere, pret, stoc);
                }
                else if (categorie == "Nealimentar")
                {
                    Random rnd = new Random();
                    int garantie = rnd.Next(1, 13);
                    ProductFactory factory = new NonFoodProductFactory(garantie);
                    product = factory.CreateProduct(codIdentificare, denumire, descriere, pret, stoc);
                }

                if (product != null)
                {
                    AddProdus(product);
                }
            }
            
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Hopa : {ex.Message}");
        }
    }
    public List<Produs> SearchProduse(string searchTerm)
    {
        return produse.Where(p => p.Denumire.IndexOf(searchTerm, StringComparison.OrdinalIgnoreCase) >= 0 || p.Descriere.IndexOf(searchTerm, StringComparison.OrdinalIgnoreCase) >= 0).ToList();
    }
    public async Task SaveProduseToXmlAsync(string filePath, string categorie)
    {
        try
        {
            XDocument xmlDoc = new XDocument(
                new XElement("produse",
                    from produs in AllProducts
                    where (categorie == "Alimentar" && produs is ProdusAlimentar) ||
                          (categorie == "Nealimentar" && produs is ProdusNealimentar)
                    select new XElement("produs",
                        new XElement("cod", produs.CodIdentificare),
                        new XElement("denumire", produs.Denumire),
                        new XElement("descriere", produs.Descriere),
                        new XElement("pret", produs.Pret),
                        new XElement("stoc", produs.Stoc),
                        categorie == "Alimentar"
                            ? new XElement("data_valabilitate", ((ProdusAlimentar)produs).DataDeExpirare.ToString("yyyy-MM-dd"))
                            : new XElement("garantie", ((ProdusNealimentar)produs).Garantie.ToString())
                    )
                )
            );

            await Task.Run(() => xmlDoc.Save(filePath));
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Eroare la salvare: {ex.Message}");
        }
    }
}
