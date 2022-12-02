//  (C) 2001-2015 Force Dimension
//  All Rights Reserved.
//
//  Version 3.6.0

// UDP libs start
#include <locale>         // std::wstring_convert
#include <codecvt>        // std::codecvt_utf8

#include<future>

#include<winsock2.h>
#pragma comment(lib,"ws2_32.lib") // Winsock Library 
#define BUFLEN 4
#define PORT 8888
#define SPORT 8899
// UDP libs end

#include <thread> 
#include <stdio.h>
#include "dhdc.h"

#include <iostream>

//using namespace std; 

#define REFRESH_INTERVAL  0.1   // sec

//using namespace std; 

inline void
MatTranspose(const double a[3][3],
	double       m[3][3])
{
	m[0][0] = a[0][0];  m[0][1] = a[1][0];  m[0][2] = a[2][0];
	m[1][0] = a[0][1];  m[1][1] = a[1][1];  m[1][2] = a[2][1];
	m[2][0] = a[0][2];  m[2][1] = a[1][2];  m[2][2] = a[2][2];
}

// UDP Receive Function Declaration
//float receiveUDPmessage(SOCKET, WSADATA, int, struct sockaddr_in&);
void receiveUDPmessage(SOCKET, WSADATA, int, struct sockaddr_in&, float*);
//void receiveUDPmessage();

void sendUDPmessage(SOCKET, WSADATA, int, struct sockaddr_in&);

void changeDouble()
{
	double dl = 0.1;
	double* fl = &dl;

	while (true)
	{
		*fl += 0.01;
		printf("%f: \n", *fl); // << std::endl;
	}
}

