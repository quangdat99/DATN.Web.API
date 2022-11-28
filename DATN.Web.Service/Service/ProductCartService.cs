﻿using DATN.Web.Service.Interfaces.Repo;
using DATN.Web.Service.Interfaces.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATN.Web.Service.Service
{
    public class ProductCartService : BaseService, IProductCartService
    {
        public ProductCartService(IProductCartRepo ProductCartRepo) : base(ProductCartRepo)
        {
        }
    }
}
