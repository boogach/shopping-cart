using ShoppingCart.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingCart.Classes
{
    public class BaseShoppingCart
    {
        public List<InventoryModel> items = new List<InventoryModel> { };
        List<InventoryModel> cartList = new List<InventoryModel> { };
        public BaseShoppingCart()
        {

        }

        public virtual void Add(int s, string name, int q, float p)
        {
        }

        public virtual List<InventoryModel> Add(int s, int q)
        {
            return cartList;
        }

        public virtual void End()
        {
        }

        public virtual List<InventoryModel> Remove(int s, int q)
        {
            return cartList;
        }

        public virtual void Checkout(List<InventoryModel> itemList)
        {
        }
    }
}
