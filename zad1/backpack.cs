using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JacekiMarcin
{
    internal class Backpack
    {
        public int weight_limit;
        public List<Items> inside = new List<Items>();
        public Backpack(int limit)
        {
            this.weight_limit = limit;
        }

        public int is_full()
        {
            int sum = 0;
            for (int i = 0; i < inside.Count(); i++) {
                sum += inside[i].weight;
            }

            if (sum == weight_limit)
                return 1;
            else if (sum > weight_limit)
                return -1;
            return 0;
        }

        public void Remove(Items item)
        {
            inside.Remove(item);
        }

        public void add_items(List<Items> item)
        {
            int j = 0;
            while (this.is_full() != 1 && j != item.Count) //putting items into backpack
            {
                inside.Add(item[j]);
                if (this.is_full() == -1) //if too much weight, take out the new item
                    this.Remove(item[j]);
                j++;
            }
        }

        public bool IsEmpty()
        {
            return inside.Count == 0;
        }


        public int ShowWorth()
        {
            int sum = 0;
            for (int i = 0; i<inside.Count(); i++) 
            {
                sum += inside[i].worth;
            }
            return sum;
        }

        public int ShowWeight()
        {
            int sum = 0;
            for (int i = 0; i < inside.Count(); i++)
            {
                sum += inside[i].weight;
            }
            return sum;
        }
    }
}


