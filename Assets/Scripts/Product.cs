using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

//enum
public enum ProductType
{
    Food,
    Medical,
    Book,
    Other
}

//use regex or string match to check name
enum Origin
{
    Local,
    Imported
}

public class Product 
{
   
    //name of the product
    public string productName;
    //price per piece
    private decimal m_rate;
    //rate*quantity
    public decimal price {get;set;}
    public bool isImported { get { return productName.Contains("imported "); } }
    //not needed really as all are unit commodities
    public int quantity;

    //tax per item
    public decimal itemTax { get; set; }

   
    
    //identifier to compare item type and string values
    private static readonly IDictionary<ProductType, string[]> itemType_Identifiers = new Dictionary<ProductType, string[]>
    {
        {ProductType.Food, new[]{ "chocolate", "chocolates" }},//redundancy check
        {ProductType.Medical, new[]{ "pills" }},
        {ProductType.Book, new[]{ "book" }}
    };

    //check if the product is tax emexmpted
    public bool IsTypeOf(ProductType productType)
    {
        return itemType_Identifiers.ContainsKey(productType) &&
            itemType_Identifiers[productType].Any(x => productName.Contains(x));
    }

    //format to name and price
    public override string ToString()
    {
        return string.Format("{0} at {1}", productName, price);
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
