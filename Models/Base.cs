using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Security.Cryptography;
using System.Text;
using System.IO;
using Models;

namespace Models
{
    public class Base
    {
        //db dblayer = new db();
        int Result = 0;
        private static Base Instance;
        private Base(){
            
        }
        public static Base getInstance()
        {
            if(Instance == null)
            {
                Instance = new Base();
            }
            return Instance;
        }

        public string GetEpochOf(DateTime date)
        {
            DateTime origin = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            TimeSpan diff = date.ToUniversalTime() - origin;
            return Math.Floor(diff.TotalSeconds).ToString();
        }
        public string CreateMD5String(string input)
        {
            //input = input + GetEpochOf(DateTime.Now);
            // Use input string to calculate MD5 hash
            using (System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create())
            {
                byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
                byte[] hashBytes = md5.ComputeHash(inputBytes);

                // Convert the byte array to hexadecimal string
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < hashBytes.Length; i++)
                {
                    sb.Append(hashBytes[i].ToString("X2"));
                }
                return sb.ToString();
            }
        }
        public string checkMD5File(string filename)
        {
            using (var md5 = MD5.Create())
            {
                using (var stream = File.OpenRead(filename))
                {
                    return BitConverter.ToString(md5.ComputeHash(stream)).Replace("-", string.Empty);
                }
            }
        }
        public string Encrypt(string inp)
        {
            string strmsg = string.Empty;
            string input = inp.Trim();
            byte[] encode = new byte[input.Length];
            encode = Encoding.UTF8.GetBytes(input);
            strmsg = Convert.ToBase64String(encode);
            return strmsg;
        }
        public string Decrypt(string encryptpwd)
        {
            string decryptpwd = string.Empty;
            UTF8Encoding encodepwd = new UTF8Encoding();
            Decoder Decode = encodepwd.GetDecoder();
            byte[] todecode_byte = Convert.FromBase64String(encryptpwd);
            int charCount = Decode.GetCharCount(todecode_byte, 0, todecode_byte.Length);
            char[] decoded_char = new char[charCount];
            Decode.GetChars(todecode_byte, 0, todecode_byte.Length, decoded_char, 0);
            decryptpwd = new String(decoded_char);
            return decryptpwd;
        }

        //public void setUniqueCodeAndNum(Users user)
        //{   string PRE;
        //    //select MAX(Num)from tblUser
        //    DataTable dt1 = new DataTable();
        //    dt1 = dblayer.SelectDatasTable("MAX(Num)", "tblLogin", 0);
        //    int inNo = 1001;
        //    string Pre = "TJC";
        //    if (dt1.Rows[0][0].ToString() == "")
        //    {
        //        user.NUM = inNo;
        //        PRE = Pre;
        //        user.CODE = Pre + inNo.ToString();
        //    }
        //    else
        //    {
        //        inNo = int.Parse(dt1.Rows[0][0].ToString());
        //        inNo++;
        //        user.NUM = inNo;
        //        PRE = Pre;
        //        user.CODE = Pre + inNo.ToString();
        //    }
        //}
        //public void setUniqueOrderCodeAndNum(Customize_order order)
        //{
        //    string PRE;
        //    //select MAX(Num)from tblUser
        //    DataTable dt1 = new DataTable();
        //    dt1 = dblayer.SelectDatasTable("MAX(Num)", "tblCustomizeOrder", 0);
        //    int inNo = 1001;

        //    string Pre = "TJO";
        //    if (dt1.Rows[0][0].ToString() == "")
        //    {
        //        order.NUM = inNo;
        //        PRE = Pre;
        //        order.CODE = Pre + inNo.ToString();
        //    }
        //    else
        //    {

        //        inNo = int.Parse(dt1.Rows[0][0].ToString());
        //        inNo++;
        //        order.NUM = inNo;
        //        PRE = Pre;
        //        order.CODE = Pre + inNo.ToString();
        //    }
        //}
        //public void setUniqueProCodeAndNum(Product product)
        //{
        //    string PRE;
        //    //select MAX(Num)from tblUser
        //    DataTable dt1 = new DataTable();
        //    dt1 = dblayer.SelectDatasTable("MAX(Num)", "tblProduct", 0);
        //    int inNo = 1001;

        //    DataTable dt = dblayer.SelectDatasTable("CatCode", "tblCategory", product.CategoryID);

        //    string Pre = dt.Rows[0]["CatCode"].ToString().Trim();
        //    if (dt1.Rows[0][0].ToString() == "")
        //    {
        //        product.NUM = inNo;
        //        PRE = Pre;
        //        product.CODE = Pre + inNo.ToString();
        //    }
        //    else
        //    {

        //        inNo = int.Parse(dt1.Rows[0][0].ToString());
        //        inNo++;
        //        product.NUM = inNo;
        //        PRE = Pre;
        //        product.CODE = Pre + inNo.ToString();
        //    }
        //}

    }
}