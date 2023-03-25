using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
[assembly: InternalsVisibleTo("UnitTestProject1"), InternalsVisibleTo("Plecak_okienko)")]

namespace JacekiMarcin{
    internal class Program{
        static void Main(string[] args){
            List<Items> Item = new List<Items>();
            int a = 0;
            int b = 0;
            Console.Write("Podaj ilość dostępnych przedmiotów: ");
            int amount = int.Parse(Console.ReadLine());
            Console.Write("Podaj seed: ");
            int seed = int.Parse(Console.ReadLine());
            Console.Write("Podaj pojemność plecaka: ");
            int backpack_limit = int.Parse(Console.ReadLine());

            Generator rng = new Generator(seed);
            Backpack storage = new Backpack(backpack_limit);

            for (int i = 0; i < amount; i++) { //list of possible items
                a = rng.rand(1, 20);
                b = rng.rand(15, 200);
                Item.Add(new Items(a, b));
                Console.WriteLine(Item[i].worth + "    " + Item[i].weight);
            }

            storage.add_items(Item);
            Console.WriteLine("");
            for(int k=0; k < storage.inside.Count; k++)
            {
                Console.WriteLine(storage.inside[k].worth + "    " + storage.inside[k].weight);
            }

            Console.Read();
        }
    }
}
