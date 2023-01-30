using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Praca_H_Bobr_A_Orkisiewicz
{
    class Receipt
    {
        public int Id { get; set; }
        public string Przedmiot { get; set; }
        public double Cena { get; set; }
        public int Ilość { get; set; }
        public string Suma { get { return string.Format("{0}$", Cena * Ilość); } }
    }
}
