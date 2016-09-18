using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class ToolTip : MonoBehaviour {

	public Item item;
	private RectTransform _transform;
	private Image _toolTipImage;
	private Text _toolTipInfo;
	private Text _toolTipName;

	void Start ()
	{
		Init();
	}

	private void Init()
	{
		_transform = transform.GetComponent<RectTransform>();
		_toolTipInfo = transform.FindChild("ToolTipInfo").GetComponent<Text>();
		_toolTipName = transform.FindChild("ToolTipName").GetComponent<Text>();
		_toolTipImage = transform.FindChild("ToolTipImage").GetComponent<Image>();
	}


	void Update ()
	{
		UpdateToolTipInfo(item);
	}

	private void UpdateToolTipInfo(Item item)
	{
		transform.position = Input.mousePosition;

		if (item != null)
		{
			_toolTipImage.sprite = item.itemImage;
			_toolTipInfo.text = item.itemInfo;
			_toolTipName.text = item.itemName + "  x " + item.itemQuantity;
			GetComponent<Image>().enabled = true;
			SetChildrenActive(true);
		} else
		{
			GetComponent<Image>().enabled = false;
			SetChildrenActive(false);
		}
	}

	private void SetChildrenActive(bool value)
	{
		for (int i = 0; i < transform.childCount; i++)
		{
			transform.GetChild(i).gameObject.SetActive(value);
		}
	}
}
