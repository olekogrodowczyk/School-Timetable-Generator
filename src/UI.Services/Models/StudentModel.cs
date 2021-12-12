using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UI.Services.Models
{
    [Serializable]
    public class StudentModel
    {
        public string id { get; set; }
        public string imie { get; set; }
        public string nazwisko { get; set; }
    }
}
