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
         //Te sa od marcina. trzeba poprawic nazwy
        
            [TestClass]
    public class UnitTest1
    {
        //Test sprawdzający, czy funkcja zwraca oczekiwany wynik dla pustego plecaka.
        [TestMethod]
        public void TestMethod1()
        {                      
            Backpack backpack = new Backpack(0);
            bool Pusty = backpack.czyPusty();
            Assert.IsTrue(Pusty);
        }

        //Test sprawdzający, czy funkcja zwraca oczekiwany wynik dla plecaka, w którym przedmioty mają różne wagi i wartości.
        [TestMethod]
        public void TestMethod2()
        {
            List<Items> list = new List<Items>();
            list.Add(new Items(2, 15));
            list.Add(new Items(1, 10));
            Backpack backpack = new Backpack(3);
            backpack.add_items(list);

            int warto = backpack.pokazWartosc();
            int wag = backpack.pokazWage();

            Assert.AreEqual(25, warto);
            Assert.AreEqual(3, wag);


        }


        //Test sprawdzający, czy funkcja zwraca oczekiwany wynik, gdy plecak może pomieścić tylko część przedmiotów.
        [TestMethod]
        public void TestMethod3()
        {
            List<Items> list = new List<Items>();
            list.Add(new Items(2, 15));
            list.Add(new Items(1, 10));
            Backpack backpack = new Backpack(2);
            backpack.add_items(list);

            int warto = backpack.pokazWartosc();
            int wag = backpack.pokazWage();

            Assert.AreEqual(15, warto);
            Assert.AreEqual(2, wag);
        }

        //Test sprawdzający, czy funkcja zwraca oczekiwany wynik dla dużych zestawów danych, które mogą powodować przeciążenia pamięci.
        [TestMethod]
        public void TestMethod4()
        {
            List<Items> list = new List<Items>();
            for (int i = 0; i < 20; i++)
            {
                for (int j = 0; j < 20; j++)
                {
                    list.Add(new Items(i, j));
                }
            }
            Backpack backpack = new Backpack(8000);
            backpack.add_items(list);

            int warto = backpack.pokazWartosc();
            int wag = backpack.pokazWage();

            Assert.AreEqual(15, warto);
            Assert.AreEqual(2, wag);
        }
    }
}
    }
}
