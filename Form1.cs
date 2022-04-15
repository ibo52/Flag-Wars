using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Threading;
using System.Net;
using System.Net.Sockets;

namespace CWG
{
    public partial class deneme : Form
    {
        private static Socket client;//connected player
        private static byte[] data = new byte[1024];//chat msg data
        //bayrakların konumları tutulacak,asenkron gelen mesajlarla dolacak
        int[,] rivalsFlagData = new int[,]{{-1,-1},{-1,-1},{ -1,-1},{ -1,-1},{ -1,-1} };//5'e 2'lik liste
        int[,] yourFlagData = new int[,] { { -1, -1 }, { -1, -1 }, { -1, -1 }, { -1, -1 }, { -1, -1 } };//5'e 2'lik liste


        private int player = 0;//0 if server ; 1 if client
        private int yourTurn = 0;//defines if player has right to make move

        private Boolean flagsPlaced=false;
        private int flagAreaWidth=50; //defines closure area of flags
        private int[] cursorOnwarMapPos =null;

        int rivalsRemainingFlags=5;
        Graphics g; //harita ekranına çizim yapmak için
        Pen p; //çizim aracı

        Bitmap[] imgList; //flags and other images list;

        public deneme()
        {
            InitializeComponent();

            g = warMap.CreateGraphics();
            p = new Pen(Color.Green,4);

            imgList = new Bitmap[4];
            Bitmap RED_FLAG = Properties.Resources.RED_FLAG;
            Bitmap BLUE_FLAG = Properties.Resources.BLUE_FLAG;

            Bitmap RED_OWNED= Properties.Resources.RED_OWNED;
            Bitmap BLUE_OWNED = Properties.Resources.BLUE_OWNED;

            RED_FLAG.MakeTransparent(RED_FLAG.GetPixel(0, 0));
            BLUE_FLAG.MakeTransparent(BLUE_FLAG.GetPixel(0, 0));
            RED_OWNED.MakeTransparent(RED_OWNED.GetPixel(0, 0));
            BLUE_OWNED.MakeTransparent(BLUE_OWNED.GetPixel(0, 0));

            imgList[0] = RED_FLAG;
            imgList[1] = BLUE_FLAG;
            imgList[2] = RED_OWNED;
            imgList[3] = BLUE_OWNED;
        }
        //server settings
        void ButtonListenOnClick(object obj, EventArgs ea)
        {
            results.Items.Add("Listening for a client...");
            Socket newsock = new Socket(AddressFamily.InterNetwork, SocketType.Stream,
            ProtocolType.Tcp);
            IPEndPoint iep = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 9050);
            newsock.Bind(iep);
            newsock.Listen(5);
            results.Items.Add(iep.Address+":"+iep.Port+" "+iep.AddressFamily);
            results.Items.Add("*-*setted as server.*-*");
            newsock.BeginAccept(new AsyncCallback(AcceptConn), newsock);
        }

