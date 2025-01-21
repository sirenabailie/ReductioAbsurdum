public static class ProductManager
{
    public static List<ProductType> ProductTypes = new List<ProductType>
    {
        new ProductType { Id = 1, Name = "Apparel" },
        new ProductType { Id = 2, Name = "Potions" },
        new ProductType { Id = 3, Name = "Enchanted Objects" },
        new ProductType { Id = 4, Name = "Wands" }
    };

    public static List<Product> Products = new List<Product>();

public static void ViewProducts()
{
    if (Products.Count == 0)
    {
        Console.WriteLine("No products in inventory.");
        return;
    }

    for (int i = 0; i < Products.Count; i++)
    {
        var product = Products[i];
        var productType = ProductTypes.FirstOrDefault(pt => pt.Id == product.ProductTypeId)?.Name ?? "Unknown";
        string availability = product.IsAvailable ? "Yes" : "No";
        Console.WriteLine($"{i + 1}. {product.Name} | ${product.Price} | Available?: {availability} | Type: {productType}");
    }
}



    public static void AddProduct()
    {
        Console.WriteLine("Enter Product Name:");
        string name = Console.ReadLine();

        Console.WriteLine("Enter Product Price:");
        if (!decimal.TryParse(Console.ReadLine(), out decimal price))
        {
            Console.WriteLine("Invalid price. Product not added.");
            return;
        }

        Console.WriteLine("Is the product available? (yes/no):");
        bool isAvailable = Console.ReadLine()?.Trim().ToLower() == "yes";

        Console.WriteLine("Choose a Product Type:");
        foreach (var productType in ProductTypes)
        {
            Console.WriteLine($"{productType.Id}. {productType.Name}");
        }

        if (!int.TryParse(Console.ReadLine(), out int productTypeId) || !ProductTypes.Any(pt => pt.Id == productTypeId))
        {
            Console.WriteLine("Invalid product type. Product not added.");
            return;
        }

        Products.Add(new Product
        {
            Name = name,
            Price = price,
            IsAvailable = isAvailable,
            ProductTypeId = productTypeId
        });

        Console.WriteLine("Product added successfully!");
    }

public static void DeleteProduct()
{
    if (Products.Count == 0)
    {
        Console.WriteLine("No products available to delete.");
        return;
    }

    ViewProducts();
    Console.WriteLine("\nEnter the number of the product to delete:");

    if (int.TryParse(Console.ReadLine(), out int index) && index >= 1 && index <= Products.Count)
    {
        var product = Products[index - 1];
        Products.RemoveAt(index - 1);
        Console.WriteLine($"Product '{product.Name}' removed successfully!");
    }
    else
    {
        Console.WriteLine("Invalid selection. Please try again.");
    }
}


public static void UpdateProduct()
{
    if (Products.Count == 0)
    {
        Console.WriteLine("No products available to update.");
        return;
    }

    ViewProducts();
    Console.WriteLine("\nEnter the number of the product to update:");

    if (int.TryParse(Console.ReadLine(), out int index) && index >= 1 && index <= Products.Count)
    {
        var product = Products[index - 1];

        Console.WriteLine("Enter New Name (leave blank to keep the current name):");
        string newName = Console.ReadLine();
        if (!string.IsNullOrWhiteSpace(newName)) product.Name = newName;

        Console.WriteLine("Enter New Price (leave blank to keep the current price):");
        string priceInput = Console.ReadLine();
        if (decimal.TryParse(priceInput, out decimal newPrice)) product.Price = newPrice;

        Console.WriteLine("Is the product available? (yes/no/leave blank):");
        string availabilityInput = Console.ReadLine()?.Trim().ToLower();
        if (availabilityInput == "yes") product.IsAvailable = true;
        if (availabilityInput == "no") product.IsAvailable = false;

        Console.WriteLine("Choose a New Product Type (or leave blank to keep the current type):");
        foreach (var productType in ProductTypes)
        {
            Console.WriteLine($"{productType.Id}. {productType.Name}");
        }

        string typeInput = Console.ReadLine();
        if (int.TryParse(typeInput, out int newProductTypeId) && ProductTypes.Any(pt => pt.Id == newProductTypeId))
        {
            product.ProductTypeId = newProductTypeId;
        }

        Console.WriteLine("Product updated successfully!");
    }
    else
    {
        Console.WriteLine("Invalid selection. Please try again.");
    }
}

public static void ViewProductsByType()
{
    Console.WriteLine("Select a product type to view:");

    // Display all product types with their corresponding numbers
    for (int i = 0; i < ProductTypes.Count; i++)
    {
        Console.WriteLine($"{i + 1}. {ProductTypes[i].Name}");
    }

    if (!int.TryParse(Console.ReadLine(), out int choice) || choice < 1 || choice > ProductTypes.Count)
    {
        Console.WriteLine("Invalid input. Returning to the main menu.");
        return;
    }

    // Get the selected product type
    var selectedType = ProductTypes[choice - 1];

    // Filter products by the selected type
    var filteredProducts = Products.Where(p => p.ProductTypeId == selectedType.Id).ToList();

    if (filteredProducts.Count == 0)
    {
        Console.WriteLine($"No products found in the category: {selectedType.Name}");
        return;
    }

    Console.WriteLine($"\nProducts in the {selectedType.Name} category:");
    for (int i = 0; i < filteredProducts.Count; i++)
    {
        var product = filteredProducts[i];
        string availability = product.IsAvailable ? "yes" : "no"; // Format availability
        Console.WriteLine($"{i + 1}. {product.Name} | ${product.Price:F2} | Available: {availability}");
    }
}


}
