using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using JacekiMarcin;
using System.Collections.Generic;

namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]

        public void TestMethod1()   //test sprawdza czy cos w plecaku jesli min jeden element spelnia ograniczenie
        {
            Backpack test_backpack = new Backpack(0);
            bool Empty = test_backpack.IsEmpty();
            Assert.IsTrue(Empty);

        }



        [TestMethod]
        public void TestMethod2()   //test czy inicjowany plecak jest pusty
        {
            List<Items> list = new List<Items>();
            list.Add(new Items(2, 15));
            for(int i=0; i<10;  i++) 
            {
                list.Add(new Items(20, 20));
            }
            Backpack test_backpack = new Backpack(5);

            test_backpack.add_items(list);

            Assert.AreNotEqual(test_backpack.inside.Count, 0);
                
        }

        [TestMethod]
        public void TestMethod3()  //test sprawdza czy plecak pusty jeśli żaden element nie spełnia ograniczenia
        {
            List<Items> list = new List<Items>();
            for (int i = 0; i < 10; i++)
            {
                list.Add(new Items(20, 20));
            }
            Backpack test_backpack = new Backpack(5);

            test_backpack.add_items(list);

            Assert.AreEqual(test_backpack.inside.Count, 0);
        }
    }
}
