using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEngine;

public class InvoiceBuilder : MonoBehaviour //this class is attached to gameobject
{
    // Entry point for code
    void Start()
    {
        //set of inputs
        var input1 = new[]
       {
            "1 book at 12.49",
            "1 music CD at 14.99",
            "1 chocolate bar at 0.85"
        };

        var input2 = new[]
        {
            "1 imported box of chocolates at 10.00",
            "1 imported bottle of perfume at 47.50"
        };

        var input3 = new[]
        {
            "1 imported bottle of perfume at 27.99",
            "1 bottle of perfume at 18.99",
            "1 packet of headache pills at 9.75",
            "1 box of imported chocolates at 11.25"
        };


        ParseInput(input1);
        Debug.Log("--------------------------------------------------");

        ParseInput(input2);
        Debug.Log("--------------------------------------------------");

        ParseInput(input3);
        Debug.Log("--------------------------------------------------");
    }

       private static void ParseInput(string[] input)
    {
        foreach (var item in input)
        {
            Console.WriteLine(item);
        }
        Console.WriteLine();

        var shoppingCart = ItemParser.Parse(input);

        var taxCalculator = new TaxCalculator();
        taxCalculator.Calculate(shoppingCart);

        //Discounting can be applied here!

        ShopingCartPrinter.Print(shoppingCart);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

class ItemParser //helper class for parsing
{
    private static readonly string ITEM_ENTRY_REGEX = "(\\d+) ([\\w\\s]* )at (\\d+.\\d{2})"; //regular expression

    private static readonly string[] food_identifier = { "chocolate", "chocolates" };
    private static readonly string[] medical_identifier = { "pills" };
    private static readonly string[] book_identifier = { "book" };

    public static ShoppingCart Parse(string[] listOfItemfullDesc) //string parses that returns an object of Shopping cart 
    {
        return new ShoppingCart
        {
            cartItems = listOfItemfullDesc.Select(Parse).ToList(), //
        };
    }

    public static ShoppingCartItem Parse(string itemfullDesc)
    {
        var regex = new Regex(ITEM_ENTRY_REGEX);
        var match = regex.Match(itemfullDesc);
        if (match.Success)
        {//if regex matches, the 1st item is the quantity of the product, an int
            var quantity = int.Parse(match.Groups[1].Value);

            //price of the product
            var _price = decimal.Parse(match.Groups[3].Value);

            //item name
            var itemName = match.Groups[2].Value.Trim(); //remove whitespaces at beginning and at end

            //creating object for each item
            var shopp = new ShoppingCartItem
            {
                Quantity = quantity,
                Product = new Product{productName = itemName, price = _price }
            };

            return shopp;//return the item
        }

        return null;//if match not found, return null
    }
}

class ShopingCartPrinter
{
    public static void Print(ShoppingCart shoppringCart)
    {
        //print items => 1 book : 12.49
        foreach (var cartItem in shoppringCart.cartItems)
        {
            Debug.Log(cartItem.ToString());
        }

        //print Taxes: 1.50
        Debug.Log(String.Format("Taxes: {0:N2}", shoppringCart.TotalTax)); //upto two decimal places only

      
        Debug.Log(String.Format("Total: {0:N2}", shoppringCart.TotalCost)); //upto two decimal places only
    }
}
