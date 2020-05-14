using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


//shopping cart blueprint
public class ShoppingCart 
{
    public IList<ShoppingCartItem> cartItems { get; set; }

    public decimal TotalTax { get { return cartItems.Sum(x => x.Tax); } }

    public decimal TotalCost { get { return cartItems.Sum(x => x.Cost); } }
}

public class ShoppingCartItem
{
    public Product Product { get; set; }

    public int Quantity { get; set; }

    public decimal Tax { get; set; }

    public decimal Cost { get { return Quantity * (Tax + Product.price); } }

    public override string ToString()
    {
        return string.Format("{0} {1} : {2:N2}", Quantity, Product.productName, Cost);
    }
}

