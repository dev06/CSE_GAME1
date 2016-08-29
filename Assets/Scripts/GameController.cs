using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {


	public ControllerProfile controllerProfile;
	public static ButtonID selectedButtonID;
	public MenuActive menuActive;
	public KeyCode[] customKey;
	public bool TogglePlayerMovement;
	public bool ToggleMouseControl;
	public GameObject Player;

	private GameObject _largeProjectile;

	private ControllerProfile[] ControllerProfileList = { ControllerProfile.WASD, ControllerProfile.TGFH};

	private int _index;

	void Awake () {
		controllerProfile = ControllerProfile.WASD;
		menuActive = MenuActive.GAME;
		customKey = new KeyCode[8];
		TogglePlayerMovement = true;
		_largeProjectile = (GameObject)Resources.Load("Prefabs/LargeProjectile");
		Player = GameObject.FindGameObjectWithTag("Player");
		EnableGameUI();
	}

	void Update ()
	{
		SwitchControllerProfile();
		ShootProjectile();
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

	void ShootProjectile()
	{
		if (Input.GetMouseButtonDown(0))
		{
			GameObject _projectile = Instantiate(_largeProjectile, Player.transform.GetChild(0).transform.position, Quaternion.identity) as GameObject;
			Vector3 _velocity = Player.transform.GetChild(0).transform.forward * 50;

			_projectile.GetComponent<Rigidbody>().velocity = _velocity;
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
