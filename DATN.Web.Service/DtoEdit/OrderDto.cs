﻿using DATN.Web.Service.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATN.Web.Service.DtoEdit
{
    public class OrderDto :OrderEntity
    {
        public List<ProductOrderEntity> Products { get; set; } = new List<ProductOrderEntity>();
    }
}
