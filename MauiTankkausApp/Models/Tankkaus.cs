using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiTankkausApp.Models
{
    class Tankkaus
    {
        public int Id { get; set; }
        public string Rekisterinumero { get; set; } = null!;
        public double Ajokilometrit { get; set; }
        public double Litraa { get; set; }
        public double Euroa { get; set; }
    }
}
