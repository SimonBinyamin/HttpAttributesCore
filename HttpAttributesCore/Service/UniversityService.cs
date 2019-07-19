using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using HttpAttributesCore.Models;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json.Linq;

namespace HttpAttributesCore.Service
{
    public class UniversityService
    {
        public University GetUniversityById(int universityId) => 
            JsonUri<University>.GetSingleFromJson("/path");

        public async Task<string> AddUniversity(University university)
        {

            using (var client = new HttpClient())
            {
                JObject JUniversity = (JObject)JToken.FromObject(new{
                    Name = university.Name,
                    Location = university.Location,
                    Numberofsections = university.Numberofsections
                });

                client.BaseAddress = new Uri("path");

                client.DefaultRequestHeaders.Add("user", "pass");

                HttpContent content = new StringContent(JUniversity.ToString(), Encoding.UTF8, "application/json");

                var result = await client.PostAsync("path", content);

                return await result.Content.ReadAsStringAsync();
            }
        }


        public async Task<string> AddFile(IFormFile File)
        {
            byte[] fileBytes;
            using (var ms = new MemoryStream())
            {
                File.CopyTo(ms);
                fileBytes = ms.ToArray();
                string s = Convert.ToBase64String(fileBytes);
            }

            HttpResponseMessage res = new HttpResponseMessage();
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("path");
            client.DefaultRequestHeaders
                  .Accept
                  .Add(new MediaTypeWithQualityHeaderValue("application/json"));

            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, "path");

            var content = new ByteArrayContent(fileBytes);

            content.Headers.Add("filekey", "filecontent/name");

            content.Headers.Add("user", "pass");

            request.Content = content;

            await client.SendAsync(request)
                  .ContinueWith(responseTask =>
                  {
                      res = responseTask.Result;

                  });

            return await res.Content.ReadAsStringAsync();
        }

    }
}
