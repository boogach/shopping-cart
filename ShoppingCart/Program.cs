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
            //number of items which can be added in inventory, can be any number
            var numberOfItems = 100;
            var userInput = string.Empty;

            //to indicate if adding items to inventory is finished
            bool stageOneFinished = false;

            var currentCart = new List<InventoryModel> { };

            //derived classed initialization
            InventoryStore inventory = new InventoryStore();
            Cart shoppingCart = new Cart(inventory.items);

            //loop so console application doesn't get closed
            for (int i = 0; i < numberOfItems; i++)
            {
                if (!stageOneFinished)
                {
                    //display message just once, on opening app
                    if (i == 0)
                        Console.WriteLine("Add items to inventory in expected input format: \nADD - sku(number) name(string) quantity(number) price(number i.e 3.5)");

                    //read user input
                    userInput = Console.ReadLine();

                    //check for user command ADD
                    if (userInput.Contains("ADD"))
                    {
                        //check number of elements in user input, splitted by space
                        if (userInput.Split(' ').Length == 6)
                        {
                            int sku, quantity;
                            float price;
                            //try parse array elements
                            bool convertSku = int.TryParse(userInput.Split(' ')[2], out sku);
                            string name = userInput.Split(' ')[3];
                            bool convertQuantity = int.TryParse(userInput.Split(' ')[4], out quantity);
                            bool convertPrice = float.TryParse(userInput.Split(' ')[5].Replace('.', ','), out price);

                            //check if all elements are succesfully parsed, if yes call add method
                            if (convertSku && convertQuantity && convertPrice)
                                inventory.Add(sku, name, quantity, price);
                            else
                                Console.WriteLine("Add item in expected input format!");
                        }
                        else
                            Console.WriteLine("Some input parameters missing!");
                    }
                    //user END command
                    else if (userInput.Contains("END"))
                    {
                        Console.WriteLine("Adding items to inventory store finished.");
                        //set stage one to finished
                        stageOneFinished = true;
                        userInput = string.Empty;
                    }
                }

                else
                {
                    //display message just once, on proceeding to stage two
                    if (inventory.items.Count == i - 1)
                        Console.WriteLine("Additem to the current shopping cart in expected input format: \nADD - sku(number) quantity(number)");

                    userInput = Console.ReadLine();

                    if (userInput.Contains("ADD"))
                    {
                        if (userInput.Split(' ').Length == 4)
                        {
                            int sku, quantity;
                            bool convertSku = int.TryParse(userInput.Split(' ')[2], out sku);
                            bool convertQuantity = int.TryParse(userInput.Split(' ')[3], out quantity);

                            if (convertSku && convertQuantity)
                                currentCart = shoppingCart.Add(sku, quantity);
                            else
                                Console.WriteLine("Add item in expected input format!");
                        }
                        else
                            Console.WriteLine("Some input parameters missing!");
                    }

                    else if (userInput.Contains("REMOVE"))
                    {
                        if (userInput.Split(' ').Length == 4)
                        {
                            var sku = int.Parse(userInput.Split(' ')[2]);
                            var quantity = int.Parse(userInput.Split(' ')[3]);

                            currentCart = shoppingCart.Remove(sku, quantity);
                        }

                        //call cart checkout method
                        else if (userInput.Contains("CHECKOUT"))
                        {
                            shoppingCart.Checkout(currentCart);
                        }

                        //exist console app
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
}
