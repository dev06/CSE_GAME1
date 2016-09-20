using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class Item
{

	public Sprite itemImage;
	public int itemQuantity;
	public string itemName;
	public string itemInfo;
	public ItemID itemID;


	public Item()
	{
		// itemImage = null;
		// itemQuantity = 0;
		// itemName = "";
		// itemInfo = "";
		// itemID = ItemID.Null;
	}

	public Item(string itemName,
	            string itemInfo,
	            Sprite itemImage,
	            int itemQuantity,
	            ItemID itemID) {

		this.itemName = itemName;
		this.itemImage = itemImage;
		this.itemInfo = itemInfo;
		this.itemQuantity = itemQuantity;
		this.itemID = itemID;
	}


	public void SetItem(Item item)
	{
		this.itemImage = item.itemImage;
		this.itemQuantity = item.itemQuantity;
		this.itemInfo = item.itemInfo;
		this.itemName = itemName;
		this.itemID = itemID;
	}
}

public enum ItemID
{
	PurpleBall,
	RedBall,
	Null,
}
