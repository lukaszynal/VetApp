using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VetApp.Classes;

namespace VetAppDal.Classes
{
    public class AnimalQueryParameters : QueryParameters
    {
        public string? Name { get; set; }
        public string? Owner { get; set; }
    }
}
