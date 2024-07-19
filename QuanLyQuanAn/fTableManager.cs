using QuanLyQuanAn.DAO;
using QuanLyQuanAn.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlTypes;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyQuanAn
{
    internal partial class fTableManager : Form
    {
        private Account accountLogin;

        public Account AccountLogin
        {
            get { return accountLogin; }
            set
            {
                accountLogin = value;
                IdentifyAccount(accountLogin.AccountType);
            }
        }

        public fTableManager(Account account)
        {
            InitializeComponent();

            this.AccountLogin = account;
          
            LoadTables();
            LoadCategory();
        }

        #region Methods

        void IdentifyAccount(int type)
        {
            adminToolStripMenuItem.Enabled = type == 1;
            thôngTinTàiKhoảnToolStripMenuItem.Text += " ( " + accountLogin.DisplayName + " )";
        }

        void LoadCategory()
        {
            List<Category> listCategory = CategoryDAO.Instance.GetListCategory();
            cbCategoryFood.DataSource = listCategory;
            cbCategoryFood.DisplayMember = "CategoryName";
        }

        void LoadFoodListByCategoryId(int id)
        {
            List<Food> listFood = FoodDAO.Instance.GetListFoodByCategoryId(id);
            cbFood.DataSource = listFood;
            cbFood.DisplayMember= "foodName";
        }

        void LoadTables()
        {
            flpTables.Controls.Clear();
            List<Table> tableList = TablesDAO.Instance.LoadTablesList();
            cbSwitchTable.DataSource = tableList;
            cbSwitchTable.DisplayMember = "Name";

            foreach (Table item in tableList)
            {
                Button btn = new Button() { Width = TablesDAO.TableWidth, Height = TablesDAO.TableHeight };
                btn.Text = item.Name + Environment.NewLine + item.Status;
                btn.Font = new Font("Arial", 12);
                btn.Click += Btn_Click;
                btn.Tag = item;


                switch (item.Status)
                {
                    case "Đã đặt":
                        btn.BackColor = Color.Orange;
                        btn.ForeColor = Color.Red;
                        break;
                    case "Có người":
                        btn.BackColor = Color.White;
                        break;
                    default:
                        btn.BackColor = Color.LightGray;
                        btn.ForeColor = Color.DarkGray;
                        break;
                }

                flpTables.Controls.Add(btn);
            }
        }

        void ShowBill(int idTable)
        {
            lvBill.Items.Clear();

            List<DTO.Menu> listMenu = MenuDAO.Instance.GetListMenuByTable(idTable);

            float totalPrice = 0;

            foreach (DTO.Menu item in listMenu)
            {
                ListViewItem lvItem = new ListViewItem();
                lvItem.Text = item.FoodName.ToString();

                totalPrice += item.Total;

                lvItem.SubItems.Add(item.Count.ToString());
                lvItem.SubItems.Add(item.Price.ToString());
                lvItem.SubItems.Add(item.Total.ToString());

                lvBill.Items.Add(lvItem);
            }
            CultureInfo culture = new CultureInfo("vi-VN");

            //txbTotalPrice.Text = totalPrice.ToString("c", culture) ; // set format của một phần control của textbox thôi 
            //Thread.CurrentThread.CurrentCulture = culture; Set toàn bộ chương trình thành tiếng việt

            txbTotalPrice.Text = totalPrice.ToString() + ",00 đ"; 
        }
        #endregion


        #region Events
        private void cbCategoryFood_SelectedIndexChanged(object sender, EventArgs e)
        {
            int id = 0;
            ComboBox cb = sender as ComboBox;

            if (cb.SelectedItem == null)
                return;

            Category selected = cb.SelectedItem as Category;
            id = selected.ID;

            LoadFoodListByCategoryId(id);
        }

        private void Btn_Click(object sender, EventArgs e)
        {
            int tableID = ((sender as Button).Tag as Table).ID;
            lvBill.Tag = (sender as Button).Tag;
            ShowBill(tableID);
        }

        private void đăngSuấtToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void thôngTinCáNhânToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fAccountProfile accountProfile = new fAccountProfile(AccountLogin);

            accountProfile.UpdateAccount += AccountProfile_UpdateAccount;
            accountProfile.ShowDialog();

        }

        private void AccountProfile_UpdateAccount(object sender, AccountEvent e)
        {
            thôngTinTàiKhoảnToolStripMenuItem.Text = "Thông tin tài khoản ( " + e.Acc.DisplayName + " )";
        }

        private void adminToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fAdmin ad = new fAdmin();
            ad.accountLogin = AccountLogin;

            ad.InsertFood += Ad_InsertFood;
            ad.DeleteFood += Ad_DeleteFood;
            ad.EditFood += Ad_EditFood;
            ad.ShowDialog();
        }

        private void Ad_EditFood(object sender, EventArgs e)
        {
            LoadFoodListByCategoryId((cbCategoryFood.SelectedItem as Category).ID);
            if (lvBill.Tag != null)
                ShowBill((lvBill.Tag as Table).ID);
        }

        private void Ad_DeleteFood(object sender, EventArgs e)
        {
            LoadFoodListByCategoryId((cbCategoryFood.SelectedItem as Category).ID);
            if (lvBill.Tag != null)
                ShowBill((lvBill.Tag as Table).ID);
            LoadTables();
        }

        private void Ad_InsertFood(object sender, EventArgs e)
        {
            LoadFoodListByCategoryId((cbCategoryFood.SelectedItem as Category).ID);
            if (lvBill.Tag != null)
                ShowBill((lvBill.Tag as Table).ID);
        }

        private void btnAddFood_Click(object sender, EventArgs e)
        {
            Table table = lvBill.Tag as Table;
            int count = (int)numericAddFood.Value;
            if (table == null)
            {
                MessageBox.Show("Chưa chọn bàn", "Thông báo");
                return;
            }

            int idBill = BillDAO.Instance.GetUncheckIDBillByTableID(table.ID);
            int idFood = (cbFood.SelectedItem as Food).ID;

            // Tạo Bill mới và thêm Bill mới được tạo 
            if (idBill == -1 && count > 0)
            {
                BillDAO.Instance.InsertBill(table.ID);
                BillInfoDAO.Instance.InsertBillInfo(BillDAO.Instance.GetMaxIdBill(), idFood, count);
                LoadTables();
            }

            // Thêm vào Bill đã tồn tại 
            else
            {
                BillInfoDAO.Instance.InsertBillInfo(idBill, idFood, count);
            }

            ShowBill(table.ID);
            
        }

        private void btnPay_Click(object sender, EventArgs e)
        {
            Table table = lvBill.Tag as Table;
                
            if (table == null)
            {
                MessageBox.Show("Chưa chọn bàn", "Thông báo");
                return;
            }

            int idBill = BillDAO.Instance.GetUncheckIDBillByTableID(table.ID);
            int discount = (int)numericDiscount.Value;

            // Lưu ý còn lỗi chưa sửa phần split cắt dấu phẩy thiếu 000 và nếu số tiền lớn hơn= 1 triệu lỗi 
            string price = txbTotalPrice.Text;
            double totalPrice = Convert.ToDouble(price.Split(',')[0]);
            double finalTotalPrice = totalPrice - (totalPrice / 100) * discount;
            CultureInfo culture = new CultureInfo("vi-VN");


            if (idBill != -1)
            {
                if (MessageBox.Show(string.Format("Bạn có muốn thanh toán cho {0} không?\n\nGiảm giá {1}% thành tiền: {2}", table.Name, discount, finalTotalPrice.ToString("c", culture)), "Thông báo", MessageBoxButtons.OKCancel) == System.Windows.Forms.DialogResult.OK)
                {
                    BillDAO.Instance.CheckOut(idBill, discount, (float)finalTotalPrice);
                    ShowBill(table.ID);
                    LoadTables();
                }
            }
            numericDiscount.Value = 0;
        }

        private void btnSwitchTable_Click(object sender, EventArgs e)
        {
            int idOld = (lvBill.Tag as Table).ID;
            int idNew = (cbSwitchTable.SelectedItem as Table).ID;
            string nameOld = (lvBill.Tag as Table).Name;
            string nameNew = (cbSwitchTable.SelectedItem as Table).Name;

            if (MessageBox.Show(string.Format("Bạn có muốn chuyển {0} sang {1} không?", nameOld, nameNew), "Thông báo", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                TablesDAO.Instance.SwitchTable(idOld, idNew);   
                LoadTables();
            }
        }




        #endregion

        private void thêmMónToolStripMenuItem_Click(object sender, EventArgs e)
        {
            btnAddFood_Click(this, new EventArgs());
        }

        private void thanhToánToolStripMenuItem_Click(object sender, EventArgs e)
        {
            btnPay_Click(this, new EventArgs());
        }

        private void xuấtHóaĐơnToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fInvoice fp = new fInvoice();
            fp.ShowDialog();
        }
    }

}
