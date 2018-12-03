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
using System.Text.RegularExpressions;
using MySql.Data.MySqlClient;
using System.Drawing.Drawing2D;



namespace _1to50Game_Client
{
    public partial class Game_Form : Form
    {
        //변수
        Socket mainSock;
        //Login_Form FormConnector;
        DateTime startTime;
        int countDownNumber;
        string PlayerID;
        int now = 1;
        int next = 26;
        int next2 = 10;
        int next3 = 2;

        int button_num = 25;
        int Last = 50;

        int x = 5;
        int y = 5;

        List<int> start_num = new List<int>();
        List<int> start_10 = new List<int>();
        List<Button> button_list = new List<Button>();

        string ID;
        string readTime;


        public Game_Form(Socket soc, string ID)
        {
            InitializeComponent();
            mainSock = soc;
            this.ID = ID;
            AsyncObject obj = new AsyncObject(4096);

            obj.WorkingSocket = mainSock;
            mainSock.BeginReceive(obj.Buffer, 0, obj.BufferSize, 0, DataReceived, obj);


            this.Text = "플레이어 " + ID;
        }

        public Game_Form(Socket soc, string ID, int x, int y, int size)
        {
            InitializeComponent();
            Last = size;
            mainSock = soc;
            this.x = x;
            this.y = y;
            next = x * y + 1;
            this.ID = ID;
            AsyncObject obj = new AsyncObject(4096);

            obj.WorkingSocket = mainSock;
            mainSock.BeginReceive(obj.Buffer, 0, obj.BufferSize, 0, DataReceived, obj);

            Mode1.Visible = false;
            Mode3.Visible = false;
            Mode5.Visible = false;
            this.Text = "플레이어 " + ID;
            
        }




        private void Game_Over_Click(object sender, EventArgs e)
        {
            this.Close();

        }

        private void Game_Form_Load(object sender, EventArgs e)
        {
            countDownNumber = 3;
           

            //////버튼 배열추가 수정할것/////
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

            ////////////////////////////////////
            for (int i = 0; i < y; i++)
                for (int j = 0; j < x; j++)
                {
                    button_list[i * x + j].Location = new Point(30 + i * 80, 80 + j * 80);
                    button_list[i * x + j].Width = 70;
                    button_list[i * x + j].Height = 70;
                }
            for (int i = x * y; i < button_num; i++)
                button_list[i].Visible = false;


            for (int i = 0; i < button_num; i++)
            {
                button_list[i].Enabled = false;
            }

            for (int i = 0; i < Last; i++)
            {
                start_num.Add(i);
            }


            for (int i = 0; i < 10; i++)
            {
                start_10.Add(i);
            }

            //Random r = new Random();
            for (int i = button_num - 1; i >= 0; i--)
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
            }
            catch
            {
            }
        }


