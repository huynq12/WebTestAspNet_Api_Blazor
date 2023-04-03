using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using WebTestApi.Repositories;
using WebTestApi.Shared.Models;

namespace WebTestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    
    public class StudentController : ControllerBase
    {
        private readonly IStudentRepository _studentRepository;
        private readonly IMapper _mapper;

        public StudentController(IStudentRepository studentRepository,IMapper mapper)
        {
            _studentRepository = studentRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll() {
            var students = await _studentRepository.GetStudents();
            //var studentDtos = _mapper.Map<List<StudentDto>>(students);
            return Ok(students);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var student = await _studentRepository.GetStudentById(id);
			if (student == null)
			{
				return NotFound("Student is not exist");
			}
			//var studentDto = _mapper.Map<StudentDto>(student);
            return Ok(student);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Student student)
        {
            await _studentRepository.Create(student);
            return Ok();
        }

        /*[HttpPut("{id}")]
        public async Task<IActionResult> UpdateStudentDto(int id, [FromBody] StudentDto studentUpdateModel)
        {
            var existingStudent = await _studentRepository.GetStudentById(id);

            if (existingStudent == null)
            {
                return NotFound();
            }

            existingStudent.Name = studentUpdateModel.Name;
            existingStudent.GPA = studentUpdateModel.GPA;

            var updatedStudent = await _studentRepository.Update(existingStudent);

            return Ok(updatedStudent);
        }*/
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateStudent(int id, [FromBody] Student studentUpdateModel)
        {
            var existingStudent = await _studentRepository.GetStudentById(id);

            if (existingStudent == null)
            {
                return NotFound();
            }

            existingStudent.Name = studentUpdateModel.Name;
            existingStudent.GPA = studentUpdateModel.GPA;
            existingStudent.Phone = studentUpdateModel.Phone;
            existingStudent.Email = studentUpdateModel.Email;

            var updatedStudent = await _studentRepository.Update(existingStudent);

            return Ok(updatedStudent);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var studentFromDb = await _studentRepository.GetStudentById(id);
            if (studentFromDb == null)
            {
                return NotFound($"{id} is not found");
            }
            var deletedStudent = await _studentRepository.Delete(studentFromDb);
            return Ok(deletedStudent);

        }
    }
}
