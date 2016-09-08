using UnityEngine;
using System.Collections;

public class Effect : MonoBehaviour {

	// Use this for initialization
	public float life;
	void Start () {

	}

	// Update is called once per frame
	void Update ()
	{
		Destroy(gameObject, life);
	}
}
