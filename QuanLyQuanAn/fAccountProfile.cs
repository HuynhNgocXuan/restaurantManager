using QuanLyQuanAn.DAO;
using QuanLyQuanAn.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyQuanAn
{
    internal partial class fAccountProfile : Form
    {
        private Account accountInfo;
        public fAccountProfile(Account accountInfo)
        {
            InitializeComponent();

            AccountInfo = accountInfo;
            LoadAccountInfo();
        }

        internal Account AccountInfo { get => accountInfo; set => accountInfo = value; }


        #region methods 
            
        void LoadAccountInfo()
        {
            txbNameLogin.Text = AccountInfo.UserName;
            txbDisplayName.Text = AccountInfo.DisplayName;
        }

        void UpdateAccountInfo()
        {
            string userName = txbNameLogin.Text;
            string displayName = txbDisplayName.Text;
            string password = txbPassword.Text;
            string newPassword = txbNewPassword.Text;
            string reEnterPassword = txbReEnPassword.Text;

            if (!reEnterPassword.Equals(newPassword))
            {
                MessageBox.Show("Sai mật khẩu nhập lại", "Thông báo");
            }
            else
            {
                if (AccountDAO.Instance.UpdateAccountInfo(userName, displayName, password, newPassword))
                {
                    MessageBox.Show("Cập nhật thành công!", "Thông báo");
                    if (updateAccount != null)
                    {
                        updateAccount(this, new AccountEvent(AccountDAO.Instance.GetAccountByUserName(userName)));
                    }
                }
                else
                {
                    MessageBox.Show("Sai mật khẩu. Vui lòng nhập lại");
                }
            }
        }

        #endregion

        #region events

        private event EventHandler<AccountEvent> updateAccount;
        public event EventHandler<AccountEvent> UpdateAccount
        {
            add { updateAccount += value; }
            remove { updateAccount -= value; }
        }


        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            UpdateAccountInfo();
        }
        #endregion
    }

    internal class AccountEvent : EventArgs
    {
        private Account acc;

        public Account Acc
        {
            get { return acc; }
            set { acc = value; }
        }

        public AccountEvent(Account acc)
        {
            this.Acc = acc;
        }
    }
}
