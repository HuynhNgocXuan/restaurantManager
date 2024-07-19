using QuanLyQuanAn.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyQuanAn.DAO
{
    internal class BillInfoDAO
    {
        private static BillInfoDAO instance;

        public BillInfoDAO() { }

        internal static BillInfoDAO Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new BillInfoDAO();
                }
                return instance;
            }
            private set { instance = value; }
        }

        public void DeleteBillInfoByIdFood(int idFood)
        {
            string query = "delete BillInfo where idFood = " + idFood;
            DataProvider.Instance.ExecuteQuery(query);
        }

        public List<BillInfo> GetListBillInfo(int id)
        {
            List<BillInfo> listBillInfo = new List<BillInfo>();

            string query = "select * from BillInfo where idBill = " + id;
            DataTable data = DataProvider.Instance.ExecuteQuery(query);

            foreach (DataRow item in data.Rows)
            {
                BillInfo itemBillInfo = new BillInfo(item);
                listBillInfo.Add(itemBillInfo);
            }

            return listBillInfo;
        }
        public void InsertBillInfo(int idBill, int idFood, int count)
        {
            string query = "USP_InsertBillInfo @idBill , @idFood , @count";
            DataProvider.Instance.ExecuteNonQuery(query, new object[] { idBill, idFood, count});
        }
    }
}
