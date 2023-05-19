using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Starter
{
    public partial class FrmLangForLINQ : Form
    {
        public FrmLangForLINQ()
        {
            InitializeComponent();           
        }

        private void button4_Click(object sender, EventArgs e)
        {
            int n1 = 100, n2 = 200;            
            MessageBox.Show(n1+" , "+n2);
            Swap(ref n1 , ref n2);
            MessageBox.Show(n1 + " , " + n2);
            //==================
            string o1 = " 一", o2 = "二";
            MessageBox.Show(o1 + " , " + o2);
            Swap(ref o1, ref o2);
            MessageBox.Show(o1 + " , " + o2);

        }
        void Swap(ref int n1,ref int n2)
        {
            int temp = n2;
            n2 = n1;
            n1= temp;
        }

        void Swap(ref string o1,ref string o2)
        {
            string temp = o2;
            o2 = o1;
            o1 = temp;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.listBox1.Items.Add("afsasf");
            this.listBox1.Items.Add(333);
        }
        void SwapObject(ref object o1, ref object o2)
        {
           //
        }

        private void button7_Click(object sender, EventArgs e)
        {
            int n1 = 100, n2 = 200;            
            MessageBox.Show(n1 + " , " + n2);
            SwapAnyType<int>(ref n1, ref n2);
            MessageBox.Show(n1 + " , " + n2);
            //=======================
            string o1 = " 一", o2 = "二";
            MessageBox.Show(o1 + " , " + o2);
            SwapAnyType(ref o1, ref o2);
            MessageBox.Show(o1 + " , " + o2);
        }

        private void SwapAnyType<T>(ref T n1,ref T n2)
        {
            T temp = n2;
            n2 = n1;
            n1 = temp; ;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.buttonX.Click += ButtonX_Click;
            this.buttonX.Click += new EventHandler(aaa);//aaa;
            //=======================
            //C# 2.0匿名方法
            this.buttonX.Click += delegate (object sender1, EventArgs e1) { MessageBox.Show("匿名方法"); };
            //( )參數，{ }方法，這方法只用在這裡，其他地方都不會用到所以可用匿名方法以等號接方法寫完而不用另外宣告。
            //========================================
            //C# 3.0匿名方法 lambda => goes to
            this.buttonX.Click += (object sender1, EventArgs e1) => { MessageBox.Show("匿名方法"); };
        }

        private void ButtonX_Click(object sender, EventArgs e)
        {            MessageBox.Show("ButtonX_Click");        }

        private void aaa(object sender, EventArgs e)
        {            MessageBox.Show("aaa");        }

        bool Test(int n)
        {            return n > 5;                      }

        bool IsEven(int n)
        {            return  n  %  2 == 0;        }
        //Step1:create delegate Class
        //Step2:create delegate Object
        //Step3:call method / invoke method
        public delegate bool MyDelegate(int n);
        private void button9_Click(object sender, EventArgs e)
        {
            bool result = Test(4);
            MessageBox.Show("result = "+result);

            MyDelegate delegateObj = Test;//new MyDelegate(Test)
            result = delegateObj(7);// delegateObj.Invoke(7)
            MessageBox.Show("result = " + result);

            delegateObj = IsEven;
            result = delegateObj(11);
            MessageBox.Show("result = " + result);
            //=======================
            //C# 2.0匿名方法
            delegateObj = delegate (int n) { return n > 5; };
            result = delegateObj(7);
            MessageBox.Show("result = " + result);
            //========================================
            //C# 3.0匿名方法 lambda expression => 
            delegateObj = n => n > 5;//省略int,省略return
            result = delegateObj(3);
            MessageBox.Show("result = " + result);
        }
        List<int> MyWhere(int[] nums, MyDelegate delegateObj)
        {
            List<int> list = new List<int>();
            foreach (int n in nums)
            {
                if (delegateObj(n))
                {                    list.Add(n);                }
            }
            return list;
        }
        private void button10_Click(object sender, EventArgs e)
        {
            int[] nums = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
            List<int> list1 = MyWhere(nums, Test);
            List<int> list2 = MyWhere(nums, IsEven);

            List<int> largeList = MyWhere(nums, n => n > 5);
            List<int> oddlist = MyWhere(nums, n=>n%2==1);
            foreach (int n in list1)
            {
                this.listBox1.Items.Add(n);
            }
            foreach (int n in list2)
            {
                this.listBox2.Items.Add(n);
            }
          
        }

        IEnumerable<int> MyIterator(int[] nums, MyDelegate delegateObj)
        {          
            foreach (int n in nums)
            {
                if (delegateObj(n))
                {
                    yield return n;
                }                
            }          
        }

        private void button13_Click(object sender, EventArgs e)
        {
            int[] nums = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
            IEnumerable<int> q = MyIterator(nums,n=>n%2==0);
            foreach (int n in q)
            {
                this.listBox1.Items.Add(n);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            int[] nums = { 1,2,3,4,5,6,7,8,9,10};
            //IEnumerable<int> q = from n in nums
            //                     where n > 5
            //                     select n;

            IEnumerable<int> q = nums.Where<int>(n => 5 > 5);
            foreach (int n in q)
            {
                this.listBox1.Items.Add(n);
            }
            //=====================
            string[] words = { "一", "二二", "三三三", "四四四四", "五五五五五", "六六六六六六", "七七七七七七七" };
            var q1 = words.Where<string>(w => w.Length >= 5);
            foreach (string s in q1)
            {
                this.listBox2.Items.Add(s);
            }
            //================
            IEnumerable<Point> q2 = nums.Where(n => n > 5).Select(n=>new Point(n,n*n));
            this.dataGridView1.DataSource = q2.ToList();

        }

        private void button45_Click(object sender, EventArgs e)
        {
            //var懶得寫(x)
            //var型別難寫
            //var for 匿名方法
            //var n;    var依等號後面的值做判斷類別，所以不能省略等號
            var s = "abc";
            s = s.ToUpper(); 

            var pt1 = new Point(3,3);
            //pt1.x
        }

        private void button41_Click(object sender, EventArgs e)
        {
            //new Font("arial",33)   //Font有13種建構子
            //() constructor建構子方法
            MyPoint pt1 = new MyPoint();
            MyPoint pt2 = new MyPoint(55);
            MyPoint pt3 = new MyPoint(3,4);

            List<MyPoint> list1 = new List<MyPoint>();
            list1.Add(pt1);
            list1.Add(pt2);
            list1.Add(pt3);
            this.dataGridView1.DataSource = list1;
            //======================
            //{ } object initialize 物件初始化
            list1.Add(new MyPoint{P1=88,P2=77,P3="mary" });
            list1.Add(new MyPoint { P1=99});//沒設定P2.P3的值，結果為預設值0

            MyPoint pt4 = new MyPoint { P2=101};
            list1.Add(pt4);
            this.dataGridView1.DataSource = list1;
            //========================

            List<MyPoint> list2 = new List<MyPoint>()
            {
                new MyPoint{ P1=9,P2=7,P3="xxx"},
                new MyPoint{ P1=9999,P2=77777,P3="xxx"},
                new MyPoint{ P1=99,P3="xxx"}
            };
            this.dataGridView2.DataSource = list2;
        }

        private void button43_Click(object sender, EventArgs e)
        {
            var pt1 = new {P1=99,P2=77,P3=66 };
            var pt2 = new { P1 = 99, P2 = 77, P3 = 66 };
            var pt3 = new { name="xxx",password="ooooo"};

            this.listBox1.Items.Add(pt1.P1);
            this.listBox1.Items.Add(pt3.name);
            this.listBox1.Items.Add(pt1.GetType());//因pt很難寫不知道類型給var值判斷，用gettype取得系統判斷型別類型
            this.listBox1.Items.Add(pt2.GetType());
            this.listBox1.Items.Add(pt2.GetType());

            //==========================
            int[] nums = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

            //var q = from n in nums
            //        where n > 5
            //        select new { N = n, Square = n * n, Cube = n * n * n };

            var q = nums.Where(n => n>5).Select(n=> new {N=n,Spuare=n*n,Cube=n*n*n });           

            this.dataGridView1.DataSource = q.ToList();
            //=============================
            this.productsTableAdapter1.Fill(nwDataSet1.Products);

            var q2 = from n in this.nwDataSet1.Products
                     where n.UnitPrice > 30
                     orderby n.UnitPrice descending
                     select new { ID = n.ProductID, 產品名稱 = n.ProductName, n.UnitPrice, n.UnitsInStock, 總價 = $"{n.UnitPrice * n.UnitsInStock:c2}" };

            var q3 = this.nwDataSet1.Products.Where(n => n.UnitPrice > 30).OrderByDescending(n => n).Select(n => new { ID = n.ProductID, 產品名稱 = n.ProductName, n.UnitPrice, n.UnitsInStock, 總價 = $"{n.UnitPrice * n.UnitsInStock:c2}" });
     
            this.dataGridView2.DataSource = q2.ToList();
        }      

        private void button32_Click(object sender, EventArgs e)
        {
            string s = "abcde";
            int count = s.WordCount();
            MessageBox.Show("count= "+count);

            string s1 = "aaaaaaaaaaaaaa";
            count = s1.WordCount();

            char ch = s.Char(2);
            MessageBox.Show("char= " + ch);
        }
    }
}

public static class MyCount
{
    public static int WordCount(this string s)
    {
        return s.Length;
    }

    public static char Char(this string s,int i)
    {
        return s[i];
    }
}

class MyPoint
{
    public MyPoint()
    {
    }

    public MyPoint(int p1)
    {
        P1 = p1;
    }
    public MyPoint(int p1, int p2)
    {
        P1 = p1;
        P2 = p2;
    }
    public int P1 { get; set; }
    public int P2 { get; set; }
    public string P3 { get; set; }
}