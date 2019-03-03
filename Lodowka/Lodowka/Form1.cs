using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Lodowka
{
    public partial class Lodowka : Form
    {
        
        public Lodowka()
        {
            InitializeComponent();
        }

        public SqlConnection setup()
        {
            //string executable = System.Reflection.Assembly.GetExecutingAssembly().Location;
            //string path = (System.IO.Path.GetDirectoryName(executable));
            String connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\Database1.mdf;Integrated Security=True;Connect Timeout=30";

            //String connectionString = @"Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename = C:\Program Files (x86)\Lodowka\Lodowka\Database1.mdf; Integrated Security = True; Connect Timeout = 30";
            
            //String connectionString = @"Data Source=WIN-FS0LQTD1JSG\SQLEXPRESS;Initial Catalog=Lodowka;Integrated Security=True";
            SqlConnection sqlcn = new SqlConnection();
            sqlcn.ConnectionString = connectionString;
            return sqlcn;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //String dd = AppDomain.CurrentDomain.GetData("DataDirectory").ToString();
            //Console.WriteLine(dd);
            //string executable = System.Reflection.Assembly.GetExecutingAssembly().Location;
            //Console.WriteLine(executable);
            //string path = (System.IO.Path.GetDirectoryName(executable));
            //Console.WriteLine(path);
            //AppDomain.CurrentDomain.SetData("DataDirectory", executable);
            //Console.WriteLine(AppDomain.CurrentDomain.GetData("DataDirectory"));
            SqlConnection sqlcn = setup();
            String q = "select * from Produkt";
            DataSet ds = new DataSet();
            sqlcn.Open();
            SqlDataAdapter sqlda = new SqlDataAdapter(q,sqlcn);
            sqlda.Fill(ds);
            sqlcn.Close();
            dataGridView1.ReadOnly = true;
            dataGridView1.DataSource = ds.Tables[0];
        }

        private void button2_Click(object sender, EventArgs e)
        {
            SqlConnection sqlcn = setup();
            int id;
            String name;
            if (!Int32.TryParse(textBox1.Text, out id))
            {
                label3.Text = "Podaj prawidłowe ID";
                return;
            }
            if (textBox2.Text.Length == 0)
            {
                label3.Text = "Podaj Nazwę";
                return;
            }
            name = textBox2.Text;
            String q = String.Format("INSERT INTO Produkt (Id, Name) VALUES ({0},'{1}');",id,name);
            Console.WriteLine(q);
            SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.CommandText = q;
            sqlCommand.Connection = sqlcn;
            sqlcn.Open();
            sqlCommand.ExecuteNonQuery();
            sqlcn.Close();

            textBox1.Text = "";
            textBox2.Text = "";
            label3.Text = "";

            button1_Click(sender,e);
        }
    }
}
 