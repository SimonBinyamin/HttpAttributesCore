using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HttpAttributesCore.Models
{

    public class JsonStruct<T>
    {
        public Root<T> _root { get; set; }
    }

    public class Root<T>
    {
        public IEnumerable<T> Objects { get; set; }
    }


}
