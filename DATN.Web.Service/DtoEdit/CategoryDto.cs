﻿using DATN.Web.Service.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATN.Web.Service.DtoEdit
{
    public class CategoryDto : CategoryEntity
    {
        public int product_count { get; set; }
    }
}
