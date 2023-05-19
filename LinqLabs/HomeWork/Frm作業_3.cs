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

namespace LinqLabs.作業
{
    public partial class Frm作業_3 : Form
    {
        System.IO.DirectoryInfo dirs = new System.IO.DirectoryInfo(@"c:\windows");
        FileInfo[] files = null;         
        public Frm作業_3()
        {
            InitializeComponent();
            this.productsTableAdapter1.Fill(nwDataSet1.Products);
            this.ordersTableAdapter1.Fill(nwDataSet1.Orders);
            this.order_DetailsTableAdapter1.Fill(nwDataSet1.Order_Details);    
        }

        private void button38_Click(object sender, EventArgs e)
        {            
            files = dirs.GetFiles();
            var q = from p in files
                    group p by MyKey(p.Length) into g                   
                    select new {MyKey=g.Key,MyCount=g.Count(),MyGroup=g };
            this.dataGridView1.DataSource = q.ToList();
            this.treeView1.Nodes.Clear();
            foreach (var group in q)
            {
                TreeNode node = treeView1.Nodes.Add($"{group.MyKey}");
                foreach (var item in group.MyGroup)
                {
                    node.Nodes.Add(item.ToString());
                }
            }

            this.chart1.DataSource = q.ToList();            
            this.chart1.Series[0].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Column;
            this.chart1.Series[0].XValueMember = "MyKey";
            this.chart1.Series[0].YValueMembers = "MyCount";
            this.chart1.Series[0].Name = "大小";
        }
        private string MyKey(long p)
        {
            if (p < 10000)
            {
                return "Small";
            }
            else if (p < 100000)
            {
                return "Median";
            }
            else
            {
                return "Large";
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            files = dirs.GetFiles();
            var q = from p in files
                    group p by p.CreationTime.Year into g
                    select new { MyYear = g.Key, MyCount = g.Count(), MyGroup=g };
            this.dataGridView1.DataSource = q.ToList();
            this.treeView1.Nodes.Clear();
            foreach (var group in q)
            {
                TreeNode node = treeView1.Nodes.Add($"{group.MyYear}");
                foreach (var item in group.MyGroup)
                {
                    node.Nodes.Add(item.ToString());
                }
            }
            this.chart1.DataSource = q.ToList();
            this.chart1.Series[0].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Column;
            this.chart1.Series[0].XValueMember = "MyYear";
            this.chart1.Series[0].YValueMembers = "MyCount";
            this.chart1.Series[0].Name = "年";
        }       

        private void button3_Click(object sender, EventArgs e)
        {
            var q = from p in this.nwDataSet1.Products
                    group p by MyKey2((int)p.UnitPrice) into g
                    orderby g.Key descending
                    select new {MyKey2=g.Key,MyCount=g.Count(), MyGroup=g };
            this.dataGridView1.DataSource = q.ToList();
            this.treeView1.Nodes.Clear();
            foreach (var group in q)
            {
                TreeNode node = treeView1.Nodes.Add($"{group.MyKey2}");
                foreach (var item in group.MyGroup)
                {
                    node.Nodes.Add(item.UnitPrice.ToString());
                }
            }
            this.chart1.DataSource = q.ToList();
            this.chart1.Series[0].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Column;
            this.chart1.Series[0].XValueMember = "MyKey2";
            this.chart1.Series[0].YValueMembers = "MyCount";
            this.chart1.Series[0].Name = "價錢";
        }

        private string MyKey2(int p)
        {
            if (p < 10)
            {
                return "最低價";
            }
            else if (p < 50)
            {
                return "中等";
            }
            else
            {
                return "最高價";
            }
        }

        private void button15_Click(object sender, EventArgs e)
        {            
            var q = from p in this.nwDataSet1.Orders
                    group p by p.OrderDate.Year into g
                    orderby g.Key descending
                    select new { MyYear= g.Key, MyCount = g.Count(), MyGroup = g };
            this.dataGridView1.DataSource = q.ToList();
            this.treeView1.Nodes.Clear();
            foreach (var group in q)
            {
                TreeNode node = treeView1.Nodes.Add($"{group.MyYear}");
                foreach (var item in group.MyGroup)
                {
                    node.Nodes.Add($"{item.OrderID},{item.OrderDate}");
                }
            }
            this.chart1.DataSource = q.ToList();
            this.chart1.Series[0].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Column;
            this.chart1.Series[0].XValueMember = "MyYear";
            this.chart1.Series[0].YValueMembers = "MyCount";
            this.chart1.Series[0].Name = "年";
        }

        private void button10_Click(object sender, EventArgs e)
        {
            var q = from p in this.nwDataSet1.Orders
                    group p by p.OrderDate.Year into g

                    from g1 in (from p in g
                                group p by p.OrderDate.Month)

                    group g1 by  g.Key into g2
                    orderby g2.Key
                    select new { Year=g2.Key,YearCount=g2.Count(),Group=g2};

            this.dataGridView1.DataSource = q.ToList();
            this.treeView1.Nodes.Clear();
            foreach (var yearGroup in q)
            {
                TreeNode yearNode = treeView1.Nodes.Add($"{yearGroup.Year},{yearGroup.YearCount}");
                foreach (var monthGroup in yearGroup.Group)
                {
                    TreeNode monthNode= yearNode.Nodes.Add($"{monthGroup.Key},{monthGroup.Count()}");
                    foreach (var item in monthGroup)
                    {
                        monthNode.Nodes.Add($"{item.OrderID},{item.OrderDate}");
                    }
                }
            }
            this.chart1.DataSource = q.ToList();
            var chartData = q.SelectMany(x => x.Group.Select(y => new
            {
                MyKey = $"{x.Year}-{y.Key:D2}",
                MyCount = y.Count()
            }));
            this.chart1.DataSource = chartData.ToList();
            this.chart1.Series[0].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Column;
            this.chart1.Series[0].XValueMember = "MyKey";
            this.chart1.Series[0].YValueMembers = "MyCount";
            this.chart1.Series[0].Name = "年";
           
            //this.chart1.Series[1].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Column;
            //this.chart1.Series[1].XValueMember = "Month";
            //this.chart1.Series[1].YValueMembers = "Count";
            //this.chart1.Series[1].Name = "月";
        }
       
    }
}
