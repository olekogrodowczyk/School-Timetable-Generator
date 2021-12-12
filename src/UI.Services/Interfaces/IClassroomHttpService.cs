using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UI.Services.Models;

namespace UI.Services.Interfaces
{
    public interface IClassroomHttpService
    {
        Task CreateClassrooms(List<ClassroomModel> models);
    }
}
