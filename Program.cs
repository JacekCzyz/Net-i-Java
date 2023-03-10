using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JacekiMarcin{
    internal class Program{
        static void Main(string[] args){
            List<Items> Przedmiot = new List<Items>();
            int a = 0;
            int b = 0;
            Console.WriteLine("Podaj ilość przedmiotów w plecaku");
            int iterations = int.Parse(Console.ReadLine());
            Console.Write("Podaj seed");
            int seed = int.Parse(Console.ReadLine());
            Generator rng = new Generator(seed);

            for (int i = 0; i < iterations; i++) {
                a = rng.rand(1, 20);
                b = rng.rand(15, 200);
                Przedmiot.Add(new Items(a, b));
            }
            for (int i = 0; i < iterations; i++)
            {
                Console.Write(i.ToString());
                Console.Write
            }

            Console.Read();
        }
    }
}
