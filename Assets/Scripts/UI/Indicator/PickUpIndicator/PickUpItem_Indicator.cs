using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class PickUpItem_Indicator : MonoBehaviour {

	private Text _itemName;
	private Image _itemImage;

	void OnEnable()
	{
		Init();
	}

	public void Init()
	{
		_itemName = transform.FindChild("ItemName").GetComponent<Text>();
		_itemImage = transform.FindChild("ItemImage").GetComponent<Image>();
	}


	public void SetText(string _text)
	{
		_itemName.text = _text;
	}

	public void SetIcon(Sprite _sprite)
	{
		if (_sprite != null)
		{
			_itemImage.sprite = _sprite;
		} else {
			_itemImage.enabled = false;
		}
	}
}
