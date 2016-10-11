using UnityEngine;
using System.Collections;

public class SpeedBuff : Buff {

	void Start ()
	{
		_duration = 5.0f;
	}

	void Update ()
	{

	}


	public override void UseBuff()
	{
		Debug.Log("Now using speed buff");
	}


}
