using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ShoppingCart.Classes;
using ShoppingCart.Models;
using System.Collections.Generic;

namespace ShoppingCartTests
{

    [TestClass]
    public class Tests
    {
        Cart cart;
        InventoryStore inventoryStore;
        List<InventoryModel> items = new List<InventoryModel> { };
        List<InventoryModel> shoppingCart = new List<InventoryModel> { };

        public Tests()
        {
            inventoryStore = new InventoryStore();

            items.Add(new InventoryModel
            {
                Sku = 1,
                Name = "test",
                Quantity = 2,
                Price = 3.43f
            });

            cart = new Cart(items);
        }
         
        [TestMethod]
        public void TestInventoryEndMethod()
        {
            inventoryStore.End();
        }

        [TestMethod]
        public void TestInventoryAddMethod()
        {
            inventoryStore.Add(1, "test", 1, 3.4f);

            Assert.IsNotNull(inventoryStore.items);
        }

        [TestMethod]
        public void TestCartItemsAdd()
        {
            shoppingCart = cart.Add(1, 2);

            Assert.IsNotNull(shoppingCart);
        }

        [TestMethod]
        public void TestCartEndMethod()
        {
            cart.End();
        }

        [TestMethod]
        public void TestCartCheckoutMethod()
        {
            cart.Checkout(shoppingCart);
        }
    }
}
