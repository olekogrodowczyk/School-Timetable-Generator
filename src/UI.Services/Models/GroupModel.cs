using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UI.Services.Models
{
    [Serializable]
    public class GroupModel
    {
        public int id { get; set; }
        public string name { get; set; }
        public int[] studentsIdArr { get; set; }
        public string[] studentsName { get; set; }
        public string teacher { get; set; }
        public string hours { get; set; }
    }
}