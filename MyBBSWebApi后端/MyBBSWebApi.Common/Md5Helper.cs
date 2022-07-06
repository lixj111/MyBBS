using System;
//using System.Security.Cryptography;
using System.Security.Cryptography;
using System.Text;


namespace MyBBSWebApi.Common
{
    public static class Md5Helper
    {
        //加上this，普通方法变拓展方法
        public static string ToMd5(this string str)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] output = md5.ComputeHash(Encoding.Default.GetBytes(str));//str先转为默认编码，再转换为字节数组
            var md5Str = BitConverter.ToString(output).Replace("-", "");//转换为bit字节串,把-替换为空字符串
            return md5Str;
        }

    }
}
