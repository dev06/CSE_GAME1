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

	private Image _slotImage; // slot background image
	private Image _slotItemImage; // slot item image
	private Text _slotItemQuantity; // slot item text


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
		if (inventoryType != InventoryType.QuickItem) { gameController.inventoryManager.inventorySlots.Add(this); }

	}

	void Update()
	{
		if (_onHover)
		{
			transform.Rotate(new Vector3(0, 0, -50f * Time.deltaTime));
			_slotItemImage.transform.Rotate(new Vector3(0, 0, 50f * Time.deltaTime));
		}

		ManageSlotItem();

	}


	private void ManageSlotItem()
	{
		if (item == null)
		{
			_slotItemImage.enabled = false;
			_slotItemQuantity.enabled = false;
		} else {
			_slotItemImage.enabled = true;
			_slotItemQuantity.enabled = true;
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

	public void UpdateItem(Item item)
	{
		this.item.itemQuantity += item.itemQuantity;
		SetItem(this.item);
	}

	public virtual void OnPointerEnter(PointerEventData data)
	{
		//	if (item != null)
		//	{
		if (HoverSprite != null)
		{
			_slotImage.sprite = HoverSprite;
			_slotImage.color = HoverColor;
		}
		transform.localScale = new Vector3(HoverSize, HoverSize, 1);
		GameObject.Find("ToolTip").GetComponent<ToolTip>().item = item;
		_onHover = true;
		//}

	}
	public virtual void OnPointerExit(PointerEventData data) {
		//	if (item != null)
		//	{
		if (RestSprite != null)
		{
			_slotImage.sprite = RestSprite;
			_slotImage.color = RestColor;
		}
		transform.localScale = new Vector3(RestSize, RestSize, 1);
		GameObject.Find("ToolTip").GetComponent<ToolTip>().item = null;
		_onHover = false;
		//	}


	}
	public virtual void OnPointerClick(PointerEventData data) {

	}

}
