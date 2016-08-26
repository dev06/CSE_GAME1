﻿using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {


	public ControllerProfile controllerProfile;
	public MenuActive menuActive;
	public bool TogglePlayerMovement;
	public GameObject Player;

	private ControllerProfile[] ControllerProfileList = { ControllerProfile.WASD, ControllerProfile.TGFH};

	private int _index;

	void Awake () {
		controllerProfile = ControllerProfile.WASD;
		menuActive = MenuActive.GAME;
		TogglePlayerMovement = true;
		Player = GameObject.FindGameObjectWithTag("Player");
		EnableGameUI();
	}

	void Update ()
	{
		SwitchControllerProfile();
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			StopCoroutine("WaitAndDisable");
			StartCoroutine("WaitAndDisable");
		}
	}

	IEnumerator WaitAndDisable()
	{

		yield return new WaitForSeconds((menuActive == MenuActive.GAME) ? 0f :  .7f);
		menuActive = (menuActive != MenuActive.CONTROL) ? MenuActive.CONTROL : MenuActive.GAME;
		EnableControlConfigUI(!GameObject.FindGameObjectWithTag("UI/ControlConfigCanvas").GetComponent<Canvas>().enabled);
		TogglePlayerMovement = !GameObject.FindGameObjectWithTag("UI/ControlConfigCanvas").GetComponent<Canvas>().enabled;

	}

	void EnableGameUI()
	{

		GameObject.FindGameObjectWithTag("UI/GameCanvas").GetComponent<Canvas>().enabled = true;
		GameObject.FindGameObjectWithTag("UI/ControlConfigCanvas").GetComponent<Canvas>().enabled = false;
	}

	void EnableControlConfigUI(bool value)
	{
		GameObject.FindGameObjectWithTag("UI/GameCanvas").GetComponent<Canvas>().enabled = !value;
		GameObject.FindGameObjectWithTag("UI/ControlConfigCanvas").GetComponent<Canvas>().enabled = value;
		GameObject.FindGameObjectWithTag("UI/ControlConfigCanvas").transform.FindChild("AlphaBackGround").transform.FindChild("ControlConfigBackground").gameObject.SetActive(value);

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
	CUSTOM,
}

public enum MenuActive
{
	GAME,
	CONTROL,
}
