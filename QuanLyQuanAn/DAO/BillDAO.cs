using QuanLyQuanAn.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyQuanAn.DAO
{
    internal class BillDAO
    {
        private static BillDAO instance;
        public BillDAO() { }
        public static BillDAO Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new BillDAO();
                }
                return instance;
            }
            private set { instance = value;}
        } 
        /// <summary>
        /// Thành công: id
        /// Thất bại: -1
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public int GetUncheckIDBillByTableID(int id)
        {
            string query = "select * from Bill where idTable = " + id + " and payState = 0";
            DataTable data = DataProvider.Instance.ExecuteQuery(query);
            if (data.Rows.Count > 0)
            {
                Bill bill = new Bill(data.Rows[0]);
                return bill.ID;
            }
            return -1;
        }
        public void InsertBill(int id)
        {
            string query = "execute USP_InsertBill @idTable";
            DataProvider.Instance.ExecuteNonQuery(query, new object[] {id});
        }
        
        public int GetMaxIdBill()
        {
            string query = "select MAX(id) from Bill";
            try
            {
                return (int)DataProvider.Instance.ExecuteScalar(query);
            }
            catch
            {
                return 1;
            }
        }

        public DataTable GetBillByDateForPage(DateTime checkIn, DateTime checkOut, int pageCount)
        {
            string query = "execute USP_GetListBillByDateForPage @checkIn , @checkOut , @pageCount";
            return DataProvider.Instance.ExecuteQuery(query, new object[] { checkIn, checkOut, pageCount });
        }

        public int GetBillCountByDate(DateTime checkIn, DateTime checkOut)
        {
            string query = "execute USP_GetBillCountByDate @checkIn , @checkOut";
            return (int)DataProvider.Instance.ExecuteScalar(query, new object[] { checkIn, checkOut });
        }

        public void CheckOut(int idBill, int discount, float totalPrice)
        {
            string query = "update Bill set payState = 1, dateCheckOut = GETDATE(), discount = " + discount + ", totalPrice = " + totalPrice +" where id = " + idBill;
            DataProvider.Instance.ExecuteNonQuery(query);
        }

        public DataTable GetListBillByDate(DateTime checkIn, DateTime checkOut)
        {
            string query = "execute USP_GetListBillByDate @checkIn , @checkOut";
            return DataProvider.Instance.ExecuteQuery(query, new object[] {checkIn, checkOut});
        }

        public DataTable GetListBillForReport(DateTime checkIn, DateTime checkOut)
        {
            string query = "execute USP_GetListBillByDateForReport @checkIn , @checkOut";
            return DataProvider.Instance.ExecuteQuery(query, new object[] { checkIn, checkOut });
        }
    }
}
