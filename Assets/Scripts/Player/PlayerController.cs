using UnityEngine;
using System.Collections;

public class PlayerController : Mob {


	void Start ()
	{
		Health = 100;
	}

	// Update is called once per frame
	void Update ()
	{


	} void OnCollisionEnter(Collision collision) {
		Debug.Log("hy8it");
	}




}
