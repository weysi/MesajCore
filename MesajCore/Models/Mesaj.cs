using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MesajCore.Models
{
    public class Mesaj
    {
        public int Id { get; set; }
        public string Yazi { get; set; }
        public DateTime Tarih { get; set; }
        public string AliciId { get; set; }
        public string GonderenId { get; set; }
        public bool OkunduMu { get; set; }
        
        public virtual Person Alici { get; set; }
        public virtual Person Gonderen { get; set; }
    }
}
