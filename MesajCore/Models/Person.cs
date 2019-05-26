using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MesajCore.Models
{
    public class Person : IdentityUser
    {
        public virtual List<Mesaj> AlinanMesajlar { get; set; }
        public virtual List<Mesaj> GonderilenMesajlar { get; set; }
    }
}
