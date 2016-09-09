﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class GameController : MonoBehaviour {


	#region ----------- PUBLIC MEMBERS----------
	public ControllerProfile controllerProfile;
	public static ButtonID selectedButtonID;
	public MenuActive menuActive;
	[HideInInspector]
	public KeyCode[] customKey;
	[HideInInspector]
	public bool TogglePlayerMovement;
	[HideInInspector]
	public bool ToggleMouseControl;
	public bool SpawnEnemy;
	public bool KeepSpawning;
	[HideInInspector]
	public GameObject Player;
	[HideInInspector]
	public int botCounter;
	[HideInInspector]
	public GameObject activeEntities;
	[HideInInspector]
	public float TotalEnemiesSpawned;
	#endregion ----------- /PUBLIC MEMBERS----------


	#region------PRIVATE MEMBERS------------
	private GameObject _largeProjectile;
	private GameObject _smallProjectile;
	private GameObject _bot;
	private float _botSpawnCounter;
	private GameObject _smoke;
	private ControllerProfile[] ControllerProfileList = { ControllerProfile.WASD, ControllerProfile.TGFH};
	private int _index;
	private Image _blankImage;
	#endregion------/PRIVATE MEMBERS------------


	void Awake () {
		SetCursorTexture((Texture2D)Resources.Load("UI/cursor"));
		controllerProfile = ControllerProfile.WASD;
		menuActive = MenuActive.MENU;
		customKey = new KeyCode[8];
		TogglePlayerMovement = true;
		_largeProjectile = (GameObject)Resources.Load("Prefabs/LargeProjectile");
		_smallProjectile = (GameObject)Resources.Load("Prefabs/SmallProjectile");
		_smoke = (GameObject)Resources.Load("Prefabs/Particles/Smoke");
		_bot = (GameObject)Resources.Load("Prefabs/Bot");
		_blankImage = GameObject.FindWithTag("UI/GameCanvas").transform.FindChild("Blank").GetComponent<Image>();
		activeEntities = GameObject.FindWithTag("ActiveEntities");
		Player = GameObject.FindGameObjectWithTag("Player");
		EnableMenu(MenuActive.MENU);

	}


	void Update ()
	{

		ShootProjectile();
		SpawnBots(Constants.StartBotSpawningDelay, Constants.BotSpawnDelay, KeepSpawning);
		DecreaseGameCanvasBlankAlpha();
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			Application.Quit();
		}
	}


	IEnumerator WaitAndDisable()
	{
		yield return new WaitForSeconds((menuActive == MenuActive.GAME) ? 0f :  .7f);
		menuActive = (menuActive != MenuActive.CONTROL) ? MenuActive.CONTROL : MenuActive.GAME;
	}

	public void EnableMenu(MenuActive _menu)
	{

		switch (_menu)
		{
			case MenuActive.GAME:
				ActivateUICanvas(false, "GameCanvas");
				GameObject.FindGameObjectWithTag("UI/GameCanvas").GetComponent<Canvas>().enabled = true;
				GameObject.FindGameObjectWithTag("UI/ControlConfigCanvas").GetComponent<Canvas>().enabled = true;
				menuActive = MenuActive.GAME;
				break;
			case MenuActive.MENU:
				GameObject.FindGameObjectWithTag("UI/MenuCanvas").GetComponent<Canvas>().enabled = true;
				ActivateUICanvas(false, "MenuCanvas");
				menuActive = MenuActive.MENU;
				break;
			case MenuActive.RETRY:
				GameObject.FindGameObjectWithTag("UI/RetryCanvas").GetComponent<Canvas>().enabled = true;
				ActivateUICanvas(false, "RetryCanvas");
				menuActive = MenuActive.RETRY;
				break;
		}


	}


	void ActivateUICanvas(bool b, string _exception)
	{
		int length = GameObject.FindWithTag("UI").transform.childCount;
		for (int i = 0; i < length; i++)
		{
			if (GameObject.FindWithTag("UI").transform.GetChild(i).name != _exception)
			{
				GameObject.FindWithTag("UI").transform.GetChild(i).GetComponent<Canvas>().enabled = b;
			}
		}
	}

	void EnableControlConfigUI(bool value)
	{
		GameObject.FindGameObjectWithTag("UI/GameCanvas").GetComponent<Canvas>().enabled = !value;
		GameObject.FindGameObjectWithTag("UI/ControlConfigCanvas").GetComponent<Canvas>().enabled = value;
		GameObject.FindGameObjectWithTag("UI/ControlConfigCanvas").transform.FindChild("AlphaBackGround").transform.FindChild("ControlConfigBackground").gameObject.SetActive(value);

	}



	void ShootProjectile()
	{
		if (Input.GetMouseButtonDown(1))
		{
			Transform _camTransform = Camera.main.transform;
			int _bulletPos = Random.Range(_camTransform.childCount - 1, _camTransform.childCount - 3);


			GameObject _l_projectile = Instantiate(_largeProjectile, _camTransform.GetChild(_bulletPos).transform.position, Quaternion.identity) as GameObject;
			_l_projectile.GetComponent<Projectile>().forward = _camTransform.GetChild(_bulletPos).transform.forward;
			_bulletPos = (_bulletPos == _camTransform.childCount - 1) ? _camTransform.childCount - 2 : _camTransform.childCount - 1;
			GameObject _smokeLarge = Instantiate(_smoke, _camTransform.GetChild(_bulletPos).transform.position, Quaternion.identity) as GameObject;


			GameObject _s_projectile = Instantiate(_smallProjectile, _camTransform.GetChild(_bulletPos).transform.position, Quaternion.identity) as GameObject;
			_s_projectile.GetComponent<Projectile>().forward = _camTransform.GetChild(_bulletPos).transform.forward;
			_l_projectile.transform.parent = activeEntities.transform;
			_s_projectile.transform.parent = activeEntities.transform;

			GameObject _smokeSmall = Instantiate(_smoke, _camTransform.GetChild(_bulletPos).transform.position, Quaternion.identity) as GameObject;

			Player.GetComponent<CameraController>().Recoil();
			_smokeLarge.transform.parent = activeEntities.transform;
			_smokeSmall.transform.parent = activeEntities.transform;

		}
	}


	void LockCursor(bool b)
	{
		Cursor.visible = !b;
		Screen.lockCursor = b;
	}

	private void DecreaseGameCanvasBlankAlpha()
	{
		if (menuActive == MenuActive.GAME)
		{
			_blankImage.color = new Color(0, 0, 0 , _blankImage.color.a - Time.deltaTime);
		}
	}


	private void SetCursorTexture(Texture2D _texture)
	{
		Cursor.SetCursor(_texture, new Vector2(0, 0), CursorMode.Auto);
	}

	private void SpawnBots(int startingDelay, int rate, bool keepSpawning)
	{
		if (menuActive == MenuActive.GAME)
		{
			if (SpawnEnemy)
			{
				if (keepSpawning)
				{
					if (botCounter < Constants.MaxBotAtTime)
					{
						if (Time.time > startingDelay)
						{
							_botSpawnCounter += Time.deltaTime;

							if (_botSpawnCounter > rate )
							{
								float _spawnX = Player.transform.position.x + Random.Range(-20.0f, 20.0f);
								float _spawnY = 4;
								float _spawnZ = Player.transform.position.z + Random.Range(-20.0f, 20.0f);
								GameObject clone = Instantiate(_bot, new Vector3(_spawnX, _spawnY, _spawnZ), Quaternion.identity) as GameObject;
								clone.transform.parent = activeEntities.transform;
								botCounter++;
								_botSpawnCounter = 0;
							}
						}
					}
				} else
				{
					if (TotalEnemiesSpawned < Constants.MaxBotAtTime)
					{
						if (Time.time > startingDelay)
						{
							_botSpawnCounter += Time.deltaTime;

							if (_botSpawnCounter > rate )
							{
								float _spawnX = Player.transform.position.x + Random.Range(-20.0f, 20.0f);
								float _spawnY = 4;
								float _spawnZ = Player.transform.position.z + Random.Range(-20.0f, 20.0f);
								GameObject clone = Instantiate(_bot, new Vector3(_spawnX, _spawnY, _spawnZ), Quaternion.identity) as GameObject;
								clone.transform.parent = activeEntities.transform;
								botCounter++;
								TotalEnemiesSpawned++;
								_botSpawnCounter = 0;
							}
						}
					}
				}
			}
		}
	}


	public void Reset()
	{
		UnityEngine.SceneManagement.SceneManager.LoadScene("Project 1");
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
	MENU,
	GAME,
	CONTROL,
	RETRY,
}

public enum ButtonID
{
	LEFT,
	RIGHT,
	UP,
	DOWN,

	ROT_LEFT,
	ROT_RIGHT,
	ROT_UP,
	ROT_DOWN,

	PLAY,
	CREDIT,
	QUIT,


	RETRY,
	MENU,


	NONE,
}

