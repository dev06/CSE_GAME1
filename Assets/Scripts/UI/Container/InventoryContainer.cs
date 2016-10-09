using UnityEngine;
using System.Collections;

public class InventoryContainer : MonoBehaviour {

	private GameController _gameController;
	private Animation  _currentAnimation;
	private GameObject _inventoryContainer_prefab;
	private GameObject _quickItemSlot_prefab;
	private GameObject _topRow;
	private GameObject _bottomRow;
	private InventoryManager _inventoryManager;
	private bool _init;

	void OnEnable()
	{
		EventManager.OnInventoryActive += AnimateContainers;
		EventManager.OnInventoryUnActive += AnimateContainers;
		EventManager.OnItemAddedOrRemoved += UpdateInventorySlotCount;
	}

	void Start ()
	{
		Init();
	}

	private void Init()
	{
		_inventoryContainer_prefab = (GameObject)Resources.Load("Prefabs/UIPrefabs/Slot");
		_quickItemSlot_prefab = (GameObject)Resources.Load("Prefabs/UIPrefabs/QuickItemSlot");
		_topRow = transform.FindChild("TopRow").gameObject;
		_bottomRow = transform.FindChild("BottomRow").gameObject;
		_gameController = GameObject.FindWithTag("GameController").GetComponent<GameController>();
		_inventoryManager = _gameController.inventoryManager;
		GenerateInventoryGrid();
	}


	private void GenerateInventoryGrid()
	{
		float xOffset = 1.4f;
		float yOffset = 1.3f;
		int yCounter = 0;
		int xCounter = 0;
		for (int i = 0; i < 12; i++)
		{

			if (i != 0 && i % 6 == 0)
			{
				yCounter++;
				xCounter = 0;
			}

			xCounter++;

			GameObject _slot = Instantiate(_inventoryContainer_prefab, Vector3.zero, Quaternion.identity) as GameObject;
			if (yCounter == 0) {
				_slot.transform.parent = GameObject.FindWithTag("ContainerControl/InventoryContainer/TopRow").transform;
			}
			else {
				_slot.transform.parent = GameObject.FindWithTag("ContainerControl/InventoryContainer/BottomRow").transform;
			}
			RectTransform _slotTransform = _slot.GetComponent<RectTransform>();
			_slotTransform.localScale = new Vector3(1, 1, 1);
			float _horizontalOffset = ((7 * _slotTransform.sizeDelta.x * _slotTransform.localScale.x * xOffset) / 2.0f)
			                          - xCounter * (_slotTransform.sizeDelta.x * _slotTransform.localScale.x) * xOffset;
			float _verticalOffset = ( _slotTransform.sizeDelta.x * _slotTransform.localScale.x * yOffset) / 2.0f
			                        - yCounter * _slotTransform.sizeDelta.y * _slotTransform.localScale.y * yOffset;
			_slotTransform.anchoredPosition = new Vector3(-_horizontalOffset, 0 , 0);
		}
	}


	void Update()
	{
		if (_currentAnimation != null)
		{
			if (_currentAnimation.IsPlaying(_currentAnimation.clip.name) == false)
			{
				_gameController.EnableMenu(MenuActive.GAME);
				_currentAnimation = null;
			}
		}
	}

	void LateUpdate()
	{
		if (_init == false)
		{
			UpdateInventorySlotCount();
			_init = true;
		}
	}

	private void UpdateInventorySlotCount()
	{
		int slotCount = _inventoryManager.InventorySlotOccupied();
		int childCount  = transform.FindChild("BottomRow").childCount;
		if (slotCount < 6)
		{
			for (int i = 0; i < childCount; i++)
			{
				transform.FindChild("BottomRow").GetChild(i).gameObject.SetActive(false);
			}
		} else
		{
			for (int i = 0; i < childCount; i++)
			{
				InventorySlot slot = transform.FindChild("BottomRow").GetChild(i).GetComponent<InventorySlot>();
				if (slot.item == null)
				{
					slot.gameObject.SetActive(false);
				} else
				{
					slot.gameObject.SetActive(true);
				}
			}
		}
	}

	private void AnimateContainers(int direction)
	{
		PlayAnimation(gameObject, direction);
	}

	private void PlayAnimation(GameObject container, int direction)
	{
		Animation animation = container.GetComponent<Animation>();
		if (direction < 0) { _currentAnimation = animation; }
		animation[animation.clip.name].time =  (direction > 0) ? 0 : animation[animation.clip.name].length;
		animation[animation.clip.name].speed = direction;
		animation.Play(animation.clip.name);
	}

	void OnDisable()
	{
		EventManager.OnInventoryActive -= AnimateContainers;
		EventManager.OnInventoryUnActive -= AnimateContainers;
		EventManager.OnItemAddedOrRemoved -= UpdateInventorySlotCount;
	}
}
