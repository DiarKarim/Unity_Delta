using System.Collections;
using System.IO.Ports; 
using System.Collections.Generic;
using System.IO;
using System.Text;
using System;

//using System.Globalization;
using UnityEngine;
using System.Linq;
using System.Threading;
using UnityEngine.UI;

// Moves the target in unity using the trajectories created in matlab
public class moveTarget : MonoBehaviour
{
	//VARIABLES

	//Input Information
	private string Pnum;
	private int cont;
	private Renderer playerRender;

	//Counters / indexes
	private int counter;
	//Counter 0-62 - move from position to outside of circle
	//Counter 63-254 - circle/ellipse trajectory
	private int traj = -1;
	//1-18 Trajectories
	private int trialnum = 0;

	//Text Files
	private List<string> trialData = new List<string> { };
	private string myPath;
	private int adapt = 0;
	private float startTime;
	private float elapsedTime;
	private int shock = 0;
	private string SHOCK = "ShockOff";
	private int sh = 0;

	//Trajectories & Target
	private int[] numbers;
	private int[] numberz;
	private int[] mixer;
	private int[] trialmix;
	private int shuffledtrajnum;
	private int[] tmix;
	private string tm;
	private string[] SN;
	private int[,] NumberZ;
	private int layer = 0;
	private int mixnum = 0;
	//private int j = 1;
	private string[][] T = new string[1953][];
	private string[][] N = new string[1953][];
	private string[] trialDatStr;
	private int num;
	private int trum;
	private Vector3 coords;
	private Vector3 targettraj;
	//private Vector3 startPos;
	public Vector3 StartPos;
	private int marker = 0;
	private int m = 1;
	private int next = 0;
	//Slerp function
	/*private Vector3 nextPos;
	private Vector3 slp;
	private float t;
	public float speed;
	private Vector3 vel = new Vector3(0.5f,0.5f,0.5f); 	*/

	//Delta Position
	private float posX;
	private float posY;
	private float posZ;
    
	//Error Calculations
	private float errorX;
	private float errorY;
	private float errorZ;
	private float error;

	//Stage 3 only
	public int repeatnum = 0;
	private int onetrial;
	//private int errorlength;
	private int level;
	private double score;
	private List<float> scoreData = new List<float> { };
	private double inbtwthresh = 0.006;
	private double averageerror;
	//Average error between all 3 axes
	private double minerrorthresh = 0.004967;
	private double maxerrorthresh = 0.007355;
	private List<float> errorData = new List<float> { };
	//private List<string> ErrorData = new List<string> { };
	private List<string> avError = new List<string> { };
	private float errorsmag;
	private SerialPort sport = new SerialPort ("COM4", 9600);
	float scoreSum;
	float scoreSize;
	float scoreAv;

	//Public Variables
	public InputField participantnum;
	public Text levelText;
	public Text scoreText;
	public int stage;
	//What stage of experiment
	public GameObject target;
	public GameObject player;
	public float scaleFactor;
	public GameObject MyCanvas;
	//public GameObject startPoint;
	public GameObject PlayerText;
	public GameObject StartingMarker;
	public float targetOffsetY;
	//public bool shock;
	public Material playerMat;
	public Vector3 errors;
	public int particpant; 
	public int assistance;
	public int AssOn;

	//Get assistance counters
	public UDPSend Assscript;  
	private int assX;
	private int assY;
	private int assZ;

	//private float[] part1; 

