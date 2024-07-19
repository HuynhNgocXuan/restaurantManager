using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyQuanAn.DTO
{
    internal class Account
    {

        public Account(string userName, string displayName, int accountType, string password = null) 
        {
            this.UserName = userName;
            this.DisplayName = displayName;
            this.AccountType = accountType;
            this.Password = password;
        }

        public Account(DataRow row)
        {
            this.UserName = row["userName"].ToString();
            this.DisplayName = row["displayName"].ToString();
            this.AccountType = (int)row["accountType"];
            this.Password = row["userPassword"].ToString();
        } 

        private string userName;
        private string displayName;
        private int accountType;
        private string password;

        public string UserName { get => userName; set => userName = value; }
        public string DisplayName { get => displayName; set => displayName = value; }
        public int AccountType { get => accountType; set => accountType = value; }
        public string Password { get => password; set => password = value; }
    }
}
