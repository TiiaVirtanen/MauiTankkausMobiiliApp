using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiTankkausApp.Models
{
    class Ajoneuvot
    {
        public int AjoneuvoId { get; set; }

        public string Rekisterinumero { get; set; } = null!;

        public string? Merkki { get; set; }

        public string? Malli { get; set; }

        public byte[]? Kuva { get; set; }

        public int? KäyttäjäId { get; set; }
    }
}
