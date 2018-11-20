using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using IBM.Data.DB2.iSeries;

namespace MyPixisAir
{
        //There should be a button that allows the entry of a work order and shows the description of the task for the workorder.
        //John Dawson
    public partial class EmpWorkOrd : Form
    {
        private BindingSource bs = new BindingSource();
        private iDB2DataAdapter da = new iDB2DataAdapter();
        DataTable table;

        public EmpWorkOrd()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            string connString = "DataSource=deathstar.gtc.edu";
            string sql = "SELECT * FROM FLIGHT2018.EMPWORKORD";

            try
            {
                dataGridView1.DataSource = bs;
                da = new iDB2DataAdapter(sql, connString);
                table = new DataTable();
                da.Fill(table);
                bs.DataSource = table;
            }
            catch(Exception ex)
            {
                listBox1.Items.Add(ex.Message);
            }
        }
        private void clear_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
        }

        private void submit_Click(object sender, EventArgs e)
        {
            iDB2Connection conn;
            string update = "INSERT INTO FLIGHT2018.EMPWORKORD (EMPNO, ORDERID, HRSWRKED, LABORCST)" +
                "VALUES ('" + textBox1.Text + "','" + textBox2.Text + "','" + textBox3.Text + "','" + textBox4.Text + "')";

            try
            {
                conn = new iDB2Connection("DataSource=deathstar.gtc.edu");
                conn.Open();
                iDB2Command cmd = new iDB2Command(update, conn);
                cmd.DeriveParameters();

                cmd.ExecuteNonQuery();
                conn.Close();
            }
            catch(Exception ex)
            {
                listBox1.Items.Add(ex.Message);
            }
        }

        private void refresh_Click(object sender, EventArgs e)
        {
            string connString = "DataSource=deathstar.gtc.edu";
            string sql = "SELECT * FROM FLIGHT2018.EMPWORKORD";
            try
            {
                dataGridView1.DataSource = bs;
                da = new iDB2DataAdapter(sql, connString);
                table = new DataTable();

                da.Fill(table);
                bs.DataSource = table;
            }
            catch(Exception ex)
            {
                listBox1.Items.Add(ex.Message);
            }
        }
    }
}
