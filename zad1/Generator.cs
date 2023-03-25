using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace JacekiMarcin{
    public class Generator{
        Random random;

        public Generator(int seed){
            this.random = new Random(seed);
        }

        public int rand(int low, int up){
            return random.Next(low, up);
        }
    }
}
