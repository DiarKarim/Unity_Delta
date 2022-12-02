using UnityEngine;
using System.Collections;

using System;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;

public class UDPSend : MonoBehaviour
{
	private static int localPort;
	public GameObject targetObj;
	public GameObject handObj;
	// prefs
	private string IP;
	// define in init
	public int port;
	// define in init

	// "connection" things
	IPEndPoint remoteEndPoint;
	UdpClient client;

	// gui
	public string strMessage = "0.1";
	private moveTarget movTgt;
	public GameObject target;
	private int cnters = 0;

	private string udpSynchOut = "0.987010";
	private string messageX = "0.01";
	private string messageY = "0.0";
	private string messageZ = "0.0";
	private float i = 0f;
	public int assX;
	public int assY;
	public int assZ;

	public float mess;

	public float errorThreshold = 0.0001f;

	//	// call it from shell (as program)
	//	private static void Main ()
	//	{
	//		//UDPSend sendObj = new UDPSend();
	//		//sendObj.init();
	//
	//		//// testing via console
	//		// sendObj.inputFromConsole();
	//
	//		//// as server sending endless
	//		//sendObj.sendEndless(" endless infos \n");
	//
	//	}
	// start from unity3d
	public void Start ()
	{
		movTgt = target.GetComponent<moveTarget> (); 
		init ();
		assX = 0;
		assY = 0;
		assZ = 0;
	}

	void Update ()
	{
		
		//Debug.Log (movTgt.errors.magnitude);

		//if (movTgt.errors.x > 0.75) {
		//float.Parse (messageX);

		// if assistance block

		try {
			//Assistance counters 
			assX = 0;
			assY = 0;
			assZ = 0;

			if (movTgt.assistance == 1) //target.activeInHierarchy)
			{
				Vector3 heading = targetObj.transform.position - handObj.transform.position; 
				float distance = heading.magnitude; 
				Vector3 direction = heading / distance; 
				//Debug.Log(" X: " + direction.x + " Y: " + direction.y + " Z: " + direction.z); 
				if (!(heading.x > errorThreshold || heading.x < -errorThreshold) ){
					direction.x = 0f; 
					assX = 1; 
					//Debug.Log("Error in X");
					//Debug.Log (messX);
				}
	
				if (!(heading.y > errorThreshold || heading.y < -errorThreshold)) {
					direction.y = 0f; 
					assY = 1;
					//Debug.Log("Error in Y");
					//Debug.Log (messY);
				} 
				if (!(heading.z > errorThreshold|| heading.z < -errorThreshold)) {
					direction.z = 0f; 
					assZ = 1;
					//Debug.Log (messZ);
					//Debug.Log("Error in Z");
				} 

				byte[] data = new byte[12];
				byte[] datax = BitConverter.GetBytes (direction.x);
				byte[] datay = BitConverter.GetBytes (direction.y);
				byte[] dataz = BitConverter.GetBytes (direction.z);

				int idx = 0;
				for(int i=0; i< datax.Length; i++)
				{
					data[idx] = datax[i];
					idx++;
				}

				for(int i=0; i< datay.Length; i++)
				{
					data[idx] = datay[i];

					idx++;
				}

				for(int i=0; i< dataz.Length; i++)
				{
					data[idx] = dataz[i];

					idx++;
				}
					

				//Debug.Log(data.Length);
			    client.Send (data, data.Length, remoteEndPoint);

			} 
			else if (movTgt.assistance == 2) {
				mess = 0f; 
				//Debug.Log ("No Assistance");
				byte[] data = BitConverter.GetBytes (mess);
				client.Send (data, data.Length, remoteEndPoint);
				assX = 0;
				assY = 0;
				assZ = 0;
			}


		} catch (Exception err) {
			print (err.ToString ());
			assX = 0;
			assY = 0;
			assZ = 0;
			//Debug.Log("Try-Catch error");
		} 



		
	}

	// OnGUI
	//    void OnGUI()
	//    {
	//        Rect rectObj = new Rect(40, 380, 200, 400);
	//        GUIStyle style = new GUIStyle();
	//        style.alignment = TextAnchor.UpperLeft;
	//        GUI.Box(rectObj, "# UDPSend-Data\n127.0.0.1 " + port + " #\n"
	//                    + "shell> nc -lu 127.0.0.1  " + port + " \n"
	//                , style);
	//
	//        // ------------------------
	//        // send it
	//        // ------------------------
	//		strMessage = GUI.TextField(new Rect(160, 360, 140, 20), strMessage);
	//        if (GUI.Button(new Rect(310, 360, 40, 20), "send"))
	//        {
	//            sendString(strMessage + "\n");
	//        }
	//    }



	// init
	public void init ()
	{
		// Endpunkt definieren, von dem die Nachrichten gesendet werden.
		print ("UDPSend.init()");

		// define
		IP = "127.0.0.1";
		port = 8888;

		// ----------------------------
		// Senden
		// ----------------------------
		remoteEndPoint = new IPEndPoint (IPAddress.Parse (IP), port);
		client = new UdpClient ();

		// status
		print ("Sending to " + IP + " : " + port);
		print ("Testing: nc -lu " + IP + " : " + port);

	}

	//// inputFromConsole
	//private void inputFromConsole()
	//{
	//    try
	//    {
	//        string text;
	//        do
	//        {
	//            text = Console.ReadLine();

	//            // Den Text zum Remote-Client senden.
	//            if (text != "")
	//            {

	//                // Daten mit der UTF8-Kodierung in das Binärformat kodieren.
	//                byte[] data = Encoding.UTF8.GetBytes(text);

	//                // Den Text zum Remote-Client senden.
	//                client.Send(data, data.Length, remoteEndPoint);
	//            }
	//        } while (text != "");
	//    }
	//    catch (Exception err)
	//    {
	//        print(err.ToString());
	//    }

	//}

	// sendData
	//	private void sendString (string messageX, string messageY, string messageZ)
	//	{
	//
	//	}


	//    // endless test
	//    private void sendEndless(string testStr)
	//    {
	//        do
	//        {
	//            sendString(testStr);
	//
	//
	//        }
	//        while (true);
	//
	//    }

}