using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UI.Services.Models
{
    [Serializable]
    public class SubjectModel
    {
        public string name { get; set; }
        public int id { get; set; }
        public List<GroupModel> groupSubjectList { get; set; }
    }
}