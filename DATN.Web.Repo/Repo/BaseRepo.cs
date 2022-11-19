using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using DATN.Web.Repo.Mysql;
using DATN.Web.Service.Attributes;
using DATN.Web.Service.Interfaces.Repo;
using Microsoft.Extensions.Configuration;

namespace DATN.Web.Repo.Repo
{
    public class BaseRepo : IBaseRepo
    {
        #region DECLARE

        /// <summary>
        /// Chuỗi thông tin kết nối
        /// </summary>
        string _connectionString;

        /// <summary>
        /// Config của project
        /// </summary>
        IConfiguration _configuration;

        protected IDataBaseProvider _dbProvider;

        #region CONSTRUCTOR

        /// <summary>
        /// Phương thức khởi tạo
        /// </summary>
        /// <param name="configuration">Config của project</param>
        public BaseRepo(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("DefaultConnection");
        }

        #endregion

        public IConfiguration GetConfiguration()
        {
            return _configuration;
        }

        protected IDataBaseProvider Provider
        {
            get
            {
                if (_dbProvider == null)
                {
                    _dbProvider = this.CreateProvider(_connectionString);
                }

                return _dbProvider;
            }
        }

        #endregion

        #region Method

        protected virtual IDataBaseProvider CreateProvider(string connectionString)
        {
            return new MySqlProvider(connectionString);
        }

        public async Task<bool> DeleteAsync(object entity)
        {
            var query = this.GetDeleteQuery(entity.GetType());
            var res = await this.Provider.ExecuteNoneQueryTextAsync(query, entity);
            return res > 0;
        }

        public async Task<List<T>> GetAsync<T>()
        {
            var table = this.GetTableName(typeof(T));
            var query = $"SELECT * FROM {table}";
            Dictionary<string, object> param = null;
            var result = await this.Provider.QueryAsync<T>(query, param);
            return result;
        }

        public async Task<T> GetByIdAsync<T>(object id)
        {
            var sql = this.BuildQueryById(typeof(T));
            var result = await this.Provider.QueryAsync<T>(sql, new Dictionary<string, object> { { "key", id } });
            return result.FirstOrDefault();
        }

        public async Task<List<T>> GetAsync<T>(string field, object value, string op = "=")
        {
            // xử lý safe toán tử
            var sop = this.SafeOperation(op);
            var param = new Dictionary<string, object>();
            var sql = this.BuildSelectByFieldQuery(typeof(T), param, field, value, op = sop);
            var result = await this.Provider.QueryAsync<T>(sql, param);
            return result;
        }

        public async Task<T> InsertAsync<T>(object entity)
        {
            var query = this.GetInsertQuery(entity.GetType(), entity);
            var res = await this.Provider.ExcuteScalarTextAsync(query, entity);
            if ((res is int && (int)res > 0) || (res is long && (long)res > 0))
            {
                this.updateEntityKey(entity, res);
            }

            var model = await GetReturnRecordAsync<T>(entity);
            if (model != null)
            {
                return model;
            }

            return default(T);
        }

        public async Task<T> UpdateAsync<T>(object entity, string fields = null)
        {
            var query = this.GetUpdateQuery(entity.GetType(), entity, fields);
            var res = await this.Provider.ExecuteNoneQueryTextAsync(query, entity);
            if (res > 0)
            {
                var model = await GetReturnRecordAsync<T>(entity);
                if (model != null)
                {
                    return model;
                }
            }

            return default(T);
        }

        public string GetTableName(Type type)
        {
            var attr = type.GetCustomAttribute<TableAttribute>();
            if (attr == null)
            {
                return null;
            }

            return "`" + attr.Table + "`";
        }

        protected virtual string BuildQueryById(Type type)
        {
            var table = this.GetTableName(type);
            var key = table.Replace("`", "");
            var prKey = $"{key}_id";
            return $"SELECT * FROM {table} WHERE {prKey} = @key";
        }

        protected string SafeOperation(string op)
        {
            if (op.Contains(";") || op.Contains("'"))
            {
                throw new Exception($"Không hỗ trợ toán tử {op}");
            }

            return op;
        }

