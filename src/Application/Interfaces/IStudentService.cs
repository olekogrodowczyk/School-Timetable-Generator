using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IStudentService
    {
        Task DeleteStudent(int studentId);
        Task<int> GetStudentsCount();
    }
}