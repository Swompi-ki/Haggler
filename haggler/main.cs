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
    public partial class main : Form
    {
        public main()
        {
            InitializeComponent();
            get_Info(list); 
        }

        void get_Info(ListView list)
        {
            string queru = "select tovar.ID, tovar.name, tovar.description as характеристика, tovar.inform as информация, booking.status as статус from tovar inner join booking ON tovar.ID = booking.ID;";
            MySqlConnection conn = DBUtils.GetDBConnection();
            MySqlCommand cmDB = new MySqlCommand(queru, conn);
            cmDB.CommandTimeout = 60;
            try
            {
                conn.Open();
                MySqlDataReader rd = cmDB.ExecuteReader();
                if (rd.HasRows)
                {
                    while (rd.Read())
                    {
                        string[] row = { rd.GetString(0), rd.GetString(1), rd.GetString(2), rd.GetString(3), rd.GetString(4) };
                        var listItem = new ListViewItem(row);
                        list.Items.Add(listItem);
                    }
                }
                conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            list.Items.Clear();
            get_Info(list);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string query = "delete from tovar where tovar.ID='" + list.Items[list.SelectedIndices[0]].Text + "';";
            MySqlConnection conn = DBUtils.GetDBConnection();
            MySqlCommand cmDB = new MySqlCommand(query, conn);
            cmDB.CommandTimeout = 60;
            try
            {
                conn.Open();
                MySqlDataReader rd = cmDB.ExecuteReader();
                conn.Close();
                foreach (ListViewItem item in list.SelectedItems)
                {
                    list.Items.Remove(item);
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Char_haggler win = new Char_haggler();
            win.Show();
            this.Hide();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Aftoriz win = new Aftoriz();
            win.Show();
            this.Hide();
        }
    }
}
