using QuanLyQuanAn.DAO;
using QuanLyQuanAn.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;


namespace QuanLyQuanAn
{
    internal partial class fAdmin : Form
    {
        BindingSource listFood = new BindingSource();
        BindingSource listTable = new BindingSource();
        BindingSource listAccount = new BindingSource();
        BindingSource listCategory = new BindingSource();

        public Account accountLogin;

        public fAdmin()
        {
            InitializeComponent();
            Load();
        }


        #region methods
        new void Load()
        {
            dtgvFood.DataSource = listFood;
            dtgvTables.DataSource = listTable;
            dtgvAccounts.DataSource = listAccount;
            dtgvCategoryFood.DataSource = listCategory;

            LoadDateTimePicker();
            LoadListBillByDate(dtpkFromDate.Value, dtpkToDate.Value, 1);
            LoadListFood();
            LoadCategoryInfo(cbCategoryFood);
            AddFoodBinding();
            LoadListAccount();
            LoadListTable();
            AddAccountBinding();
            AddTableBinding();
            LoadCategory();
            AddCategoryBinding();
        }

        void LoadListTable()
        {
            listTable.DataSource = TablesDAO.Instance.LoadListTableForAdmin();
        }

        void LoadListAccount()
        {
            listAccount.DataSource = AccountDAO.Instance.GetListAccount();
        }

        void AddCategoryBinding()
        {
            tbIDCategory.DataBindings.Add(new Binding("Text", dtgvCategoryFood.DataSource, "id", true, DataSourceUpdateMode.Never));
            tbCategoryName.DataBindings.Add(new Binding("Text", dtgvCategoryFood.DataSource, "CategoryName", true, DataSourceUpdateMode.Never));
        }

        void AddTableBinding()
        {
            txbIDTable.DataBindings.Add(new Binding("Text", dtgvTables.DataSource, "STT", true, DataSourceUpdateMode.Never));
            txbTableName.DataBindings.Add(new Binding("Text", dtgvTables.DataSource, "Tên bàn", true, DataSourceUpdateMode.Never));
        }

        void AddFoodBinding()
        {
            txbIDFood.DataBindings.Add(new Binding("Text", dtgvFood.DataSource, "id", true, DataSourceUpdateMode.Never));
            txbFoodName.DataBindings.Add(new Binding("Text", dtgvFood.DataSource, "foodName", true, DataSourceUpdateMode.Never));
            numericPrice.DataBindings.Add(new Binding("Value", dtgvFood.DataSource, "price", true, DataSourceUpdateMode.Never));
        }

        void AddAccountBinding()
        {
            txbAccountName.DataBindings.Add(new Binding("Text", dtgvAccounts.DataSource, "Tên tài khoản", true,DataSourceUpdateMode.Never));
            txbDisplayName.DataBindings.Add(new Binding("Text", dtgvAccounts.DataSource, "Tên hiển thị", true, DataSourceUpdateMode.Never));
            numericAccountType.DataBindings.Add(new Binding("Value", dtgvAccounts.DataSource, "Loại tài khoản", true, DataSourceUpdateMode.Never));
        }

        void LoadCategoryInfo(ComboBox cb)
        {
            cb.DataSource = CategoryDAO.Instance.GetListCategory();
            cb.DisplayMember = "categoryName";
        } 

        void LoadListBillByDate(DateTime dateIn, DateTime dateOut, int pageNum)
        {
            DataTable data = BillDAO.Instance.GetBillByDateForPage(dateIn, dateOut, pageNum);
            dtgvBill.DataSource = data;
        }

        void LoadDateTimePicker()
        {
            DateTime today = DateTime.Now;
            dtpkFromDate.Value = new DateTime(today.Year, today.Month, 1);
            dtpkToDate.Value = dtpkFromDate.Value.AddMonths(1).AddDays(-1);
        }

