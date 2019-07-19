using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using HttpAttributesCore.Models;
using Newtonsoft.Json.Linq;

namespace HttpAttributesCore.Service
{
    public class StudentService
    {
        public List<Student> GetStudentsByUniversityId(int universityId)
        {
            if (JsonUri<Student>.ServerStatus("path") == (int)HttpStatusCode.OK)
            {
                var StudentJson = JsonUri<JsonStruct<Student>>.GetSingleFromJson("path/" + universityId);
                return (from v in StudentJson._root.Objects
                        select v).ToList();
            }
            else
            {
                var StudentJson = JsonUri<JsonStruct<Student>>.GetSingleFromLocalJson("localPath");
                return (from v in StudentJson._root.Objects
                        select v).ToList();
            }
        }

        public async Task<string> PutStudent(Student student, int id)
        {
            using (var client = new HttpClient())
            {

                JObject JStudent = (JObject)JToken.FromObject(new
                {
                    Name = student.Name,
                    University = student.University
                });

                client.BaseAddress = new Uri("path/" + id);

                client.DefaultRequestHeaders.Add("user", "pass");

                HttpContent content = new StringContent(JStudent.ToString(), Encoding.UTF8, "application/json");

                var result = await client.PutAsync("path/" + id, content);

                string resultContent = await result.Content.ReadAsStringAsync();

                return resultContent;

            }
        }

        public async Task<string> AddStudent(Student student)
        {

            using (var client = new HttpClient())
            {
                JObject JStudent = (JObject)JToken.FromObject(new
                {
                    Name = student.Name,
                    University = student.University,
                    File = student.File
                });

                client.BaseAddress = new Uri("path");

                client.DefaultRequestHeaders.Add("user", "pass");

                HttpContent content = new StringContent(JStudent.ToString(), Encoding.UTF8, "application/json");

                var result = await client.PostAsync("path", content);

                return await result.Content.ReadAsStringAsync();
            }
        }


    }
}
