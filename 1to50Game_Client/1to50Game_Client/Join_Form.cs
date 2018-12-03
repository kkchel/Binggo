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
    public partial class Join_Form : Form
    {


        //변수들 
        Login_Form FormConnector;
        private Boolean idCheckd = false;
        private Boolean pwCheckd = false;



        private MySqlConnection MConn;
        public string strSql = "server=localhost;Database=game;UID=root;Pwd=mysql2017";


        public Join_Form()
        {
            InitializeComponent();
        }

        public Join_Form(Login_Form _frm)
        {
            InitializeComponent();
        }



        private void DuplicateConfirm_Click(object sender, EventArgs e)
        {
            if (newID.Text != "")
            {
                MConn = new MySqlConnection(strSql);
                MConn.Open();
                string sql = "SELECT ID, Password, Time, win, lose FROM user WHERE ID= '" + newID.Text + "'";
                var Comm = new MySqlCommand(sql, MConn);
                MySqlDataReader myRead = Comm.ExecuteReader();

                if (myRead.HasRows)
                {

                    if (myRead.Read())
                    {

                        if (myRead["ID"].ToString() == newID.Text)
                        {
                            MessageBox.Show("아이디 중복");
                            MConn.Close();
                            idCheckd = false;
                        }
                    }
                }
                else
                {
                    MessageBox.Show("아이디 사용 가능");
                    idCheckd = true;
                }
            }
            else
            {
                MessageBox.Show("아이디를 입력하세요");
            }
        }

        // 비밀번호 확인
        private void pwCheck()
        {
            if (newPW.Text == conPW.Text)
            {
                pwCheckd = true;
            }
            else
            {
                pwCheckd = false;
            }
        }

        private void complete_Join_Click(object sender, EventArgs e)
        {
            if (idCheckd)   // 중복체크
            {
                pwCheck();

                if (pwCheckd)
                {
                    MConn = new MySqlConnection(strSql);
                    MConn.Open();
                    string sql = "INSERT INTO user(ID, Password, Time, win, lose) values('" + newID.Text + "', '" + newPW.Text + "', '" + null + "', '" + 0 + "', '" + 0 + "')";
                    var Comm = new MySqlCommand(sql, MConn);
                    int i = Comm.ExecuteNonQuery();

                    if (i == 1)
                    {
                        MConn.Close();
                        newID.Focus();
                        MessageBox.Show("가입이 완료되었습니다", "확인", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    MessageBox.Show("패스워드가 일치하지 않습니다.", "확인", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    newPW.Focus();
                }
                this.Close();
            }

            else
            {
                MessageBox.Show("ID 중복체크를 해주시기 바랍니다", "확인", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                newID.Focus();
            }
            

        }
    }
}