        void LoadListFood()
        {
            listFood.DataSource = FoodDAO.Instance.GetListFood();
        }

        DataTable SearchFoodByName(string name)
        {
            return FoodDAO.Instance.SearchFoodByName(name);
        }

        void AddAccount(string userName, string displayName, int type)
        {
            List<Account> data = AccountDAO.Instance.GetListUserName();

            foreach (Account item in data)
            {
                if (item.UserName.Equals(userName))
                {
                    MessageBox.Show("Tên tài khoản đã tồn tại!", "Thông báo");
                    return;
                }
            }
          
            if (AccountDAO.Instance.AddAccount(userName, displayName, type))
            {
                MessageBox.Show("Thêm tài khoản thành công!", "Thông báo");
            }
            else
            {
                MessageBox.Show("Thêm tài khoản thất bại", "Thông báo");
            }
            LoadListAccount();
        }

        void UpdateAccount(string userName, string displayName, int type)
        {
            if (AccountDAO.Instance.UpdateAccount(userName, displayName, type))
            {
                MessageBox.Show("Cập nhật tài khoản thành công!", "Thông báo");
            }
            else
            {
                MessageBox.Show("Cập nhật tài khoản thất bại", "Thông báo");
            }
            LoadListAccount();
        }

        void DeleteAccount(string userName)
        {
            if (accountLogin.UserName.Equals(userName))
            {
                MessageBox.Show("Tài khoản đang đăng nhập!", "Thông báo");
            }
            else if (AccountDAO.Instance.DeleteAccount(userName))
            {
                MessageBox.Show("Xóa tài khoản thành công!", "Thông báo");
            }
            else
            {
                MessageBox.Show("Xóa tài khoản thất bại", "Thông báo");
            }
            LoadListAccount();
        }

        void ResetPassword(string userName)
        {
            if (AccountDAO.Instance.ResetPassword(userName))
            {
                MessageBox.Show("Đặt lại mật khẩu thành công!", "Thông báo");
            }
            else
            {
                MessageBox.Show("Đặt lại mật khẩu thất bại", "Thông báo");
            }
        }

        void AddTable(string name)
        {
            List<Table> data = TablesDAO.Instance.LoadTablesList();

            foreach (Table item in data)
            {
                if (item.Name.Equals(name))
                {
                    MessageBox.Show("Tên Bàn đã tồn tại!", "Thông báo");
                    return;
                }
            }

            if (TablesDAO.Instance.AddTable(name))
            {
                MessageBox.Show("Thêm bàn thành công!", "Thông báo");
                LoadListTable();
            }
            else
            {
                MessageBox.Show("Thêm bàn thất bại", "Thông báo");
            }
        }
        void DeleteTable(int id, string name)
        {
            if (MessageBox.Show("Bạn chắc chắn muốn xóa " + name , "Thông báo", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                if (TablesDAO.Instance.DeleteTable(id))
                {
                    MessageBox.Show("Xóa bàn thành công!", "Thông báo");
                    LoadListTable();
                }
                else
                {
                    MessageBox.Show("Xóa bàn thất bại", "Thông báo");
                }
            }

        }

        void EditTable(int id, string name)
        {
            if (TablesDAO.Instance.EditTable(id, name))
            {
                MessageBox.Show("Sửa tên bàn thành công!", "Thông báo");
                LoadListTable();
            }
            else
            {
                MessageBox.Show("Sửa tên bàn thất bại", "Thông báo");
            }
        }
        
        void LoadCategory()
        {
            listCategory.DataSource = CategoryDAO.Instance.GetListCategory(); ;
        }


        void AddCategory(string name)
        {
            if (CategoryDAO.Instance.AddCategory(name))
            {
                MessageBox.Show("Thêm loại thức ăn thành công!", "Thông báo");
                LoadCategory();
            }
            else
            {
                MessageBox.Show("Thêm loại thức ăn thất bại", "Thông báo");
            }
        }

        void DeleteCategory(int id)
        {
            if (CategoryDAO.Instance.DeleteCategory(id))
            {
                MessageBox.Show("Xóa loại thức ăn thành công!", "Thông báo");
                LoadCategory();
            }
            else
            {
                MessageBox.Show("Xóa loại thức ăn thất bại", "Thông báo");
            }
        }

        void EditCategory(int id, string name)
        {
            if (CategoryDAO.Instance.EditCategory(id, name))
            {
                MessageBox.Show("Sửa loại thức ăn thành công!", "Thông báo");
                LoadCategory();
            }
            else
            {
                MessageBox.Show("Sửa loại thức ăn thất bại", "Thông báo");
            }
        }


        #endregion

        #region events
        private void btnAddCategory_Click(object sender, EventArgs e)
        {
            string name = tbCategoryName.Text;
            AddCategory(name);
        }

        private void btnDeleteCategory_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(tbIDCategory.Text);
            DeleteCategory(id);
        }

