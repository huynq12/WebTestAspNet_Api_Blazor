using Microsoft.AspNetCore.Mvc;
using WebTestApi.Shared.Models;

namespace WebTestApi.Repositories
{
    public interface IStudentRepository
    {
        Task<IEnumerable<Student>> GetStudents();
        Task<Student> GetStudentById(int id);
        Task<Student> Create(Student student);
        Task<Student> Update(Student student);
        Task<Student> Delete(Student student);
    }
}
