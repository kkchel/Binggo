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


namespace _1to50Game_Server
{
    public partial class Form1 : Form
    {

        //변수
        Socket mainSock;
        IPAddress thisAddress;
        List<Member> connectedClients = new List<Member>();
        //List<Socket> connectedClients = new List<Socket>();


        delegate void AppendTextDelegate(Control ctrl, string s);
        AppendTextDelegate _textAppender;



        private MySqlConnection MConn;
        public string strSql = "server=localhost;Database=game;UID=root;Pwd=mysql2017";
        public string strSql2 = "server=localhost;Database=rank;UID=root;Pwd=mysql2017";

        string gameCountWin = "";
        string gameCountLose = "";


        string readID;
        string readPW;
        string readTime;
        string readwin;
        string readlose;


        public Form1()
        {
            mainSock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.IP);
            _textAppender = new AppendTextDelegate(AppendText);

            InitializeComponent();
        }

        void AppendText(Control ctrl, string s)
        {
            if (ctrl.InvokeRequired) ctrl.Invoke(_textAppender, ctrl, s);
            else
            {
                string source = ctrl.Text;
                ctrl.Text = source + Environment.NewLine + s;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            IPHostEntry he = Dns.GetHostEntry(Dns.GetHostName());

            foreach (IPAddress addr in he.AddressList)
            {
                if (addr.AddressFamily == AddressFamily.InterNetwork)
                {
                    thisAddress = addr;
                    break;
                }
            }

            if (thisAddress == null)
                thisAddress = IPAddress.Loopback;

            //BeginStartServer

            int port;
            String temp_port = "15000";
            port = int.Parse(temp_port);
            if (!int.TryParse(temp_port, out port))
            {
                MsgBoxHelper.Error("포트 번호가 잘못 입력되었거나 입력되지 않았습니다.");
                return;
            }
            IPEndPoint serverEP = new IPEndPoint(thisAddress, port);
            mainSock.Bind(serverEP);
            mainSock.Listen(10);
            mainSock.BeginAccept(AcceptCallback, null);
            AppendText(txtLog, string.Format("게임서버가 정상적으로 실행되었습니다"));




        }

        //클라이언트 접속
        void AcceptCallback(IAsyncResult ar)
        {
            Socket client = mainSock.EndAccept(ar);
            mainSock.BeginAccept(AcceptCallback, null);
            AsyncObject obj = new AsyncObject(4096);
            obj.WorkingSocket = client;
            connectedClients.Add(new Member("non", client));

            AppendText(txtLog, string.Format("클라이언트 (@ {0})가 연결되었습니다.", client.RemoteEndPoint));
            client.BeginReceive(obj.Buffer, 0, 4096, 0, DataReceived, obj);
        }

        void DataReceived(IAsyncResult ar)
        {
            AsyncObject obj = (AsyncObject)ar.AsyncState;
            int received;
            try
            {
                 received = obj.WorkingSocket.EndReceive(ar);
            }
            catch (SocketException e)
            {
                obj.WorkingSocket.Close();
                return;
            }
            
            if (received <= 0)
            {
                obj.WorkingSocket.Close();
                return;
            }

            

            string bufStr = Encoding.UTF8.GetString(obj.Buffer);
            string[] tokens = bufStr.Split('\x01');
            string flag = tokens[0];
            string clientData = tokens[1];


            switch(flag){
                case "00": AppendText(txtLog, clientData); break;
                case "01": IDInsert(tokens); break;
                case "100": Data_Record(tokens); break;
                case "101": Login_Check(tokens, obj.WorkingSocket); break;
                case "102": SendData(tokens); break;
                case "103": VSgame(tokens, obj.WorkingSocket); break;
            }

            obj.ClearBuffer();
            obj.WorkingSocket.BeginReceive(obj.Buffer, 0, 4096, 0, DataReceived, obj);
        }

        private void IDInsert(string[] tokens)
        {
            for (int i = connectedClients.Count - 1; i >= 0; i--)
            {
                string tmp = "";
                try
                {
                    tmp = connectedClients[i].getId();
                }
                catch
                {
                    // 오류 발생하면 전송 취소하고 리스트에서 삭제한다.
                    try { connectedClients[i].client.Dispose(); }
                    catch { }
                    connectedClients.RemoveAt(i);
                }
                if (tmp == "non")
                {
                    connectedClients[i].setId(tokens[1]);
                    return;
                }
            }
        }




        private void Data_Record(string[] tokens)
        {
            string clientData = tokens[1];
            string clientData2 = tokens[2];
            string clientData3 = tokens[3];
            readTime = clientData2;
            MConn = new MySqlConnection(strSql);
            MConn.Open();
            //string sqlAfter = "INSERT INTO user(ID, Password, Time) values('" + readID + "', " + "'" + readPW + "', " + "'" + readTime + "')";
            string updatesql = "UPDATE user SET Time='" + readTime + "' where ID='" + clientData + "'";
            var Comm = new MySqlCommand(updatesql, MConn);
            Comm.ExecuteNonQuery();
            MConn.Close();

           
            MConn = new MySqlConnection(strSql2);
            MConn.Open();
            string ranksql = "INSERT INTO rank(ID, Time, Mode) values('" + clientData + "', " + "'" + readTime + "', " + "'" + clientData3 + "')";
            Comm = new MySqlCommand(ranksql, MConn);
            Comm.ExecuteNonQuery();
            MConn.Close();



        }

        private void Login_Check(string[] tokens,Socket client)
        {
            string id = tokens[1];
            string password = tokens[2];
            
            MConn = new MySqlConnection(strSql);
            MConn.Open();

            int value = 0;
            

            string sql = "SELECT ID, Password FROM user WHERE ID= '" + id + "'";
            var Comm = new MySqlCommand(sql, MConn);
            MySqlDataReader myRead = Comm.ExecuteReader();

            if (myRead.HasRows)
            {
                if (myRead.Read())
                {
                    if (myRead["Password"].ToString() == password)
                    {
                        MConn.Close();
                        value = 0;
                    }
                    else
                    {
                        value = 1;
                    }
                }
            }
            else
            {
                value = 2;
            }
            myRead.Close();
            MConn.Close();
            byte[] allPID = Encoding.UTF8.GetBytes("200" + '\x01' + value.ToString() + '\x01');
            client.Send(allPID);



            return;
        }

        private void SendData(string[] tokens)
        {
            for (int i = connectedClients.Count -1; i >= 0; i--)
            {
                Socket tmp = connectedClients[i].client;

                byte[] send_data = Encoding.UTF8.GetBytes("201" + '\x01' + tokens[1] + '\x01' + tokens[2] + '\x01');
                try { tmp.Send(send_data); }
                catch
                {
                    // 오류 발생하면 전송 취소하고 리스트에서 삭제한다.
                    try { tmp.Dispose(); }
                    catch { }
                    connectedClients.RemoveAt(i);
                }
               
            }
        }


        private void timer1_Tick(object sender, EventArgs e)
        {
            string member_list = "";
            int member_num = 0;
            for (int i = connectedClients.Count - 1; i >= 0; i--)
            {
                try
                {
                    member_list += connectedClients[i].getId() + '\x01';
                    member_num++;
                }
                catch
                {
                    // 오류 발생하면 전송 취소하고 리스트에서 삭제한다.
                    try { connectedClients[i].client.Dispose(); }
                    catch { }
                    connectedClients.RemoveAt(i);
                }
            }

            for (int i = connectedClients.Count - 1; i >= 0; i--)
            {
                Socket tmp = connectedClients[i].client;

                byte[] send_data = Encoding.UTF8.GetBytes("202" + '\x01' + member_num.ToString() + '\x01' + member_list + '\x01');
                try { tmp.Send(send_data); }
                catch
                {
                    // 오류 발생하면 전송 취소하고 리스트에서 삭제한다.
                    try { tmp.Dispose(); }
                    catch { }
                    connectedClients.RemoveAt(i);
                }
            }
        }

        private void VSgame(string[] tokens, Socket soc)
        {
            switch (tokens[1])
            {
                case "0": VSgame_send(tokens, soc); break;
                case "1": VSgame_Received(tokens); break;
                case "2": VSgame_Start(tokens); break;
                case "3": VSgame_now(tokens); break;

                case "7": VSgame_End(tokens); break;
            }
        }


        private void VSgame_send(string[] tokens, Socket soc)
        {
            string vs_id = tokens[3];

            int x = Search(vs_id);

            byte[] send_data = Encoding.UTF8.GetBytes("203" + '\x01' + tokens[2] + '\x01' + tokens[3] + '\x01');
            try
            {
                connectedClients[x].client.Send(send_data);
            }
            catch
            {
                try { connectedClients[x].client.Dispose(); }
                catch { }
                connectedClients.RemoveAt(x);
            }

        }

        private void VSgame_Received(string[] tokens)
        {
            string tmp_id = tokens[2];
            string vs_id = tokens[3];
            int x = Search(vs_id);
            int y = Search(tmp_id);
            if (tokens[4] == "yes")
            {
                //게임시작
                byte[] send_data = Encoding.UTF8.GetBytes("204" + '\x01' + tokens[2] + '\x01' + tokens[3] + '\x01' + "yes" + '\x01');
                try
                {
                    connectedClients[y].client.Send(send_data);
                }
                catch
                {
                    // 오류 발생하면 전송 취소하고 리스트에서 삭제한다.
                    try { connectedClients[y].client.Dispose(); }
                    catch { }
                    connectedClients.RemoveAt(y);
                }
            }
            else
            {
                //게임취소
                byte[] send_data = Encoding.UTF8.GetBytes("204" + '\x01' + tokens[2] + '\x01' + tokens[3] + '\x01' + "no" + '\x01');

                try
                {
                    connectedClients[y].client.Send(send_data);
                }
                catch
                {
                    // 오류 발생하면 전송 취소하고 리스트에서 삭제한다.
                    try { connectedClients[y].client.Dispose(); }
                    catch { }
                    connectedClients.RemoveAt(y);
                }
            }
        }

        private void VSgame_Start(string[] tokens)
        {
            string tmp_id = tokens[2];
            string vs_id = tokens[3];
            int x = Search(vs_id);
            int y = Search(tmp_id);

            byte[] send_data = Encoding.UTF8.GetBytes("205" + '\x01' + tokens[3] + '\x01' + tokens[2] + '\x01');
            try
            {
                connectedClients[x].client.Send(send_data);
            }
            catch
            {
                // 오류 발생하면 전송 취소하고 리스트에서 삭제한다.
                try { connectedClients[x].client.Dispose(); }
                catch { }
                connectedClients.RemoveAt(x);
            }

            send_data = Encoding.UTF8.GetBytes("205" + '\x01' + tokens[2] + '\x01' + tokens[3] + '\x01');
            try
            {
                connectedClients[y].client.Send(send_data);
            }
            catch
            {
                // 오류 발생하면 전송 취소하고 리스트에서 삭제한다.
                try { connectedClients[y].client.Dispose(); }
                catch { }
                connectedClients.RemoveAt(y);
            }

        }
        private void VSgame_now(string[] tokens)
        {
            int x = Search(tokens[3]);

            byte[] send_data = Encoding.UTF8.GetBytes("302" + '\x01' + tokens[4] + '\x01');
            try
            {
                connectedClients[x].client.Send(send_data);

            }
            catch
            {
                // 오류 발생하면 전송 취소하고 리스트에서 삭제한다.
                try { connectedClients[x].client.Dispose(); }
                catch { }
                connectedClients.RemoveAt(x);
            }
        }

        private void VSgame_End(string[] tokens)
        {
            string vs_id = tokens[3];
            int x = Search(vs_id);
           
            byte[] send_data = Encoding.UTF8.GetBytes("301" + '\x01' + tokens[2] + '\x01');
            try
            {
                connectedClients[x].client.Send(send_data);

            }
            catch
            {
                // 오류 발생하면 전송 취소하고 리스트에서 삭제한다.
                try { connectedClients[x].client.Dispose(); }
                catch { }
                connectedClients.RemoveAt(x);
            }

            MConn = new MySqlConnection(strSql);
            MConn.Open();

            string sqlBofere = "SELECT ID, Password,Time, win, lose FROM user WHERE ID= '" + tokens[2] + "'";
            var Comm = new MySqlCommand(sqlBofere, MConn);
            MySqlDataReader myRead = Comm.ExecuteReader();

            if (myRead.HasRows)
            {

                if (myRead.Read())
                {
                    readID = myRead["ID"].ToString();
                    
                    readwin = myRead["win"].ToString();
                    readlose = myRead["lose"].ToString();
                    gameCountWin = (int.Parse(readwin) + 1).ToString();

                    MConn.Close();

                }
            }

            MConn = new MySqlConnection(strSql);
            MConn.Open();

            string updatesql = "UPDATE user SET win='" + gameCountWin + "' where ID='" + tokens[2] + "'";
            //string updatesql = "UPDATE user SET Time='" + readTime + "' where ID='" + clientData + "'";

            Comm = new MySqlCommand(updatesql, MConn);
            Comm.ExecuteNonQuery();
            MConn.Close();




            // 진사람

            MConn = new MySqlConnection(strSql);
            MConn.Open();

            string sqlBofere2 = "SELECT ID, Password,Time, win, lose FROM user WHERE ID= '" + tokens[2] + "'";
            Comm = new MySqlCommand(sqlBofere2, MConn);
            myRead = Comm.ExecuteReader();

            if (myRead.HasRows)
            {

                if (myRead.Read())
                {
                    readID = myRead["ID"].ToString();

                    readwin = myRead["win"].ToString();
                    readlose = myRead["lose"].ToString();
                    gameCountLose = (int.Parse(readlose) + 1).ToString();

                    MConn.Close();

                }
            }

            MConn = new MySqlConnection(strSql);
            MConn.Open();

            string updatesq2 = "UPDATE user SET lose='" + gameCountLose + "' where ID='" + tokens[3] + "'";
            //string updatesql = "UPDATE user SET Time='" + readTime + "' where ID='" + clientData + "'";

            Comm = new MySqlCommand(updatesq2, MConn);
            Comm.ExecuteNonQuery();
            MConn.Close();

        }

        private int Search(string id)
        {
            int x = 0; 
            for (int i = connectedClients.Count - 1; i >= 0; i--)
            {

                try
                {
                    if (id == connectedClients[i].getId())
                    {
                        x = i;
                        break;
                    }
                }
                catch
                {
                    // 오류 발생하면 전송 취소하고 리스트에서 삭제한다.
                    try { connectedClients[i].client.Dispose(); }
                    catch { }
                    connectedClients.RemoveAt(i);
                }
            }
            return x;
        }

        

    }
}
