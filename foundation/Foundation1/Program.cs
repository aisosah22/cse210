using System;
using System.Collections.Generic;

// Class representing a Product
class Product
{
    private string _name;
    private string _productId;
    private decimal _pricePerUnit;
    private int _quantity;

    // Constructor for Product
    public Product(string name, string productId, decimal pricePerUnit, int quantity)
    {
        _name = name;
        _productId = productId;
        _pricePerUnit = pricePerUnit;
        _quantity = quantity;
    }

    // Method to calculate the total cost of the product (price * quantity)
    public decimal GetTotalCost()
    {
        return _pricePerUnit * _quantity;
    }

    // Get the product name
    public string GetName()
    {
        return _name;
    }

    // Get the product ID
    public string GetProductId()
    {
        return _productId;
    }
}

// Class representing a Customer
class Customer
{
    private string _name;
    private Address _address;

    // Constructor for Customer
    public Customer(string name, Address address)
    {
        _name = name;
        _address = address;
    }

    // Get the customer name
    public string GetName()
    {
        return _name;
    }

    // Get the customer address (as a string)
    public string GetAddressString()
    {
        return _address.GetAddressString();
    }

    // Check if the customer lives in the USA
    public bool LivesInUSA()
    {
        return _address.IsInUSA();
    }
}

// Class representing an Address
class Address
{
    private string _streetAddress;
    private string _city;
    private string _state;
    private string _country;

    // Constructor for Address
    public Address(string streetAddress, string city, string state, string country)
    {
        _streetAddress = streetAddress;
        _city = city;
        _state = state;
        _country = country;
    }

    // Method to check if the address is in the USA
    public bool IsInUSA()
    {
        return _country.ToLower() == "usa";
    }

    // Method to return the full address as a string
    public string GetAddressString()
    {
        return $"{_streetAddress}\n{_city}, {_state}\n{_country}";
    }
}

// Class representing an Order
class Order
{
    private List<Product> _products;
    private Customer _customer;

    // Constructor for Order
    public Order(Customer customer)
    {
        _products = new List<Product>();
        _customer = customer;
    }

    // Add a product to the order
    public void AddProduct(Product product)
    {
        _products.Add(product);
    }

    // Calculate the total cost of the order (sum of products + shipping cost)
    public decimal GetTotalCost()
    {
        decimal total = 0;

        // Add the total cost of all products
        foreach (var product in _products)
        {
            total += product.GetTotalCost();
        }

        // Add shipping cost (5 USD for USA, 35 USD for other countries)
        total += _customer.LivesInUSA() ? 5 : 35;

        return total;
    }

    // Generate a packing label listing product names and IDs
    public string GetPackingLabel()
    {
        string label = "Packing Label:\n";

        foreach (var product in _products)
        {
            label += $"{product.GetName()} (ID: {product.GetProductId()})\n";
        }

        return label;
    }

    // Generate a shipping label with the customer's name and address
    public string GetShippingLabel()
    {
        return $"Shipping Label:\n{_customer.GetName()}\n{_customer.GetAddressString()}";
    }
}

// Main program demonstrating the encapsulation principle
class Program
{
    static void Main()
    {
        // Create some products
        Product product1 = new Product("Laptop", "LAP123", 1200m, 1);
        Product product2 = new Product("Mouse", "MOU456", 20m, 2);
        Product product3 = new Product("Keyboard", "KEY789", 50m, 1);

        // Create addresses
        Address address1 = new Address("123 Apple St", "New York", "NY", "USA");
        Address address2 = new Address("456 Maple Rd", "Toronto", "ON", "Canada");

        // Create customers
        Customer customer1 = new Customer("John Doe", address1);
        Customer customer2 = new Customer("Jane Smith", address2);

        // Create orders
        Order order1 = new Order(customer1);
        order1.AddProduct(product1);
        order1.AddProduct(product2);

        Order order2 = new Order(customer2);
        order2.AddProduct(product3);

        // Display order details
        Console.WriteLine(order1.GetPackingLabel());
        Console.WriteLine(order1.GetShippingLabel());
        Console.WriteLine($"Total Price: ${order1.GetTotalCost():F2}\n");

        Console.WriteLine(order2.GetPackingLabel());
        Console.WriteLine(order2.GetShippingLabel());
        Console.WriteLine($"Total Price: ${order2.GetTotalCost():F2}");
    }
}
