/*
 
    -----------------------
    UDP-Receive (send to)
    -----------------------
    // [url]http://msdn.microsoft.com/de-de/library/bb979228.aspx#ID0E3BAC[/url]
   
   
    // > receive
    // 127.0.0.1 : 8051
   
    // send
    // nc -u 127.0.0.1 8051
 
*/
using UnityEngine;
using System.Collections;
 
using System;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;

public class UDPReceive : MonoBehaviour
{
   
	// receiving Thread
	Thread receiveThread;
 
	// udpclient object
	UdpClient client;

	public float heightOffset; 
	public int cnter = 0;
	public int eR = 0;
	public float eX;
	public float eY;
	public float eZ;
	public GameObject player;

	// public
	// public string IP = "127.0.0.1"; default local
	public int port;
	// define > init
 
	// infos
	public string lastReceivedUDPPacket = "";
	public string allReceivedUDPPackets = "";
	// clean up this from time to time!
   
   
	// start from shell
	private static void Main ()
	{
		UDPReceive receiveObj = new UDPReceive ();
		receiveObj.init ();
 
		string text = "";
		do {
			text = Console.ReadLine ();
		} while(!text.Equals ("exit"));
	}
	// start from unity3d
	public void Start ()
	{
       
		init ();
	}

	void Update ()
	{
		Vector3 roboPos = new Vector3 (eY, eZ+heightOffset, -eX);
		Quaternion roboOrient = Quaternion.identity;
		player.transform.SetPositionAndRotation (roboPos, roboOrient);
	}

	//// OnGUI
	//void OnGUI()
	//{
	//    Rect rectObj=new Rect(40,10,200,400);
	//        GUIStyle style = new GUIStyle();
	//            style.alignment = TextAnchor.UpperLeft;
	//    GUI.Box(rectObj,"# UDPReceive\n127.0.0.1 "+port+" #\n"
	//                + "shell> nc -u 127.0.0.1 : "+port+" \n"
	//                + "\nLast Packet: \n"+ lastReceivedUDPPacket
	//                + "\n\nAll Messages: \n"+allReceivedUDPPackets
	//            ,style);
	//}
       
	// init
	private void init ()
	{
		// Endpunkt definieren, von dem die Nachrichten gesendet werden.
		print ("UDPSend.init()");
       
		// define port
		port = 8899;
 
		// status
		print ("Sending to 127.0.0.1 : " + port);
		print ("Test-Sending to this Port: nc -u 127.0.0.1  " + port + "");
 
   
		// ----------------------------
		// Abhören
		// ----------------------------
		// Lokalen Endpunkt definieren (wo Nachrichten empfangen werden).
		// Einen neuen Thread für den Empfang eingehender Nachrichten erstellen.
		receiveThread = new Thread (
			new ThreadStart (ReceiveData));
		receiveThread.IsBackground = true;
		receiveThread.Start ();
 
	}
 
    
	// receive thread
	private  void ReceiveData ()
	{
 
		client = new UdpClient (port);
		while (true) {
 
			try {
				// Bytes empfangen.

				if (cnter == 0) {
					IPEndPoint anyIPR = new IPEndPoint (IPAddress.Any, 0);
					byte[] dataR = client.Receive (ref anyIPR);  
					string textr = Encoding.UTF8.GetString (dataR);
					eR = int.Parse (textr);
				} else if (cnter	== 1 && eR == 100) {
					IPEndPoint anyIPX = new IPEndPoint (IPAddress.Any, 0);
					byte[] dataX = client.Receive (ref anyIPX);                
					string textx = Encoding.UTF8.GetString (dataX);
					eX = float.Parse (textx);
				} else if (cnter	== 2 && eR == 100) {
					IPEndPoint anyIPY = new IPEndPoint (IPAddress.Any, 0);
					byte[] dataY = client.Receive (ref anyIPY);  
					string texty = Encoding.UTF8.GetString (dataY);
					eY = float.Parse (texty);
				} else if (cnter	== 3 && eR == 100) {
					IPEndPoint anyIPZ = new IPEndPoint (IPAddress.Any, 0);
					byte[] dataZ = client.Receive (ref anyIPZ);  
					string textz = Encoding.UTF8.GetString (dataZ);
					eZ = float.Parse (textz);
					cnter = 0;
				}

				cnter++;

			} catch (Exception err) {
				//print (err.ToString ());
			}
		}
	}
   
	//// getLatestUDPPacket
	//// cleans up the rest
	//public string getLatestUDPPacket()
	//{
	//    allReceivedUDPPackets="";
	//    return lastReceivedUDPPacket;
	//}
}