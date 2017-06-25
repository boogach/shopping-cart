using ShoppingCart.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingCart.Classes
{
    class Cart : BaseShoppingCart
    {
        public List<InventoryModel> inventory = new List<InventoryModel> { };
        public List<InventoryModel> cart = new List<InventoryModel> { };

        public Cart(List<InventoryModel> i) : base()
        {
            inventory = i;
        }

        public override List<InventoryModel> Add(int s, int q)
        {
            int inventoryQuantity = inventory.Where(x => x.Sku == s).Select(x => x.Quantity).FirstOrDefault();

            if (cart.Any())
            {
                var existingItem = cart.FirstOrDefault(x => x.Sku == s);

                if (existingItem != null)
                {
                    if (existingItem.Quantity < inventoryQuantity)
                        existingItem.Quantity = existingItem.Quantity + q;
                    else
                        Console.WriteLine("Can't add that many items to cart.");
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
                        Console.WriteLine("Can't add that many items to cart.");
                }
            }
            else
            {
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
                    Console.WriteLine("Can't add that many items to cart.");

            }

            base.Add(s, q);

            return cart;
        }

        public override void End()
        {
            base.End();
        }

        public override List<InventoryModel> Remove(int s, int q)
        {
            //cart.RemoveAll(f => f.Condition);

            var existingItem = cart.FirstOrDefault(x => x.Sku == s);

            if (existingItem != null)
            {
                existingItem.Quantity = existingItem.Quantity - q;
                if (existingItem.Quantity == 0)
                    cart.Remove(cart.Where(x => existingItem.Sku == s).FirstOrDefault());
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

                total = total + itemsTotal;
            }

            Console.WriteLine(string.Format("TOTAL = {0}", total));
            base.Checkout(itemsInCart);
        }
    }
}
