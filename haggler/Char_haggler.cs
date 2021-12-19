using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace haggler
{
    public partial class Char_haggler : Form
    {
        public Char_haggler()
        {
            InitializeComponent();
            getStatus(statusBox);

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string query = "insert into tovar(ID, name, description, inform) values(" + nameBox + ", '" + descriptionBox + "', '" + informBox + "');";
            MySqlConnection conn = DBUtils.GetDBConnection();
            MySqlCommand cmDB = new MySqlCommand(query, conn);
            cmDB.CommandTimeout = 60;
            try
            {
                conn.Open();
                MySqlDataReader rd = cmDB.ExecuteReader();
                conn.Close();

                string queryF = "insert into booking(status) values('" + Convert.ToString((statusBox.SelectedIndex) + 1) + "');";
                MySqlCommand cmDBF = new MySqlCommand(queryF, conn);
                try
                {
                    conn.Open();
                    MySqlDataReader rdF = cmDBF.ExecuteReader();
                    conn.Close();
                    main Win = new main();
                    Win.Show();
                    this.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        //Выбор статуса товара
        void getStatus(ComboBox sttusBox)
            {
                string query = "select booking.status from booking;";
                MySqlConnection conn = DBUtils.GetDBConnection();
                MySqlCommand cmDB = new MySqlCommand(query, conn);
                cmDB.CommandTimeout = 60;
                try
                {
                    conn.Open();
                    MySqlDataReader rd = cmDB.ExecuteReader();
                    if (rd.HasRows)
                    {
                        while (rd.Read())
                        {
                            string row = rd.GetString(0);
                            statusBox.Items.Add(row);
                        }
                    }
                    conn.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            main win = new main();
            win.Show();
            this.Hide();
        }
    }
 }


