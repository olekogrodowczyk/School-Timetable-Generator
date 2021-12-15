using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UI.Services.Models;

namespace UI.Services.Interfaces
{
    public interface ISubjectHttpService
    {
        Task AddSubjectsWithGroups(List<SubjectModel> subjectsWithGroups, string className);
    }
}