	void Start ()
	{

		//Set Player color to blue
		playerMat.SetColor ("_Color", Color.blue);

		// Load 18 trajectories into array T
		T [0] = File.ReadAllLines (@"D:\LizziePC\CoordinateFolder\T1_unity.txt");
		T [1] = File.ReadAllLines (@"D:\LizziePC\CoordinateFolder\T2_unity.txt");
		T [2] = File.ReadAllLines (@"D:\LizziePC\CoordinateFolder\T3_unity.txt");
		T [3] = File.ReadAllLines (@"D:\LizziePC\CoordinateFolder\T4_unity.txt");
		T [4] = File.ReadAllLines (@"D:\LizziePC\CoordinateFolder\T5_unity.txt");
		T [5] = File.ReadAllLines (@"D:\LizziePC\CoordinateFolder\T6_unity.txt");
		T [6] = File.ReadAllLines (@"D:\LizziePC\CoordinateFolder\T7_unity.txt");
		T [7] = File.ReadAllLines (@"D:\LizziePC\CoordinateFolder\T8_unity.txt");
		T [8] = File.ReadAllLines (@"D:\LizziePC\CoordinateFolder\T9_unity.txt");
		T [9] = File.ReadAllLines (@"D:\LizziePC\CoordinateFolder\T10_unity.txt");
		T [10] = File.ReadAllLines (@"D:\LizziePC\CoordinateFolder\T11_unity.txt");
		T [11] = File.ReadAllLines (@"D:\LizziePC\CoordinateFolder\T12_unity.txt");
		T [12] = File.ReadAllLines (@"D:\LizziePC\CoordinateFolder\T13_unity.txt");
		T [13] = File.ReadAllLines (@"D:\LizziePC\CoordinateFolder\T14_unity.txt");
		T [14] = File.ReadAllLines (@"D:\LizziePC\CoordinateFolder\T15_unity.txt");
		T [15] = File.ReadAllLines (@"D:\LizziePC\CoordinateFolder\T16_unity.txt");
		T [16] = File.ReadAllLines (@"D:\LizziePC\CoordinateFolder\T17_unity.txt");
		T [17] = File.ReadAllLines (@"D:\LizziePC\CoordinateFolder\T18_unity.txt");

		// Load shuffled orders for stage 3 into array N
		N [0] = File.ReadAllLines (@"D:\LizziePC\CoordinateFolder\S3_shufflednum\numshuff1_unity.txt");
		N [1] = File.ReadAllLines (@"D:\LizziePC\CoordinateFolder\S3_shufflednum\numshuff2_unity.txt");
		N [2] = File.ReadAllLines (@"D:\LizziePC\CoordinateFolder\S3_shufflednum\numshuff3_unity.txt");
		N [3] = File.ReadAllLines (@"D:\LizziePC\CoordinateFolder\S3_shufflednum\numshuff4_unity.txt");
		N [4] = File.ReadAllLines (@"D:\LizziePC\CoordinateFolder\S3_shufflednum\numshuff5_unity.txt");
		N [5] = File.ReadAllLines (@"D:\LizziePC\CoordinateFolder\S3_shufflednum\numshuff6_unity.txt");
		N [6] = File.ReadAllLines (@"D:\LizziePC\CoordinateFolder\S3_shufflednum\numshuff7_unity.txt");
		N [7] = File.ReadAllLines (@"D:\LizziePC\CoordinateFolder\S3_shufflednum\numshuff8_unity.txt");
		N [8] = File.ReadAllLines (@"D:\LizziePC\CoordinateFolder\S3_shufflednum\numshuff9_unity.txt");
		N [9] = File.ReadAllLines (@"D:\LizziePC\CoordinateFolder\S3_shufflednum\numshuff10_unity.txt");
		N [10] = File.ReadAllLines (@"D:\LizziePC\CoordinateFolder\S3_shufflednum\numshuff11_unity.txt");
		N [11] = File.ReadAllLines (@"D:\LizziePC\CoordinateFolder\S3_shufflednum\numshuff12_unity.txt"); 

		//Stimulator
		sport.Open ();
		sport.ReadTimeout = 2; 
			
			counter = 63;
			averageerror = inbtwthresh;
			assistance = 2;
			myPath = @"C:\Users\SyMon\Desktop\DeltaProject_Data\Dataz"; // Application.dataPath; //Path to save data
			Debug.Log ("Please submit the participant number to continue");

			//Randomly shuffle trajectory number
			//Number of repeats of trajectory depends on stage 
			if (stage == 1) { //Familiarity stage
				numbers = new[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17 };
				Shuffle (numbers);
			}

			if (stage == 2) { //Baseline measures stage
				numberz = new [] {
					0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17
				}; //For trajectories 
				mixer = new[] {
					2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1
				}; //For stimulation
				trialmix = new[] {
					0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 30, 31, 32, 33, 34, 35
				}; //Mix to randomise order
				Shuffle (trialmix);
			}

			if (stage == 3) { //Adaptive stage
				SN = N[particpant];
				NumberZ = new int[6, 3] { { 0, 1, 2 }, { 3, 4, 5 }, { 6, 7, 8 }, { 9, 10, 11 }, { 12, 13, 14 }, { 15, 16, 17 } };
				layer = 0;
				level = 1;
				score = 0;
				SetCountText ();
				traj = 0; //Need to set for first trial

			}
	}

