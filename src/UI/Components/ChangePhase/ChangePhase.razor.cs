using Blazored.Toast.Services;
using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;
using UI.Services.Interfaces;

namespace UI.Components.ChangePhase
{
    public partial class ChangePhase
    {
        [Parameter]
        public string Link { get; set; }

        [Inject]
        public IClassHttpService ClassHttpService { get; set; }

        [Inject]
        public IStudentHttpService StudentHttpService { get; set; }

        [Inject]
        public ITeacherHttpService TeacherHttpService { get; set; }

        [Inject]
        public IClassroomHttpService ClassroomHttpService { get; set; }

        [Inject]
        public ITimetableStateHttpService TimetableStateHttpService { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        [Inject]
        public IToastService ToastService { get; set; }

        protected async Task GoToSecondPhase()
        {
            int classCount = await ClassHttpService.GetClassessCount();
            int studentCount = await StudentHttpService.GetStudentsCount();
            int teacherCount = await TeacherHttpService.GetTeachersCount();
            int classroomCount = await ClassroomHttpService.GetClassroomsCount();
            if (classCount == 0 || studentCount == 0 || teacherCount == 0 || classroomCount == 0)
            {
                ToastService.ShowError("Nie wszystkie dane są wypełnione");
                return;
            }
            int destinationPhase = 2;
            await TimetableStateHttpService.ChangeCurrentPhase(destinationPhase);
            NavigationManager.NavigateTo("addsubjects");
        }
    }
}