using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace game
{
    public class Project
    {
        public string name;
        public int[] checkindexes;
        public bool used;

        public Project(string name, int[] checkindexes)
        {
            this.name = name;
            this.checkindexes = checkindexes;
            this.used = false;
        }

        public bool IsUsed()
        {
            if(used) return true;
            else
            {
                used = true;
                return false;
            }
        }

    }
}
