using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UI.Services.Models
{
    [Serializable]
    public class ClassroomModel
    {
        public int id { get; set; }
        public string kod { get; set; }
        public string nazwa { get; set; }
        public string ilosc_miejsc { get; set; }
    }
}
