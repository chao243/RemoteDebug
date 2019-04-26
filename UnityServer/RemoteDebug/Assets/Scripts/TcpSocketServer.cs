using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using UnityEngine;


public class TcpSocketServer : MonoBehaviour {

	// Use this for initialization
	void Start () {
        StartListening();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	
	// Thread signal.  
    public ManualResetEvent allDone = new ManualResetEvent(false);  
  
  
    public void StartListening() {  
        // Establish the local endpoint for the socket.  
        // The DNS name of the computer  
        // running the listener is "host.contoso.com".  
        IPHostEntry ipHostInfo = Dns.GetHostEntry(Dns.GetHostName());  
        IPAddress ipAddress = ipHostInfo.AddressList[0];  
        IPEndPoint localEndPoint = new IPEndPoint(ipAddress, 11000);  
  
        // Create a TCP/IP socket.  
        Socket listener = new Socket(ipAddress.AddressFamily,  
            SocketType.Stream, ProtocolType.Tcp );  
  
        // Bind the socket to the local endpoint and listen for incoming connections.  
        try {  
            listener.Bind(localEndPoint);  
            listener.Listen(100);  
  
//            while (true) {  
                // Set the event to nonsignaled state.  
//                allDone.Reset();  
  
                // Start an asynchronous socket to listen for connections.  
                BeginAccept(listener);
  
                // Wait until a connection is made before continuing.  
//                allDone.WaitOne();  
//            }  
  
        } catch (Exception e) {  
            Debug.LogError(e.ToString());  
        }  
    }

    private void BeginAccept(Socket listener)
    {
                Debug.Log("Waiting for a connection...");  
                listener.BeginAccept(   
                    new AsyncCallback(AcceptCallback),  
                    listener );  
    }
    
  
    public void AcceptCallback(IAsyncResult ar) {  
        // Signal the main thread to continue.  
//        allDone.Set();  
  
        // Get the socket that handles the client request.  
        Socket listener = (Socket) ar.AsyncState;  
        Socket handler = listener.EndAccept(ar);  
  
        // Create the state object.  
        SocketState state = new SocketState();  
        state.workSocket = handler;  
        
        BeginAccept(listener);
        state.BeginReceive();
    }  
  
 
  
}
