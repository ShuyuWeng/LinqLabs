using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace Starter
{
    public partial class FrmLINQ_To_XXX : Form
    {
        public FrmLINQ_To_XXX()
        {
            InitializeComponent();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            //int[] nums = { 1,2,3,4,5,6,7,8,9,10};
            //IEnumerable<IGrouping<int, int>> q = from n in nums //IEnumerable<>設定列舉的值；IGrouping<> 設定key的型別，int,int->key1為int；key2為int
            //                                     group n by (n % 2);
            //this.dataGridView1.DataSource = q.ToList();         

            int[] nums = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
           var q = from n in nums 
                         group n by (n % 2 == 0 ? "Even" : "Odd");

            this.dataGridView1.DataSource = q.ToList();
            //TreeView
            foreach (var group in q)
            {
                TreeNode node=treeView1.Nodes.Add(group.Key.ToString());
                foreach (var item in group)
                {
                    node.Nodes.Add(item.ToString());                  
                }
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            int[] nums = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
            var q = from n in nums
                    group n by (n % 2 == 0 ? "Even" : "Odd") into g
                    select new { MyKey = g.Key, MyCount = g.Count(), MyAvg = g.Average(), MyGroup = g };

            this.dataGridView1.DataSource = q.ToList();
            //TreeView
            foreach (var group in q)
            {
                TreeNode node = treeView1.Nodes.Add($"{group.MyKey}({group.MyGroup})");
                foreach (var item in group.MyGroup)
                {
                    node.Nodes.Add(item.ToString());
                }
            }

            this.chart1.DataSource = q.ToList();
            this.chart1.Series[0].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Column;
            this.chart1.Series[0].XValueMember = "MyKey";
            this.chart1.Series[0].YValueMembers = "MyCount";

            this.chart1.Series[1].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Column;
            this.chart1.Series[1].XValueMember ="MyKey";
            this.chart1.Series[1].YValueMembers = "MyAvg";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int[] nums = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
            var q = from n in nums
                    group n by MyKey(n) into g
                    select new { MyKey = g.Key, MyCount = g.Count(), MyAvg = g.Average(), MyGroup = g };

            this.dataGridView1.DataSource = q.ToList();
            //TreeView
            foreach (var group in q)
            {
                TreeNode node = treeView1.Nodes.Add($"{group.MyKey}({group.MyCount})");
                foreach (var item in group.MyGroup)
                {
                    node.Nodes.Add(item.ToString());
                }
            }
        }

        private string MyKey(int n)
        {
            if (n<5)
            {
                return "Small";
            }
            else if (n<10)
            {
                return "Median";
            }
            else
            {
                return "Large";
            }
        }

        private void button38_Click(object sender, EventArgs e)
        {          
            System.IO.DirectoryInfo dirs = new System.IO.DirectoryInfo(@"c:\windows");
            FileInfo[] files = dirs.GetFiles();           

            var q = from f in files
                    group f by f.Extension into g
                    orderby g.Count() descending
                    select new { MyKey = g.Key, MyCount = g.Count(),MyGroup=g };

            this.dataGridView1.DataSource = q.ToList();
            //TreeView
            foreach (var group in q)
            {
                TreeNode node = treeView1.Nodes.Add($"{group.MyKey}({group.MyCount})");
                foreach (var item in group.MyGroup)
                {
                    node.Nodes.Add(item.ToString());
                }
            }
        }
       
    }
}
