using ShoppingCart.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingCart.Classes
{
    public class Cart : BaseShoppingCart
    {
        //Lists used only inside Cart class
        private List<InventoryModel> inventory = new List<InventoryModel> { };
        private List<InventoryModel> cart = new List<InventoryModel> { };

        //Cart constructor taking list of all inventory items
        public Cart(List<InventoryModel> i) : base()
        {
            inventory = i;
        }

        //overriden methods
        public override List<InventoryModel> Add(int s, int q)
        {
            //check for specific item quantity
            int inventoryQuantity = inventory.Where(x => x.Sku == s).Select(x => x.Quantity).FirstOrDefault();

            //check if there is already some items in cart
            if (cart.Any())
            {
                //check for existing item in cart by sku
                var existingItem = cart.FirstOrDefault(x => x.Sku == s);

                //check if user is adding item that already exists in current cart
                //if item already exists proceed with adding just item quantity to cart
                if (existingItem != null)
                {
                    if (existingItem.Quantity < inventoryQuantity)
                    {
                        //if input quantity(q) is is lower or equal then difference between existing item quantity(existingItem.Quantity)
                        //and current inventory quantity(inventoryQuantity) increase its quantity in current shopping cart
                        if (q <= (inventoryQuantity - existingItem.Quantity))
                            existingItem.Quantity = existingItem.Quantity + q;
                        //inform user how many of that item is left in inventory
                        else
                            Console.WriteLine(string.Format("Can't add that many items to cart, only {0} available", (inventoryQuantity - existingItem.Quantity)));
                    }
                    else
                        Console.WriteLine(string.Format("Can't add that many items to cart, only {0} available", (inventoryQuantity - existingItem.Quantity)));
                }
                else
                {
                    if (q <= inventoryQuantity)
                        cart.Add(new InventoryModel
                        {
                            Sku = s,
                            Quantity = q,
                            Name = inventory.Where(x => x.Sku == s).Select(x => x.Name).FirstOrDefault(),
                            Price = inventory.Where(x => x.Sku == s).Select(x => x.Price).FirstOrDefault(),
                        });
                    else
                        Console.WriteLine(string.Format("Can't add that many items to cart, only {0} available", inventoryQuantity));
                }
            }
            //if cart is empty
            else
            {
                //check if user quantity input isn't greater then available items in inventory
                if (q <= inventoryQuantity)
                {
                    cart.Add(new InventoryModel
                    {
                        Sku = s,
                        Quantity = q,
                        Name = inventory.Where(x => x.Sku == s).Select(x => x.Name).FirstOrDefault(),
                        Price = inventory.Where(x => x.Sku == s).Select(x => x.Price).FirstOrDefault(),
                    });
                }
                else
                    Console.WriteLine(string.Format("Can't add that many items to cart, only {0} available", inventoryQuantity));

            }

            base.Add(s, q);

            return cart;
        }

        public override void End()
        {
            Environment.Exit(0);
            base.End();
        }

        public override List<InventoryModel> Remove(int s, int q)
        {
            //same as add method, check for existing item by sku
            var existingItem = cart.FirstOrDefault(x => x.Sku == s);

            if (existingItem != null)
            {
                //if item exists only lower its quantity
                existingItem.Quantity = existingItem.Quantity - q;

                //if existing item quantity is equal to 0 remove it from the list
                if (existingItem.Quantity == 0)
                    cart.RemoveAll(f => f.Sku == s);
            }

            base.Remove(s, q);

            return cart;
        }

        public override void Checkout(List<InventoryModel> itemsInCart)
        {
            float total = 0;
            foreach (var i in itemsInCart)
            {
                float itemsTotal = i.Quantity * i.Price;
                Console.WriteLine(string.Format("{0} {1} x {2} = {3}", i.Name, i.Quantity, i.Price, itemsTotal));

                //sum all items total
                total = total + itemsTotal;
            }

            Console.WriteLine(string.Format("TOTAL = {0}", total));
            base.Checkout(itemsInCart);
        }
    }
}
