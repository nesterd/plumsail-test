using Plumsail.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Plumsail.Api.Models.Somethings
{
    public class SomethingCreateViewModel
    {
        [Required]
        public string Name { get; set; }
        public int TypeId { get; set; }
        public SomethingStatus Status { get; set; }
        [Required]
        public string Time { get; set; }
        public bool IsImportant { get; set; }
    }
}
