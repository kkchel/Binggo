using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MySql.Data.MySqlClient;


namespace _1to50Game_Client
{
    public partial class Record_Form : Form
    {
        private MySqlConnection MConn;
        public string strSql2 = "server=localhost;Database=rank;UID=root;Pwd=mysql2017";
        List<Label> label_list = new List<Label>();

        string ID;
        string Times;
        
        public Record_Form()
        {
            InitializeComponent();
        }

        public Record_Form(string str)
        {
            
            InitializeComponent();
            ID = str;
            label_list.Add(label11);
            label_list.Add(label12);
            label_list.Add(label13);
            label_list.Add(label14);
            label_list.Add(label15);
            label_list.Add(label16);
            label_list.Add(label17);
            label_list.Add(label18);
            label_list.Add(label19);
            label_list.Add(label20);
            label_list.Add(label21);
            label_list.Add(label22);
            label_list.Add(label23);
            label_list.Add(label24);
            label_list.Add(label25);
            label_list.Add(label26);
            label_list.Add(label27);
            label_list.Add(label28);
            label_list.Add(label29);
            label_list.Add(label30);

        }
        private void Record_Form_Load(object sender, EventArgs e)
        {
            
        }


        // 폼 닫기
        private void CloseForm_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void form_set(Label label1, Label label2, rank tmp)
        {
            label1.Text = tmp.name;
            label2.Text = tmp.time;
        }

        private void form_reset()
        {
            for (int i = 0; i < 10; i++)
            {
                label_list[i * 2 + 1].Text = "----";
                label_list[i * 2].Text = "--:--:--";
            }
        }

        private void MyRecord(object sender, EventArgs e)
        {
            form_reset();
            MConn = new MySqlConnection(strSql2);
            MConn.Open();
            string ranksql;
            ranksql = "SELECT * FROM rank WHERE ID= '" + ID + "'";
            if (radioButton1.Checked == true)
                ranksql = "SELECT * FROM rank WHERE ID= '" + ID + "' and Mode='3X3'";
            else if (radioButton2.Checked == true)
                ranksql = "SELECT * FROM rank WHERE ID= '" + ID + "' and Mode='5X5'";
            else if (radioButton3.Checked == true)
                ranksql = "SELECT * FROM rank WHERE ID= '" + ID + "' and Mode='1X1'";
            var Comm = new MySqlCommand(ranksql, MConn);
            MySqlDataReader myRead = Comm.ExecuteReader();

            int x = myRead.VisibleFieldCount;
            int row = 0;
            rank[] Rank = new rank[30];
            while (myRead.Read())
            {
                Rank[row] = new rank();
                Rank[row].add(myRead["ID"].ToString(), myRead["Time"].ToString());
                row++;

            }

            for (int i = 0; i < row - 1; i++)
            {
                string min = Rank[i].time;
                int min_int = i;
                for (int j = i; j < row; j++)
                {
                    if (String.Compare(Rank[min_int].time, Rank[j].time) == 1)
                    {//함수쓰기
                        min_int = j;
                    }

                }
                rank temp = Rank[min_int];
                Rank[min_int] = Rank[i];
                Rank[i] = temp;
            }
            MConn.Close();
            for (int i = 0; i < row; i++)
            {
                form_set(label_list[i * 2 + 1], label_list[i * 2], Rank[i]);
            }

        }

        private void Search(object sender, EventArgs e)
        {
            form_reset();
            MConn = new MySqlConnection(strSql2);
            MConn.Open();
            string ranksql = "";
            if(radioButton1.Checked == true)
            ranksql = "SELECT * FROM rank WHERE ID= '" + textBox1.Text + "' and Mode='3X3'";
            else if(radioButton2.Checked == true)
                ranksql = "SELECT * FROM rank WHERE ID= '" + textBox1.Text + "' and Mode='5X5'";
            else if(radioButton3.Checked == true)
                ranksql = "SELECT * FROM rank WHERE ID= '" + textBox1.Text + "' and Mode='1X1'";
            var Comm = new MySqlCommand(ranksql, MConn);
            MySqlDataReader myRead = Comm.ExecuteReader();

            int x = myRead.VisibleFieldCount;
            int row = 0;
            rank[] Rank = new rank[100];
            while (myRead.Read())
            {
                Rank[row] = new rank();
                Rank[row].add(myRead["ID"].ToString(), myRead["Time"].ToString());
                row++;

            }

            for (int i = 0; i < row - 1; i++)
            {
                string min = Rank[i].time;
                int min_int = i;
                for (int j = i; j < row; j++)
                {
                    if (String.Compare(Rank[min_int].time, Rank[j].time) == 1)
                    {//함수쓰기
                        min_int = j;
                    }

                }
                rank temp = Rank[min_int];
                Rank[min_int] = Rank[i];
                Rank[i] = temp;
            }
            MConn.Close();
            for (int i = 0; i < 10 && i < row ; i++)
            {
                form_set(label_list[i * 2 + 1], label_list[i * 2], Rank[i]);
            }

        }

        
    }
}
