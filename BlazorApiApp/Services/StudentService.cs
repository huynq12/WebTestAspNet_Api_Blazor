using Microsoft.AspNetCore.Components;
using System.Net;
using WebTestApi.Shared.Models;
using static System.Net.WebRequestMethods;

namespace BlazorApiApp.Services
{
	public class StudentService : IStudentService
	{
		private readonly HttpClient _httpClient;
        private readonly NavigationManager _navigationManager;

        public StudentService(HttpClient httpClient,NavigationManager navigationManager) {
			_httpClient = httpClient;
			_navigationManager = navigationManager; 
		}
		public List<Student> Students { get ; set ; } = new List<Student>();

		public async Task CreateStudent(Student student)
		{
			await _httpClient.PostAsJsonAsync("/api/Student", student);
			_navigationManager.NavigateTo("students");
		}

		public async Task DeleteStudent(int id)
		{
			await _httpClient.DeleteAsync($"/api/Student/{id}");
			_navigationManager.NavigateTo("students");
		}

		public async Task<Student?> GetStudentById(int id)
		{
			var result = await _httpClient.GetAsync($"/api/Student/{id}");
			if(result.StatusCode == HttpStatusCode.OK)
			{
				return await result.Content.ReadFromJsonAsync<Student>();
			}
			return null;
		}

		public async Task GetStudents()
		{
			var result = await _httpClient.GetFromJsonAsync<List<Student>>("/api/Student");
			if(result != null)
			{
				Students = result;
			}
		}

		public async Task UpdateStudent(int id, Student student)
		{
			await _httpClient.PutAsJsonAsync($"/api/Student/{id}", student);
			_navigationManager.NavigateTo("students");
		}
	}
}
