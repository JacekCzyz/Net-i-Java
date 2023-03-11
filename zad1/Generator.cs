using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace JacekiMarcin{
    internal class Generator{
        Random random;

        public Generator(int seed){
            this.random = new Random(seed);
        }

        public int rand(int low, int up){
            return random.Next(low, up);
        }
        public override string ToString()
        {
            int x = 1;
            string str = "tekst" + x.ToString() + " test";
            return "to jest nasz generator";
        }
    }
}