	// Update is called once per frame
	void Update ()
	{
//		Debug.Log (Application.isPlaying);
//
//		if(!Application.isPlaying){
//			udpReceive.receiveThread.Abort();
//		}

		if (cont == 1) {
			if (stage == 1 || stage == 2) { // PILOT - Familiarity or Baseline measures stage
				if (Input.GetKeyDown ("]")) { //Go to next trial
					++traj;
					counter = 63; // To get circle to repeat not including moving from centre to outer circle
					++trialnum;
					//target.SetActive (true); //Activate target  
					//startTime = Time.time;
					Debug.Log ("Next Trial, Number:" + trialnum.ToString ());
					next = 1;
					marker = 0;
					m = 1;
				}

				if (Input.GetKeyDown ("[")) { //Go to previous trial
					counter = 63; // To get circle to repeat not including moving from centre to outer circle
					//target.SetActive (true); //Activate target
					//startTime = Time.time;
					if (trialnum > 1) { //Otherwise they go too low
						--traj;
						--trialnum; //Re-writes trial data already saved
					}
					Debug.Log ("Previous Trial, Number:" + trialnum.ToString ());
					next = 1;
					marker = 0;
					m = 1;
				}
			}

			if (stage == 3) //EXPERIMENT - Adaptive stage
{                            // If the trial increases or decreases in difficulty the trial number still increases

				if (Input.GetKeyDown ("]")) { //Press button to continue to next trial

					if (averageerror < minerrorthresh){ //|| repeatnum == 4){ //If error is low or trajectory has been repeated 5 times go to harder trial
						Debug.Log ("Next Trajectory");
						//Debug.Log ("AE:" + averageerror.ToString());
						++traj;
						counter = 63; // To get circle to repeat not including moving from centre to outer circle
						++trialnum;
						++mixnum;
						averageerror = inbtwthresh;
						Debug.Log ("Trajectory:" + (traj+1));
						//target.SetActive (true); //Activate target
						adapt = 3;
						errorData.Clear (); //Clear error list as difficulty has changed
						repeatnum = 0; //Reset repeatnum
						++repeatnum;
						Debug.Log ("Repeat Number:" + repeatnum);
						//startTime = Time.time;
						if (repeatnum == 1) //Level must increase
						{
							++level;
							SetCountText ();
						}
						if (traj == 18) {
							FinishStage(stage, trialnum, traj, repeatnum, cont);
						}

					} else if (averageerror > maxerrorthresh) { //Error is high go to easier trial
						Debug.Log ("Previous Trajectory");
						//Debug.Log ("AE:" + averageerror.ToString());
						--traj;
						repeatnum = 0; //Reset repeatnum
						++repeatnum;
						if (repeatnum == 1) //Level must decrease
						{
							--level;
						}
						if (traj == -1) { //Catch for first trajectory 
							traj = 0;
							layer = 0;
							mixnum = 0;
							level = 1;
						}
						if (traj == 0) {
							level = 1;
						}
						SetCountText ();
						counter = 63; // To get circle to repeat not including moving from centre to outer circle
						++trialnum; //Doesn't re-write the trial data already saved
						--mixnum;
						averageerror = inbtwthresh;
						Debug.Log ("Trajectory:" + (traj+1));
						//target.SetActive (true); //Activate target
						adapt = 1;
						errorData.Clear (); //Clear error list as difficulty has changed
						Debug.Log ("Repeat Number:" + repeatnum);
						//startTime = Time.time;

					} else {  //Difficulty doesnt change - Repeat same trajectory 
						Debug.Log ("Same Trajectory");
						counter = 63; // To get circle to repeat not including moving from centre to outer circle
						++trialnum; //Doesn't re-write the trial data already saved
						//target.SetActive (true); //Activate target
						//mixnum = 0;
						adapt = 2;
						++repeatnum;
						Debug.Log ("Repeat Number:" + repeatnum);
						//startTime = Time.time;
					}

					marker = 0; //Starting marker
					m = 1; //Starting marker
					next = 1; //Assigning traj and showing starting marker
					//Debug.Log (mixnum.ToString());
					if (mixnum == 3) { //If adapting up every third trajectory
						if (repeatnum == 1) {
							//Shuffle (tmix);
							++layer;
						}
						mixnum = 0;
					} 
					else if (mixnum == -1) //If adapting down every third trajectory
					{
						if (repeatnum == 1) 
						{
							if (traj == 0) 
							{
								layer = 0;
							} 
							else 
							{
								--layer;
							}
						}
						mixnum = 0;
					}
				}					
			}				
				
			if (next == 1) {
				//Assign trajectory
				var AJ = Assigntraj (trialDatStr, traj, trialmix, trum, num, numberz, T, numbers, layer, tmix, NumberZ, SN);
				trialDatStr = AJ.Key;
				num = AJ.Value;
				next = 2;
			}

			if (next == 2) {
		
				//Show starting marker
				var SM = StartMarker (trialDatStr, marker, m);
				marker = SM.Key;
				next = SM.Value;
				m++;				
			}

			if (target.activeInHierarchy) { //If the target is active
				try {
					if (marker == 1) { //After starting marker has been shown

						//Get target to follow the trajectory
						float x = float.Parse (trialDatStr [counter]);
						float y = float.Parse (trialDatStr [counter + 1]);
						float z = float.Parse (trialDatStr [counter + 2]);
						Vector3 coords = new Vector3 (x, y, z);
									
						//Set scale factor in unity
						//startPos = startPoint.transform.position; 			
						Vector3 targettraj = new Vector3 (coords.x, coords.y + targetOffsetY, coords.z) * scaleFactor;
						target.transform.position = targettraj;
						//target.transform.position = new Vector3(slp.x,slp.y+targetOffsetY,slp.z) * scaleFactor;
					
						counter += 3;
						//Debug.Log(counter);

						//Calculate error
						posX = player.transform.position.x; 
						posY = player.transform.position.y; 
						posZ = player.transform.position.z; 

						errorX = targettraj.x - posX;
						errorY = targettraj.y - posY;
						errorZ = targettraj.z - posZ;
						errors = new Vector3 (errorX, errorY, errorZ);
						//Debug.Log("Realtime Errors: " + errors.magnitude.ToString());

						//Debug.Log("Error: " + errors.ToString());

						errorsmag = errors.magnitude;

						//Absolute error values
						errorX = Mathf.Abs(errorX);
						errorY = Mathf.Abs(errorY);
						errorZ = Mathf.Abs(errorZ);

						//Debug.Log("Error: " + errorX.ToString() + " " + errorY.ToString() + " " + errorZ.ToString());

						//Save error data to error list
						//errorData.Add (errorX + errorY + errorZ);
						//scoreData.Add (errorX + errorY + errorZ);
						error = ((errorX + errorY + errorZ)/3);
						errorData.Add (error);
						scoreData.Add (error);

						//Stimulation
						//Function returns two values - key and pair
						var SH = Stimulation (stage, mixer, trum, sport, traj, shock, sh, trialmix, counter, num); 
						shock = SH.Key;
						sh = SH.Value;
						//Debug.Log(sh);

						//Assistance counters
						assX = Assscript.assX; 
						assY = Assscript.assY; 
						assZ = Assscript.assZ; 
                   
						//Save all data to a trial data list
						if (stage == 2 || stage == 3)
						{
							shuffledtrajnum = num + 1;
						} 
						else if (stage == 1) 
						{
							shuffledtrajnum = numbers [traj] + 1; //Get correct trajectory number
						} 
						elapsedTime = Time.time - startTime;
						trialData.Add (Pnum + " " + stage.ToString () + " " + trialnum.ToString () + " " + shuffledtrajnum.ToString () + " " + targettraj.x.ToString () + " " + targettraj.y.ToString () + " " + targettraj.z.ToString () + " " + posX.ToString () + " " + posY.ToString () + " " + posZ.ToString () + " " + errorX.ToString () + " " + errorY.ToString () + " " + errorZ.ToString () + " " + elapsedTime.ToString () + " " + errorsmag.ToString () + " " + sh.ToString() + " " + assX.ToString() + " " + assY.ToString() + " " + assZ.ToString());
						//trialData.Add(Pnum + " " + stage.ToString() + " " + trialnum.ToString() + " " + shuffledtrajnum.ToString() + " " + slp.x.ToString() + " " + (y+targetOffsetY).ToString() + " " + slp.z.ToString() + " " + posX.ToString() + " " + posY.ToString() + " " + posZ.ToString() + " " + errorX.ToString() + " " + errorY.ToString() + " " + errorZ.ToString() + " " + elapsedTime.ToString() + " " + errorsmag.ToString() );
					}
				} catch (IndexOutOfRangeException) { //If counter is bigger than array length
					Debug.Log ("Duration: " + elapsedTime.ToString ());
					playerMat.SetColor ("_Color", Color.blue);
					target.SetActive (false); //Deactivate target
					//StartCoroutine (nexttrial (shock, adapt, SHOCK, scoreData, minerrorthresh, repeatnum, onetrial, errorData, averageerror, errorlength, avError, Pnum, trialnum, shuffledtrajnum, myPath, trialData, scoreSum, scoreSize, scoreAv)); //Enter nexttrial function to save list to txt file
					StartCoroutine (nexttrial (shock, adapt, SHOCK));
					cont = FinishStage (stage, trialnum, traj, repeatnum, cont);
					next = 0;
					StartingMarker.SetActive (false);
					if (cont == 1) {
						Debug.Log ("Press button to continue");
					}
				}
			}

		}
	}

