using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.WebRequestMethods;

namespace LinqLabs.作業
{
    public partial class Frm作業_1 : Form
    {
        System.IO.DirectoryInfo dirs = new System.IO.DirectoryInfo(@"c:\windows");
        FileInfo[] files = null;
        public Frm作業_1()
        {
            InitializeComponent();
            this.ordersTableAdapter1.Fill(nwDataSet1.Orders);
            this.productsTableAdapter1.Fill(nwDataSet1.Products);
            this.order_DetailsTableAdapter1.Fill(nwDataSet1.Order_Details);
            var q = (from p in this.nwDataSet1.Orders select p.OrderDate.Year).Distinct();            
            this.comboBox1.DataSource=q.ToList();
         }

        private void button14_Click(object sender, EventArgs e)
        {
            this.lblMaster.Text = "LOG Files";            
            files = dirs.GetFiles();
            var q = from p in files
                    where p.Extension.Contains("log")
                    select p;
            this.dataGridView1.DataSource = q.ToList();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.lblMaster.Text = "LOG Files";            
            files = dirs.GetFiles();
            var q = from p in files
                    where p.CreationTime.Year == 2022
                    select p;
            this.dataGridView1.DataSource = q.ToList();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.lblMaster.Text = "LOG Files";           
            files = dirs.GetFiles();
            var q = from p in files
                    where p.Length >= 100000
                    select p;
            this.dataGridView1.DataSource = q.ToList();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            this.ordersTableAdapter1.Fill(nwDataSet1.Orders);
            this.dataGridView1.DataSource = this.nwDataSet1.Orders;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.lblMaster.Text = "LOG Files";
            int x= int.Parse(this.comboBox1.Text);
            var q = from p in this.nwDataSet1.Orders
                    where p.OrderDate.Year == x
                    select p;
            this.bindingSource1.DataSource = q.ToList();
            this.dataGridView1.DataSource =this.bindingSource1;
            this.bindingSource1.CurrentChanged += bindingSource1_CurrentChanged;
        }

        private void bindingSource1_CurrentChanged(object sender, EventArgs e)
        {
            int OrderID = ((NWDataSet.OrdersRow)this.bindingSource1.Current).OrderID;
            var q = from od in this.nwDataSet1.Order_Details
                    where od.OrderID == OrderID
                    select od;
            this.dataGridView2.DataSource = q.ToList();
        }
       
        int y = 0;
        private void button12_Click(object sender, EventArgs e)
        {
           int x = int.Parse(this.textBox1.Text);
            y = y-x;          
            var q = from p in this.nwDataSet1.Products                    
                         select p;
            this.dataGridView1.DataSource = q.Skip(y).Take(x).ToList();           
        }

        private void button13_Click(object sender, EventArgs e)
        {
            int x = int.Parse(this.textBox1.Text);
            y = y + x;
            var q = from p in this.nwDataSet1.Products
                    select p;
            this.dataGridView1.DataSource = q.Skip(y).Take(x).ToList();
        }

        //private void datagridview1_cellcontentclick(object sender, datagridviewcelleventargs e)
        //{
        //    datagridviewcell selecttedcell = datagridview1.selectedcells[0];
        //    int site = selecttedcell.rowindex;
        //    datatable datatable = nwdataset1.orders;
        //    int x = (int)datatable.rows[site][0];

        //    var ods = from o in nwdataset1.order_details
        //              join oo in nwdataset1.orders
        //              on o.orderid
        //              equals x
        //              select o;
        //    this.datagridview2.datasource = ods.tolist();
        //}       
    }
}
