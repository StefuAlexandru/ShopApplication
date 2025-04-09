using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public abstract class ProductFactory
{
    public abstract Produs CreateProduct(string cod, string denumire, string descriere, float pret, int stoc);
}
public class FoodProductFactory : ProductFactory
{
    private DateTime perioadaValabilitate;

    public FoodProductFactory(DateTime perioadaValabilitate)
    {
        this.perioadaValabilitate = perioadaValabilitate;
    }

    public override Produs CreateProduct(string cod, string denumire, string descriere, float pret, int stoc)
    {
        return new ProdusAlimentar(cod, denumire, descriere,"Alimentar", pret, stoc, perioadaValabilitate);
    }
}

public class NonFoodProductFactory : ProductFactory
{
    private int garantie;

    public NonFoodProductFactory(int garantie)
    {
        this.garantie = garantie;
    }

    public override Produs CreateProduct(string cod, string denumire, string descriere, float pret, int stoc)
    {
        return new ProdusNealimentar(cod, denumire, descriere,"Nealimentar", pret, stoc, garantie);
    }
}