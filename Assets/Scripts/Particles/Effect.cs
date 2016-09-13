//Devan Patel
//Applications and Scripting
//Sep.12.2016
using UnityEngine;
using System.Collections;

public class Effect : MonoBehaviour {


	public float life;

	void Start ()
	{

	}

	void Update ()
	{
		Destroy(gameObject, life);
	}
}