    //Functions

    //Save data and change trial
	//IEnumerator nexttrial(int shock, int adapt, string SHOCK, List<float> scoreData, double minerrorthresh, int repeatnum, int onetrial, List<float> errorData, int errorlength, List<string> avError, string Pnum, int trialnum, int shuffledtrajnum, string myPath, List<string> trialData, float scoreSum, float scoreSize, float scoreAv)
	IEnumerator nexttrial(int shock, int adapt, string SHOCK)
	{

		scoreAv = scoreData.Count > 0 ? scoreData.Average() : float.NaN; 

		Debug.Log ("Score Data: " + scoreAv.ToString()); 

		//Score - need to set appropriate boundaries 
		if (scoreAv <= minerrorthresh) {
			score = 5;
		} else if ((scoreAv <= 0.005445) && (scoreAv > minerrorthresh)) { 
			score = 4;
		} else if ((scoreAv <= 0.005922) && (scoreAv > 0.005445)) { //difference between is 
			score = 3;
		} else if ((scoreAv <= 0.063998) && (scoreAv > 0.005922)) {
			score = 2;
		} else if ((scoreAv <= 0.008774) && (scoreAv > 0.063998)) {
			score = 1;
		} else {
			score = 0;
		}

		SetCountText();
        scoreData.Clear();

        //For stage 3 
		if (repeatnum == 1) //Get length of list after one trial
        {
			onetrial = errorData.Count ();
			//onetrial = 630;
        }

        if (repeatnum > 3) //If repeatnum is 4 or 5 delete the first repeat in the list
        {
            errorData.RemoveRange(0, onetrial); //ErrorData only ever has 3 repeats of trajectory in
        }

        //Calculate average error of 3 repeats of the same trajectory 
        if (repeatnum > 2) //After trajectory has been performed 3+ times
        {
            //averageerror = errorData.Average();
			averageerror = errorData.Count > 0 ? errorData.Average() : float.NaN;
			//avError.Add(" " + averageerror.ToString());
        }

		Debug.Log ("Average Error:" + averageerror.ToString());

		//Convert shock for text file name
		if (shock == 1) 
		{
			SHOCK = "Shock";
		} 
		else if (shock == 2) 
		{
			SHOCK = "NoShock";
		}

		//errorlength = errorData.Count ();

		//Save once per trial data to list for stage 3
		if (stage == 3)
		{
			avError.Add (Pnum + " " + stage.ToString() + " " + trialnum.ToString() + " " + shuffledtrajnum.ToString() + " " + score.ToString () + " " + averageerror.ToString() + " " + adapt.ToString() + " " + shock.ToString() + " " + AssOn.ToString());
		File.WriteAllLines(myPath + Pnum + "_" + stage.ToString() + "_" + trialnum.ToString() + "_" + SHOCK + "_AE" + ".txt", avError.ToArray()); //Once per trial data
		//ErrorData.Add (errorlength.ToString () + " " + onetrial.ToString());
		//File.WriteAllLines(myPath + Pnum + "_" + stage.ToString() + "_" + trialnum.ToString() + "_" + SHOCK + "_ED" + ".txt", ErrorData.ToArray());
		}

		//Save data to text file
		File.WriteAllLines(myPath + Pnum + "_" + stage.ToString() + "_" + trialnum.ToString() + "_" + SHOCK + "_RT" + ".txt", trialData.ToArray()); //Realtime Data
		trialData.Clear(); // Clear list for next trial
		avError.Clear();
		Debug.Log("Saving Data");

        yield return null;
    }

    //Randomly shuffle int array
    public void Shuffle(int[] array)
    {
        System.Random random = new System.Random();
        int n = array.Count();

        while (n > 1)
        {
            n--;
            int i = random.Next(n + 1);
            int temp = array[i];
            array[i] = array[n];
            array[n] = temp;
        }
    }


    //Input experiment info
    public void OnSubmit() //Submit experimental info

    {
        //Set private variables to variables in text field
        Pnum = participantnum.text;
        Debug.Log(Pnum);
        Debug.Log("Please press ] to start");
        Debug.Log("Then do not press the button again until you are told.");
        cont = 1;
        MyCanvas.SetActive(false); //Hide text fields
        if (stage == 1 || stage == 2)
        {
            PlayerText.SetActive(false);
        }
    }

	public void SetCountText() //Update in game view
    {
        levelText.text = "Level: " + level.ToString() + "/18";
		scoreText.text = "Score: " + score.ToString() + "/5";
    }



	public void startStimulation(int stim, SerialPort sPort)
{
	// Input arduino stuff
	if(stim==1)
	{
		//Debug.Log (stim);
//		shock = 1;
		//Turn stimulator on
		sPort.Write("1");
//		string readArduino = sPort.ReadLine();
		//Debug.Log(readArduino);
	}
	else if (stim == 0)
	{
		//Turn stimulator off
		sPort.Write("2");
		//shock = 0; 
	}
			
}

