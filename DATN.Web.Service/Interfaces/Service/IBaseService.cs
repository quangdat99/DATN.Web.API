﻿using DATN.Web.Service.DtoEdit;
using DATN.Web.Service.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATN.Web.Service.Interfaces.Service
{
    public interface IBaseService
    {
        /// <summary>
        /// Thêm dữ liệu
        /// </summary>
        /// <param name="entity">dữu liệu</param>
        Task<object> InsertAsync<T>(object entity);


        /// <summary>
        /// Cập nhật dữ liệu
        /// </summary>
        /// <param name="entity">dữu liệu</param>
        Task<object> UpdateAsync<T>(object entity);


        /// <summary>
        /// Lấy dữ liệu Combobox
        /// </summary>
        Task<IList> GetComboboxPaging<T>(string columns, string filter, string sort);

        /// <summary>
        /// Lấy dữ liệu bảng
        /// </summary>
        Task<DAResult> GetDataTable<T>(FilterTable filterTable);

    }
}
