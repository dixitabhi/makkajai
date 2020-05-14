using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class Tax 
{
    abstract public bool isApplicable(Product item);
    abstract public decimal rate { get; }
    public decimal Calculate(Product item)
    {
        if (isApplicable(item))
        {
            //sales tax are that for a tax rate of n%, a shelf price of p contains (np/100)
            var tax = (item.price * rate) / 100;

            //The rounding rules: rounded up to the nearest 0.05 as stated in doc
            tax = Math.Ceiling(tax / 0.05m) * 0.05m;

            return tax;
        }

        return 0;
    }
}

//basic tax on non-exempted items
public class BasicTax : Tax
{
    private ProductType[] expemtedItems = new[] { ProductType.Food, ProductType.Medical, ProductType.Book };


    public override decimal rate { get { return 10.00M; } }

    //return false if product belongs to exemted items
    public override bool isApplicable(Product item)
    {
        return !(expemtedItems.Any(x => item.IsTypeOf(x)));
    }
}

//imported products tax
class ImportedDutySalesTax : Tax
{
    //true if the description has imported label
    public override bool isApplicable(Product item)
    {
        return item.isImported;
    }
    //5% tax
    public override decimal rate { get { return 5.00M; } }
}


class TaxCalculator
{
    //instantiate tax classes as an array
    private Tax[] _taxes = new Tax[] { new BasicTax(), new ImportedDutySalesTax() };

    public void Calculate(ShoppingCart shoppingCart)
    {
        //for each product, calculate tax here
        foreach (var cartItem in shoppingCart.cartItems)
        {
            cartItem.Tax = _taxes.Sum(x => x.Calculate(cartItem.Product)); //cart item tax using Linq.sum method
        }
    }
}