	//Finishing function
	public int FinishStage(int stage, int trialnum, int traj, int repeatnum, int cont) 
	{
		if (stage == 1) 
		{
			if (trialnum == 18) 
			{
				target.SetActive(false);
				player.SetActive(false);
				Debug.Log ("Stage Finished.. Please stop play mode");
				cont = 2;
			}
		}
		if (stage == 2) 
		{
			if (trialnum == 36) 
			{
				target.SetActive(false);
				player.SetActive(false);
				Debug.Log ("Stage Finished.. Please stop play mode");
				cont = 2;
			}
		}
		if (stage == 3) 
		{
			if (traj == 18) //Must repeat the last trajectory 3 times before finish
			{
				target.SetActive(false);
				player.SetActive(false);
				Debug.Log ("Stage Finished.. Please stop play mode");
				cont = 2;
			}
		}
		return cont;
	}
		
	public KeyValuePair<string[],int> Assigntraj(string[] trialDatStr, int traj, int[] trialmix, int trum, int num, int[] numberz, string[][] T, int[] numbers, int layer, int[] tmix, int[,] NumberZ, string[] SN)
	{
		if (stage == 2) 
		{
			trum = trialmix [traj];
			//Debug.Log ("Trum" + trum.ToString ());
			num = numberz [trum];
			//string[] trialDatStr2 = T [numberz [0, trialmix [traj]]];
			trialDatStr = T [num];
			//trialDatStr = trialDatStr2;
		} 
		else if (stage == 1) 
		{
			trialDatStr = T [numbers [traj]];
			//trialDatStr = trialDatStr1;
		} 
		else if (stage == 3) 
		{
			float A = float.Parse(SN [traj]);
			//Debug.Log ("Traj:" + traj.ToString ());
			//Debug.Log ("A:" + A.ToString ());
			trum = Convert.ToInt16(A);
			//Debug.Log ("trum:" + trum.ToString ());
			//Debug.Log ("layer:" + layer.ToString ());
			num = NumberZ [layer, trum];
			trialDatStr = T [num];
			//trialDatStr = trialDatStr3;
			//Debug.Log ("trum:" + trum.ToString ());
			//Debug.Log ("num:" + num.ToString ());
		}
		return new KeyValuePair<string[],int>(trialDatStr, num);	
	}

	//Set starting marker to first point of trajectory
	public KeyValuePair<int,int> StartMarker(string[] trialDatStr, int marker, int m)
	{
		if (marker == 0) 
		{ 
			//StartingMarker.SetActive (true);
			if (m < 200)
			{
				StartingMarker.SetActive (true);
				float X = float.Parse (trialDatStr [63]);
				float Y = float.Parse (trialDatStr [64]);
				float Z = float.Parse (trialDatStr [65]);
				Vector3 StartPos = new Vector3 (X, Y, Z);
				StartingMarker.transform.position = new Vector3 (StartPos.x, StartPos.y + targetOffsetY, StartPos.z) * scaleFactor;
				next = 2;
				assistance = 2;
			}
		}
		if (m == 200) 
		{
			marker = 1;
			next = 0;
			StartingMarker.SetActive(false);
			target.SetActive (true); //Activate target 
			startTime = Time.time;
			//Turn Assistance on for assistance participants
			if (AssOn == 1) 
			{
				assistance = 1;
			} 
			else 
			{
				assistance = 2;
			}
		}
			
		return new KeyValuePair<int,int>(marker, next);
	}

	public KeyValuePair<int,int> Stimulation (int stage, int[] mixer, int trum, SerialPort sport, int traj, int shock, int sh, int[] trialmix, int counter, int num)
	{
		if (stage == 2) { //Stimulation on for every one of two repeats of each trajectory
			trum = trialmix [traj];
			//Debug.Log ("Trum" + trum.ToString ());
			int mixed = mixer [trum]; 
			//Debug.Log ("mixed" + mixed.ToString ());
			//Debug.Log ("traj" + traj.ToString ());

			if (mixed == 1) { //1 = stim, 2 = off
				if (num == 0 || num == 3 || num == 6 || num == 9 || num == 12 || num == 15) { //XY trajectories
					if (counter < 1444) { //1380+63
						startStimulation (1, sport); // 1 is on and 2 is off
						sh = 1;
					} else {
						startStimulation (2, sport); // 1 is on and 2 is off
						sh = 2;
					}
				}
				if (num == 1 || num == 4 || num == 7 || num == 10 || num == 13 || num == 16) { //YZ trajectories
					if (counter > 754 && counter < 2134) {  
						startStimulation (1, sport); // 1 is on and 2 is off
						sh = 1;
					} else {
						startStimulation (2, sport); // 1 is on and 2 is off
						sh = 2;
					}			
				}
				if (num == 2 || num == 5 || num == 8 || num == 11 || num == 14 || num == 17) { //ZX trajectories
					if (counter > 64 && counter < 754) {  
						startStimulation (1, sport); // 1 is on and 2 is off
						sh = 1;
					} else if (counter > 2134 && counter < 2761) {
						startStimulation (1, sport); // 1 is on and 2 is off
						sh = 1;				
					} else {
						startStimulation (2, sport); // 1 is on and 2 is off
						sh = 2;
					}			
				}
				shock = 1;
			} else if (mixed == 2) {
				shock = 2;
				sh = 2;
			}
		}
		if (stage == 3) {
			if (num == 0 || num == 3 || num == 6 || num == 9 || num == 12 || num == 15) { //XY trajectories
				if (counter < 1444) { 
					startStimulation (1, sport); // 1 is on and 2 is off
					sh = 1;
				} else {
					startStimulation (2, sport); // 1 is on and 2 is off
					sh = 2;
				}
			}
			if (num == 1 || num == 4 || num == 7 || num == 10 || num == 13 || num == 16) { //YZ trajectories
				if (counter > 754 && counter < 2134) {  
					startStimulation (1, sport); // 1 is on and 2 is off
					sh = 1;
				} else {
					startStimulation (2, sport); // 1 is on and 2 is off
					sh = 2;
				}			
			}
			if (num == 2 || num == 5 || num == 8 || num == 11 || num == 14 || num == 17) { //ZX trajectories
				if (counter > 64 && counter < 754) {  
					startStimulation (1, sport); // 1 is on and 2 is off
					sh = 1;
				} else if (counter > 2134 && counter < 2761) {
					startStimulation (1, sport); // 1 is on and 2 is off
					sh = 1;				
				} else {
					startStimulation (2, sport); // 1 is on and 2 is off
					sh = 2;
				}			
			}
			shock = 1;
		}

		return new KeyValuePair<int,int> (shock, sh);
	}
	}

