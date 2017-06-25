using ShoppingCart.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingCart.Classes
{
    public class InventoryStore : BaseShoppingCart
    {
        public InventoryStore() : base()
        {

        }

        public override void Add(int s, string n, int q, float p)
        {
            items.Add(new InventoryModel
            {
                Sku = s,
                Quantity = q,
                Price = p,
                Name = n
            });
            base.Add(s, n, q, p);
        }

        public override void End()
        {
            base.End();
        }
    }
}
