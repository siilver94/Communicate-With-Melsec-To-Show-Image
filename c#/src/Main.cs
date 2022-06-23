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
        public TCPClient_K mel;

        int NumOfImage = 4;

        public Main()
        {
            InitializeComponent();
        }

        //fffffffffffffffffffffff
        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void ShowImage(int value)        //이미지 출력
        {

            PictureBox[] picture = new PictureBox[]
            {
             pictureBox1,pictureBox2,pictureBox3,pictureBox4
            };
            
            for (int i = 0; i < NumOfImage; i++)
            {
                picture[i].Load(@"C:\Users\Vector\source\repos\GetSignalShowImage\" + (value + i) + ".jpg");
                picture[i].SizeMode = PictureBoxSizeMode.StretchImage;
            }

        }
        private void ChngBtn_Click(object sender, EventArgs e)   //체인지 버튼
        {
            i = 1;
            ShowImage(0);
            i += 4;
            if (i >= 6)
                i = 1;
        }


        TCPClient_K client;

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
                ShowImage(0);
            }

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
                {//데이터 보기
                    if (Convert.ToInt32(data2) == 1)
                    {
                        
                        ShowImage(2);
                    }

                    if (Convert.ToInt32(data2) == 2)
                    {
                        
                        ShowImage(3);
                    }
                }

            }));

        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            client.CommStop();
        }


    }
}
