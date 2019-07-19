using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HttpAttributesCore.Models;
using HttpAttributesCore.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace HttpAttributesCore.Controllers
{
    [Route("[controller]")]
    public class AttributeController : Controller
    {
        private UniversityService _universityService;
        private StudentService _studentService;
        public AttributeController()
        {
            _universityService = new UniversityService();
            _studentService = new StudentService();
        }
        public IActionResult Index()
        {
            return View();
        }


        [Route("GetStudents/{universityId?}")]
        public async void GetStudents(int universityId)
        {
            try
            {
                University university;
                university = _universityService.GetUniversityById(universityId);

                try
                {
                    IFormFile Ifile = null;
                    var onAddFile = await _universityService.AddFile(Ifile);
                    var filePostBack = JsonConvert.DeserializeObject<FileStruct>(onAddFile);

                    try
                    {
                        Student student = new Student
                        {
                            Name = "Pat Rogi",
                            University = university.Id,
                            File = filePostBack.Id
                        };

                        var onAddStudent = await _studentService.AddStudent(student);
                        
                    }
                    catch (Exception)
                    {
                        throw;
                    }
                }
                catch (Exception)
                {
                    throw;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}