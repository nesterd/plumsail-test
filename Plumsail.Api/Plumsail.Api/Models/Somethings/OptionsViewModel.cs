using Plumsail.Api.Models.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Plumsail.Api.Models.Somethings
{
    public class OptionsViewModel
    {
        public List<IntOption> TypeOptions { get; set; }
        public List<IntOption> StatusOptions { get; set; }
    }
}
