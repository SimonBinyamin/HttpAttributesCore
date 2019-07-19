using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace HttpAttributesCore.Models
{
    public class FileStruct
    {
        public int Id { get; set; }
        public string Contenttype { get; set; }
        public string Filename { get; set; }
        public int? size { get; set; }

        [NotMapped]
        public byte[] Byte { get; set; }
    }
}
