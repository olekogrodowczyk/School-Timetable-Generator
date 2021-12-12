using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UI.Services.Models;

namespace UI.Services.Models
{
    [Serializable]
    public class ClassModel
    {
        public string id { get; set; }
        public string name { get; set; }
        public List<StudentModel> studentsArr  { get; set; }
    }
}