        private void myTimer_Tick(object sender, EventArgs e)
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
                if (label1.Text == "0")
                {
                    ////////////////////////////////////////////////////////////
                    if (Mode5.Checked == true)
                    {
                        countDownTimer.Stop();
                        Random r = new Random();
                        for (int i = x * y - 1; i > 0; i--)
                        {
                            button_list[i].Enabled = true;

                            int set = r.Next() % i+1;
                            button_list[i].Text = String.Format("{0}", start_num[set] + 1);
                            start_num.RemoveAt(set);

                        }
                        button_list[0].Enabled = true;
                        button_list[0].Text = String.Format("{0}", start_num[0] + 1);
                        start_num.RemoveAt(0);
                        label1.Text = String.Format("지금 : {0}", now);
                        myTimer.Enabled = !myTimer.Enabled;
                        startTime = DateTime.Now;
                    }
                    else if(Mode3.Checked == true)
                    {
                        //3X3 모드 추가
                        Mode3_Check();
                        countDownTimer.Stop();
                        Random r = new Random();
                        for (int i = 8; i > 0; i--)
                        {
                            button_list[i].Enabled = true;

                            int x = r.Next() % i;
                            button_list[i].Text = String.Format("{0}", start_10[x] + 1);
                            start_10.RemoveAt(x);

                        }
                        button_list[0].Enabled = true;
                        button_list[0].Text = String.Format("{0}", start_10[0] + 1);
                        label1.Text = String.Format("지금 : {0}", now);
                        myTimer.Enabled = !myTimer.Enabled;
                        startTime = DateTime.Now;

                    }
                    else if (Mode1.Checked == true)
                    {
                        countDownTimer.Stop();
                        this.button1.Location = new System.Drawing.Point(142, 120);
                        this.button1.Size = new System.Drawing.Size(195, 177);
                        button_list[0].Enabled = true;
                        button_list[0].Text = String.Format("{0}", start_num[0] + 1);
                        label1.Text = String.Format("지금 : {0}", now);
                        myTimer.Enabled = !myTimer.Enabled;
                        startTime = DateTime.Now;
                        for (int i = 1; i < 25; i++)
                            button_list[i].Visible = false;
                    }


                }
            }
        }


        private void button_Click(object sender, EventArgs e)
        {
            if (Mode5.Checked == true)
            {
                for (int i = 0; i < button_num; i++)
                {
                    if (sender.Equals(button_list[i]) && int.Parse(button_list[i].Text) == now)
                    {
                        if (now == 50)
                        {
                            //게임끝내서 서버에 데이터 보내기
                            Game_Over.Enabled = true;
                            Game_End();
                            return;
                        }
                        if (next > Last)
                        {
                            button_list[i].Enabled = false;

                        }
                        else
                        {
                            Random r = new Random();
                            int tmp = r.Next() % start_num.Count;
                            button_list[i].Text = String.Format("{0}", start_num[tmp]+1);
                            start_num.RemoveAt(tmp);
                        }
                        now++;
                        next++;
                        label1.Text = String.Format("지금 : {0}", now);
                    }
                }
            }
            else if (Mode3.Checked == true)
            {
                for (int i = 0; i < 10; i++)
                {
                    if (sender.Equals(button_list[i]) && int.Parse(button_list[i].Text) == now)
                    {
                        if (now == Last)
                        {
                            //게임끝내서 서버에 데이터 보내기
                            Game_Over.Enabled = true;
                            Game_End();
                            return;
                        }
                        if (next2 > Last)
                        {
                            button_list[i].Enabled = false;
                        }
                        else
                        {
                            button_list[i].Text = String.Format("{0}", next2);
                        }
                        now++;
                        next2++;
                        label1.Text = String.Format("지금 : {0}", now);

                    }
                }
            }
            else if (Mode1.Checked == true)
            {
                if (now == Last)
                {
                    //게임끝내서 서버에 데이터 보내기
                    Game_Over.Enabled = true;
                    Game_End();
                    return;
                }
                
                
                    button_list[0].Text = String.Format("{0}", next3);
                
                now++;
                next3++;
                label1.Text = String.Format("지금 : {0}", now);
            }
        }


        private void Game_End()   //클리어 시간 인자로 추가할것
        {

            myTimer.Stop();
            readTime = lblResult.Text;
            if (Mode3.Visible)
            {
                if (Mode3.Checked == true)
                {
                    byte[] allPID = Encoding.UTF8.GetBytes("100" + '\x01' + ID + '\x01' + readTime + '\x01' + "3X3" + '\x01');

                    mainSock.Send(allPID);
                }
                else if (Mode5.Checked == true)
                {
                    byte[] allPID = Encoding.UTF8.GetBytes("100" + '\x01' + ID + '\x01' + readTime + '\x01' + "5X5" + '\x01');

                    mainSock.Send(allPID);
                }
                else if (Mode1.Checked == true)
                {
                    byte[] allPID = Encoding.UTF8.GetBytes("100" + '\x01' + ID + '\x01' + readTime + '\x01' + "1X1" + '\x01');

                    mainSock.Send(allPID);
                }
            }

        }



        private void button26_Click(object sender, EventArgs e)
        {
            countDownTimer.Start();

            string str = "플레이어 " + PlayerID + "님께서 게임을 시작했습니다.";
            byte[] sPID = Encoding.UTF8.GetBytes("00" + '\x01' + str);
            mainSock.Send(sPID);

            GameStart.Enabled = false;
            Game_Over.Enabled = false;
            Mode3.Enabled = false;
        }




        private void Mode3_Check()
        {
            if (Mode3.Checked == true)
            {
                button1.Location = new Point(30, 80);
                button1.Height = 90;
                button1.Width = 90;

                button2.Location = new Point(136, 80);
                button2.Height = 90;
                button2.Width = 90;

                button3.Location = new Point(242, 80);
                button3.Height = 90;
                button3.Width = 90;

                button4.Location = new Point(30, 186);
                button4.Height = 90;
                button4.Width = 90;

                button5.Location = new Point(136, 186);
                button5.Height = 90;
                button5.Width = 90;

                button6.Location = new Point(242, 186);
                button6.Height = 90;
                button6.Width = 90;

                button7.Location = new Point(30, 295);
                button7.Height = 90;
                button7.Width = 90;

                button8.Location = new Point(136, 295);
                button8.Height = 90;
                button8.Width = 90;

                button9.Location = new Point(242, 295);
                button9.Height = 90;
                button9.Width = 90;

                for (int i = 9; i < 25; i++)
                {
                    button_list[i].Visible = false;
                }
            }
            
            
        }








    }

}
