using QuanLyQuanAn.DAO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyQuanAn.DTO
{
    internal class Food
    {
        public Food(DataRow row)
        {
            this.ID = (int)row["id"];
            this.FoodName = row["foodName"].ToString();
            this.Price = (float)Convert.ToDouble(row["price"].ToString());
            this.IDCategory = (int)row["idCategory"];
        }

        public Food(int id, string foodName, float price, int iDCategory)
        {
            this.ID = id;
            this.FoodName = foodName;
            this.Price = price;
            this.IDCategory = iDCategory;
        }

        private int iD;
        private string foodName;
        private float price;
        private int iDCategory;

        public int ID { get => iD; set => iD = value; }
        public string FoodName { get => foodName; set => foodName = value; }
        public float Price { get => price; set => price = value; }
        public int IDCategory { get => iDCategory; set => iDCategory = value; }

       
    }
}
