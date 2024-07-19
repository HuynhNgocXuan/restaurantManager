using QuanLyQuanAn.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyQuanAn.DAO
{
    internal class AccountDAO
    {
        private AccountDAO() { }

        private static AccountDAO instance;

        internal static AccountDAO Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new AccountDAO();
                }
                return instance;
            }
            private set 
            { 
                instance = value; 
            }
        }

        public Account GetAccountByUserName(string userName)
        {
            string query = "select * from Account where userName = '" + userName + "'";
            DataTable data = DataProvider.Instance.ExecuteQuery(query);

            foreach (DataRow item in data.Rows)
            {
                return new Account(item);
            }
            return null;
        }

        public bool UpdateAccountInfo(string userName, string displayName, string password, string newPassword)
        {
            byte[] data = ASCIIEncoding.ASCII.GetBytes(password);
            byte[] hashData = new MD5CryptoServiceProvider().ComputeHash(data);
            string listPassword = "";

            foreach (byte item in hashData)
            {
                listPassword += item;
            }

            byte[] newData = ASCIIEncoding.ASCII.GetBytes(newPassword);
            byte[] hashNewData = new MD5CryptoServiceProvider().ComputeHash(newData);
            string listNewPassword = "";

            foreach (byte item in hashNewData)
            {
                listNewPassword += item;
            }

            string query = "execute USP_UpdateAccount @userName , @displayName , @password , @newPassword";
            int result = DataProvider.Instance.ExecuteNonQuery(query, new object[] { userName, displayName, listPassword, listNewPassword });
            
            return result > 0;
        }


        public bool Login(string userName, string passWord)
        {
            byte[] data = ASCIIEncoding.ASCII.GetBytes(passWord);
            byte[] hashData = new MD5CryptoServiceProvider().ComputeHash(data);

            string listPassword = "";
            foreach (byte item in hashData)
            {
                listPassword += item;
            }

            string query = @"execute USP_Login @userName , @userPassWord";

            DataTable result = DataProvider.Instance.ExecuteQuery(query, new object[] {userName, listPassword });

            return result.Rows.Count > 0;
        }

        public DataTable GetListAccount()
        {
            string query = "execute USP_GetListAccount";
            return DataProvider.Instance.ExecuteQuery(query);
        }

        public bool AddAccount(string userName, string displayName, int type) 
        {
            string query = string.Format("insert Account (userName, displayName, accountType) values (N'{0}', N'{1}', {2})", userName, displayName, type);

            int result = DataProvider.Instance.ExecuteNonQuery(query);
            return result == 1;
        }

        public bool UpdateAccount(string userName, string displayName, int type)
        {
            string query = string.Format("update Account set displayName = N'{1}', accountType = {2} where userName = N'{0}'", userName, displayName, type);

            int result = DataProvider.Instance.ExecuteNonQuery(query);
            return result == 1;
        }

        public bool DeleteAccount(string userName)
        {
            string query = string.Format("delete Account where userName = N'{0}'", userName);

            int result = DataProvider.Instance.ExecuteNonQuery(query);
            return result == 1;
        }

        public bool ResetPassword(string userName)
        {
            string query = string.Format("update Account set userPassword = '1962026656160185351301320480154111117132155' where userName = N'{0}'", userName);

            int result = DataProvider.Instance.ExecuteNonQuery(query);
            return result == 1;
        }

        public List<Account> GetListUserName()
        {
            List<Account> listUserName = new List<Account>();

            string query = "select * from Account";

            DataTable data = DataProvider.Instance.ExecuteQuery(query);
            foreach (DataRow item in data.Rows)
            {
                Account acc = new Account(item);
                listUserName.Add(acc);
            }

            return listUserName;
        }
    }
}
