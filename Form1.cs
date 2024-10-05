using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {

        private string connectionString;
        private SqlDataAdapter phoneAdapter;
        private SqlDataAdapter orderAdapter;
        private SqlDataAdapter manufacturerAdapter;
        private SqlCommandBuilder phoneBuilder = new SqlCommandBuilder();
        private SqlCommandBuilder orderBuilder = new SqlCommandBuilder();

        private DataSet dataSet = new DataSet();
        public Form1()
        {
            InitializeComponent();

            connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["sushi"].ConnectionString;
            // Создание объектов NpgsqlDataAdapter.
            phoneAdapter = new SqlDataAdapter("Select * from Addon", connectionString);
            //orderAdapter = new SqlDataAdapter("Select * from \"order\";", connectionString);
            //manufacturerAdapter = new SqlDataAdapter("Select * from manufacturer", connectionString);

            // Автоматическая генерация команд SQL.
            
            //orderBuilder = new SqlCommandBuilder(orderAdapter);

            // Заполнение таблиц в DataSet.
            phoneAdapter.Fill(dataSet, "Addon");
            phoneBuilder = new SqlCommandBuilder(phoneAdapter);
            //orderAdapter.Fill(dataSet, "order");
            // manufacturerAdapter.Fill(dataSet, "manufacturer");

            // Связывание элементов управления с данными.


            dataGridView1.DataSource = dataSet.Tables["Addon"]; 
            dataGridView3.DataSource = dataSet.Tables["Addon"];
            //dataGridViewOrders.DataSource = dataSet.Tables["order"];

            // Заполнение комбобокса "Производитель" в таблице "Товары".


            // Заполнение комбобокса "Производитель" для отчета1.
            //FillReport1Combobox();
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();
                SqlCommand sqlCommand =
                    new SqlCommand("SELECT DISTINCT CAST(order_date AS DATE) As Data FROM [dbo].[Order]", sqlConnection);
                SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                while (sqlDataReader.Read())
                {
                    comboBox1.Items.Add(Convert.ToString(sqlDataReader["Data"]).Substring(0, 10));
                    comboBox2.Items.Add(Convert.ToString(sqlDataReader["Data"]).Substring(0, 10));
                }
               
                sqlDataReader.Close();
            }

           

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();
                SqlCommand sqlCommand =
                    new SqlCommand("EXEC GetTotalRevenueByDate '" + comboBox1.Text + "'", sqlConnection);
                SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                while (sqlDataReader.Read())
                {
                    label1.Text = "Сумма выручки: " + Convert.ToString(sqlDataReader["TotalRevenue"]);
                }

                sqlDataReader.Close();
            }


        }
        //SELECT COUNT(*) AS CanceledOrders
        //        FROM[dbo].[Order]
        //        WHERE order_status = 'Отменен'
        //AND CAST(order_date AS DATE) = '2024-09-27';
        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();
                SqlCommand sqlCommand =
                    new SqlCommand("SELECT COUNT(*) AS CanceledOrders \n FROM[dbo].[Order] \n WHERE order_status = 'Отменен' \n AND CAST(order_date AS DATE) = '" + comboBox2.Text + "'", sqlConnection);
                SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                while (sqlDataReader.Read())
                {
                    label2.Text = "Количество отмененных заказов: " + Convert.ToString(sqlDataReader["CanceledOrders"]);
                }

                sqlDataReader.Close();
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            phoneAdapter.Update(dataSet, "Addon");
        }
        private void button4_Click(object sender, EventArgs e)
        {


           
    
        }
        private void FillReport1Combobox()
        {
            using (var sqlConnection = new SqlConnection(connectionString))
            {
               SqlDataAdapter sqlAdapter = new SqlDataAdapter("select * from Addon", sqlConnection);

                // Заполнение dataSet данными из sqlAdapter.
               DataSet dataSet = new DataSet();
                sqlAdapter.Fill(dataSet, "Addon");

                // Связывание комбобокса cbSupplier с таблицей suppliers из dataSet.
                //comboBox1.DataSource = dataSet.Tables["Addon"];
                //comboBox1.DisplayMember = "name";
                //comboBox1.ValueMember = "id";
            }
        }

        /// <summary>
        /// Заполнить комбобокс "Материал" в таблице "Детали".
        /// </summary>
       
        private void Form1_Load(object sender, EventArgs e)
        {

        }
       

        private void dataGridView3_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void tabPage4_Click(object sender, EventArgs e)
        {

        }

        private void tabPage3_Click(object sender, EventArgs e)
        {

        }

        private void tabPage7_Click(object sender, EventArgs e)
        {

        }

      

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

       

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click_1(object sender, EventArgs e)
        {

        }

       
    }
}
