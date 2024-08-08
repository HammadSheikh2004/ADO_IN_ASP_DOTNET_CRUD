using ADO.NET_Crud.DataAccess;
using ADO.NET_Crud.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Diagnostics;

namespace ADO.NET_Crud.Controllers
{
    public class StudentController : Controller
    {
        private readonly StudentDataAccess _studentDataAccess;
        private readonly IWebHostEnvironment _web;

        public StudentController(StudentDataAccess studentDataAccess, IWebHostEnvironment web)
        {
            _studentDataAccess = studentDataAccess;
            _web = web;
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpGet]
        public IActionResult DisplayStudent()
        {
            List<Student_Info> students = _studentDataAccess.GetData();
            return View(students);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Student_Info student, IFormFile Std_Image)
        {
            if (Std_Image == null)
            {
                return View();
            }

            _studentDataAccess.AddRecord(student, Std_Image);
            ViewBag.Message = "Data Inserted Successfully!";
            ModelState.Clear();
            return RedirectToAction("DisplayStudent");
        }

        public IActionResult Edit(int id)
        { 
           Student_Info student = _studentDataAccess.GetDataById(id);
           return View(student);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Student_Info studentData, IFormFile Std_Image)
        {
            if (Std_Image == null)
            {
                return View();
            }
            _studentDataAccess.UpdateData(studentData, Std_Image);
            return RedirectToAction("DisplayStudent");
        }

        public IActionResult Delete(int id)
        {
            _studentDataAccess.DeleteData(id);
            return RedirectToAction("DisplayStudent");
        }
    }
}
