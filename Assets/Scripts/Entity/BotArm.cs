using UnityEngine;
using System.Collections;

public class BotArm : MonoBehaviour {

	// Use this for initialization
	private GameObject _camera;
	void Start () {
		_camera = GameObject.FindWithTag("Player");
	}

	// Update is called once per frame
	void Update () {

	}

	void OnCollisionEnter(Collision col)
	{

	}
}
