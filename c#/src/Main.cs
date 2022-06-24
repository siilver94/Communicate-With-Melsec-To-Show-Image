using GetSignalShowImageF;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace GetSignalShowImage
{
    public partial class Main : Form
    {
        
        private delegate void dele();

        int currentImage = 1;
        int i;

        TcpClient tc;
        
        TCPClient_K client;

        int NumOfImage = 4;

        public Main()
        {
            InitializeComponent();
        }

        //fffffffffffffffffffffff
        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private static DateTime Delay(int MS)
        {
            DateTime ThisMoment = DateTime.Now;
            TimeSpan duration = new TimeSpan(0, 0, 0, 0, MS);
            DateTime AfterWards = ThisMoment.Add(duration);

            while (AfterWards >= ThisMoment)
            {
                System.Windows.Forms.Application.DoEvents();
                ThisMoment = DateTime.Now;
            }

            return DateTime.Now;
        }
        //private void ShowImage(int value)        //이미지 출력
        //{

        //    PictureBox[] picture = new PictureBox[]
        //    {
        //     pictureBox1,pictureBox2,pictureBox3,pictureBox4
        //    };
            
        //    for (int i = 0; i < NumOfImage; i++)
        //    {
        //        picture[i].Load(@"C:\Users\Vector\source\repos\GetSignalShowImage\" + (value + i) + ".jpg");
        //        picture[i].SizeMode = PictureBoxSizeMode.StretchImage;
        //    }

        //}
        private void ChngBtn_Click(object sender, EventArgs e)   //체인지 버튼
        {
            
          //  ShowImage(1);
                       
        }



        void ClientStart()
        {
            int port = Convert.ToInt32(textBox2.Text);

            client = new TCPClient_K(textBox1.Text, Convert.ToInt32(textBox2.Text), 1000, "192.168.100.5", 0);
            client.TalkingComm += client_TalkingComm;
            client.ConnectStart(0);

        }
        private void button1_Click(object sender, EventArgs e)  //접속
        {
            ClientStart();
            tc = new TcpClient(textBox1.Text, Convert.ToInt32(textBox2.Text));
            MessageBox.Show("접속되었습니다.");
            if (currentImage == 1)
            {
              // ShowImage(0);
            }

        }

        public static string ByteSubstring(string Data, int StartIndex, int ByteLength)
        {
            String str = "";

            byte[] TEMP = Encoding.Default.GetBytes(Data);

            str = Encoding.Default.GetString(TEMP, StartIndex, ByteLength);

            return str;
        }

        private void button2_Click(object sender, EventArgs e)  //접속 종료
        {
            tc.Dispose();
            MessageBox.Show("접속이 종료 되었습니다.");

        }

      

        void client_TalkingComm(string name, object data, string data2, string data3, string data4, string data5, string data6, string data7, string data8, string data9)  //데이터 받기
        {
            

            this.Invoke(new dele(() =>
            {

                //byte[] bt = (byte[])data;
                

                if (name.Equals("Image2"))
                {
                    if (Convert.ToInt32(data2) == 1)
                    {
                        
                     //  ShowImage(2);
                    }

                    if (Convert.ToInt32(data2) == 2)
                    {
                        
                     //   ShowImage(3);
                    }
                }

                if (name.Equals("BarCode"))
                {

                    string barcode1 = data2;
                    string barcode2 = data3;
                    string barcode3 = data4;
                    string barcode4 = data5;
                    string barcode5 = data6;
                   

                    string Wdata1 = data7;
                    string Wdata2 = data8;

                    string bridge = "";
                  
                    textBox5.Text = barcode1 + bridge + barcode2 + bridge + barcode3 + bridge + barcode4 + bridge + barcode5 + bridge;

                    //int code1 = Convert.ToInt32(Wdata1);
                    //int code2 = Convert.ToInt32(Wdata2);

                    //char Ccode1 = Convert.ToChar(Wdata1);
                    //char Ccode2 = Convert.ToChar(Wdata2);

                    int intWdata1 = Convert.ToInt32(Wdata1);
                    int intWdata2 = Convert.ToInt32(Wdata2);

                    string strWdata1 = Convert.ToString(intWdata1, 2);
                    string strWdata2 = Convert.ToString(intWdata2, 2);

                    //textBox6.Text = Convert.ToString(a, 2) + Convert.ToString(b, 2); ;

                    string sub1, sub2;

                    sub1 = ByteSubstring(strWdata1, 1, 9);
                    sub2 = ByteSubstring(strWdata2, 10, 9);

                    textBox6.Text = sub1.ToString();

                }

            }));

        }

        private void button3_Click(object sender, EventArgs e)  //D번지 데이터 보내기 쓰기
        {
            client.MCWrite_D(Convert.ToInt32(textBox4.Text), Convert.ToInt32(textBox3.Text));

        }

        private void button4_Click(object sender, EventArgs e)      //바코드 데이터 받기
        {
            client.MCWrite_D(1003, 1);
            Delay(500);
            client.MCWrite_D(1003, 0);
        }

        

        private void button5_Click(object sender, EventArgs e)      //데이터 + 바코드 합치기
        {
            string result;
            result = textBox6.Text + textBox5.Text;
            textBox7.Text = result;
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            client.CommStop();
        }
    }
}