int main(int  argc, char **argv)
{
	

	// UDP Stuff start
	// ***********************************************************************
	// ***********************************************************************
	// ***********************************************************************
	float aPos;
	float* ptr = &aPos;

	int slen;
	char buf[BUFLEN];

	SOCKET s;
	WSADATA wsa;
	struct sockaddr_in server, si_other;


	//Initialise winsock
	printf("\nInitialising Winsock...");
	if (WSAStartup(MAKEWORD(2, 2), &wsa) != 0)
	{
		printf("Failed. Error Code : %d", WSAGetLastError());
		exit(EXIT_FAILURE);
	}
	printf("Initialised.\n");

	//Create a socket
	if ((s = socket(AF_INET, SOCK_DGRAM, 0)) == INVALID_SOCKET)
	{
		printf("Could not create socket : %d", WSAGetLastError());
	}
	printf("Socket created.\n");

	//Prepare the sockaddr_in structure
	server.sin_family = AF_INET;
	server.sin_addr.s_addr = INADDR_ANY;
	server.sin_port = htons(PORT);

	//Bind
	if (bind(s, (struct sockaddr *)&server, sizeof(server)) == SOCKET_ERROR)
	{
		printf("Bind failed with error code : %d", WSAGetLastError());
		exit(EXIT_FAILURE);
	}
	puts("Bind done");

	slen = sizeof(si_other);

	//receiveUDPmessage(s,wsa,slen,recv_len);

	// ***********************************************************************
	// ***********************************************************************
	// ***********************************************************************
	// UDP Stuff end 


	// UDP Send start
	// ***********************************************************************
	// ***********************************************************************
	// ***********************************************************************
	float aPosS;
	int slenS;
	char bufS[BUFLEN];

	SOCKET sS;
	WSADATA wsaS;
	struct sockaddr_in serverS, si_otherS;


	//Initialise winsock
	printf("\nInitialising Winsock...");
	if (WSAStartup(MAKEWORD(2, 2), &wsaS) != 0)
	{
		printf("Failed. Error Code : %d", WSAGetLastError());
		exit(EXIT_FAILURE);
	}
	printf("Initialised.\n");

	//Create a socket
	if ((sS = socket(AF_INET, SOCK_DGRAM, 0)) == INVALID_SOCKET)
	{
		printf("Could not create socket : %d", WSAGetLastError());
	}
	printf("Socket created.\n");

	//Prepare the sockaddr_in structure
	serverS.sin_family = AF_INET;
	serverS.sin_addr.s_addr = INADDR_ANY;
	serverS.sin_port = htons(SPORT);

	//Bind
	if (bind(sS, (struct sockaddr *)&serverS, sizeof(serverS)) == SOCKET_ERROR)
	{
		printf("Bind failed with error code : %d", WSAGetLastError());
		exit(EXIT_FAILURE);
	}
	puts("Bind done");

	slenS = sizeof(si_otherS);

	//receiveUDPmessage(s,wsa,slen,recv_len);

	// ***********************************************************************
	// ***********************************************************************
	// ***********************************************************************
	// UDP Send end 


	const double K = 40;

	double px, py, pz;
	double fx, fy, fz;
	double j0, j1, j2;
	double g0, g1, g2;
	double q0, q1, q2;
	double J[3][3];
	double Jt[3][3];
	double freq = 0.0;
	double t1, t0 = dhdGetTime();
	bool   spring = false;
	int    done = 0;
	int    sat;

	// message
	int major, minor, release, revision;
	dhdGetSDKVersion(&major, &minor, &release, &revision);
	printf("Force Dimension - Jacobian Usage Example %d.%d.%d.%d\n", major, minor, release, revision);
	printf("(C) 2001-2015 Force Dimension\n");
	printf("All Rights Reserved.\n\n");


	// required to get the Jacobian matrix
	dhdEnableExpertMode();

	// open the first available device
	if (dhdOpen() < 0) {
		printf("error: cannot open device (%s)\n", dhdErrorGetLastStr());
		dhdSleep(2.0);
		return -1;
	}

	// identify device
	printf("%s device detected\n\n", dhdGetSystemName());

	// emulate button on supported devices
	dhdEmulateButton(DHD_ON);

	// display instructions
	printf("press BUTTON to enable virtual spring\n");
	printf("         'q' to quit\n\n");

	// enable force
	dhdEnableForce(DHD_ON);

	//std::future<float> fu = std::async(std::launch::async, receiveUDPmessage, s, wsa, slen, si_other); //std::launch::async,
	//std::future<void> fu = std::async(receiveUDPmessage, s, wsa, slen, si_other);
	//std::thread receiveThread(aPos = receiveUDPmessage(s, wsa, slen, si_other));

	//aPos = fu.get();
	// ****************************************************************
	//*****************************************************************
	//*****************************************************************
	//try
	//{

	// UDP stuff start
	//aPos = receiveUDPmessage(s, wsa, slen, si_other);
	//sendUDPmessage(sS, wsaS, slenS, si_otherS);

	//aPos = fu.get();
	//receiveThread.join(); 
	//printf("Rcv: %f \n", aPos);

	// UDP stuff end
	//}
	//catch (const std::exception& e)
	//{
	////printf(" " + e);
	//}

	std::thread udp1(receiveUDPmessage,s, wsa, slen, si_other, ptr);
	udp1.detach();

	//std::thread udpSend(sendUDPmessage, sS, wsaS, slenS, si_otherS);
	//udpSend.detach(); 

	// ****************************************************************
	//*****************************************************************
	//*****************************************************************



	// loop while the button is not pushed
	while (!done) {

		//aPos = 0.1;

		//aPos = receiveUDPmessage(s, wsa, slen, si_other);
		//sendUDPmessage(sS, wsaS, slenS, si_otherS);

		// retrieve joint angles
		if (dhdGetPosition(&px, &py, &pz) < DHD_NO_ERROR) {
			printf("error: cannot get joint angles (%s)\n", dhdErrorGetLastStr());
			done = 1;
		}

		//// compute force to apply
		//if (spring) {
		//	fx = -K * px;
		//	fy = -K * py;
		//	fz = -K * pz;
		//}
		// compute force to apply
		if (spring) {
			fx = -K * px + aPos;
			fy = -K * py - aPos;
			fz = -K * pz + aPos;
		}
		else fx = fy = fz = 0.0;

		//printf("%f \r", aPos);

		// retrieve joint angles
		if (dhdGetDeltaJointAngles(&j0, &j1, &j2) < DHD_NO_ERROR) {
			printf("error: cannot get joint angles (%s)\n", dhdErrorGetLastStr());
			done = 1;
		}

		// compute jacobian
		if (dhdDeltaJointAnglesToJacobian(j0, j1, j2, J) < DHD_NO_ERROR) {
			printf("error: cannot compute jacobian (%s)\n", dhdErrorGetLastStr());
			done = 1;
		}

		// compute joint torques required for gravity compensation
		if (dhdDeltaGravityJointTorques(j0, j1, j2, &g0, &g1, &g2) < DHD_NO_ERROR) {
			printf("error: cannot compute gravity compensation joint torques (%s)\n", dhdErrorGetLastStr());
			done = 1;
		}

		// compute joint torques Q = ((J)T) * F
		MatTranspose(J, Jt);
		q0 = Jt[0][0] * fx + Jt[0][1] * fy + Jt[0][2] * fz;
		q1 = Jt[1][0] * fx + Jt[1][1] * fy + Jt[1][2] * fz;
		q2 = Jt[2][0] * fx + Jt[2][1] * fy + Jt[2][2] * fz;

		// combine gravity compensation and requested force
		q0 += g0;
		q1 += g1;
		q2 += g2;

		// apply joint torques
		if ((sat = dhdSetDeltaJointTorques(q0, q1, q2)) < DHD_NO_ERROR) {
			printf("error: cannot set joint torques (%s)\n", dhdErrorGetLastStr());
			done = 1;
		}

		// display refresh rate and position at 10Hz
		t1 = dhdGetTime();
		if ((t1 - t0) > REFRESH_INTERVAL) {

			// retrieve information to display
			freq = dhdGetComFreq();
			t0 = t1;

			// write down position
			if (dhdGetPosition(&px, &py, &pz) < 0) {
				printf("error: cannot read position (%s)\n", dhdErrorGetLastStr());
				done = 1;
			}
			if (sat == DHD_MOTOR_SATURATED) printf("[*] ");
			else                            printf("[-] ");
			//      printf ("q = (%+0.03f, %+0.03f, %+0.03f) [Nm]  |  freq = %0.02f [kHz]       \r", q0, q1, q2, freq);
			//printf("q = (%+0.03f, %+0.03f, %+0.03f) [m]  |  freq = %0.02f [kHz]       \r", px, py, pz, freq);

			// test for exit condition
			if (dhdGetButtonMask()) spring = false;
			else                    spring = true;
			if (dhdKbHit()) {
				switch (dhdKbGet()) {
				case 'q': done = 1; break;
				}
			}
		}
	}

	// close the connection
	dhdClose();

	// happily exit
	printf("\ndone.\n");
	return 0;
}


