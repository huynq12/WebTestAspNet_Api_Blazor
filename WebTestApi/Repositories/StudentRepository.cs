using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebTestApi.Data;
using WebTestApi.Shared.Models;

namespace WebTestApi.Repositories
{
    public class StudentRepository : IStudentRepository

    {
        private readonly DataContext _context;

        public StudentRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<Student> Create(Student student)
        {
            _context.Students.Add(student);        
            await _context.SaveChangesAsync();
            return student;
        }

        public async Task<Student> Delete(Student student)
        {
            _context.Students.Remove(student);
            await _context.SaveChangesAsync();
            return student;
        }

        public async Task<Student> GetStudentById(int id)
        {
            return await _context.Students.FindAsync(id);
        }

        public async Task<IEnumerable<Student>> GetStudents()
        {
            return await _context.Students.ToListAsync();
        }

        public async Task<Student> Update(Student student)
        {
            _context.Students.Update(student);
            await _context.SaveChangesAsync();
            return student;
        }
    }
}
