using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Sockets;/*for socket.*/
using System.Net;
using System.Threading;/*jitne bi clients aain ge, server har aik client ko aik alag thread me manage kare ga.*/
namespace P2P_Server
{
    public partial class Form1 : Form
    {
        class CLIENT
        {
            public Socket Connector = null;
            /*har aik client ki apni aik socket (binded) create hogi, jo k us client ko mere
              sath (isi server k sath) connect kare gi.
              aur ye socket 'Connector' us client ki us socket ka address apne ander le le gi, taake server
              ko us socket per data send/receive kar sake.*/
            public String IP = "";
            public int Port = -1;/*initially hamare paas is client ka ip, aur port_no saved nai he.*/
        }
        Socket melistener = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        int mePort = 7777;
        String meIP;

        private List<CLIENT> clients = new List<CLIENT>();
        /*server k paas clients ki list hogi, k kon kon se clients mere sath connected hein?
          initially, sirf list create hui he, lekin koi bi client is list me add nai he.*/
        private String getLocalIPAddress(){
            /*this function returns the IP address of this system.*/
            IPHostEntry host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (IPAddress ip in host.AddressList){
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                    return ip.ToString();
            }
            return "127.0.0.1";/*if there above is nothing to return, means that there is no
                               internet connection, so return this ip, means this is a local ip address.*/
        }
        public Form1(){
            InitializeComponent();
            meIP = getLocalIPAddress();
            CheckForIllegalCrossThreadCalls = false;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            /*Turn ON the server.*/
            try
            {
                melistener.Bind(new IPEndPoint(IPAddress.Parse(meIP), mePort));
                /*is server ka ip:port set kar k ham socket bana rahe hein (bind/create kar rahe hein), taake future
                  me koi client is ip, aur port per aa kar connect ho sake.*/
                ServerIP.Text = "Server IP Address : " + meIP;
                ServerPort.Text = "Server Port # : " + mePort;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);/*agar ham aik se ziada server ka instances on kar lein, tu aik se ziada servers
                                              k same port_no nai ho sakte, isi liay error aaya he.*/
                this.Close();/*close this 2nd server.*/
                return;
            }
            startThread(() => continuouslyListeningNewClients());
            /*server ON hona k baad backend per server continuously dekhta rahe ga, k kya mera koi new peer client tu nai aaya?*/
            /*listbox shows the list of clients, and all notifications.*/
            listBox1.Items.Add("Server is Turned ON");
            button1.Enabled = false;
            button1.Text = "TURNED ON";
        }
        private void continuouslyListeningNewClients(){
            /*is function me sirf ye ho rha he, k jab bi koi new client aae ga, tu server us client ko list me add kar de ga, aur
              us k liay aik alag se thread bana de ga, aur phir dubara wait krna shuru kar de ga, new clients k arrive hone ka.*/
            while (true){
                melistener.Listen(0);/*setting that only 1 new client can come in one time.*/
                Socket temp = melistener.Accept();/*continuously waiting for new client to come, and when new client requests for connection
                                           to this server, then accept connection, aur new client ki socket ko get kia he, taake server us
                                           new client ki socket per us client ko message kar sake.*/
                CLIENT newClient = new CLIENT();
                newClient.Connector = temp;
                temp = null;
                clients.Add(newClient);/*appending new client to my list.*/
                startThread(() => continuouslyReceivingMessagesFromThisClient(newClient));
            }
        }
        String getActualMessage(byte[] buffer, int msgLength){
            char[] chars = new char[msgLength];
            Encoding.Default.GetDecoder().GetChars(buffer, 0, msgLength, chars, 0);
            String message = new String(chars);
            message = message.Replace("\0", "");
            return message;
        }
        private void continuouslyReceivingMessagesFromThisClient(CLIENT thisClient){
            /*this function always waits for thisclient to receive some message.*/
            while (true){
                try{
                    byte[] buffer = new byte[1500];/*1500 k packets me data receive kia jae ga.*/
                    int msgLength = thisClient.Connector.Receive(buffer, 0, buffer.Length, 0);
                    /*this 'Receive' function waits for some message from 'thisClient', and when some message arrives,
                      then get that message in buffer, and returns the length of the msg that comes in this server
                      from 'thisClient'.
                      note that agar koi packet empty size ka aae, tu ye function return nai karta, ye function tab hi
                      return karta he, jab koi packet ka koi na koi size ho, yani k packet me koi na koi data ho.*/
                    String message = getActualMessage(buffer, msgLength);
                    listBox1.Items.Add(message);
                    if (message.Contains("My address is:")){
                        /*'thisClient' sends its IP,port to server.
                          tu server ko ab 'thisClient' ki ip, port apne paas save kar leni chahiye.*/
                        String extractedMsg = message.Substring(message.IndexOf(':') + 1);
                        thisClient.IP = extractedMsg.Substring(0, extractedMsg.IndexOf("_"));/*saving IP*/
                        thisClient.Port = Convert.ToInt32(extractedMsg.Substring(extractedMsg.IndexOf('_') + 1));/*saving Port*/
                        listBox1.Items.Add("New Client added, IP:" + thisClient.IP + ", and Port:" + thisClient.Port);
                    }
                    else if (message == "Send me all available clients"){
                        /*'thisClient' asks for available clients on the network from this server.*/
                        String AllAvailableClients = "all available clients:";/*server send this message about all available clients on the network.*/
                        for (int i = 0; i < clients.Count(); i++){
                            if (clients[i] == thisClient)
                                continue;/*isi 'thisClient' ko isi ka apna ip:port nai send karein ge.*/
                            AllAvailableClients += clients[i].IP + ":" + clients[i].Port + "_";/*'_' differ 2 clients.*/
                        }
                        /*server is replying the list of all available clients to 'thisClient'*/
                        SendMessageToClient(AllAvailableClients, thisClient.Connector);
                    }
                    else{
                        /*client ki taraf se aae normal msg ka mtlab he, k client ye msg group me send krna chahta he, tu 
                          server sare group me (sare clients ko) send kare ga.*/
                        for (int i = 0; i < clients.Count(); i++){
                            if (clients[i] == thisClient)
                                continue;/*isi client1 ko msg group me dubara send nai krna.*/
                            String msgToSendInGroup = message;
                            SendMessageToClient(thisClient.Port + " : " + msgToSendInGroup, clients[i].Connector);/*msg to deliver in group.*/
                        }
                    }
                }
                catch (Exception ex)
                {
                    listBox1.Items.Add(thisClient.Port + " client left. " + ex.Message);
                    clients.Remove(thisClient);
                    return;/*no more this client alive, so we close accpting things from it.*/
                }
            }
        }
        private void SendMessageToClient(String msg, Socket destinClient){
            /*this function sends 'msg' to this 'destinClient'.
             msg ko UTF8 format me encode kar k byte[] me convert kar k client ko send krna he.*/
            destinClient.Send(Encoding.Default.GetBytes(msg));
        }
        private void startThread(ThreadStart s1){
            Thread newThread = new Thread(s1);
            newThread.IsBackground = true;
            newThread.Start();
        }
        private void button2_Click(object sender, EventArgs e){
            /*clear all irrelevant notifications, and only show the current connected peers.*/
            listBox1.Items.Clear();
            for (int i = 0; i < clients.Count(); i++){
                if (i == 0)
                    listBox1.Items.Add("Current connected clients info :");
                listBox1.Items.Add("IP:" + clients[i].IP + ", and Port:" + clients[i].Port);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void ServerIP_Click(object sender, EventArgs e)
        {

        }

        private void ServerPort_Click(object sender, EventArgs e)
        {

        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}
