using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class InventorySlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler {

	public GameController gameController;
	public Item item;
	public Sprite HoverSprite;
	public Sprite RestSprite;
	public Color HoverColor;
	public Color RestColor;
	public float HoverSize;
	public float RestSize;
	public InventoryType inventoryType;

	private bool _onHover;
	private InventoryManager _inventoryManager;

	private Image _slotImage; // slot background image
	private Image _slotItemImage; // slot item image
	private Text _slotItemQuantity; // slot item text
	private Text _slotItemQuickSelect;


	Quaternion rotation;
	void Start ()
	{
		Init();
		rotation = _slotItemImage.transform.rotation;
	}

	protected void Init()
	{
		gameController = GameObject.FindWithTag("GameController").GetComponent<GameController>();
		_slotImage = GetComponent<Image>();
		_slotItemImage = transform.FindChild("ItemImage").GetComponent<Image>();
		_slotImage.color = RestColor;
		_slotItemQuantity = _slotItemImage.transform.FindChild("ItemQuantity").GetComponent<Text>();
		_inventoryManager = gameController.inventoryManager;
		if (inventoryType != InventoryType.QuickItem)
		{
			gameController.inventoryManager.inventorySlots.Add(this);
			_slotItemQuickSelect = _slotItemImage.transform.FindChild("ItemQuickSelect").GetComponent<Text>();
		}

	}

	void Update()
	{
		if (_onHover)
		{
			transform.Rotate(new Vector3(0, 0, -50f * Time.deltaTime));
			_slotItemImage.transform.Rotate(new Vector3(0, 0, 50f * Time.deltaTime));

			if (inventoryType != InventoryType.QuickItem)
			{
				foreach (KeyCode key in Constants.QuickItemKeys)
				{
					if (Input.GetKeyDown(key))
					{
						int qsIndex;
						gameController.AssignToQuickItem(key, out qsIndex);

					}
				}
			}

		}
		// if (inventoryType != InventoryType.QuickItem)
		// {
		// 	for (int i = 0; i < _inventoryManager.quickItemSlots.Count; i++)
		// 	{
		// 		if (_inventoryManager.quickItemSlots[i].item != null)
		// 		{
		// 			if (_inventoryManager.quickItemSlots[i].item == item)
		// 			{
		// 				_slotItemQuickSelect.text = "" + (i + 1);
		// 				Debug.Log(i);
		// 			}
		// 		}
		// 	}
		// }

		ManageSlotItem();

	}


	private void ManageSlotItem()
	{
		if (item == null)
		{
			_slotItemImage.enabled = false;
			_slotItemQuantity.enabled = false;
			if (inventoryType != InventoryType.QuickItem)_slotItemQuickSelect.enabled = false;
		} else {
			_slotItemImage.enabled = true;
			_slotItemQuantity.enabled = true;
			if (inventoryType != InventoryType.QuickItem)_slotItemQuickSelect.enabled = true;
		}

	}

	public void SetItem(Item item)
	{
		if (item != null)
		{
			this.item = item;
			_slotItemImage.sprite = item.itemImage;
			_slotItemQuantity.text = item.itemQuantity.ToString();
		}
	}

	public void SetItem(Item item, int index)
	{
		if (item != null)
		{
			this.item = item;
			_slotItemImage.sprite = item.itemImage;
			_slotItemQuantity.text = item.itemQuantity.ToString();

		}
	}

	public void RemoveSlotItem()
	{
		this.item = null;
	}

	public void UpdateItem(Item item)
	{
		this.item.itemQuantity += item.itemQuantity;
		SetItem(this.item);
	}

	public virtual void OnPointerEnter(PointerEventData data)
	{
		if (item != null)
		{
			if (HoverSprite != null)
			{
				_slotImage.sprite = HoverSprite;
				_slotImage.color = HoverColor;
			}
			gameController.inventoryManager.hoverItem = item;
			transform.localScale = new Vector3(HoverSize, HoverSize, 1);
			if (inventoryType != InventoryType.QuickItem)
			{
				GameObject.Find("ToolTip").GetComponent<ToolTip>().item = item;
			}
			_onHover = true;
		}

	}
	public virtual void OnPointerExit(PointerEventData data) {
		if (item != null)
		{
			if (RestSprite != null)
			{
				_slotImage.sprite = RestSprite;
				_slotImage.color = RestColor;
			}
			gameController.inventoryManager.hoverItem = null;
			transform.localScale = new Vector3(RestSize, RestSize, 1);
			if (inventoryType != InventoryType.QuickItem)
			{
				GameObject.Find("ToolTip").GetComponent<ToolTip>().item = null;
			}

			_onHover = false;
		}


	}
	public virtual void OnPointerClick(PointerEventData data) {

	}

}
