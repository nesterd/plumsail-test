using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Plumsail.Domain
{
    public class Something
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int TypeId { get; set; }
        [ForeignKey("TypeId")]
        public SomethingType Type { get; set; }
        public SomethingStatus Status { get; set; }
        public DateTime Time { get; set; }
        public bool IsImportant { get; set; }
        public string Comment { get; set; }
    }

    public enum SomethingStatus
    {
        [Description("Планируется")]
        Planning = 0,
        [Description("В работе")]
        InWork = 1,
        [Description("Готово")]
        Done = 2
    }
}
