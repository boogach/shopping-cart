using ShoppingCart.Classes;
using ShoppingCart.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingCart
{
    class Program
    {
        static void Main(string[] args)
        {
            bool exitShop = false;
            var numberOfItems = 100;
            var userInput = string.Empty;
            bool stageOneFinished = false;

            var currentCart = new List<InventoryModel> { };

            InventoryStore inventory = new InventoryStore();
            Cart shoppingCart = new Cart(inventory.items);

            for (int i = 0; i < numberOfItems; i++)
            {
                if (!stageOneFinished)
                {
                    if (i == 0)
                        Console.WriteLine("Add items to inventory in expected input format: \nADD - sku(number), name(string), quantity(number), price(number i.e 3.5)");

                    userInput = Console.ReadLine();

                    if (userInput.Contains("ADD"))
                    {
                        if (userInput.Split(' ').Length == 6)
                        {
                            var sku = int.Parse(userInput.Split(' ')[2]);
                            var name = userInput.Split(' ')[3];
                            var quantity = int.Parse(userInput.Split(' ')[4]);
                            var price = float.Parse(userInput.Split(' ')[5].Replace('.', ','));

                            inventory.Add(sku, name, quantity, price);
                        }
                    }
                    else
                    {
                        Console.WriteLine("Adding items to inventory store finished.");
                        stageOneFinished = true;
                        userInput = string.Empty;
                    }
                }

                else
                {
                    if (inventory.items.Count == i - 1)
                        Console.WriteLine("Additem to the current shopping cart in expected input format: \n ADD - sku(number), quantity(number);");

                    userInput = Console.ReadLine();

                    if (userInput.Contains("ADD"))
                    {
                        if (userInput.Split(' ').Length == 4)
                        {
                            var sku = int.Parse(userInput.Split(' ')[2]);
                            var quantity = int.Parse(userInput.Split(' ')[3]);

                            currentCart = shoppingCart.Add(sku, quantity);
                        }
                    }

                    else if (userInput.Contains("CHECKOUT"))
                    {
                        shoppingCart.Checkout(currentCart);
                    }

                    else if(userInput.Contains("REMOVE"))
                    {
                        if (userInput.Split(' ').Length == 4)
                        {
                            var sku = int.Parse(userInput.Split(' ')[2]);
                            var quantity = int.Parse(userInput.Split(' ')[3]);

                            currentCart = shoppingCart.Remove(sku, quantity);
                        }  
                    }

                    else if (userInput.Contains("END"))
                    {
                        exitShop = true;
                        Environment.Exit(0);
                    }
                }
            }

            Console.ReadKey(exitShop);
        }
    }
}