/* //numbers = new[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17 };
tmix = new[] { 0, 1, 2 };
Shuffle (tmix);
NumberZ = new int[6, 3] { { 0, 1, 2 }, { 3, 4, 5 }, { 6, 7, 8 }, { 9, 10, 11 }, { 12, 13, 14 }, { 15, 16, 17 } };
layer = 0;
level = 1;
score = 0;
SetCountText ();
traj = 0; //Need to set for first trial */

//trum = tmix [traj];
/*trum = tmix [mixnum];
			num = NumberZ [layer, trum];
			trialDatStr = T [num]; */


//						if (traj == 8)
//						{
//Vector3 targettraj1 = new Vector3 (coords.x, coords.y + targetOffsetY, coords.z) * scaleFactor;
//Vector3 targettraj = new Vector3((0.261f/0.38f) * targettraj1.x, targettraj1.y, (0.261f/0.38f) * targettraj1.z);
//Debug.Log(targettraj);
//						}
//						else
//						{
//							Debug.Log(targettraj);
//
//						}



/*					//Slerp function
float x2 = float.Parse(trialDatStr[counter + 3]);
float y2 = float.Parse(trialDatStr[counter + 4]);
float z2 = float.Parse(trialDatStr[counter + 5]);
Vector3 nextPos = new Vector3(x2, y2, z2);

//					float x3 = float.Parse(trialDatStr[counter + 1800]);
//					float y3 = float.Parse(trialDatStr[counter + 1801]);
//					float z3 = float.Parse(trialDatStr[counter + 1802]);
//					Vector3 lastCoords = new Vector3(x3, y3, z3);

//float fracComplete = (Time.time - startTime) / 1f;
//Vector3 slp = Vector3.Slerp(coords, nextPos, 0.1f); 
t += Time.deltaTime; 
Vector3 slp = Vector3.Lerp (coords, nextPos, t / speed); */




		
	/* public KeyValuePair<int,int> Stimulation(int stage, int[] mixer, int trum, SerialPort sport, float errorsmag, int traj, float posX, float posY, float posZ, int shock, int sh, int[] trialmix )
	{

		if (stage == 2) //Stimulation on for every one of two repeats of each trajectory
		{
			trum = trialmix [traj];
			//Debug.Log ("Trum" + trum.ToString ());
			int mixed = mixer [trum]; 
			//Debug.Log ("mixed" + mixed.ToString ());

			if (mixed == 1) 
			{ //1 = stim, 2 = off
				if (traj == 0 || traj == 3 || traj == 6 || traj == 9 || traj == 12 || traj == 15) //XY trajectories
				{
					if (posY > ((0 + targetOffsetY) * scaleFactor)) 
					{
						startStimulation (1, sport); // 1 is on and 2 is off
						sh = 1;
					} 
					else 
					{
						startStimulation (2, sport); // 1 is on and 2 is off
						sh = 2;
					}
				}
				else if (traj == 1 || traj == 10 || traj == 16) //YZ circle S, ellipse S and L 
				{
					if (posZ < (0 * scaleFactor)) 
					{
						startStimulation (1, sport); // 1 is on and 2 is off
						sh = 1;
					} 
					else 
					{
						startStimulation (2, sport); // 1 is on and 2 is off
						sh = 2;
					}
				}
				else if (traj == 4 || traj == 13) //YZ circle M, ellipse M 
				{
					if (posZ < (-0.02 * scaleFactor)) 
					{
						startStimulation (1, sport); // 1 is on and 2 is off
						sh = 1;
					} 
					else 
					{
						startStimulation (2, sport); // 1 is on and 2 is off
						sh = 2;
					}
				}
				else if (traj == 7) //YZ circle L 
				{
					if (posZ < (-0.04 * scaleFactor)) 
					{
						startStimulation (1, sport); // 1 is on and 2 is off
						sh = 1;
					} 
					else 
					{
						startStimulation (2, sport); // 1 is on and 2 is off
						sh = 2;
					}
				}
				else if (traj == 2 || traj == 11 || traj == 17) //ZX circle S, ellipse S and L 
				{
					if (posZ > (0 * scaleFactor)) 
					{
						startStimulation (1, sport); // 1 is on and 2 is off
						sh = 1;
					} 
					else 
					{
						startStimulation (2, sport); // 1 is on and 2 is off
						sh = 2;
					}
				}
				else if (traj == 5 || traj == 14) //ZX circle M, ellipse M 
				{
					if (posZ > (-0.02 * scaleFactor)) 
					{
						startStimulation (1, sport); // 1 is on and 2 is off
						sh = 1;
					} 
					else 
					{
						startStimulation (2, sport); // 1 is on and 2 is off
						sh = 2;
					}
				}
				else if (traj == 8) //ZX circle L 
				{
					if (posZ > (-0.04 * scaleFactor)) 
					{
						startStimulation (1, sport); // 1 is on and 2 is off
						sh = 1;
					} 
					else 
					{
						startStimulation (2, sport); // 1 is on and 2 is off
						sh = 2;
					}
				}
							
				shock = 1;
			} 
			else if (mixed == 2)
			{
				shock = 2;
				sh = 2;
			}
		}
		if (stage == 3) 
		{
			if (traj == 0 || traj == 3 || traj == 6 || traj == 9 || traj == 12 || traj == 15) //XY trajectories
			{
				if (posY > ((0 + targetOffsetY) * scaleFactor)) 
				{
					startStimulation (1, sport); // 1 is on and 2 is off
					sh = 1;
				} 
				else 
				{
					startStimulation (2, sport); // 1 is on and 2 is off
					sh = 2;
				}
			}
			else if (traj == 1 || traj == 10 || traj == 16) //YZ circle S, ellipse S and L 
			{
				if (posZ < (0 * scaleFactor)) 
				{
					startStimulation (1, sport); // 1 is on and 2 is off
					sh = 1;
				} 
				else 
				{
					startStimulation (2, sport); // 1 is on and 2 is off
					sh = 2;
				}
			}
			else if (traj == 4 || traj == 13) //YZ circle M, ellipse M 
			{
				if (posZ < (-0.02 * scaleFactor)) 
				{
					startStimulation (1, sport); // 1 is on and 2 is off
					sh = 1;
				} 
				else 
				{
					startStimulation (2, sport); // 1 is on and 2 is off
					sh = 2;
				}
			}
			else if (traj == 7) //YZ circle L 
			{
				if (posZ < (-0.04 * scaleFactor)) 
				{
					startStimulation (1, sport); // 1 is on and 2 is off
					sh = 1;
				} 
				else 
				{
					startStimulation (2, sport); // 1 is on and 2 is off
					sh = 2;
				}
			}
			else if (traj == 2 || traj == 11 || traj == 17) //ZX circle S, ellipse S and L 
			{
				if (posZ > (0 * scaleFactor)) 
				{
					startStimulation (1, sport); // 1 is on and 2 is off
					sh = 1;
				} 
				else 
				{
					startStimulation (2, sport); // 1 is on and 2 is off
					sh = 2;
				}
			}
			else if (traj == 5 || traj == 14) //ZX circle M, ellipse M 
			{
				if (posZ > (-0.02 * scaleFactor)) 
				{
					startStimulation (1, sport); // 1 is on and 2 is off
					sh = 1;
				} 
				else 
				{
					startStimulation (2, sport); // 1 is on and 2 is off
					sh = 2;
				}
			}
			else if (traj == 8) //ZX circle L 
			{
				if (posZ > (-0.04 * scaleFactor)) 
				{
					startStimulation (1, sport); // 1 is on and 2 is off
					sh = 1;
				} 
				else 
				{
					startStimulation (2, sport); // 1 is on and 2 is off
					sh = 2;
				}
			}

			shock = 1;
		} 
			
		return new KeyValuePair<int,int>(shock, sh);
	} */



