using QuanLyQuanAn.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyQuanAn.DAO
{
    internal class MenuDAO
    {
        private static MenuDAO instance;

        internal static MenuDAO Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new MenuDAO();
                }
                return instance;
            }
            private set
            {
                instance = value;
            }
        }

        public List<Menu> GetListMenuByTable(int id)
        {
            List<Menu> listMenu = new List<Menu>();

            string query = @"select foodName, count, price, price*count as total from Bill as b, BillInfo as bi, Food as f where bi.idBill = b.id and bi.idFood = f.id and b.payState = 0 and b.idTable = " + id;
           
            DataTable data = DataProvider.Instance.ExecuteQuery(query); 
            foreach (DataRow item in data.Rows)
            {
                Menu menu = new Menu(item);
                listMenu.Add(menu);
            }
            return listMenu;
        }
    }
}
