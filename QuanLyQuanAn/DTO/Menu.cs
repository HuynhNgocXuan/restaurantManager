using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyQuanAn.DTO
{
    internal class Menu
    {
        public Menu(string foodName, int count, float price, float total)
        {
            this.FoodName = foodName;
            this.Count = count;
            this.Price = price;
            this.Total = total;
        }

        public Menu(DataRow row)
        {
            this.FoodName = row["foodName"].ToString();
            this.Count = (int)row["count"];
            this.Price = (float)Convert.ToDouble(row["price"].ToString());
            this.Total = (float)Convert.ToDouble(row["total"].ToString());
        }

        private string foodName;
        private int count;
        private float price;
        private float total;

        public string FoodName { get => foodName; set => foodName = value; }
        public int Count { get => count; set => count = value; }
        public float Price { get => price; set => price = value; }
        public float Total { get => total; set => total = value; }
    }
}
