﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Plumsail.Api.Models.Options
{
    public class BaseOption<T>
    {
        public T Value { get; set; }
        public string Text { get; set; }
    }
}
