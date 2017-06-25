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
        //List used for Inventory Store
        public List<InventoryModel> items = new List<InventoryModel> { };

        //List used for current cart
        List<InventoryModel> cartList = new List<InventoryModel> { };

        //base class constructor
        public BaseShoppingCart()
        {

        }

        //Base class virtual methods
        public virtual void Add(int s, string name, int q, float p)
        {
        }

        //overloaded add method by type and number of arguments
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
