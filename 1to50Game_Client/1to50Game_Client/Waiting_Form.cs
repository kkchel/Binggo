using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;
using MySql.Data.MySqlClient;

namespace _1to50Game_Client
{
    public partial class Waiting_Form : Form
    {
        delegate void AppendTextDelegate(Control ctrl, string s);
        AppendTextDelegate _textAppender;
        Socket mainSock;
        string ID;



        string readID;
        string readPW;
        string readwin;
        string readlose;



        private MySqlConnection MConn;

        public string strSql = "server=localhost;Database=game;UID=root;Pwd=mysql2017";





        public Waiting_Form(string ID, Socket tmpsocket)
        {
            InitializeComponent();
            mainSock = tmpsocket;
            _textAppender = new AppendTextDelegate(AppendText);
            this.ID = ID;
            label1.Text = ID;
            AsyncObject obj = new AsyncObject(4096);

            obj.WorkingSocket = mainSock;
            mainSock.BeginReceive(obj.Buffer, 0, obj.BufferSize, 0, DataReceived, obj);
        }

        private void Close_All(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        void AppendText(Control ctrl, string s)
        {
            if (ctrl.InvokeRequired) ctrl.Invoke(_textAppender, ctrl, s);
            else
            {
                string source = ctrl.Text;
                if (source == "")
                    ctrl.Text = source + s;
                else
                    ctrl.Text = source + Environment.NewLine + s;
            }
            txtChat.SelectionStart = txtChat.TextLength + 1;
            txtChat.ScrollToCaret();
            txtChat.Refresh();
        }

        //메세지 보내기
        private void SendData(object sender, EventArgs e)
        {
            
            // 보낼 텍스트
            string text = txtTTS.Text.Trim();
            if (string.IsNullOrEmpty(text))
            {
                MessageBox.Show("텍스트가 입력되지 않았습니다!");
                txtTTS.Focus();
                return;
            }

            // 서버 ip 주소와 메세지를 담도록 만든다.
            
            // 문자열을 utf8 형식의 바이트로 변환한다.
            byte[] bDts = Encoding.UTF8.GetBytes("102" + '\x01' + ID + '\x01' + text + '\x01');

            // 서버에 전송한다.
            mainSock.Send(bDts);

            // 전송 완료 후 텍스트박스에 추가하고, 원래의 내용은 지운다.
            txtTTS.Clear();
        }

        void DataReceived(IAsyncResult ar)
        {
            AsyncObject obj = (AsyncObject)ar.AsyncState;
            try
            {
                int received = obj.WorkingSocket.EndReceive(ar);
                if (received <= 0)
                {
                    obj.WorkingSocket.Close();
                    return;
                }
                
                string bufStr = Encoding.UTF8.GetString(obj.Buffer);
                string[] tokens = bufStr.Split('\x01');
                string flag = tokens[0];
                string serverData = tokens[1];
                switch (flag)
                {
                    case "201": Receive_Data(tokens);  break;
                    case "202": Set_memberlist(tokens); break;
                    case "203": Receive_VS(tokens); break;
                    case "204": VS_Check(tokens); break;
                    case "205": VS_Start(tokens); break;


                }
            }
            catch
            {
            }
            obj.ClearBuffer();

            // 수신 대기
            obj.WorkingSocket.BeginReceive(obj.Buffer, 0, 4096, 0, DataReceived, obj);
        }

        private void Receive_Data(string[] tokens)
        {
            AppendText(txtChat, string.Format("{0}: {1}", tokens[1], tokens[2]));
        }

        private void Set_memberlist(string[] tokens)
        {
            listBox1.Items.Clear();
            for (int i = 0; i < int.Parse(tokens[1]); i++)
            {
                //MessageBox.Show(tokens[2]);
                listBox1.Items.Add(tokens[i + 2]);
            }

        }

        private void txtTTS_Enter(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                SendData(sender, e);
            }
        }
        

        private void Game_start(object sender, EventArgs e)
        {
            Game_Form form1 = new Game_Form(mainSock, ID);
            form1.Show();
        }

        private void Record_View(object sender, EventArgs e)
        {
            Record_Form ffrm = new Record_Form(ID);

            ffrm.Show();
        }

        private void Button_VS(object sender, EventArgs e)
        {

            //선택한게 있는지 확인하는거 넣기
            string vs_id = listBox1.SelectedItem.ToString();
            if (vs_id == null || vs_id == ID)
                return;

            byte[] bDts = Encoding.UTF8.GetBytes("103" + '\x01' + "0" + '\x01' + ID + '\x01' + vs_id + '\x01');

            mainSock.Send(bDts);
        }

        private void Receive_VS(string[] tokens)
        {
            byte[] bDts;
            if (MessageBox.Show(string.Format("{0}님께서 배틀을 신청하였습니다.", tokens[1]), "Message", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                bDts = Encoding.UTF8.GetBytes("103" + '\x01' + "1" + '\x01' + tokens[1] + '\x01' + tokens[2] + '\x01' + "yes" + '\x01');
            }
            else
            {
                bDts = Encoding.UTF8.GetBytes("103" + '\x01' + "1" + '\x01' + tokens[1] + '\x01' + tokens[2] + '\x01' + "no" + '\x01');
            }

            mainSock.Send(bDts);
        }

        private void VS_Check(string[] tokens)
        {
            if (tokens[3] == "no")
            {
                MessageBox.Show(tokens[2] + "님이 거절하였습니다.");
                return;
            }

            byte[] bDts = Encoding.UTF8.GetBytes("103" + '\x01' + "2" + '\x01' + tokens[1] + '\x01' + tokens[2] + '\x01');

            mainSock.Send(bDts);

        }

        private void VS_Start(string[] tokens)
        {
            Vsgame_Form vsf = new Vsgame_Form(mainSock, tokens[1], tokens[2]);
            vsf.ShowDialog();

            record.Refresh();
        }

        //사용자 설정 게임
        private void button5_Click(object sender, EventArgs e)
        {
            Setting_Form sst = new Setting_Form();
            Game_Form gf;
            sst.ShowDialog();
            if (sst.OK)
            {
                gf = new Game_Form(mainSock, ID, sst.x, sst.y, sst.num);
                gf.Show();
            }

        }
 
        ////////////////////////////////////////////////////////////////////////////////////////

        private void Waiting_Form_Load(object sender, EventArgs e)
        {
            MConn = new MySqlConnection(strSql);
            MConn.Open();

            string sqlBofere = "SELECT ID, Password, win, lose FROM user WHERE ID= '" + ID + "'";
            var Comm = new MySqlCommand(sqlBofere, MConn);
            MySqlDataReader myRead = Comm.ExecuteReader();


            if (myRead.HasRows)
            {
                int per = 0;
                if (myRead.Read())
                {
                    readID = myRead["ID"].ToString();
                    readPW = myRead["Password"].ToString();
                    readwin = myRead["win"].ToString();
                    readlose = myRead["lose"].ToString();

                    int total = int.Parse(readwin) + int.Parse(readlose);
                    if (total != 0)
                        per = (int)(int.Parse(readwin) / (float)total * 100);
                    else
                        per = 0;
                    record.Text = total.ToString() + "전" + readwin + "승 " + readlose + "패 승률 : " + per + "%";
                    MConn.Close();

                }
            }
            MConn = new MySqlConnection(strSql);
            MConn.Open();
        }
        ///////////////////////////////////////////////////////////////////////////////
    }

}
