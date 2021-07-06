using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ToDo.DB
{
    public class Generic
    {
        internal string toDoConnection = ConfigurationManager.ConnectionStrings["ToDoConnection"].ConnectionString;

        #region Enums
        public enum Values
        {
            BLLErrorCode
        }
        #endregion
        public T DataReaderMapToObject<T>(IDataReader dr)
        {
            try
            {
                T obj = default(T);
                while (dr.Read())
                {
                    obj = Activator.CreateInstance<T>();
                    foreach (PropertyInfo prop in obj.GetType().GetProperties())
                    {
                        Type pt = prop.PropertyType;
                        if (pt == typeof(Guid) || pt == typeof(string) || pt == typeof(int) || pt == typeof(int?) || pt == typeof(TimeSpan) || pt == typeof(DateTime) || pt == typeof(DateTime?) || pt == typeof(decimal) || pt == typeof(decimal?) || pt == typeof(bool) || pt == typeof(bool?) || pt == typeof(short) || pt == typeof(short?) || pt == typeof(Double) || pt == typeof(Double?) || pt == typeof(long) || pt == typeof(long?) || pt == typeof(byte[]))
                        {

                            if (!object.Equals(dr[prop.Name], DBNull.Value))
                            {
                                prop.SetValue(obj, dr[prop.Name], null);
                            }
                        }
                    }
                }
                return obj;
            }
            catch (Exception)
            {

                throw;
            }

        }
        public List<T> DataReaderMapToList<T>(IDataReader dr)
        {
            try
            {
                List<T> list = new List<T>();
                T obj = default(T);
                while (dr.Read())
                {
                    obj = Activator.CreateInstance<T>();
                    foreach (PropertyInfo prop in obj.GetType().GetProperties())
                    {
                        Type pt = prop.PropertyType;
                        if (pt == typeof(string) || pt == typeof(int) || pt == typeof(int?) || pt == typeof(long) || pt == typeof(long?) || pt == typeof(decimal) || pt == typeof(decimal?) || pt == typeof(DateTime) || pt == typeof(DateTime?) || pt == typeof(bool) || pt == typeof(bool?) || pt == typeof(short) || pt == typeof(short?) || pt == typeof(Double) || pt == typeof(Double?) || pt == typeof(byte[]) || pt == typeof(Guid))
                        {

                            if (!object.Equals(dr[prop.Name], DBNull.Value))
                            {
                                prop.SetValue(obj, dr[prop.Name], null);
                            }
                        }
                    }
                    list.Add(obj);
                }
                return list;
            }
            catch (Exception)
            {

                throw;
            }

        }
       

    }
    public class StatusAPIOutput
    {

        public int code { get; set; }
        public string message { get; set; }
        public Object callBackObj { get; set; }
        public bool isSuccess { get; set; }
        public List<ErrorMessage> errorMessages { get; set; }
        public string RequestID { get; set; }
        public string errorID { get; set; }

    }
    public class ErrorMessage
    {
        public string errorCode { get; set; }
        public string errorText { get; set; }
    }

    public class RequestInput
    {
        public string RequestID { get; set; }
        public dynamic dPO { get; set; }
        public string RequestIPAddress { get; set; }
        public string LoggedInUserName { get; set; }
        public string LoggedInEmailID { get; set; }
        public string LoggedInUserID { get; set; }
        public string LoggedInFirstName { get; set; }
        public string LoggedInLastName { get; set; }
        public string LoggedHistoryID { get; set; }
        public string LoggedInRoleID { get; set; }
    }
  
}
