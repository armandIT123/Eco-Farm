﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    [AttributeUsage(AttributeTargets.Property)]
    public class IgnorePropertyAttribute : Attribute
    {
    }
}
