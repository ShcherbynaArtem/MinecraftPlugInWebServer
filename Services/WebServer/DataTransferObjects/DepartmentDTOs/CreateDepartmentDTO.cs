﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataTransferObjects
{
    public class CreateDepartmentDTO
    {
        public int id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
    }
}