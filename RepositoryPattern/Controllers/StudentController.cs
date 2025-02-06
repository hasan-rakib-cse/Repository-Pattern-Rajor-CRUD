using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RepositoryPattern.IRepositories;
using RepositoryPattern.Models;
using RepositoryPattern.ViewModel;

namespace RepositoryPattern.Controllers
{
    public class StudentController : Controller
    {
        private readonly IStudentRepository _studentRepository;

        public StudentController(IStudentRepository studentRepository)
        {
            _studentRepository = studentRepository;
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult<VmResponseMessage>> Create(Student student)
        {
            if (!ModelState.IsValid)
            {
                return View(student);
            }
            var response = await _studentRepository.CreateStudent(student);
            TempData["Response"] = JsonConvert.SerializeObject(response);
            return RedirectToAction("Index", "Student");
        }

        [HttpGet]
        public async Task<ActionResult<List<Student>>> Index()
        {
            var response = await _studentRepository.GetStudentList();
            return View(response);
        }

        public async Task<ActionResult> Edit(int? id)
        {
            var response = await _studentRepository.EditStudent(id);
            if(response == null)
            {
                return NotFound();
            }
            return View(response);
        }

        [HttpPost]
        public async Task<ActionResult<VmResponseMessage>> Edit(int? id, Student student)
        {
            if (id == null || id != student.Id)
            {
                return View(student);
            }
            if (!ModelState.IsValid)
            {
                return View(student);
            }
            var response = await _studentRepository.EditStudent(id, student);
            TempData["Response"] = JsonConvert.SerializeObject(response);

            if(response.Type == "Success")
            {
                return RedirectToAction("Index", "Student");
            }
            return View(student);
        }
        public async Task<ActionResult> Delete(int? id)
        {
            var response = await _studentRepository.DeleteStudent(id);
            if (response == null)
            {
                return NotFound();
            }
            return View(response);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<ActionResult<VmResponseMessage>> DeleteConfirmed (int? id)
        {
            if(id is null)
            {
                return View();
            }
            if (!ModelState.IsValid)
            {
                return View();
            }
            var response = await _studentRepository.DeleteConfirmed(id);
            TempData["Response"] = JsonConvert.SerializeObject(response);
            if(response.Type == "Success")
            {
                return RedirectToAction("Index", "Student");
            }
            return View();
        }
    }
}
