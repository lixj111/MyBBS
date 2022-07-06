using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace MyBBSWebApi.Dal.Core
{
    public class SqlHelper
    {
        //程序中变量的null和数据库中的DBNull不同
        /// <summary>
        /// value为null时，输入数据库前转换为数据库的DBNull
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static object ToDbValue(object value)
        {
            return value==null?DBNull.Value:value;
        }

        /// <summary>
        /// 从数据库取数据时，取出的是否为DBNull,是则转换为null，反之返回正常数据
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static object FromDbValue(object value){
            return value==DBNull.Value?null:value;
        }
    }
}
