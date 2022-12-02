//  (C) 2001-2015 Force Dimension
//  All Rights Reserved.
//
//  Version 3.6.0

// Socket start
#include<winsock2.h>

#pragma comment(lib,"ws2_32.lib") // Winsock Library 

#define BUFLEN 512 
#define PORT 8888

// Socket end

#include <stdio.h>
#include <stdlib.h>
#define _USE_MATH_DEFINES
#include <math.h>
#include <algorithm>

#include <fstream>
#include <iostream>
#include<string>
#include<cstdlib>
using namespace std; 

// some platforms do not define M_PI
#ifndef M_PI
#define M_PI 3.14159265358979323846
#endif

#include "drdc.h"
#include "dhdc.h"
//Time
#include <cstdio>
#include <ctime>
#include <sstream>

int
main(int  argc,
	char **argv)
{

	// Socket start

	SOCKET s;
	struct sockaddr_in server, si_other;
	int slen, recv_len;
	char buf[BUFLEN];
	stringstream cordSS;
	string cords;
	string cordYY;
	double cordYYd; 
	WSADATA wsa;

	slen = sizeof(si_other);

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

	//keep listening for data
	//while (1)
	//{
	//	//printf("Waiting for data...");
	//	fflush(stdout);

	//	//clear the buffer by filling null, it might have previously received data
	//	memset(buf, '\0', BUFLEN);

	//	//try to receive some data, this is a blocking call
	//	if ((recv_len = recvfrom(s, buf, BUFLEN, 0, (struct sockaddr *) &si_other, &slen)) == SOCKET_ERROR)
	//	{
	//		printf("recvfrom() failed with error code : %d", WSAGetLastError());
	//		exit(EXIT_FAILURE);
	//	}

	//	//print details of the client/peer and the data received
	//	//printf("Received packet from %s:%d\n", inet_ntoa(si_other.sin_addr), ntohs(si_other.sin_port));
	//	printf("Data: %s\n", buf);

	//	//now reply the client with the same data
	//	if (sendto(s, buf, recv_len, 0, (struct sockaddr*) &si_other, slen) == SOCKET_ERROR)
	//	{
	//		printf("sendto() failed with error code : %d", WSAGetLastError());
	//		exit(EXIT_FAILURE);
	//	}
	//}

	closesocket(s);
	WSACleanup();


	// Socket end
	//void errorz(const char *msg)
	//{
	//	perror(msg);
	//	exit(0);
	//}

	//Set files
	//Load Trajectories
	ifstream infile;
	infile.open("C:/Users/Lizzie/Desktop/CoordinateFolder/T2.txt");
	/*ifstream infile2;
	infile2.open("C:/Users/Lizzie/Desktop/CoordinateFolder/T2.txt");
	ifstream infile3;
	infile3.open("C:/Users/Lizzie/Desktop/CoordinateFolder/T3.txt");
	ifstream infile4;
	infile4.open("C:/Users/Lizzie/Desktop/CoordinateFolder/T4.txt");
	ifstream infile5;
	infile5.open("C:/Users/Lizzie/Desktop/CoordinateFolder/T5.txt");
	ifstream infile6;
	infile6.open("C:/Users/Lizzie/Desktop/CoordinateFolder/T6.txt");
	ifstream infile7;
	infile7.open("C:/Users/Lizzie/Desktop/CoordinateFolder/T7.txt");
	ifstream infile8;
	infile8.open("C:/Users/Lizzie/Desktop/CoordinateFolder/T8.txt");
	ifstream infile9;
	infile9.open("C:/Users/Lizzie/Desktop/CoordinateFolder/T9.txt");
	ifstream infile10;
	infile10.open("C:/Users/Lizzie/Desktop/CoordinateFolder/T10.txt");
	ifstream infile11;
	infile11.open("C:/Users/Lizzie/Desktop/CoordinateFolder/T11.txt");
	ifstream infile12;
	infile12.open("C:/Users/Lizzie/Desktop/CoordinateFolder/T12.txt");
	ifstream infile13;
	infile13.open("C:/Users/Lizzie/Desktop/CoordinateFolder/T13.txt");
	ifstream infile14;
	infile14.open("C:/Users/Lizzie/Desktop/CoordinateFolder/T14.txt");
	ifstream infile15;
	infile15.open("C:/Users/Lizzie/Desktop/CoordinateFolder/T15.txt");
	ifstream infile16;
	infile16.open("C:/Users/Lizzie/Desktop/CoordinateFolder/T16.txt");
	ifstream infile17;
	infile17.open("C:/Users/Lizzie/Desktop/CoordinateFolder/T17.txt");
	ifstream infile18;
	infile18.open("C:/Users/Lizzie/Desktop/CoordinateFolder/T18.txt");*/

	/*ofstream outfile;
	outfile.open("C:/Users/Lizzie/Desktop/CoordinateFolder/deltaposition.txt");
	ofstream erroroutfile;
	erroroutfile.open("C:/Users/Lizzie/Desktop/CoordinateFolder/error.txt");
	ofstream timer;
	timer.open("C:/Users/Lizzie/Desktop/CoordinateFolder/timer.txt"); */
	ofstream errorthresh;
	errorthresh.open("C:/Users/Lizzie/Desktop/CoordinateFolder/errorthresh.txt");

	//Outfile for trial number, running error in x,y,z , running delta position in x,y,z and running timer
	ofstream T_data;
	T_data.open("C:/Users/Lizzie/Desktop/CoordinateFolder/T_data.txt");

	int const stepz = 5000;

	//Set variables
	//Trajectory
	string coordX;
	string coordY;
	string coordZ;
	int T, t = 1;
	double cX;
	double cY;
	double cZ;
	double cdX[stepz];
	double cdY[stepz];
	double cdZ[stepz];

	//Delta Position
	double posX, posY, posZ;
	double deltaX[stepz];
	double deltaY[stepz];
	double deltaZ[stepz];

	//Error
	double errorX;
	double errorY;
	double errorZ;
	double errX[stepz];
	double errY[stepz];
	double errZ[stepz];// double error[50000];
	double maxerrX;
	double minerrX;
	double maxerrY;
	double minerrY;
	double maxerrZ;
	double minerrZ;

	//Outfile Headings
	string Trial_Number;
	string Error_X;
	string Error_Y;
	string Error_Z;
	string DeltaPos_X;
	string DeltaPos_Y;
	string DeltaPos_Z;
	string Time;

	//Other variables
	int point;
	int    i;
	int    done;
	int	   loopz;
	double px = 0.0, py = 0.0, pz = 0.0;
	double p[10];
	double timez[stepz];
	int limitz = 255; //212
	int trialnum = 1;

	//Initialise 
	// message
	int major, minor, release, revision;
	dhdGetSDKVersion(&major, &minor, &release, &revision);
	printf("Force Dimension - Robot Control Example %d.%d.%d.%d\n", major, minor, release, revision);
	printf("(C) 2001-2015 Force Dimension\n");
	printf("All Rights Reserved.\n\n");

	// open the first available device
	if (drdOpen() < 0) {
		printf("error: cannot open device (%s)\n", dhdErrorGetLastStr());
		dhdSleep(2.0);
		return -1;
	}

	// print out device identifier
	if (!drdIsSupported()) {
		printf("unsupported device\n");
		printf("exiting...\n");
		dhdSleep(2.0);
		drdClose();
		return -1;
	}
	printf("%s haptic device detected\n\n", dhdGetSystemName());

	// display instructions
	printf("press 'q' to quit demo\n\n");

	if (!drdIsInitialized() && dhdGetSystemType() == DHD_DEVICE_FALCON) {
		printf("please initialize Falcon device...\r"); fflush(stdout);
		while (!drdIsInitialized()) dhdSetForce(0.0, 0.0, 0.0);
		printf("                                  \r");
		dhdSleep(0.5);
	}

	// initialize if necessary
	if (!drdIsInitialized() && (drdAutoInit() < 0)) {
		printf("error: initialization failed (%s)\n", dhdErrorGetLastStr());
		dhdSleep(2.0);
		return -1;
	}

	// start robot control loop
	if (drdStart() < 0) {
		printf("error: control loop failed to start properly (%s)\n", dhdErrorGetLastStr());
		dhdSleep(2.0);
		return -1;
	}

	// get workspace limits
	for (i = 0; i < 10; i++) p[i] = -0.04 + i*(0.08 / 10.0);

	// reset motion control parameters
	drdSetPosMoveParam(1.0, 1.0, -1.0);

	// move to centre (0,0,0)
	done = 0;
	if (done > -1) drdMoveToPos(0, 0, 0);

	// enable trajectory interpolator and adjust its parameters
	drdEnableFilter(true);
	drdSetPosTrackParam(2.0, 1.0, -1.0);

	//Should result in delta moving freely
	// enable force
	//dhdEnableForce(DHD_ON);
	//drdRegulatePos(false); //Completely freely - but can you still track position??


	//Start main code
	//Save coordinates of trajectory in 3 coordinate arrays
	for (loopz = 0; loopz < limitz; ++loopz) {
		
		//Get XYZ coordinates from file line by line
		getline(infile, coordX);
		getline(infile, coordY);
		getline(infile, coordZ);
		//cout << coordX << " " << coordY << " " << coordZ << " " << endl;
		
		//Convert coordinates from string to double
		cX = atof(coordX.c_str());
		cY = atof(coordY.c_str());
		cZ = atof(coordZ.c_str());
		//cout << cX << " " << cY << " " << cZ << " " << endl;

		//Save in array
		cdX[loopz] = cX;
		cdY[loopz] = cY;
		cdZ[loopz] = cZ;
		//cout << coord[loopz] << " " << coord[loopz + 1] << " " << coord[loopz + 2] << " " << endl; 
	} 

	//Reset variables
	if (done < 1) done = 0; //done > -1
    point = 0;

	//Start timer 
	clock_t start = clock();

	//Get delta to follow trajectory and calculate error
	while (!done) {
		for (loopz = 0; loopz < limitz; ++loopz) {

			// Receive data from UNITY start

			//printf("Waiting for data...");
			fflush(stdout);

			//clear the buffer by filling null, it might have previously received data
			memset(buf, '\0', BUFLEN);

			//try to receive some data, this is a blocking call
			if ((recv_len = recvfrom(s, buf, BUFLEN, 0, (struct sockaddr *) &si_other, &slen)) == SOCKET_ERROR)
			{
				printf("recvfrom() failed with error code : %d", WSAGetLastError());
				exit(EXIT_FAILURE);
			}

			//print details of the client/peer and the data received
			//printf("Received packet from %s:%d\n", inet_ntoa(si_other.sin_addr), ntohs(si_other.sin_port));
			printf("Data: %s\n", buf);

			// Convert buf char to string
			cordSS << buf;
			cordSS >> cords;

			cordYYd = atof(cordYY.c_str());
			printf("%d", cordYYd);

			//Delta follows trajectory
			drdTrackPos(cordYYd, cordYYd, cordYYd);

			//now reply the client with the same data
			if (sendto(s, buf, recv_len, 0, (struct sockaddr*) &si_other, slen) == SOCKET_ERROR)
			{
				printf("sendto() failed with error code : %d", WSAGetLastError());
				exit(EXIT_FAILURE);
			}

			// REceive data from UNITY end


			
			//cout << coord[loopz] << " " << coord[loopz + 1] << " " << coord[loopz + 2] << " " << endl;

			//Read Delta Position
			dhdGetPosition(&posX, &posY, &posZ);
			//cout << posX << " " << posY << " " << posZ << " " << endl;

			//Get time taken
			timez[loopz] = ((double)clock() - start) / CLOCKS_PER_SEC;

			//Save delta position in 3 arrays
			deltaX[point] = posX;
			deltaY[point] = posY;
			deltaZ[point] = posZ;
			//cout << delta[point] << " " << delta[point+1] << " " << delta[point+2] << " " << endl; 

// HOW DOES THIS WORK?????? 
			//Calculate realtime error between target and delta 
			errorX = std::abs(cX - posX);
			errorY = std::abs(cY - posY);
			errorZ = std::abs(cZ - posZ);
			//cout << errorX << " " << errorY << " " << errorZ << " " << endl;

			//Save error values in 3 arrays
			errX[point] = errorX;
			errY[point] = errorY;
			errZ[point] = errorZ;
			++point;

			// check for exit condition
			if (!drdIsRunning()) done = -2;
			if (dhdKbHit()) {
				if (dhdKbGet() == 'q') done = -1;
			}

			// throttle trajectory updates to 20 Hz
			//printf ("control running at %0.03f kHz, tracing sphere surface...     \r", drdGetCtrlFreq());
			drdSleep(0.05); //controls speed of movement
		}
		if (loopz == limitz) done = -1;
	}
	
	// Write to outfiles
	//Column Headings
	T_data << Trial_Number << " " << Error_X  << " " << Error_Y << " " << Error_Z << " " << DeltaPos_X << " " << DeltaPos_Y << " " << DeltaPos_Z << " " << Time << " " << "\n";
	/* erroroutfile << trialnum;
	erroroutfile << "\n";
	outfile << trialnum;
	outfile << "\n";
	timer << trialnum;
	timer << "\n"; */
	// limitz size is important - based on length of trajectory in matlab
		for (point = 0; point < limitz; ++point) {
			/*//Write error 
			erroroutfile << errX[point];
			erroroutfile << "\n";
			erroroutfile << errY[point];
			erroroutfile << "\n";
			erroroutfile << errZ[point];
			erroroutfile << "\n";
			//Writes Delta Position 
			outfile << deltaX[point];
			outfile << "\n";
			outfile << deltaY[point];
			outfile << "\n";
			outfile << deltaZ[point];
			outfile << "\n";
			//Write timer
			timer << timez[point];
			timer << "\n";*/
			//When/if combining them into same file
			T_data << trialnum << " " << errX[point]  << " " << errY[point] << " " << errZ[point] << " " << deltaX[point] << " " << deltaY[point] << " " << deltaZ[point] << " " << timez[point] << " " << "\n";
		}

	// Calculate error thresholds and write to outfile
		maxerrX = *std::max_element(errX, errX + limitz);
		maxerrY = *std::max_element(errY, errY + limitz);
		maxerrZ = *std::max_element(errZ, errZ + limitz);
		minerrX = *std::min_element(errX, errX + limitz);
		minerrY = *std::min_element(errY, errY + limitz);
		minerrZ = *std::min_element(errZ, errZ + limitz);

		errorthresh << "Max X = " << maxerrX << "\n";
		errorthresh << "Max Y = " << maxerrY << "\n";
		errorthresh << "Max Z = " << maxerrZ << "\n";
		errorthresh << "Min X = " << minerrX << "\n";
		errorthresh << "Min Y = " << minerrY << "\n";
		errorthresh << "Min Z = " << minerrZ << "\n";

  // report control loop errors
  if (done == -2) printf ("error: robot control thread aborted (%s)\n", dhdErrorGetLastStr ());

  // close the connection
  printf ("cleaning up...                                             \n");
  drdClose ();

  // happily exit
  printf ("\ndone.\n");

  //Close open files
  infile.close(); 
 /* outfile.close();
  erroroutfile.close();
  timer.close(); */
  errorthresh.close();
  T_data.close();
  
  return 0;
}
