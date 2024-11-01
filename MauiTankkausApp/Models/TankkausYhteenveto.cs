using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiTankkausApp.Models
{
    class TankkausYhteenveto
    {
        public int AjoneuvoId { get; set; }
        public int Tankkauskerrat {  get; set; }
        public decimal Kokonaiskulutus { get; set; }
        public decimal KäytettyEuromäärä {  get; set; }
    }
}
