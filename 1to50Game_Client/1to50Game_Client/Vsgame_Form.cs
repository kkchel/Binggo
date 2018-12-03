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


namespace _1to50Game_Client
{

    public partial class Vsgame_Form : Form
    {
        Socket mainSock;

        List<Button> button_list = new List<Button>();
        List<int> start_25 = new List<int>();
        DateTime startTime;
        int now = 1;
        int next = 26;
        int countDownNumber;

        string readTime;

        string id;
        string vs_id;


        public Vsgame_Form(Socket soc,string ID, string vs_id)
        {
            InitializeComponent();
            id = ID;
            this.vs_id = vs_id;

            mainSock = soc;
            AsyncObject obj = new AsyncObject(4096);

            obj.WorkingSocket = mainSock;
            mainSock.BeginReceive(obj.Buffer, 0, obj.BufferSize, 0, DataReceived, obj);

            button_list.Add(button1);
            button_list.Add(button2);
            button_list.Add(button3);
            button_list.Add(button4);
            button_list.Add(button5);
            button_list.Add(button6);
            button_list.Add(button7);
            button_list.Add(button8);
            button_list.Add(button9);
            button_list.Add(button10);
            button_list.Add(button11);
            button_list.Add(button12);
            button_list.Add(button13);
            button_list.Add(button14);
            button_list.Add(button15);
            button_list.Add(button16);
            button_list.Add(button17);
            button_list.Add(button18);
            button_list.Add(button19);
            button_list.Add(button20);
            button_list.Add(button21);
            button_list.Add(button22);
            button_list.Add(button23);
            button_list.Add(button24);
            button_list.Add(button25);

            for (int i = 0; i < 50; i++)
            {
                start_25.Add(i);
            }


            //Random r = new Random();
            for (int i = 24; i >= 0; i--)
            {
                button_list[i].Text = String.Format("00");

            }
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
              
                if (flag == "301")
                {
                    MessageBox.Show(tokens[1] + "의 승리");
                    this.Close();
                }
                else if (flag == "302")
                {
                    label2.Text = "상대방 : " + tokens[1];
                }

                    
                
                
            }
            catch
            {
            }
            obj.ClearBuffer();

            // 수신 대기
            obj.WorkingSocket.BeginReceive(obj.Buffer, 0, 4096, 0, DataReceived, obj);
        }

        private void VSGame_Form_Load(object sender, EventArgs e)
        {
            countDownNumber = 5;
            for (int i = 0; i < 25; i++)
            {
                button_list[i].Enabled = false;
            }

            countDownTimer.Start();

        }

        private void timer1_Tick(object sender, EventArgs e)
        {

            TimeSpan span = new TimeSpan(DateTime.Now.Ticks - startTime.Ticks);
            lblResult.Text = span.ToString("mm\\:ss\\.ff");

        }

        private void countDownTimer_Tick(object sender, EventArgs e)
        {
            if (countDownNumber >= 0)
            {
                label1.Text = countDownNumber.ToString();
                countDownNumber--;
            }
            else
            {
                label1.Font = new Font(label1.Font.Name, 20);

                countDownTimer.Stop();
                Random r = new Random();
                for (int i = 24; i > 0; i--)
                {
                    button_list[i].Enabled = true;

                    int x = r.Next() % i + 1;
                    button_list[i].Text = String.Format("{0}", start_25[x] + 1);
                    start_25.RemoveAt(x);

                }
                button_list[0].Enabled = true;
                button_list[0].Text = String.Format("{0}", start_25[0] + 1);
                start_25.RemoveAt(0);
                label1.Text = String.Format("지금 : {0}", now);
                myTimer.Enabled = !myTimer.Enabled;
                startTime = DateTime.Now;
            }
        }

        private void button_Click(object sender, EventArgs e)
        {

            for (int i = 0; i < 25; i++)
            {
                if (sender.Equals(button_list[i]) && int.Parse(button_list[i].Text) == now)
                {
                    if (now == 3)
                    {
                        //게임끝내서 서버에 데이터 보내기
                        Game_End();
                        return;
                    }
                    if (next > 50)
                    {
                        button_list[i].Enabled = false;

                    }
                    else
                    {
                        Random r = new Random();
                        int tmp = r.Next() % start_25.Count;
                        button_list[i].Text = String.Format("{0}", start_25[tmp] + 1);
                        start_25.RemoveAt(tmp);
                    }
                    now++;
                    next++;
                    label1.Text = String.Format("지금 : {0}", now);
                    byte[] allPID = Encoding.UTF8.GetBytes("103" + '\x01' + "3" + '\x01' + id + '\x01' + vs_id + '\x01' + now.ToString() + '\x01');
                    mainSock.Send(allPID);
                }
                else if (sender.Equals(button_list[i]) && int.Parse(button_list[i].Text) != now)
                {
                    for (int j = 0; j < 25; j++)
                        button_list[j].Visible = false;
                    
                    System.Threading.Thread.Sleep(1000);
                    
                    for (int j = 0; j < 25; j++)
                        button_list[j].Visible = true;
                }
            }
        }

        private void Game_End()
        {

            myTimer.Stop();
            
            byte[] allPID = Encoding.UTF8.GetBytes("103" + '\x01' + "7" + '\x01' + id + '\x01' + vs_id + '\x01' + id + '\x01');
            mainSock.Send(allPID);
            this.Close();

        }    
    }
}

