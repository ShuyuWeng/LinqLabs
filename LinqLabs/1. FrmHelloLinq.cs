using LinqLabs;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Starter
{
    public partial class FrmHelloLinq : Form
    {
        public FrmHelloLinq()
        {
            InitializeComponent();
            this.productsTableAdapter1.Fill(this.nwDataSet1.Products);
            this.ordersTableAdapter1.Fill(this.nwDataSet1.Orders);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            //public interface IEnumerator<T>
            //摘要：
            //公開支援指定類型集合上簡單反覆運算的列舉值。

            int[] nums = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
            this.listBox1.Items.Clear();
            foreach (int n in nums)
            {
                this.listBox1.Items.Add(n);
            }
            //====================
            // 嚴重性 程式碼 說明 專案  檔案 行   隱藏項目狀態
            //錯誤  CS1579 因為 'int' 不包含 'GetEnumerator' 的公用執行個體或延伸模組定義，
            //所以 foreach 陳述式無法在型別 'int' 的變數上運作 LinqLabs    C:\Shared\Shared\student\LinqLabs(StartUp)\LinqLabs\1.FrmHelloLinq.cs 30  作用中
            //            int w = 100;
            //            foreach (int n in w)
            //            { 
            //            }

            //C# 轉譯
            this.listBox1.Items.Add("=================================");
            System.Collections.IEnumerator en = nums.GetEnumerator();
            while (en.MoveNext())
            {
                this.listBox1.Items.Add(en.Current);
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
          
        }

        private void button2_Click(object sender, EventArgs e)
        {
            // Step 1: define Data Source object
            //Step 2: define Query
            //Step 3: execute Query
            //==========================
            //step1: define data source
            int[] nums = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

            //Setp2: Define Query
            //define query  (IEnumerable<int> q 是一個  Iterator(跌代器) 物件)　, 如陣列集合一般 (陣列集合也是一個  Iterator 物件 迭代器
            //IEnumerable<int> q -  公開支援指定型別集合上簡單反覆運算的列舉值。
            //這裡只是定義還沒有呼叫方法
            IEnumerable<int> p = from n in nums
                                 where n % 2 == 0 && n >= 5 && n <= 8
                                 select n;

            //Step 3: Execute Query
            //execute query(執行 iterator - 逐一查看集合的item)
            //foreach才開始呼叫方法n，再從方法n回傳直給q
            foreach (int n in p)
            {
                this.listBox1.Items.Add(n);
            }                        
        }

        private void button6_Click(object sender, EventArgs e)
        {
            int[] nums = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

            IEnumerable<int> p = from n in nums
                                     //where n % 2 == 0&&n>=5&&n<=8
                                 where isEven(n)
                                 select n;
            foreach (int n in p)
            {
                this.listBox1.Items.Add(n);
            }            
        }
        bool isEven(int n)
        {
            return n % 2 == 0;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            int[] nums = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

            IEnumerable<Point> p = from n in nums
                                     //where n % 2 == 0&&n>=5&&n<=8
                                 where isEven(n)
                                 select new Point(n,n*n);//Point(x,y)Point座標ˊ
            //foreach ... execute query
            foreach (Point n in p)
            {
                this.listBox1.Items.Add(n);
            }
            //====================================
            //TOXXX ... excute query
            List<Point> list = p.ToList();
            this.dataGridView1.DataSource = list;
            //====================================
            //Chart
            this.chart1.DataSource = list;
            this.chart1.Series[0].XValueMember = "X";
            this.chart1.Series[0].YValueMembers = "Y";
            this.chart1.Series[0].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Column;
        }


        private void button1_Click(object sender, EventArgs e)
        {
            string[]words= { "aaa", "apPle", "PineApple", "xxxApple", "xxx" };

            IEnumerable<string> q = from w in words
                                    where w.ToUpper().Contains("APPLE") && w.Length> 5//w.Contains("Apple")&&w.StartsWith("x")      //w.Contains("Apple")||w.Contains("apple")
                                    select w;

            foreach (string s in q)
            {
                this.listBox1.Items.Add(s);
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            //adapter Fill()
            //var n = 200;
            var q = from p in this.nwDataSet1.Products
                    where !p.IsUnitPriceNull()&& p.UnitPrice > 5 && p.UnitPrice <200 && p.ProductName.StartsWith("G")
                    select p;
            this.dataGridView1.DataSource = q.ToList();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            var q = from p in this.nwDataSet1.Orders
                    where   p.OrderDate.Year==1998
                    select p;
            //this.dataGridView1.DataSource = q.ToList();

            this.bindingSource1.DataSource = q.ToList();
            
            this.dataGridView1.DataSource = this.bindingSource1;
            this.bindingSource1.CurrentChanged += bindingSource1_CurrentChanged;

        }

        private void bindingSource1_CurrentChanged(object sender, EventArgs e)
        {
            int OrderID = ((NWDataSet.OrdersRow)this.bindingSource1.Current).OrderID;

            var q = from od in this.nwDataSet1.Order_Details
                    where od.OrderID ==OrderID
                    select od;
            this.dataGridView2.DataSource = q.ToList();
        }

        private void button49_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            int[] nums = { 2, 3 };

            //var q = nums.Where(.......).Select(............);
        }
    }    
}
