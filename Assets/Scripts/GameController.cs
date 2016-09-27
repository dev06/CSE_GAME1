﻿//Devan Patel
//Applications and Scripting
//Sep.12.2016
using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class GameController : MonoBehaviour {


	#region ----------- PUBLIC MEMBERS----------
	public ControllerProfile controllerProfile;
	public InventoryManager inventoryManager;
	public static ButtonID selectedButtonID;
	public MenuActive menuActive;
	[HideInInspector]
	public KeyCode[] customKey;
	[HideInInspector]
	public bool TogglePlayerMovement;
	[HideInInspector]
	public bool ToggleMouseControl;
	public bool onContainer;
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

		inventoryManager = new InventoryManager();
		AddQuickItemSlotToList();
		EnableMenu(MenuActive.MENU);


		if (EventManager.OnShoot != null)
		{
			EventManager.OnShoot();
		}
	}

	void Start()
	{


	}


	void Update ()
	{

		ShootProjectile((GameObject)Resources.Load("Prefabs/Projectile/BlueBullet"), GameObject.Find("BulletRight").transform.position);
		ShootProjectile((GameObject)Resources.Load("Prefabs/Projectile/BlueBullet"), GameObject.Find("BulletLeft").transform.position);

		SpawnBots(Constants.StartBotSpawningDelay, Constants.BotSpawnDelay, KeepSpawning);
		DecreaseGameCanvasBlankAlpha();
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			Application.Quit();
		}

		if (Input.GetKeyDown(KeyCode.E) && menuActive != MenuActive.MENU && menuActive != MenuActive.RETRY)
		{
			if (menuActive != MenuActive.INVENTORY)
			{
				EnableMenu(MenuActive.INVENTORY);
				if (EventManager.OnInventoryActive != null) {
					EventManager.OnInventoryActive(1);
				}
			} else {
				if (EventManager.OnInventoryUnActive != null) {
					EventManager.OnInventoryUnActive(-1);
				}
			}
		}

		if (Input.GetMouseButtonDown(0) && menuActive == MenuActive.GAME) {
			inventoryManager.AddItem(new Item("Purple Ball",
			                                  "A powerfull ball that is capable of destroying the enemies in 10 seconds. ",
			                                  Resources.Load<Sprite>("Item/purpleBall"), 1, ItemID.PurpleBall));
			inventoryManager.AddItem(new Item("Blue Ball",
			                                  "A great ball that will slow down the enemies for certain time period. ",
			                                  Resources.Load<Sprite>("Item/blueBall"), 1, ItemID.BlueBall));

			inventoryManager.AddItem(new Item("Yellow Ball",
			                                  "This ball allows you to teleport to a certain location. ",
			                                  Resources.Load<Sprite>("Item/yellowBall"), 1, ItemID.YellowBall));

			inventoryManager.AddItem(new Item("Basic Medkit",
			                                  "Repletes 10 health points.  ",
			                                  Resources.Load<Sprite>("Item/greenHealth"), 1, ItemID.GreenHealth));

			inventoryManager.AddItem(new Item("Effective Medkit",
			                                  "Repletes 20 health points.  ",
			                                  Resources.Load<Sprite>("Item/redHealth"), 1, ItemID.RedHealth));

			inventoryManager.AddItem(new Item("Advanced Medkit",
			                                  "Repletes 30 health points.  ",
			                                  Resources.Load<Sprite>("Item/blueHealth"), 1, ItemID.BlueHealth));

		}

		if (Input.GetKeyDown(KeyCode.T) && menuActive == MenuActive.GAME)
		{
			inventoryManager.AddItem(new Item("Premium Medkit",
			                                  "Repletes 40 health points.  ",
			                                  Resources.Load<Sprite>("Item/orangeHealth"), 1, ItemID.OrangeHealth));

		}

		inventoryManager.SelectQuickItemSlot();

	}


	/// <summary>
	/// Enables a given menu
	/// </summary>
	/// <param name="_menu"></param>
	public void EnableMenu(MenuActive _menu)
	{

		switch (_menu)
		{
			case MenuActive.GAME:
				ActivateUICanvas(false, "GameCanvas");
				GameObject.FindGameObjectWithTag("UI/GameCanvas").GetComponent<Canvas>().enabled = true;
				ActivateChild(GameObject.FindWithTag("UI/GameCanvas"), "", true);
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
			case MenuActive.INVENTORY:
				GameObject.FindGameObjectWithTag("UI/InventoryCanvas").GetComponent<Canvas>().enabled = true;
				ActivateUICanvas(false, "InventoryCanvas");
				ActivateChild(GameObject.FindWithTag("UI/GameCanvas"), "QuickItem", false);
				menuActive = MenuActive.INVENTORY;
				break;
		}
	}


	public void AssignToQuickItem(KeyCode key, out int qsIndex)
	{
		qsIndex = 0;
		GameObject quickItemInventory = GameObject.FindWithTag("ContainerControl/InventoryContainer/QuickItem").gameObject;
		switch (key)
		{
			case KeyCode.Alpha1:
				inventoryManager.AddToQuickItem(inventoryManager.hoverItem, quickItemInventory.transform.FindChild("QS_Slot1").GetComponent<InventorySlot>());
				qsIndex = 1;
				break;
			case KeyCode.Alpha2:
				inventoryManager.AddToQuickItem(inventoryManager.hoverItem, quickItemInventory.transform.FindChild("QS_Slot2").GetComponent<InventorySlot>());
				qsIndex = 2;
				break;
			case KeyCode.Alpha3:
				inventoryManager.AddToQuickItem(inventoryManager.hoverItem, quickItemInventory.transform.FindChild("QS_Slot3").GetComponent<InventorySlot>());
				qsIndex = 3;
				break;
			case KeyCode.Alpha4:
				inventoryManager.AddToQuickItem(inventoryManager.hoverItem, quickItemInventory.transform.FindChild("QS_Slot4").GetComponent<InventorySlot>());
				qsIndex = 4;
				break;

		}
	}



	public void AddQuickItemSlotToList()
	{
		GameObject quickItemList = GameObject.FindWithTag("ContainerControl/InventoryContainer/QuickItem").gameObject;
		for (int i = 0; i < quickItemList.transform.childCount; i++)
		{
			inventoryManager.quickItemSlots.Add(quickItemList.transform.GetChild(i).GetComponent<InventorySlot>());
		}
	}

	/// <summary>
	/// Sets all the canvases but the expection active
	/// </summary>
	/// <param name="b"></param>
	/// <param name="_exception"></param>
	private void ActivateUICanvas(bool b, string _exception)
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

	private void ActivateChild(GameObject canvas, string child, bool all)
	{
		canvas.GetComponent<Canvas>().enabled = true;
		if (!all)
		{
			for (int i = 0; i < canvas.transform.childCount; i++)
			{
				canvas.transform.GetChild(i).gameObject.SetActive(false);
			}
			canvas.transform.FindChild(child).gameObject.SetActive(true);
		} else
		{
			for (int i = 0; i < canvas.transform.childCount; i++)
			{
				canvas.transform.GetChild(i).gameObject.SetActive(true);
			}
		}
	}

	/// <summary>
	/// Enables the Control configuration.
	/// </summary>
	/// <param name="value"></param>
	private void EnableControlConfigUI(bool value)
	{
		GameObject.FindGameObjectWithTag("UI/GameCanvas").GetComponent<Canvas>().enabled = !value;
		GameObject.FindGameObjectWithTag("UI/ControlConfigCanvas").GetComponent<Canvas>().enabled = value;
		GameObject.FindGameObjectWithTag("UI/ControlConfigCanvas").transform.FindChild("AlphaBackGround").transform.FindChild("ControlConfigBackground").gameObject.SetActive(value);
	}


	/// <summary>
	/// Shoots the projectile
	/// </summary>
	private void ShootProjectile(GameObject _prefab, Vector3 _position)
	{
		if (Input.GetMouseButtonDown(1))
		{


			GameObject _blueBullet = Instantiate(_prefab, _position, Quaternion.identity) as GameObject;
			GameObject _effect = Instantiate((GameObject)Resources.Load("Prefabs/Particles/ShootEffect"), _position, Quaternion.identity) as GameObject;
			_blueBullet.GetComponent<Projectile>().forward = Camera.main.transform.forward;
			if (EventManager.OnShoot != null)
			{
				EventManager.OnShoot();
			}

			// Transform _camTransform = Camera.main.transform;
			// int _bulletPos = Random.Range(_camTransform.childCount - 1, _camTransform.childCount - 3);


			// GameObject _l_projectile = Instantiate(_largeProjectile, _camTransform.GetChild(_bulletPos).transform.position, Quaternion.identity) as GameObject;
			// _l_projectile.GetComponent<Projectile>().forward = _camTransform.GetChild(_bulletPos).transform.forward;
			// _bulletPos = (_bulletPos == _camTransform.childCount - 1) ? _camTransform.childCount - 2 : _camTransform.childCount - 1;
			// GameObject _smokeLarge = Instantiate(_smoke, _camTransform.GetChild(_bulletPos).transform.position, Quaternion.identity) as GameObject;


			// GameObject _s_projectile = Instantiate(_smallProjectile, _camTransform.GetChild(_bulletPos).transform.position, Quaternion.identity) as GameObject;
			// _s_projectile.GetComponent<Projectile>().forward = _camTransform.GetChild(_bulletPos).transform.forward;
			// _l_projectile.transform.parent = activeEntities.transform;
			// _s_projectile.transform.parent = activeEntities.transform;

			// GameObject _smokeSmall = Instantiate(_smoke, _camTransform.GetChild(_bulletPos).transform.position, Quaternion.identity) as GameObject;

			Player.GetComponent<CameraController>().Recoil();
			// _smokeLarge.transform.parent = activeEntities.transform;
			// _smokeSmall.transform.parent = activeEntities.transform;

		}
	}

	/// <summary>
	/// Lowers the beginning alpha
	/// </summary>
	private void DecreaseGameCanvasBlankAlpha()
	{
		if (menuActive == MenuActive.GAME)
		{
			_blankImage.color = new Color(0, 0, 0 , _blankImage.color.a - Time.deltaTime);
		}
	}

	/// <summary>
	/// Sets the cursor texture
	/// </summary>
	/// <param name="_texture"></param>
	private void SetCursorTexture(Texture2D _texture)
	{
		Cursor.SetCursor(_texture, new Vector2(0, 0), CursorMode.Auto);
	}

	/// <summary>
	/// Spawns the bots based on the starting delay, rate and bool for keep spawning or not
	/// </summary>
	/// <param name="startingDelay"></param>
	/// <param name="rate"></param>
	/// <param name="keepSpawning"></param>
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

	/// <summary>
	/// Resets the Game
	/// </summary>
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
	INVENTORY
}

public enum InventoryType
{
	Inventory,
	QuickItem
}

public enum GameItem
{
	PURPLEBALL,
	BlUEBALL,
	YELLOWBALL,
	GREENHEALTH,
	REDHEALTH,
	BLUEHEALTH,
	ORANGEHEALTH,
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
