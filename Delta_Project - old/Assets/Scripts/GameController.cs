//Master script to control Delta Project

/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Text;
using System;
using System.Globalization;
using System.Linq;

public class GameController : MonoBehaviour {

    //Trajectories
    public int traj = 0; //1-18 Trajectories
    public moveTarget movetarget;
    private List<string> trialData = new List<string> { };
    private string myPath;
    private int trialnum = 1;

    //Error
    private double error = 1; 


    // Use this for initialization
    void Start () {
        myPath = Application.dataPath;
    }
	
	// Update is called once per frame
	void FixedUpdate () {

        //Save data to a list

        trialData.Add(trialnum.ToString() + " " + traj.ToString());
        Debug.Log(movetarget.counter);
        int Counter = movetarget.counter;
        if (Counter > 254)
        {
            //Save list of data to text file
            File.WriteAllLines(myPath + trialnum.ToString() + ".txt", trialData.ToArray());

            trialData.Clear();
            ++traj;
            ++trialnum;
           
        }

    }
}

//Get counter from moveTarget  - needs fixing
//GameObject Target = GameObject.Find("Target");
//moveTarget targetmove = Target.GetComponent<moveTarget>();

//if (moveTarget.instance.counter > 254) */
