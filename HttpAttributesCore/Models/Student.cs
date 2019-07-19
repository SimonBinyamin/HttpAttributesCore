using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HttpAttributesCore.Models
{
    public class Student
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? University { get; set; }
        public int? File { get; set; }
    }
}
