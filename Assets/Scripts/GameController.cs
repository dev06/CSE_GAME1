using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {


	public ControllerProfile controllerProfile;
	public GameObject Player;

	private ControllerProfile[] ControllerProfileList = { ControllerProfile.WASD, ControllerProfile.TGFH};

	private int _index;

	void Awake () {
		controllerProfile = ControllerProfile.WASD;
		Player = GameObject.FindGameObjectWithTag("Player");
	}

	void Update ()
	{
		SwitchControllerProfile();
	}

	void SwitchControllerProfile()
	{
		if (Input.GetKey(KeyCode.LeftControl))
		{
			if (Input.GetKeyDown(KeyCode.Q))
			{
				_index = (_index < ControllerProfileList.Length - 1) ? _index + 1 : 0;
				controllerProfile = ControllerProfileList[_index];
			}
		}
	}
}

public enum ControllerProfile
{
	WASD,
	TGFH,
}
