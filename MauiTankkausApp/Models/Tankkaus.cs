using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiTankkausApp.Models
{
    class Tankkaus
    {
        public int TankkausId { get; set; }

        public int AjoneuvoId { get; set; }

        public int? Ajokilometrit { get; set; }

        public decimal? Litraa { get; set; }

        public decimal? Euroa { get; set; }

        public DateOnly? Päivämäärä { get; set; }

        public virtual Ajoneuvot? Ajoneuvo { get; set; }
    }
}
