using QuanLyQuanAn.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyQuanAn.DAO
{
    internal class CategoryDAO
    {
        private static CategoryDAO instance;

        internal static CategoryDAO Instance
        {
           get
            {
                if (instance == null)
                {
                    instance = new CategoryDAO();
                }
                return instance;
            }
            private set
            {
                instance = value;
            }
        }

        public CategoryDAO() { }

        public Category GetCategoryById(int id)
        {
            Category category;
            string query = "select * from FoodCategory where id = " + id;

            DataTable data = DataProvider.Instance.ExecuteQuery(query);

            category = new Category(data.Rows[0]);

            return category;
        }

        public List<Category> GetListCategory()
        {
            List<Category> listCategory = new List<Category>();

            string query = "select * from FoodCategory";
            DataTable data = DataProvider.Instance.ExecuteQuery(query);

            foreach (DataRow item in data.Rows)
            {
                Category category = new Category(item);
                listCategory.Add(category); 
            }

            return listCategory;
        }

        public bool AddCategory(string name)
        {
            string query = string.Format("insert FoodCategory (foodCategoryName) values (N'{0}')", name);
            int result = DataProvider.Instance.ExecuteNonQuery(query);

            return result == 1;
        }
        public bool DeleteCategory(int id)
        {
            string query = string.Format("delete FoodCategory where id = {0}", id);
            int result = DataProvider.Instance.ExecuteNonQuery(query);

            return result == 1;
        }
        public bool EditCategory(int id, string name)
        {
            string query = string.Format("update FoodCategory set foodCategoryName = N'{0}' where id = {1} ", name, id);
            int result = DataProvider.Instance.ExecuteNonQuery(query);

            return result == 1;
        }
    }
}
