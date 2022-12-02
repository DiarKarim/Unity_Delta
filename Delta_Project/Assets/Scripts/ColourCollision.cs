using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColourCollision : MonoBehaviour
{

	public Material targetMat;
	public GameObject playerCenter;

	public void OnTriggerEnter (Collider col)
	{
		if (col.gameObject.tag == "PlayerCenter") 
		{
			targetMat.SetColor("_Color",Color.cyan);
			//player.gameObject.GetComponent<Renderer>().material.color = Color.cyan;
		}
	}

	public void OnTriggerStay (Collider col)
	{
		if (col.gameObject.tag == "PlayerCenter") {
			targetMat.SetColor("_Color",Color.cyan);
		} 
		//player.gameObject.GetComponent<Renderer>().material.color = Color.cyan;
	}

	public void OnTriggerExit (Collider col)
	{
		if (col.gameObject.tag == "PlayerCenter") {
			targetMat.SetColor("_Color",Color.white);
			//player.gameObject.GetComponent<Renderer>().material.color = Color.cyan;
		}
	}
		
	//public void target.activeInHierarchy = false

}
