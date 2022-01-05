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
        Task CreateClass(ClassModel model);
        Task<IEnumerable<ClassVm>> GetAllClassess();
        Task<IEnumerable<string>> GetAllClassessNames();
        Task<IEnumerable<StudentVm>> GetAllStudentsFromClass(string className);
        Task<int> GetClassessCount();
    }
}