using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyQuanAn.DTO
{
    internal class Category
    {
        public Category(DataRow row)
        {
            this.ID = (int)row["id"];
            this.CategoryName = row["foodCategoryName"].ToString();
        }

        public Category(int id, string categoryName)
        {
            this.ID = id;
            this.CategoryName = categoryName;
        }

        private int iD;
        private string categoryName;

        public int ID { get => iD; set => iD = value; }
        public string CategoryName { get => categoryName; set => categoryName = value; }
    }
}
