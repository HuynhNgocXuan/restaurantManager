using QuanLyQuanAn.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyQuanAn.DAO
{
    public class TablesDAO
    {
        private static TablesDAO instance;

        public static int TableWidth = 125;
        public static int TableHeight = 100;

        public static TablesDAO Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new TablesDAO();
                }
                return instance;
            }
            private set { instance = value; }
        }
        public TablesDAO() { }

        public List<Table> LoadTablesList()
        {
            List<Table> tablesList = new List<Table>();

            string query = "execute USP_GetTablesList";

            DataTable data = DataProvider.Instance.ExecuteQuery(query);

            foreach (DataRow item in data.Rows)
            {
                Table table = new Table(item);
                tablesList.Add(table);
            }

            return tablesList;
        }

        public DataTable LoadListTableForAdmin()
        {
            string query = "execute USP_GetListTableForAdmin";
            return DataProvider.Instance.ExecuteQuery(query);
        }
        
        public void SwitchTable(int idOldTable,  int idNewTable)
        {
            string query = "execute USP_SwitchTable @idOldTable , @idNewTable";
            DataProvider.Instance.ExecuteQuery(query, new object[] {idOldTable, idNewTable});
        }

        public bool AddTable(string name)
        {
            string query = string.Format("insert EatTable (tableName) values (N'{0}')", name);
            int result = DataProvider.Instance.ExecuteNonQuery(query);

            return result == 1;
        }
        public bool DeleteTable(int id)
        {
            string query = string.Format("delete EatTable where id = {0}", id);
            int result = DataProvider.Instance.ExecuteNonQuery(query);

            return result == 1;
        }
        public bool EditTable(int id, string name)
        {
            string query = string.Format("update EatTable set tableName = N'{0}' where id = {1} ", name, id);
            int result = DataProvider.Instance.ExecuteNonQuery(query);

            return result == 1;
        }
    }
}
