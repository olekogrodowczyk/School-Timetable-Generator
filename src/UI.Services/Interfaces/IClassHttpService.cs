using Shared.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UI.Services.Models;

namespace UI.Services.Interfaces
{
    public interface IClassHttpService
    {
        Task CreateClasses(List<ClassModel> model);

        Task<IEnumerable<string>> GetAllClassessNames();

        Task<IEnumerable<StudentVm>> GetAllStudentsFromClass(string className);
    }
}