//Don't Delete
/* public string Stimulation(int stage, int[] mixer, int trum, SerialPort sport, float errorsmag, string shock )
	{
		if (stage == 2) //Stimulation on for every one of two repeats of each trajectory
		{
			if (mixer [trum] == 1) 
			{ //1 = stim, 2 = off
				if (errorsmag < 0.9) 
				{ //Stimulation dependent on postion of delta //0.795
					// Electrical stimulation (EMS)
					startStimulation (1, sport); // 1 is on and 2 is off
				} 
				else 
				{
					startStimulation (2, sport); // 1 is on and 2 is off
				}
				shock = "shock";
			} 
			else
			{
				shock = "No shock";
			}
		}
		if (stage == 3) 
		{
			if (errorsmag < 0.9) { //Stimulation dependent on postion of delta //0.795
				// Electrical stimulation (EMS)
				startStimulation (1, sport); // 1 is on and 2 is off
			} 
			else 
			{
				startStimulation (2, sport); // 1 is on and 2 is off
			}
			shock = "shock";
		}
		return shock;
	} */


  //Turn stimulator on and off - potentailly need to move 
/* if (stage == 2)
{
	if (stim[1, numbers[traj]] == 2)
	{
		//Turn stimulator on
		sPort.Write("1");
		string readArduino = sPort.ReadLine();
		Debug.Log(readArduino);
	}
	else
	{
		//Turn stimulator off
		sPort.Write("2");
	}
} */


//Select trajectory that target follows
//T = string.Format("T{0}", traj);
//trialDatStr = new String[] { T };
//trialDatStr = new String() <"T">;
//string t = traj.ToString();
//string Tt = string.Concat(T, t);
//trialDatStr = new String[] { Tt };

//Moving traj back to origin
//private Vector3 originalPos;
//originalPos = target.transform.position;
//coords = new Vector3(0f,0f,0f);
//target.transform.Translate(coords * scaleFactor);
//target.transform.Translate(originalPos);


/*using (StreamWriter dataz = new StreamWriter("Data.txt"))
{
    dataz.WriteLine(trajectNum.ToArray());
}*/

//Traj is determined in GameController - needs fixing
//GameObject Controller = GameObject.Find("Controller");
//GameController gamecontroller = Controller.GetComponent<GameController>();

//public GameController gamecontroller;
//trialDatStr = T[gamecontroller.traj];

//shuffled = numbers2.OrderBy(n => Guid.NewGuid()).ToArray();

//Stimulation

//Stimulation - error.magnitude is realtime error
/*if (errorsmag < 0.795) {
	// Electrical stimulation (EMS)
	startStimulation (1, sport); // 1 is on and 2 is off

} else {
	startStimulation (2, sport); // 1 is on and 2 is off
}*/

