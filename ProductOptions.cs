public static class ProductManager
{
    public static List<ProductType> ProductTypes = new List<ProductType>
    {
        new ProductType { Id = 1, Name = "Apparel" },
        new ProductType { Id = 2, Name = "Potions" },
        new ProductType { Id = 3, Name = "Enchanted Objects" },
        new ProductType { Id = 4, Name = "Wands" }
    };

    public static List<Product> Products = new List<Product>
    {
        new Product
        {
            Id = 1,
            Name = "Encahnted Scarf",
            Price = 19.99M,
            IsAvailable = true,
            ProductTypeId = 1,
            DateStocked = DateTime.Now.AddDays(-10)
        },
        new Product
        {
            Id = 2,
            Name = "Draught of Living Death",
            Price = 2999.00M,
            IsAvailable = false,
            ProductTypeId = 1,
            DateStocked = DateTime.Now.AddDays(-30)
        },
        new Product
        {
            Id = 3,
            Name = "Pensieve",
            Price = 49.99M,
            IsAvailable = true,
            ProductTypeId = 3,
            DateStocked = DateTime.Now.AddDays(-5)
        },
        new Product
        {
            Id = 4,
            Name = "Polyjuice Potion",
            Price = 49.20M,
            IsAvailable = true,
            ProductTypeId = 2,
            DateStocked = DateTime.Now.AddDays(-60)
        },
        new Product
        {
            Id = 5,
            Name = "Walnut and Dragon Heartstring Wand",
            Price = 999.99M,
            IsAvailable = false,
            ProductTypeId = 4,
            DateStocked = DateTime.Now.AddDays(-90)
        }
    };


    public static void ViewProducts()
    {
        if (Products.Count == 0)
        {
            Console.WriteLine("No products in inventory.");
            return;
        }

        Console.WriteLine("Products in inventory:");
        for (int i = 0; i < Products.Count; i++)
        {
            var product = Products[i];
            var productType = ProductTypes.FirstOrDefault(pt => pt.Id == product.ProductTypeId)?.Name ?? "Unknown";
            string availability = product.IsAvailable ? "Yes" : "No";

            Console.WriteLine($"{i + 1}. {product.Name} | ${product.Price:F2} | Available: {availability} | Type: {productType} | Days on Shelf: {product.DaysOnShelf}");
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

        Console.WriteLine("Enter the date the product was stocked (yyyy-MM-dd):");
        if (!DateTime.TryParseExact(Console.ReadLine(), "yyyy-MM-dd", null, System.Globalization.DateTimeStyles.None, out DateTime dateStocked))
        {
            Console.WriteLine("Invalid date. Product not added.");
            return;
        }

        Products.Add(new Product
        {
            Name = name,
            Price = price,
            IsAvailable = isAvailable,
            ProductTypeId = productTypeId,
            DateStocked = dateStocked // Set DateStocked
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

            Console.WriteLine("Enter New Stock Date (yyyy-MM-dd, leave blank to keep the current date):");
            string dateInput = Console.ReadLine();
            if (DateTime.TryParseExact(dateInput, "yyyy-MM-dd", null, System.Globalization.DateTimeStyles.None, out DateTime newDateStocked))
            {
                product.DateStocked = newDateStocked;
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

        for (int i = 0; i < ProductTypes.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {ProductTypes[i].Name}");
        }

        if (!int.TryParse(Console.ReadLine(), out int choice) || choice < 1 || choice > ProductTypes.Count)
        {
            Console.WriteLine("Invalid input. Returning to the main menu.");
            return;
        }

        var selectedType = ProductTypes[choice - 1];
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
            string availability = product.IsAvailable ? "Yes" : "No";
            Console.WriteLine($"{i + 1}. {product.Name} | ${product.Price:F2} | Available: {availability} | Days on Shelf: {product.DaysOnShelf}");
        }
    }
}
