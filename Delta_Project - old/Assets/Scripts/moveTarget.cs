using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Text;
using System;
using System.Globalization;
using System.Linq;
using System.Threading;


// Moves the target in unity using the trajectories created in matlab


public class moveTarget : MonoBehaviour
{
    //Counter 0-62 - move from position to outside of circle
    //Counter 63-254 - circle/ellipse trajectory
    private int counter;
    private int traj = 0; //1-18 Trajectories
    private Vector3 coords;
    private string[][] T = new string[254][];
    private string[] trialDatStr;
    private int trialnum = 0;
    private List<string> trialData = new List<string> { };
    private string myPath;
    private int[] numbers;
    //private int[,] stim;
    private int shuffledtrajnum;


    public int stage = 1; //What stage of experiment
    public GameObject target;
    public float scaleFactor = 0.2f;


    // Use this for initialization
    void Start()
    {

        // Load 18 trajectories into array T
        T[0] = File.ReadAllLines(@"c:\Users\Lizzie\Desktop\CoordinateFolder\T1_unity.txt");
        T[1] = File.ReadAllLines(@"c:\Users\Lizzie\Desktop\CoordinateFolder\T2_unity.txt");
        T[2] = File.ReadAllLines(@"c:\Users\Lizzie\Desktop\CoordinateFolder\T3_unity.txt");
        T[3] = File.ReadAllLines(@"c:\Users\Lizzie\Desktop\CoordinateFolder\T4_unity.txt");
        T[4] = File.ReadAllLines(@"c:\Users\Lizzie\Desktop\CoordinateFolder\T5_unity.txt");
        T[5] = File.ReadAllLines(@"c:\Users\Lizzie\Desktop\CoordinateFolder\T6_unity.txt");
        T[6] = File.ReadAllLines(@"c:\Users\Lizzie\Desktop\CoordinateFolder\T7_unity.txt");
        T[7] = File.ReadAllLines(@"c:\Users\Lizzie\Desktop\CoordinateFolder\T8_unity.txt");
        T[8] = File.ReadAllLines(@"c:\Users\Lizzie\Desktop\CoordinateFolder\T9_unity.txt");
        T[9] = File.ReadAllLines(@"c:\Users\Lizzie\Desktop\CoordinateFolder\T10_unity.txt");
        T[10] = File.ReadAllLines(@"c:\Users\Lizzie\Desktop\CoordinateFolder\T11_unity.txt");
        T[11] = File.ReadAllLines(@"c:\Users\Lizzie\Desktop\CoordinateFolder\T12_unity.txt");
        T[12] = File.ReadAllLines(@"c:\Users\Lizzie\Desktop\CoordinateFolder\T13_unity.txt");
        T[13] = File.ReadAllLines(@"c:\Users\Lizzie\Desktop\CoordinateFolder\T14_unity.txt");
        T[14] = File.ReadAllLines(@"c:\Users\Lizzie\Desktop\CoordinateFolder\T15_unity.txt");
        T[15] = File.ReadAllLines(@"c:\Users\Lizzie\Desktop\CoordinateFolder\T16_unity.txt");
        T[16] = File.ReadAllLines(@"c:\Users\Lizzie\Desktop\CoordinateFolder\T17_unity.txt");
        T[17] = File.ReadAllLines(@"c:\Users\Lizzie\Desktop\CoordinateFolder\T18_unity.txt");

        counter = 63;
        myPath = Application.dataPath; //Path to save data

        //Randomly shuffle trajectory number
        //Number of repeats of trajectory depends on stage 
        if (stage == 1) //Familiarity stage
        {
            numbers = new[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17 };
            //Debug.Log(numbers.ToString());
            Shuffle(numbers);
            //Debug.Log(numbers.ToString());
            //shuffled = numbers1.OrderBy(n => Guid.NewGuid()).ToArray();
        }

        if (stage == 2) //Baseline measures stage
        {
            numbers = new[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17 };
            Shuffle(numbers);
            //stim = new int[2,18] { { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17 }, { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 } };
            //shuffled = numbers2.OrderBy(n => Guid.NewGuid()).ToArray();
        }

        if (stage == 3) //Adaptive stage
        {
            numbers = new[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17 };
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (counter < 254)
        {
            //Assign randomly shuffled trajectory
            string[] trialDatStr = T[numbers[traj]];

            //Get target to follow the trajectory
            float x = float.Parse(trialDatStr[counter]);
            float y = float.Parse(trialDatStr[counter + 1]);
            float z = float.Parse(trialDatStr[counter + 2]);
            Vector3 coords = new Vector3(x, y, z);
            //Set scale factor in unity
            target.transform.Translate(coords * scaleFactor); //target.transform.Translate(coords * speed * Time.deltaTime);
            counter += 3;

            //Save data to a list
            shuffledtrajnum = numbers[traj] + 1; //Get correct trajectory number
            trialData.Add(stage.ToString() + " " + trialnum.ToString() + " " + shuffledtrajnum.ToString());

        }
        else if (counter > 254)
        {
            if (Input.GetKeyDown("]")) //Go to next trial
            {
                ++traj;
                counter = 63; // To get circle to repeat not including moving from centre to outer circle
                //counter = 0; // Includes line to outside of circle
                ++trialnum;
                StartCoroutine(nexttrial());
            }
            else if (Input.GetKeyDown("[")) //Go to previous trial
            {
                --traj;
                counter = 63; // To get circle to repeat not including moving from centre to outer circle
                //counter = 0; // Includes line to outside of circle
                --trialnum;
                StartCoroutine(nexttrial());
            }

            Debug.Log("Wait for button press...");
        }
        
    }

    //Functions

    //Save data and change trial
    IEnumerator nexttrial()
    {
        //Save data to text file
        File.WriteAllLines(myPath + stage.ToString() + "_" + trialnum.ToString() + ".txt", trialData.ToArray());
        trialData.Clear(); // Clear list for next trial
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

}



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