//mixer = new int[] { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2 };
//stim = new int[3, 36] { { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17 }, { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2 }};
//for (int i = 0; i < 37; i++) 
//{
//	stim [1, i] = trialmix [i];
//}
//Randomly shuffle 1 and 2 to determine with or without stimulation
/* stim = new int[2, 18] { { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17 }, { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 } };
            ss = new int[] { 1, 1, 1, 1, 1, 1, 1, 1, 1, 2, 2, 2, 2, 2, 2, 2, 2, 2 };
            Shuffle(ss);
            for (int i = 0; i < 18; i++)
            {
                stim[1, i] = ss[i];
                /*if (stim[1, i] == 1)
                {
                    stim[2, i] = 2;
                }
                else
                {
                    stim[2, i] = 1;
                }
            } */

// arrStimuli = [1-18][1-18]
// mixer = [1-36]; ints of half zeros and the other half ones
// arrTrialNum = [1-36]; ints of 1-36 (shuffle this only)

//elapsedTime = Time.time - startTime;
//		Debug.Log (elapsedTime);
//trialData.Add(elapsedTime.ToString());

//Debug.Log("StartP: " + startPos.ToString() + " TargetP: "+ target.transform.position.ToString()); 
//Debug.Log(x.ToString() + " " + y.ToString() + " " + z.ToString());
//target.transform.Translate((coords-startPos) * scaleFactor); //target.transform.Translate(coords * speed * Time.deltaTime);

/*tmix1 = new [] { 0, 1, 2 };
			tmix2 = new[] { 0, 1, 2 };
			tmix3 = new[] { 0, 1, 2 };
			tmix4 = new[] { 0, 1, 2 };
			tmix5 = new[] { 0, 1, 2 };
			tmix6 = new[] { 0, 1, 2 };
			Shuffle (tmix1);
			Shuffle (tmix2);
			Shuffle (tmix3);
			Shuffle (tmix4);
			Shuffle (tmix5);
			Shuffle (tmix6);

			//tmix = new int [6][3] {{ tmix1 }, { tmix2 }, { tmix3 }, { tmix4 }, { tmix5 }, { tmix6 }} ;

			/*for (int j = 0; j > 6; j++)
			{
				Shuffle (tmix1);
				for(int i = 0; i > 3; i++)
				{
					tmix = new int[j,i] {tmix1[i]};
				}
			}

			TMIX.Add (tmix1 [0].ToString () + tmix1 [1].ToString () + tmix1 [2].ToString () + tmix2 [0].ToString () + tmix2 [1].ToString () + tmix2 [2].ToString () + tmix3 [0].ToString () + tmix3 [1].ToString () + tmix3 [2].ToString () + tmix4 [0].ToString () + tmix4 [1].ToString () + tmix4 [2].ToString () + tmix5 [0].ToString () + tmix5 [1].ToString () + tmix5 [2].ToString () + tmix6 [0].ToString () + tmix6 [1].ToString () + tmix6 [2].ToString ());
				//Make list of tmix1 x6 all individually shuffled
			
	
			File.WriteAllLines(myPath + Pnum + "_" + "_TMIX" + ".txt", TMIX.ToArray());*/

/*public KeyValuePair<int[],int> shufflemixer(int j, int next)
{
	//tmix = new[] { 0, 1, 2 };
	tmix1 = new [] { 0, 1, 2 };
	tmixint = new[]  {0, 0, 0};
	if (j < 7) {
		tmix1 = new [] { 0, 1, 2 };
		Shuffle (tmix1);
		TMIX.Add (tmix1 [0].ToString () + tmix1 [1].ToString () + tmix1 [2].ToString ());
	} 
	if (j == 6) 
	{
		File.WriteAllLines (myPath + Pnum + "_" + "_TMIX" + ".txt", TMIX.ToArray ());
		tmixarr = TMIX.ToArray ();
		tmixint = Array.ConvertAll (tmixarr, int.Parse);
		//Debug.Log (tmixint [0] + tmixint [1] + tmixint [2] + tmixint [3] + tmixint [4] + tmixint [5] + tmixint [6] + tmixint [7] + tmixint [8] + tmixint [9]);
		next = 1; //Assigning traj and showing starting marker
	}

	return new KeyValuePair<int[],int>(tmixint, next);


	//TMIX.Add (tmix1 [0].ToString () + tmix1 [1].ToString () + tmix1 [2].ToString () + tmix2 [0].ToString () + tmix2 [1].ToString () + tmix2 [2].ToString () + tmix3 [0].ToString () + tmix3 [1].ToString () + tmix3 [2].ToString () + tmix4 [0].ToString () + tmix4 [1].ToString () + tmix4 [2].ToString () + tmix5 [0].ToString () + tmix5 [1].ToString () + tmix5 [2].ToString () + tmix6 [0].ToString () + tmix6 [1].ToString () + tmix6 [2].ToString ());
	//TMIX.Add (tmix1 [0,0].ToString () + tmix1 [0,1].ToString () + tmix1 [0,2].ToString () + tmix2 [1,0].ToString () + tmix2 [1,1].ToString () + tmix2 [1,2].ToString () + tmix3 [2,0].ToString () + tmix3 [2,1].ToString () + tmix3 [2,2].ToString () + tmix4 [3,0].ToString () + tmix4 [3,1].ToString () + tmix4 [3,2].ToString () + tmix5 [4,0].ToString () + tmix5 [4,1].ToString () + tmix5 [4,2].ToString () + tmix6 [5,0].ToString () + tmix6 [5,1].ToString () + tmix6 [5,2].ToString ());
	//Make list of tmix1 x6 all individually shuffled
}*/