        protected virtual string BuildSelectByFieldQuery(Type type, Dictionary<string, object> param, string field,
            object value, string op = "=", string columns = "*")
        {
            var sop = this.SafeOperation(op);
            var table = this.GetTableName(type);
            var sb = new StringBuilder($"SELECT {columns} FROM {table} WHERE {field} {sop} ");
            if (sop == "in" || sop == "not in")
            {
                IList vl = (IList)value;
                sb.Append("(");
                for (int i = 0; i < vl.Count; i++)
                {
                    if (i > 0)
                    {
                        sb.Append(",");
                    }

                    var p = $"p{i}";
                    sb.Append($"@{p}");
                    param[p] = vl[i];
                }

                sb.Append(")");
            }
            else
            {
                sb.Append("@value");
                param["value"] = value;
            }

            return sb.ToString();
        }

        protected virtual string GetInsertQuery(Type type, object entity)
        {
            var fields = this.GetTableColumns(type);
            var tableName = this.GetTableName(type);
            var query =
                $"INSERT INTO {tableName} (`{string.Join("`,`", fields)}`) VALUE(@{string.Join(",@", fields)});";
            var keys = this.GetKeyFields(type);
            if (keys.Count == 1)
            {
                query += "select last_insert_id()";
            }

            return query;
        }

        public List<string> GetTableColumns(Type type)
        {
            var fields = new List<string>();

            var prs = type.GetProperties();
            foreach (var item in prs)
            {
                if (item.GetCustomAttribute<IgnoreUpdateAttribute>() == null)
                {
                    fields.Add(item.Name);
                }
            }

            return fields;
        }

        public PropertyInfo GetKeyField(Type type)
        {
            return GetKeyFields(type).FirstOrDefault();
        }

        public List<PropertyInfo> GetKeyFields(Type type)
        {
            var keys = this.GetPropertys<KeyAttribute>(type);
            return keys.Select(n => n.Key).ToList();
        }

        public Dictionary<PropertyInfo, TAttribute> GetPropertys<TAttribute>(Type type) where TAttribute : Attribute
        {
            if (type == null)
            {
                return null;
            }

            var result = new Dictionary<PropertyInfo, TAttribute>();
            var prs = type.GetProperties();
            foreach (var pr in prs)
            {
                var attr = pr.GetCustomAttribute<TAttribute>(true);
                if (attr != null)
                {
                    result.Add(pr, attr);
                }
            }

            return result;
        }

        /// <summary>
        /// Cập nhật khóa chính cho entity sau khi insert
        /// Câu lệnh insert sẽ trả về newid
        /// </summary>
        /// <param name="entity">Dữ liệu mang đi cất</param>
        /// <param name="excecuteResult">Kết quả thực hiện</param>
        protected virtual void updateEntityKey(object entity, object excecuteResult)
        {
            if (excecuteResult != null)
            {
                var pkId = GetKeyField(entity.GetType());
                if (pkId != null)
                {
                    if (pkId.PropertyType == typeof(Int32))
                    {
                        pkId.SetValue(entity, Convert.ToInt32(excecuteResult));
                    }
                    else if (pkId.PropertyType == typeof(Int64))
                    {
                        pkId.SetValue(entity, Convert.ToInt64(excecuteResult));
                    }
                }
            }
        }

        protected virtual async Task<T> GetReturnRecordAsync<T>(object model)
        {
            var keyField = this.GetKeyField(model.GetType());
            var masterId = keyField.GetValue(model);

            var data = await this.GetByIdAsync<T>(masterId);
            return data;
        }

        protected virtual string GetUpdateQuery(Type type, object entity, string fields = null)
        {
            var columns = GetTableColumns(type);
            var key = GetKeyField(type);
            List<string> updateFields;
            if (string.IsNullOrEmpty(fields))
            {
                updateFields = columns.Where(n => n != key.Name).ToList();
            }
            else
            {
                updateFields = new List<string>();
                foreach (var column in fields.Split(","))
                {
                    foreach (var filed in columns)
                    {
                        if (filed.Equals(column, StringComparison.OrdinalIgnoreCase))
                        {
                            updateFields.Add(filed);
                        }
                    }
                }
            }

            var table = this.GetTableName(type);
            if (string.IsNullOrEmpty(table)) throw new Exception($"Not found table in type {type} ");

            var query =
                $"UPDATE {table} SET {string.Join(", ", updateFields.Select(n => $"`{n}`=@{n}"))} WHERE `{key.Name}`=@{key.Name};";
            return query;
        }

        protected virtual string GetDeleteQuery(Type type)
        {
            var key = GetKeyField(type);
            var table = this.GetTableName(type);
            var query = $"DELETE FROM {table} WHERE {key.Name} = @{key.Name};";
            return query;
        }

        #endregion
    }
}