//float receiveUDPmessage(SOCKET sz, WSADATA wsaz, int slenz, struct sockaddr_in& si_otherz)
void receiveUDPmessage(SOCKET sz, WSADATA wsaz, int slenz, struct sockaddr_in& si_otherz, float* val)
{
	// UDP init start
	float afz;
	char bufz[4];
	int recv_len;
	//struct sockaddr_in si_other; 

	while (true)
	{
		

		//printf("Waiting for data...");
		fflush(stdout);

		//clear the buffer by filling null, it might have previously received data
		memset(bufz, '\0', BUFLEN);

		//try to receive some data, this is a blocking call
		if ((recv_len = recvfrom(sz, bufz, BUFLEN, 0, (struct sockaddr *) &si_otherz, &slenz)) == SOCKET_ERROR)
		{
			printf("recvfrom() failed with error code : %d", WSAGetLastError());
			exit(EXIT_FAILURE);
		}

		memcpy(&afz, bufz, sizeof afz);
		//printf("AF: %f \n", afz);

		*val = afz; 
		//printf("%f: \n", *val); // << std::endl;

		//*************************************************************
		//************************** Send *****************************
		//*************************************************************
		
		char sendMsg[4] = {"78"};
		char bufsend[4];

		memcpy(&bufsend, sendMsg, sizeof bufsend);
		printf("%s: \r", bufsend); 

		//now reply the client with the same data
		int sentz = sendto(sz, bufsend, recv_len, 0, (struct sockaddr*) &si_otherz, slenz);
		if (sendto(sz, bufsend, recv_len, 0, (struct sockaddr*) &si_otherz, slenz) == SOCKET_ERROR)
		{
			printf("sendto() failed with error code : %d", WSAGetLastError());
			exit(EXIT_FAILURE);
		}



	}

	//return afz;
}


///////////////////////////////////////////////////////////////////////////////////////////////
///////////////////////////////////////////////////////////////////////////////////////////////
///////////////////////////////////////////////////////////////////////////////////////////////
///////////////////////////////////////////////////////////////////////////////////////////////
///////////////////////////////////////////////////////////////////////////////////////////////


void sendUDPmessage(SOCKET sz, WSADATA wsaz, int slenz, struct sockaddr_in& si_otherz)
//void receiveUDPmessage(SOCKET sz, WSADATA wsaz, int slenz, struct sockaddr_in& si_otherz)

{
	// UDP init start
	// **********************************************************
	// *********************************************************
	float sfz = 0.999;
	char bufsend[4];
	//int recv_len;
	char sendMsg[4] = { '1' };
	//char Array_[10] = { 'a' };


	// UDP SEND 
	//**********************************************************************
	//**********************************************************************
	memcpy(&bufsend, sendMsg, sizeof bufsend);
	printf("Sent: %s \n \r", bufsend);

	//now reply the client with the same data
	int sentz = sendto(sz, bufsend, 4, 0, (struct sockaddr*) &si_otherz, slenz);
	if (sendto(sz, bufsend, 4, 0, (struct sockaddr*) &si_otherz, slenz) == SOCKET_ERROR)
	{
		printf("sendto() failed with error code : %d", WSAGetLastError());
		exit(EXIT_FAILURE);
	}

}

