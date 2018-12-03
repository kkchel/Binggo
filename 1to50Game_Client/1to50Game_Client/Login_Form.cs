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
    public partial class Login_Form : Form
    {
        // 변수들
        public Waiting_Form frm;
        Socket mainSock;

        public Login_Form()
        {
            InitializeComponent();
        }

        // 폼 로드시 
        private void Login_Form_Load(object sender, EventArgs e)
        {
            this.Text = "1 to 50 Game";
            ClientSize = new System.Drawing.Size(475, 250);


            IPHostEntry he = Dns.GetHostEntry(Dns.GetHostName());

            // 처음으로 발견되는 ipv4 주소를 사용한다.
            IPAddress defaultHostAddress = null;
            foreach (IPAddress addr in he.AddressList)
            {
                if (addr.AddressFamily == AddressFamily.InterNetwork)
                {
                    defaultHostAddress = addr;
                    break;
                }
            }

            // 주소가 없다면..
            if (defaultHostAddress == null)
                // 로컬호스트 주소를 사용한다.
                defaultHostAddress = IPAddress.Loopback;

            txtIP.Text = defaultHostAddress.ToString();


            ID.Text = Properties.Settings.Default.savedID1;

            mainSock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.IP);


            if (mainSock.Connected)
            {
                MsgBoxHelper.Error("이미 연결되어 있습니다!");
                return;
            }

            int port;
            if (!int.TryParse("15000", out port))
            {
                MsgBoxHelper.Error("포트 번호가 잘못 입력되었거나 입력되지 않았습니다.");
                this.Close();
                return;
            }

            try { mainSock.Connect(txtIP.Text, 15000); }
            catch (Exception ex)
            {
                MsgBoxHelper.Error("연결에 실패했습니다!\n오류 내용: {0}", MessageBoxButtons.OK, ex.Message);
                this.Close();
                return;
            }

            AsyncObject obj = new AsyncObject(4096);
            obj.WorkingSocket = mainSock;
            mainSock.BeginReceive(obj.Buffer, 0, obj.BufferSize, 0, DataReceived, obj);
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
                    case "200": Login(tokens); break;
                }
            }
            catch
            {
            }
            obj.ClearBuffer();

            // 수신 대기
            obj.WorkingSocket.BeginReceive(obj.Buffer, 0, 4096, 0, DataReceived, obj);
        }




        // 게임 시작 버튼
        private void Game_Start_Click_1(object sender, EventArgs e)
        {
            
            if (this.ID.Text == "")
            {
                MessageBox.Show("로그인 아이디를 입력하세요");
                this.ID.Focus();
            }
            else if (this.Password.Text == "")
            {
                MessageBox.Show("비밀번호를 입력하세요");
                this.Password.Focus();
            }

            else if (this.ID.Text != "" && this.Password.Text != "")
            {
                byte[] allPID = Encoding.UTF8.GetBytes("101" + '\x01' + ID.Text + '\x01' + Password.Text + '\x01');
                mainSock.Send(allPID);
            }
            
        }

        private void Login(string[] tokens){
            switch (int.Parse(tokens[1]))
            {
                case 0: MessageBox.Show("로그인 되었습니다", "확인", MessageBoxButtons.OK, MessageBoxIcon.Information); break;
                case 1: MessageBox.Show("암호가 일치하지 않습니다"); return; 
                case 2: MessageBox.Show("등록되지 않은 아이디 입니다."); return;
            }
            byte[] IDSend = Encoding.UTF8.GetBytes("01" + '\x01' + ID.Text + '\x01');
            mainSock.Send(IDSend);
            frm = new Waiting_Form(ID.Text, mainSock);
            this.Visible = false;
            frm.ShowDialog();

        }



        // 회원 가입 여부 선택 시
        private void Join_Start_Click_1(object sender, EventArgs e)
        {
            Join_Form frm = new Join_Form(this);

            frm.Show();
        }

    }
}
