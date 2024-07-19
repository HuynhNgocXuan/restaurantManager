using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyQuanAn.DTO
{
    public class Table
    {
        public Table(DataRow row)
        {
            this.ID = (int)row["id"];
            this.Name = row["tableName"].ToString();
            this.Status = row["tableStatus"].ToString();
        }

        public Table(int id, string name, string status)
        {
            this.ID = id;
            this.Name = name;
            this.Status = status;
        }

        private int iD;
        private string name;
        private string status;

        public string Status { get => status; set => status = value; }
        public string Name { get => name; set => name = value; }
        public int ID { get => iD; set => iD = value; }
    }
}
