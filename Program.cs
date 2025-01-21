using System;

class Program
{
    static void Main()
    {
        string greeting = @"
Welcome to Reductio & Absurdum! 🧙‍♂️🔮";

        Console.WriteLine(greeting);

        string choice;
        do
        {
            Console.WriteLine(@"
Choose an option:
1. View All Products
2. Add a Product
3. Delete a Product
4. Update a Product
5. View Products by Type
0. Exit");

            choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    Console.Clear();
                    ProductManager.ViewProducts(); 
                    break;
                case "2":
                    Console.Clear();
                    ProductManager.AddProduct();
                    break;
                case "3":
                    Console.Clear();
                    ProductManager.DeleteProduct();
                    break;
                case "4":
                    Console.Clear();
                    ProductManager.UpdateProduct(); 
                    break;
                case "5":
                    Console.Clear();
                    ProductManager.ViewProductsByType(); 
                    break;
                case "0":
                    Console.Clear();
                    Console.WriteLine("Goodbye!✨");
                    break;
                default:
                    Console.WriteLine("Invalid choice. Try again.");
                    break;
            }

            // Pause before returning to the menu
            if (choice != "0")
            {
                Console.WriteLine("\nPress Enter to continue...");
                Console.ReadLine();
                Console.Clear();
            }
        }
        while (choice != "0");
    }
}
