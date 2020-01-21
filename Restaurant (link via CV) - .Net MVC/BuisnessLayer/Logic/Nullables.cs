using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuisnessLayer.Logic
{
    public class Nullables
    {
        public string isnah (string value)
        {
            if (value == "N/A"){ return null;}
            else return value;
        }
    }
}
