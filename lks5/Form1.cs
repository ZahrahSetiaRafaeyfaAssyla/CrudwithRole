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
using System.Xml.Linq;

namespace lks5
{
    public partial class Form1 : Form
    {
        static SqlConnection sqlConnection = new SqlConnection(@"Data Source = ASUS-VIVOBOOK\SQLEXPRESS; Initial Catalog=Lks; Integrated Security = True");

        public Form1()
        {
            InitializeComponent(); 
        }

        private void openConection() //Connection to Database
        {
            try
            {
                if (sqlConnection.State == ConnectionState.Closed)
                {
                    sqlConnection.Open();
                }
            }
            catch (Exception e)  
            {
                MessageBox.Show(e.Message);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            loadDgv();//DGV = Data grid view
        }

        private void LoadCbRole()
        {
            comboBoxRole.Items.Clear();

            openConection();

            string query = "SELECT * FROM role";
            SqlDataAdapter sqldataadapter = new SqlDataAdapter(query, sqlConnection);

            DataTable dt = new DataTable();
            sqldataadapter.Fill(dt);

            comboBoxRole.DataSource = dt;
            comboBoxRole.ValueMember = "id";
            comboBoxRole.DisplayMember = "Name";
            Convert.ToString(comboBoxRole.SelectedValue);


        }

        private void loadDgv()
        {
            dataGridView1.Rows. Clear();

            openConection();

            string query = "SELECT * FROM siswa";
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(query, sqlConnection);

            DataTable dt = new DataTable();
            sqlDataAdapter.Fill(dt);

            foreach (DataRow dr in dt.Rows) 
            {
                dataGridView1.Rows.Add(dr["id"], dr["Name"], dr["Email"], dr["PhoneNumber"]);
            }
        }

        private void btnInsert_Click(object sender, EventArgs e)
        {
            try
            {
                openConection();

                SqlCommand cmd = new SqlCommand($"INSERT INTO  siswa VALUES ('{textBoxId.Text}', '{textBoxName.Text}', '{textBoxEmail.Text}', '{textBoxPhone.Text}')", sqlConnection);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Insert Succes");

                loadDgv();
            } catch (Exception ex) 
            {
                MessageBox.Show(ex.Message);
            }
            


        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                openConection();
                SqlCommand cmd = new SqlCommand($"UPDATE siswa SET Name = '{textBoxName.Text}', Email = '{textBoxEmail.Text}', PhoneNumber = '{textBoxPhone.Text}' WHERE id = '{textBoxId.Text}'",
            sqlConnection);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Update Succes!");
                loadDgv();
            }
            catch (Exception ex) 
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                openConection();

                SqlCommand cmd = new SqlCommand($"DELETE siswa WHERE Id = '{textBoxId.Text}'", sqlConnection);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Delete Succes!");

                loadDgv();


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void comboBoxRole_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
