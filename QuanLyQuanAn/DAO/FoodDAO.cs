using QuanLyQuanAn.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyQuanAn.DAO
{
    internal class FoodDAO
    {
        private static FoodDAO instance;
        public FoodDAO() { }

        internal static FoodDAO Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new FoodDAO();
                }
                return instance;
            }
            private set
            {
                instance = value;
            }
        }

        public List<Food> GetListFoodByCategoryId(int id)
        {
            List<Food> listFood = new List<Food>();

            string query = "select * from Food where idCategory = " + id;

            DataTable data = DataProvider.Instance.ExecuteQuery(query);

            foreach (DataRow item in data.Rows)
            {
                Food food = new Food(item);
                listFood.Add(food);
            } 

            return listFood;
        }

        public DataTable GetListFood()
        {
            string query = "execute USP_GetListFood";
            return DataProvider.Instance.ExecuteQuery(query);
        }

        public List<Food> GetListFoodForAdmin()
        {
            List<Food> data = new List<Food>();

            string query = "select * from Food";

            DataTable table = DataProvider.Instance.ExecuteQuery(query);

            foreach (DataRow item in table.Rows)
            {
                Food food = new Food(item);
                data.Add(food);
            }
            return data;
        }

        public bool InsertFood(string name, float price, int idCategory)
        {
            string query = string.Format("insert Food (foodName, price, idCategory) values (N'{0}', {1}, {2})", name, price, idCategory);

            int result = DataProvider.Instance.ExecuteNonQuery(query);

            return result > 0;
        }

        public bool UpdateFood(int id, string name, float price, int idCategory)
        {
            string query = string.Format("update Food set foodName = N'{0}', price = {1}, idCategory = {2} where id = {3}", name, price, idCategory, id);

            int result = DataProvider.Instance.ExecuteNonQuery(query);

            return result > 0;
        }

        public bool DeleteFood(int id)
        {
            BillInfoDAO.Instance.DeleteBillInfoByIdFood(id);

            string query = "delete Food where id = " + id;

            int result = DataProvider.Instance.ExecuteNonQuery(query);

            return result > 0;
        }

        public DataTable SearchFoodByName(string name)
        {
            string query = string.Format("select * from Food where dbo.fuConvertToUnsign1(foodName) like N'%' + dbo.fuConvertToUnsign1(N'{0}') + '%'", name);
            return DataProvider.Instance.ExecuteQuery(query);
        }
    }
}
