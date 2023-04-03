
using WebTestApi.Shared.Models;

namespace BlazorApiApp.Services
{
    public interface IStudentService
    {
        List<Student> Students { get; set; }
        Task GetStudents();
        Task<Student?> GetStudentById(int id);
        Task CreateStudent(Student student);
        Task UpdateStudent(int id,Student student);
        Task DeleteStudent(int id);

    }
}