        private void btnEditCategory_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(tbIDCategory.Text);
            string name = tbCategoryName.Text;
            EditCategory(id, name);
        }

        private void btnShowCategory_Click(object sender, EventArgs e)
        {
            LoadCategory();
        }
        // TABLE 
        private void btnAddTable_Click(object sender, EventArgs e)
        {
            string name = txbTableName.Text;

            AddTable(name);
        }

        private void btnDeleteTable_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(txbIDTable.Text);
            string name = txbTableName.Text.ToString();
            DeleteTable(id, name);
        }

        private void btnEditTable_Click(object sender, EventArgs e)
        {
            string name = txbTableName.Text;
            int id = Convert.ToInt32(txbIDTable.Text);
            EditTable(id, name);
        }

        // TABLE
        // ACCOUNT 
        private void btnShowAccount_Click(object sender, EventArgs e)
        {
            LoadListAccount();
        }

        private void btnAddAccount_Click(object sender, EventArgs e)
        {
            string userName = txbAccountName.Text;
            string displayName = txbDisplayName.Text;
            int type = (int)numericAccountType.Value;

            AddAccount(userName, displayName, type);
        }
        private void btnEditAccount_Click(object sender, EventArgs e)
        {
            string userName = txbAccountName.Text;
            string displayName = txbDisplayName.Text;
            int type = (int)numericAccountType.Value;

            UpdateAccount(userName, displayName, type);
        }

        private void btnDeleteAccount_Click(object sender, EventArgs e)
        {
            string userName = txbAccountName.Text;

            DeleteAccount(userName);
        }


        private void btnResetPassword_Click(object sender, EventArgs e)
        {
            string userName = txbAccountName.Text;

            ResetPassword(userName);
        }
        // ACCOUNT 

        private void btnViewBill_Click(object sender, EventArgs e)
        {
            txbPageNum.Text = "1";
            LoadListBillByDate(dtpkFromDate.Value, dtpkToDate.Value, 1);
        }

        private void btnShow_Click(object sender, EventArgs e)
        {
            LoadListFood();
        }

        private void txbIDFood_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (dtgvFood.SelectedCells.Count > 0)
                {
                    int id = (int)dtgvFood.SelectedCells[0].OwningRow.Cells["IDCategory"].Value;
                    Category category = CategoryDAO.Instance.GetCategoryById(id);

                    int index = -1;
                    int i = 0;

                    foreach (Category item in cbCategoryFood.Items)
                    {
                        if (item.ID == category.ID)
                        {
                            index = i;
                            break;
                        }
                        i++;
                    }

                    cbCategoryFood.SelectedIndex = index;
                }
            }
            catch 
            {
            }
        }

        private void btnAddFood_Click(object sender, EventArgs e)
        {
            string name = txbFoodName.Text.ToString();
            float price = (float)numericPrice.Value;
            int idCategory = (cbCategoryFood.SelectedItem as Category).ID;

            List<Food> data = FoodDAO.Instance.GetListFoodForAdmin();

            foreach (Food item in data)
            {
                if (item.FoodName.Equals(name))
                {
                    MessageBox.Show("Tên Món đã tồn tại!", "Thông báo");
                    return;
                }
            }

            if (FoodDAO.Instance.InsertFood(name, price, idCategory)) {
                MessageBox.Show("Thêm món thành công!", "Thông báo");
                LoadListFood();

                if (insertFood != null)
                    insertFood(this, new EventArgs());
            }
            else
            {
                MessageBox.Show("Thêm món thất bại", "Thông báo");
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(txbIDFood.Text);
            string name = txbFoodName.Text.ToString();
            float price = (float)numericPrice.Value;
            int idCategory = (cbCategoryFood.SelectedItem as Category).ID;

            if (FoodDAO.Instance.UpdateFood(id, name, price, idCategory))
            {
                MessageBox.Show("Sửa món thành công!", "Thông báo");
                LoadListFood();

                if (editFood != null)
                    editFood(this, new EventArgs());
            }
            else
            {
                MessageBox.Show("Sửa món thất bại", "Thông báo");
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(txbIDFood.Text);

            if (FoodDAO.Instance.DeleteFood(id)) 
            {
                MessageBox.Show("Xóa món thành công!", "Thông báo");
                LoadListFood();

                if (deleteFood != null)
                {
                    deleteFood(this, new EventArgs());
                }
            }
            else
            {
                MessageBox.Show("Xóa món thất bại", "Thông báo");
            }
        }

        private event EventHandler insertFood;
        public event EventHandler InsertFood
        {
            add { insertFood += value; }
            remove { insertFood -= value; }
        }


        private event EventHandler deleteFood;
        public event EventHandler DeleteFood
        {
            add { deleteFood += value; }
            remove { deleteFood -= value; }
        }

        private event EventHandler editFood;
        public event EventHandler EditFood
        {
            add { editFood += value; }
            remove { editFood -= value; }
        }

        private void btnSearchFood_Click(object sender, EventArgs e)
        {
            string name = txbSearchFood.Text;
            listFood.DataSource = SearchFoodByName(name);
        }


        private void btnShowTable_Click(object sender, EventArgs e)
        {
            LoadListTable();
        }

        private void btnFirstPage_Click(object sender, EventArgs e)
        {
            txbPageNum.Text = "1";
        }
        private void btnLastPage_Click(object sender, EventArgs e)
        {
            int sumRecord = BillDAO.Instance.GetBillCountByDate(dtpkFromDate.Value, dtpkToDate.Value);
            if (sumRecord > 20)
            {
                int lastPage = sumRecord / 20;
                if (sumRecord / 20 != 0) lastPage++;
                txbPageNum.Text = lastPage.ToString();
            }

        }

        private void btnPreviousPage_Click(object sender, EventArgs e)
        {
            int pageNum = Convert.ToInt32(txbPageNum.Text);
            if (pageNum > 1) pageNum--;

            txbPageNum.Text = pageNum.ToString();
        }

        private void btnBehindPage_Click(object sender, EventArgs e)
        {
            int pageNum = Convert.ToInt32((txbPageNum.Text));

            int sumRecord = BillDAO.Instance.GetBillCountByDate(dtpkFromDate.Value, dtpkToDate.Value);
            int lastPage = sumRecord / 20;
            if (sumRecord / 20 != 0) lastPage++;

            if (pageNum < lastPage) pageNum++;

            txbPageNum.Text = pageNum.ToString();
        }

        private void txbPageNum_TextChanged(object sender, EventArgs e)
        {
            dtgvBill.DataSource = BillDAO.Instance.GetBillByDateForPage(dtpkFromDate.Value, dtpkToDate.Value, Convert.ToInt32(txbPageNum.Text));
        }

        #endregion
    }
}
