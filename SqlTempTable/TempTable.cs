using System;
using System.Collections.Generic;
using System.Text;

namespace SqlTempTable
{
    public static class TempTable<T> where T : new()
    {
        public static string CreateQuery(string TempTableName)
        {
            T _tempClass = new T();
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append("CREATE TABLE #").Append(TempTableName).Append("(").Append("\n");
            var length = _tempClass.GetType().GetProperties().Length - 1;
            for (int i = 0; i < length; i++)
            {
                var pi = _tempClass.GetType().GetProperties()[i];
                stringBuilder.Append("\t").Append(pi.Name).Append(" ").Append(SqlHelper.GetDbType(pi.PropertyType)).Append(",").Append("\n");
            }
            var lastpi = _tempClass.GetType().GetProperties()[length];
            stringBuilder.Append("\t").Append(lastpi.Name).Append(" ").Append(SqlHelper.GetDbType(lastpi.PropertyType)).Append("\n").Append(")");
            return stringBuilder.ToString();
        }
        public static string DropQuery(string TempTableName)
        {

            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append("DROP TABLE #").Append(TempTableName);
            return stringBuilder.ToString();
        }
    }
}
