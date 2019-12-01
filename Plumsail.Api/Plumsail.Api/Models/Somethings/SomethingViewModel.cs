using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Plumsail.Api.Models.Somethings
{    
    public class SomethingViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int TypeId { get; set; }
        public string TypeName { get; set; }
        public string Time { get; set; }
        public int Status { get; set; }
        public string StatusName { get; set; }
        public bool IsImportant { get; set; }
        public string Comment { get; set; }
    }
}