        //client connection settings
        void ButtonConnectOnClick(object obj, EventArgs ea)
        {
            results.Items.Add("Connecting...");
            client = new Socket(AddressFamily.InterNetwork, SocketType.Stream,
            ProtocolType.Tcp);
            IPEndPoint iep = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 9050);
            client.BeginConnect(iep, new AsyncCallback(Connected), client);
        }

        //text send settings
        void ButtonSendOnClick(object obj, EventArgs ea)
        {
            if (client == null)
            {
                userInformText.ForeColor = Color.DarkRed;
                userInformText.BackColor = Color.AliceBlue;
                userInformText.Text = "No connection to send message!";
            }
            else
            {
                byte[] message = Encoding.ASCII.GetBytes(newText.Text);
                results.Items.Add("<you>:" + newText.Text);
                newText.Clear();
                client.BeginSend(message, 0, message.Length, 0,
                new AsyncCallback(SendData), client);
            }
        }
        void AcceptConn(IAsyncResult iar)
        {
            Socket oldserver = (Socket)iar.AsyncState;
            client = oldserver.EndAccept(iar);
            results.Items.Add("Connection from: " + client.RemoteEndPoint.ToString());
            Thread receiver = new Thread(new ThreadStart(ReceiveData));
            
            userInformText.Text = "Connection established.Game Starting.";

            player = 0;//means you are host

            results.Items.Add("--------------------------");
            receiver.Start();

            Thread.Sleep(1000);//wait 1s before game start
            placeFlags();
        }
        void Connected(IAsyncResult iar)
        {
            try
            {
                client.EndConnect(iar);
                results.Items.Add("Connected to: " + client.RemoteEndPoint.ToString());
                Thread receiver = new Thread(new ThreadStart(ReceiveData));
                receiver.Start();

                player = 1;//means you are client
                userInformText.Text = "Connection established.Game Starting.";
                results.Items.Add("--------------------------");

                Thread.Sleep(1000);//wait 1s before game start
                placeFlags();
            }
            catch (SocketException)
            {
                results.Items.Add("Error connecting");
            }
        }
        void SendData(IAsyncResult iar)
        {
            Socket remote = (Socket)iar.AsyncState;
            int sent = remote.EndSend(iar);
        }
        void ReceiveData()
        {
            int recv; //length of received data
            string stringData; // string of byte message
            int playerDataindex=0; //this is game variable,defines where incoming flag location to be placed(on list)
            while (true)
            {
                recv = client.Receive(data);
                stringData = Encoding.ASCII.GetString(data, 0, recv);

                if (stringData == "bye") {
                break;
                }
                //parse the incoming messages, thus we can determine what to do according to definitions
                String[] split = stringData.Split(new char[] { ':' },StringSplitOptions.None);

                //kullanıcı konum bilgisi gönderdiyse listeye al
                if (split[0] == "LOCATION")
                {
                    rivalsFlagData[playerDataindex, 0] = Convert.ToInt32(split[1]);
                    rivalsFlagData[playerDataindex, 1] = Convert.ToInt32(split[2]);
                    playerDataindex++;
                }
                
                //if client sends ready message to you,it means it placed the plags
                else if (split[0] == "PLACED")
                {
                    gameInfo.Items.Add(stringData);
                    if (player==0) //server have to right to make the first move on game start
                    {
                        userInformText.Text = "Sir,start the game by making first Attack!";
                        yourTurn = 1;
                    }
                }
                else if (split[0]== "FOUND")
                {
                    gameInfo.Items.Add("FOUND:RIVAL Found YOUR flag!");

                    int r_idx = (player == 0) ? 1 : 0;//düşman bayrağını seninkinin üstüne koy
                    int receivedIdx = Convert.ToInt32(split[1]);

                    g.DrawImage(imgList[r_idx+2],
                        yourFlagData[receivedIdx,0] - flagAreaWidth / 2, yourFlagData[receivedIdx, 1] - flagAreaWidth / 2, flagAreaWidth, flagAreaWidth);
                    g.DrawImage(imgList[r_idx],
                        yourFlagData[receivedIdx, 0] - 20, yourFlagData[receivedIdx, 1] - 50, 40, 50);

                    int incomingIdx = Convert.ToInt32(split[1]);
                    yourFlagData[incomingIdx,0] = -100;
                    yourFlagData[incomingIdx, 1] = -100;

                }
                //inform receives that we can make our move now
                else if (split[0]== "YOURTURN")
                {
                    gameInfo.Items.Add(split[1]);
                    yourTurn=1;
                    userInformText.ForeColor = Color.Cyan;
                    userInformText.BackColor = Color.DarkBlue;
                    userInformText.Text = "Sir, its your turn to Attack!";
                }
                //if this message received, you lose the game
                else if (split[0]== "GAMEOVER")
                {
                    //gameEnd = true;
                    //yourTurn = 0;
                    gameInfo.Items.Add("GAMEOVER:You lose the game");
                    userInformText.ForeColor= Color.Cyan;
                    userInformText.BackColor= Color.Red;
                    userInformText.Text = "Sir, We lose all of our flag. Game over";
                }

                //eğer gelen sıradan bir mesajsa(yani iki kişi sohbet ediyorsa)
                else
                {
                    results.Items.Add("<rival user>:" + stringData);
                }
            }
            //kapatma mesajını karşı tarafa da gönder o da kapansın
            stringData = "bye";
            byte[] message = Encoding.ASCII.GetBytes(stringData);
            client.Send(message);
            client.Close();
            results.Items.Add("Connection stopped");
            return;
        }

        //when pressed a location on map, return location info
        void Mouse_clicked(object obj, MouseEventArgs ea)
        {

            //firstly flags have to be placed
            if (!flagsPlaced)
            {
                //this variable defining where a flag to be placed, other function will acces to this data later
                cursorOnwarMapPos =new int[]{ ea.X,ea.Y };
            }

            //if flags placed, and it players turn to make move, let it be
            else if (yourTurn == 1)
            {
                switch (ea.Button)
                {
                    case MouseButtons.Left:
                        gameInfo.Items.Add("MOVE:player->"+player+" made its move:"+ ea.Location);
                        break;
                    default:
                        break;
                }

                browseForFlags(ea.X, ea.Y);
                userInformText.BackColor = Color.Aqua;
                userInformText.ForeColor = Color.SeaShell;
                userInformText.Text = "Waiting for other player to Make Move";
                yourTurn = 0;

                //check if game ended
                if (rivalsRemainingFlags <= 0)
                {
                    byte[] gameOverMsg = Encoding.ASCII.GetBytes("GAMEOVER:p" + player);
                    client.BeginSend(gameOverMsg, 0, gameOverMsg.Length, 0,
                new AsyncCallback(SendData), client);

                    userInformText.BackColor = Color.Green;
                    userInformText.ForeColor = Color.White;
                    userInformText.Text = "-*-Sir, We WON the game!-*-";
                }
                else
                {

                    //inform user to make her/his move
                    byte[] locationMsg = Encoding.ASCII.GetBytes("YOURTURN:player->" + player + " made its move {" + ea.X + "," + ea.Y + "}");
                    client.BeginSend(locationMsg, 0, locationMsg.Length, 0,
                new AsyncCallback(SendData), client);
                }
            }
        }

        //browses for flags in a certain area according the given coordinates
        void browseForFlags(int x, int y)
        {
            //flagAreaWidth aranacak alan büyüklüğünü tanımlıyor unutma

            for (int i = 0; i < rivalsFlagData.GetLength(0); i++)
            {
                int flagX = rivalsFlagData[i, 0];
                int flagY = rivalsFlagData[i, 1];

                // do not find the flags that found before
                if (flagX == -100 && flagY == -100)
                {
                    continue;
                }
                //results.Items.Add(x+","+y+ " suraya bakılıyor:"+flagX+ ", "+ flagY);
                //calculating if flag is in certain area
                if (flagX - flagAreaWidth <= x && x <= flagX + flagAreaWidth)
                {
                    if (flagY - flagAreaWidth <= y && y <= flagY + flagAreaWidth)
                    {
                        gameInfo.Items.Add("FOUND:YOU Found one of Rival's flag");
                        rivalsFlagData[i, 0] = -100;
                        rivalsFlagData[i, 1] = -100;
                        int r_idx = (player == 0) ? 1:0;//düşman bayrağını seninkinin üstüne koymak için

                        g.DrawImage(imgList[player+2], 
                            flagX - flagAreaWidth/2,flagY - flagAreaWidth/2, flagAreaWidth,flagAreaWidth);
                        g.DrawImage(imgList[player],
                            flagX - 20, flagY - 50, 40, 50);

                        rivalsRemainingFlags--;

                        //send information to the client
                        //Thread.Sleep(300);
                        byte[] locationMsg = Encoding.ASCII.GetBytes("FOUND:" + i);
                        client.BeginSend(locationMsg, 0, locationMsg.Length, 0,
                    new AsyncCallback(SendData), client);
                        break;
                    }
                }


            }

        }

        //when user connected; start this as initial
        void placeFlags()
        {
            int numberToPlace = 5;
            userInformText.ForeColor = Color.Green;

                for (int i = 0; i < numberToPlace; i++)
                {

                    userInformText.Text = "Mark " + (i + 1) + "th Land to place flag:";
                    while (cursorOnwarMapPos == null)
                    {
                        Thread.Sleep(300);//while user didnt click on map sleep 400 ms
                    }
                    yourFlagData[i, 0] = cursorOnwarMapPos[0];
                    yourFlagData[i, 1] = cursorOnwarMapPos[1];
                    g.DrawRectangle(p, cursorOnwarMapPos[0]-flagAreaWidth/2
                        , cursorOnwarMapPos[1]-flagAreaWidth/2, flagAreaWidth,flagAreaWidth);

                g.DrawImage(imgList[player], cursorOnwarMapPos[0]-20, cursorOnwarMapPos[1]-50,40,50);

                    //send your placed flag data to client
                    byte[] locationMsg = Encoding.ASCII.GetBytes("LOCATION:" + cursorOnwarMapPos[0] + ":" + cursorOnwarMapPos[1]);
                    client.BeginSend(locationMsg, 0, locationMsg.Length, 0,
                new AsyncCallback(SendData), client);

                    cursorOnwarMapPos = null;
                }
            
            flagsPlaced = true;
            
            gameInfo.Items.Add("PLACED: YOU placed your flags!");

            //setting the user info text
            userInformText.ForeColor =Color.Black;
            userInformText.Text = "Flags placed sir. We are ready!";

            //send info (that you are redy) to client
            byte[] message = Encoding.ASCII.GetBytes("PLACED: RIVAL placed the flags!");
            client.BeginSend(message, 0, message.Length, 0,
            new AsyncCallback(SendData), client);

        }
    }
}
