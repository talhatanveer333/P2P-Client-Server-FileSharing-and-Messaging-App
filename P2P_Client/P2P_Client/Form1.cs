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
using System.Net;/*for IPEndPoint.*/
using System.Threading;
using System.IO;

namespace P2P_Client
{
    public partial class Form1 : Form
    {
        class peerClient
        {
            public String IP = "";
            public int Port = -1;
            public Socket Connector = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp); /*another socket to connect to peer.*/
            public String message = "";/*muje is peer ki taraf se jo message aaya he, wo is String me save hoga.*/
            public String sendStatus = "";
            /*""  = I am in idle state.
              "*" = Waiting for file from destin peer.
              "1" = sending file to destin peer.
             */
            public String receiveStatus = "";/*
                                       ""  =  I am in idle state.
                                       "#" =  agar me peer se koi file download krna chah raha hu, tu is case me muje notification
                                              ka wait bi krna pare ga, tu is case me me "#" wali state bi add karu ga (See definition
                                              of 'Download File' button.
                                       "*" =  peer ki taraf se notification aaya he, aur peer ne aik file ka name send kia he, tu
                                              wo pooch raha he, k me ye file send kar du?, tu notification aane per me ye status
                                              "*" kar du ga.
                                       "1" =  agar us file ko me ne accept kar lia he, tu me is peer ko +ve ack send karu ga, k
                                              Ha, ap muje ye file send kar do, aur mera receiving File status "1" ho jae ga,
                                              yani k me ab file k aane ka wait karu ga, aur jese hi file aae gi, tu me is peer ka
                                     
                                       note that lekin agar us file ko me reject kar du ga, tu me is peer ko -ve ack send karu ga,
                                       k muje ye file nai chahiye, tu mera receiving File status "" ho jae ga, yani k me file k
                                       aane ka koi wait nai karu ga, aur normal mesages receive krne k status "" per chala jau ga.
                                        */
        }
        Socket Listener = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        public String meIP;
        public int mePort = -1;
        public bool IsSocketBound = false;
        public String Folder = "";
        String srvrDIP;
        int srvrPort = 7777;
        Socket srvrConnector = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        private List<peerClient> peer = new List<peerClient>();/*only the empty list. No peer is added.*/
        public Form1()
        {
            InitializeComponent();
        }
        private String getLocalIPAddress(){
            /*chunke ye app aik hi system k ander he, tu is ka apna hi ip address ham lein ge.*/
            IPHostEntry host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (IPAddress ip in host.AddressList){
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                    return ip.ToString();
            }
            return "127.0.0.1";/*if there above is nothing to return, then return this ip, means
                                that this is also a local ip address.*/
        }
        private void Form1_Load(object sender, EventArgs e){
            srvrDIP = getLocalIPAddress();
            destinPortNo.Enabled = false;
            sendBtn.Enabled = false;
            downloadBtn.Enabled = false;
            CheckForIllegalCrossThreadCalls = false;
            srvrIP.Text = srvrDIP;
            srvrPortNo.Text = srvrPort.ToString();
            srvrPortNo.Enabled = false;
        }
        public int getFreePort(){
            /*this function returns the free available port in the system.*/
            TcpListener l = new TcpListener(IPAddress.Loopback, 0);
            l.Start();
            int port = ((IPEndPoint)l.LocalEndpoint).Port;
            l.Stop();
            return port;
        }
        private void ConnectToServer(){
            try{
                srvrDIP = srvrIP.Text;
                srvrConnector.Connect(new IPEndPoint(IPAddress.Parse(srvrDIP), srvrPort));
                listBox1.Items.Add("Successfully connected to server.");
                srvrIP.Enabled = false;
                Thread newThread = new Thread(() => continuouslyReceivingMessagesFromServer());
                newThread.IsBackground = true;
                newThread.Start();
                Thread newThread2 = new Thread(() => continuouslyListeningNewPeers());
                newThread2.IsBackground = true;
                newThread2.Start();
                Thread.Sleep(200);
                srvrConnector.Send(Encoding.Default.GetBytes("My address is:" + meIP + "_" + mePort));
                button1.Text = "Get clients";
                destinPortNo.Enabled = true;
            }
            catch(Exception ex) { MessageBox.Show(ex.Message); }
            button1.Enabled = true;
        }
        private void setConnectedToDestinPeerState(){
            button1.Enabled = false;
            destinPortNo.Enabled = false;
            sendBtn.Enabled = true;
            downloadBtn.Enabled = true;
        }
        private void ConnectToDestinPeer(){
            try{
                /*destination peer k sath directly connect krna he.*/
                String destinationPeer = destinPortNo.Text;
                peerClient newPeer = new peerClient();
                /*separating destination IP, and port to connect to destination peer directly.*/
                newPeer.IP = destinationPeer.Substring(0, destinationPeer.IndexOf(":"));
                newPeer.Port = Convert.ToInt32(destinationPeer.Substring(destinationPeer.IndexOf(':') + 1));
                for (int i = 0; i < peer.Count(); i++){
                    if (peer[i].IP == newPeer.IP && peer[i].Port == newPeer.Port){
                        /*I am already connected to destin. peer, tu me dobara is k sath
                        connection establish nai karu ga.*/
                        listBox1.Items.Add("Already Connected to peer " + newPeer.Port);
                        setConnectedToDestinPeerState();
                        destinPeer.SelectedIndex = destinPeer.FindStringExact(newPeer.Port.ToString());
                        newPeer = null;
                        return;
                    }
                }
                listBox1.Items.Add("Connecting to peer:" + newPeer.Port);
                newPeer.Connector.Connect(new IPEndPoint(IPAddress.Parse(newPeer.IP), newPeer.Port));
                peer.Add(newPeer);/*adding a new peer in the peers list.*/
                listBox1.Items.Add("Connected to the peer : " + newPeer.Port);
                setConnectedToDestinPeerState();
                destinPeer.Items.Add(newPeer.Port);
                destinPeer.SelectedIndex = destinPeer.FindStringExact(newPeer.Port.ToString());
                /*now receiving messages from this peer continuously in this thread.*/
                Thread newThread2 = new Thread(() => continuouslyReceivingMessagesFromThisPeer(newPeer));
                newThread2.IsBackground = true;
                newThread2.Start();
                newPeer.Connector.Send(Encoding.Default.GetBytes("My address is:" + meIP + "_" + mePort));
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }
        private void button1_Click(object sender, EventArgs e){
            if (button1.Text == "Connect to server"){
                /*ye client pehle apna socket (ip:port) bind kare ga, aur phir server k sath connect hoga.*/
                if (!IsSocketBound){
                    meIP = getLocalIPAddress();
                    mePort = getFreePort();
                    Listener.Bind(new IPEndPoint(IPAddress.Parse(meIP), mePort));
                    listBox1.Items.Add("Successfully, your socket is bound with ip:" + meIP + ", and port:" + mePort);
                    this.Text = "Client :  " + meIP + " _ " + mePort.ToString();
                    Folder = "Peer " + mePort;
                    Directory.CreateDirectory(Folder);
                    Folder = "Peer " + mePort + "\\p2p-files";
                    Directory.CreateDirectory(Folder);
                    IsSocketBound = true;
                }
                ConnectToServer();
            }
            else if (button1.Text == "Get clients"){
                destinPortNo.Items.Clear();
                srvrConnector.Send(Encoding.Default.GetBytes("Send me all available clients"));
                listBox1.Items.Add("Requesting available clients from server.");
                Thread.Sleep(50);/*kuch deir tak be sure he, k sare latest available clients server ne reply kar diay hun ge.*/
            }
            else/*Connect to peer*/
                ConnectToDestinPeer();
        }
        private void continuouslyListeningNewPeers(){
            while (true){
                try{
                    Listener.Listen(0);/*setting that only 1 new client can come in one time.*/
                    Socket temp = Listener.Accept();
                    listBox1.Items.Add("New peer connected.");
                    peerClient newPeer = new peerClient();
                    newPeer.Connector = temp;
                    temp = null;
                    peer.Add(newPeer);
                    Thread newThread2 = new Thread(() => continuouslyReceivingMessagesFromThisPeer(newPeer));
                    newThread2.IsBackground = true;
                    newThread2.Start();
                }
                catch (Exception ex) { MessageBox.Show(ex.Message); }
            }
        }
        String getActualMessage(byte[] buffer, int msgLength){
            char[] chars = new char[msgLength];
            Encoding.Default.GetDecoder().GetChars(buffer, 0, msgLength, chars, 0);
            String message = new String(chars);
            message = message.Replace("\0", "");
            return message;
        }
        void sendFileToPeerIfItAAccepts(peerClient p1, String sourcefileCompleteNameWithPath){
            sendBtn.Enabled = false;
            downloadBtn.Enabled = false;
            p1.sendStatus = "*";
            while (p1.sendStatus == "*") ;
            if (p1.sendStatus == "1"){
                /*this peer accepts the file, and I will send the file to it.*/
                p1.Connector.SendFile(sourcefileCompleteNameWithPath);
                Thread.Sleep(75);
                p1.Connector.Send(Encoding.Default.GetBytes("file completed"));
                listBox1.Items.Add("File sent successfully");
            }
            else/*p1.sendStatus == ""*/
                listBox1.Items.Add("File is rejected by peer " + p1.Port);
            p1.sendStatus = "";
            sendBtn.Enabled = true;/*ab me koi aur file bi send kar sakta hu.*/
            downloadBtn.Enabled = true;
        }
        private void receiveFileFromPeer(peerClient thisPeer, String fileName){
            String completePathOfFile = Folder + "\\" + fileName;
            if (File.Exists(completePathOfFile))
                File.Delete(completePathOfFile);
            /*creating new file.*/
            Stream dest = File.OpenWrite(completePathOfFile);/*ye received file is client k apne folder me save hogi.*/
            listBox1.Items.Add("receiving file '" + fileName + "' from the peer " + thisPeer.Port + "...");
            while (true){
                byte[] buffer = new byte[1500000];
                int msgLength = thisPeer.Connector.Receive(buffer, 0, buffer.Length, 0);
                if (getActualMessage(buffer, msgLength) == "file completed"){
                    listBox1.Items.Add("File received successfully, and saved in your folder.");
                    dest.Close();/*file completely received, so closes the file so that file will be completely received.
                                   aur phir is peer se normal messages receive krne shuru kar dein ge.*/
                    thisPeer.receiveStatus = "";/*ab dubara normal messages receive karein ge.*/
                    return;/*returning from this function to normally receiving messages state from this peer.*/
                }
                char[] chars = new char[msgLength];
                Encoding.Default.GetDecoder().GetChars(buffer, 0, msgLength, chars, 0);
                dest.Write(buffer, 0, msgLength);/*buffer me se 0th index se msgLength tak ka sara data 'dest' ki current
                                                   position (end) per write (append) kar dena he.*/
            }
        }
        private void sendBtn_Click(object sender, EventArgs e){
            /*select file to send (upload) to destin peer.*/
            try{
                OpenFileDialog dlg = new OpenFileDialog();
                dlg.Filter = "All Files (*.*)|*.*";
                dlg.Title = "Select the file to send";
                if (dlg.ShowDialog() == DialogResult.OK){
                    for (int i = 0; i < peer.Count(); i++){
                        if (peer[i].Port == Convert.ToInt32(destinPeer.Text)){
                            String sourcefileCompleteNameWithPath = dlg.FileName.ToString();
                            String fileName = Path.GetFileName(sourcefileCompleteNameWithPath);
                            String fileSizeInBytes = new FileInfo(sourcefileCompleteNameWithPath).Length.ToString();
                            peer[i].Connector.Send(Encoding.Default.GetBytes("Can I send this file to you:" + fileName));
                            /*asking destin peer that Can I send you this file, having size 'fileSizeInBytes' bytes, and waiting for ack.*/
                            listBox1.Items.Add("Sending this file '" + fileName + "' to peer : " + peer[i].Port + "...");
                            Thread newThread2 = new Thread(() => sendFileToPeerIfItAAccepts(peer[i], sourcefileCompleteNameWithPath)); 
                            newThread2.IsBackground = true;
                            newThread2.Start();
                            break;
                        }
                    }
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }
        private void continuouslyReceivingMessagesFromThisPeer(peerClient thisPeer){
            while (true){
                try{
                    byte[] buffer = new byte[1500];/*maximum aik packet ka size 1500 hoga.*/
                    int msgLength = thisPeer.Connector.Receive(buffer, 0, buffer.Length, 0);
                    thisPeer.message = getActualMessage(buffer, msgLength);
                    if (thisPeer.message.Contains("I need this file from you:")){
                        /*this peer is requesting for some file from me.*/
                        String fileName = thisPeer.message.Substring(thisPeer.message.IndexOf(':') + 1);
                        if (File.Exists(Folder + "\\" + fileName)){
                            thisPeer.Connector.Send(Encoding.Default.GetBytes("Ok, I am gonna sending you this file:" + fileName));
                            /*asking destin peer that Can I send you this file, having size 'fileSizeInBytes' bytes, and waiting for ack.*/
                            listBox1.Items.Add("Sending this file '" + fileName + "' to peer : " + thisPeer.Port + "...");
                            Thread t1 = new Thread(() => sendFileToPeerIfItAAccepts(thisPeer, Folder + "\\" + fileName));
                            t1.IsBackground=true;
                            t1.Start();
                        }
                        else
                            thisPeer.Connector.Send(Encoding.Default.GetBytes("I don`t have this file"));/*me us peer ko -ve ack bhej raha hu, k mere paas ye file nai he.*/
                    }
                    else if (thisPeer.message.Contains("Ok, I am gonna sending you this file:"))
                    {
                        /*me ne us peer ko file ki request ki thi, tu us peer ne ab kaha he, k us k paas file he, aur wo
                          peer muje file send krne laga he, tu muje ab ye file zaroor accept krni hogi, kiu k download krne k liay
                          koi prompt nai hota, balke +ve ack hi jae gi.*/
                        String fileName = thisPeer.message.Substring(thisPeer.message.IndexOf(':') + 1);
                        thisPeer.receiveStatus = "*";
                        Thread.Sleep(50);
                        thisPeer.Connector.Send(Encoding.Default.GetBytes("Yes, send this file to me"));/*telling to the peer that OK, ye wali file 'fileName' muje
                                                                          send kar do, aur me is file ko receive kar lu ga.*/
                        thisPeer.receiveStatus = "1";
                        receiveFileFromPeer(thisPeer, fileName);
                        thisPeer.receiveStatus = "";
                    }
                    else if (thisPeer.message == "I don`t have this file")
                        thisPeer.receiveStatus = "";
                    else if (thisPeer.message == "Yes, send this file to me")
                        thisPeer.sendStatus = "1";
                    else if (thisPeer.message == "No, don`t send this file to me")
                        thisPeer.sendStatus = "";
                    else if (thisPeer.message.Contains("My address is:")){
                        /*receive message from thisPeer about his ip:port, so I save it.*/
                        String extractedMsg = thisPeer.message.Substring(thisPeer.message.IndexOf(':') + 1);
                        String IP = extractedMsg.Substring(0, extractedMsg.IndexOf("_"));
                        int Port = Convert.ToInt32(extractedMsg.Substring(extractedMsg.IndexOf('_') + 1));
                        for (int i = 0; i < peer.Count(); i++){
                            /*kya ye peer pehle se exist krta he?*/
                            if (peer[i].Port == Port){
                                listBox1.Items.Add(thisPeer.Port + " peer already connected.");
                                peer.Remove(thisPeer);
                                return;
                            }
                        }
                        thisPeer.IP = IP;
                        thisPeer.Port = Port;
                        listBox1.Items.Add("Connected to the peer : " + thisPeer.Port);
                        sendBtn.Enabled = true;
                        downloadBtn.Enabled = true;
                        destinPeer.Items.Add(thisPeer.Port);
                        destinPeer.SelectedIndex = destinPeer.FindStringExact(thisPeer.Port.ToString());
                    }
                    else if (thisPeer.message.Contains("Can I send this file to you:")){
                        String fileName = thisPeer.message.Substring(thisPeer.message.IndexOf(':') + 1);
                        thisPeer.receiveStatus = "*";/*I get notification to accept/reject the file that`s name received from peer.*/
                        DialogResult dialogResult = MessageBox.Show("Do you want to save the file '" + fileName + "' ", "New File received from the peer " + thisPeer.Port, MessageBoxButtons.YesNo);
                        if (dialogResult == DialogResult.Yes){
                            thisPeer.Connector.Send(Encoding.Default.GetBytes("Yes, send this file to me"));/*telling to the peer that OK, ye wali file 'fileName' muje
                                                                          send kar do, aur me is file ko receive kar lu ga.*/
                            thisPeer.receiveStatus = "1";
                            receiveFileFromPeer(thisPeer, fileName);
                        }
                        else
                            thisPeer.Connector.Send(Encoding.Default.GetBytes("No, don`t send this file to me"));/*(-ve ack) telling to the peer that do not send me this file 'filename'*/
                        thisPeer.receiveStatus = "";
                        /*is peer se agar ye file completely acctepted/rejected ho gai he, tu phir dubara ham normal messages ko receive karein ge.*/
                    }
                    else
                        listBox1.Items.Add("Peer " + thisPeer.Port + " : " + thisPeer.message);
                }
                catch (Exception ex)
                {
                    listBox1.Items.Add(thisPeer.Port + " peer left");
                    peer.Remove(thisPeer);
                    return;/*no more this peer alive, so we close accpting things from it.*/
                }
            }
        }
        private void continuouslyReceivingMessagesFromServer(){
            while (true){
                try{
                    byte[] buffer = new byte[1500];
                    int msgLength = srvrConnector.Receive(buffer, 0, buffer.Length, 0);
                    String receivedMessage = getActualMessage(buffer, msgLength);
                    if (receivedMessage.Contains("all available clients:")){
                        String AllAvailableClients = receivedMessage.Substring(receivedMessage.IndexOf(':') + 1);
                        while (true){/*jitne bi available clients server ne send kiay hein, wo sare combobox me add hote jain.*/
                            int index = AllAvailableClients.IndexOf("_");
                            if (index < 0)/*now '_' not exist.*/
                                break;
                            String newSingleClient = AllAvailableClients.Substring(0, index);
                            destinPortNo.Items.Add(newSingleClient);
                            destinPortNo.SelectedIndex = destinPortNo.FindStringExact(newSingleClient);
                            button1.Text = "Connect to peer";
                            /*ye single Client tu combobox me add ho gya he, lekin is k elawa all remaining clients hi ab
                             avaiable ho jain ge.*/
                            AllAvailableClients = AllAvailableClients.Substring(index + 1);
                        }
                        listBox1.Items.Add("Server is telling about available clients");
                    }
                    else/*server ki taraf se aae normal message ko group me ham send karein ge.*/
                        listBox2.Items.Add(receivedMessage);
                }
                catch (Exception ex) { MessageBox.Show(ex.Message); }
            }
        }
        private void textBox2_KeyDown(object sender, KeyEventArgs e){
            if (e.KeyCode == Keys.Enter){
                srvrConnector.Send(Encoding.Default.GetBytes(textBox2.Text));
                listBox2.Items.Add("You : " + textBox2.Text);/*print your message in your group.*/
                textBox2.Clear();
                textBox2.Focus();
            }
        }
        private void downloadBtn_Click(object sender, EventArgs e){
            /*Download any file from another peer.*/
            SearchFile fileDialog = new SearchFile();
            fileDialog.ShowDialog();
            if (fileDialog.fileName == null) ;//I canceled the download
            else{
                Thread t1 = new Thread(() => downloadFile(fileDialog.fileName));
                t1.IsBackground = true;
                t1.Start();
            }
        }
        void downloadFile(String fileName){
            listBox1.Items.Add("File '" + fileName + "' will be searched and downloaded from any peers if available.");
            for (int i = 0; i < peer.Count(); i++){
                peer[i].Connector.Send(Encoding.Default.GetBytes("I need this file from you:" + fileName));
                peer[i].receiveStatus = "#";/*waiting for notification (reply) for +ve/-ve ack from this peer for the file.*/
                while (peer[i].receiveStatus == "#" || peer[i].receiveStatus == "*")
                { }/*aur "*" ka mtlab he, k peer ki taraf se notification tu aa gai he, lekin abi ham ne wo accept/reject nai ki.*/
                if (peer[i].receiveStatus == "1")
                    return; /*yaha per file receiving ho rahi he 'receiveFileFromPeer' wale thread me, yani k file hamein mil gai he.*/
                else ;/*is ka mtlab he, k peer ki taraf se -ve ack aae he, aur file nai mili.*/
            }
            /*control yaha per aane ka mtlab he, k kisi peer ne bi file send nai ki.*/
            listBox1.Items.Add("File '" + fileName + "' not available at any peer to download.");
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
