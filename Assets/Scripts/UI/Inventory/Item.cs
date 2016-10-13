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
	public ItemType itemType;


	public Item() {}


	public Item(string itemName,
	            string itemInfo,
	            Sprite itemImage,
	            int itemQuantity,
	            ItemID itemID,
	            ItemType itemType) {

		this.itemName = itemName;
		this.itemImage = itemImage;
		this.itemInfo = itemInfo;
		this.itemQuantity = itemQuantity;
		this.itemID = itemID;
		this.itemType = itemType;
	}


	public void SetItem(Item item)
	{
		this.itemImage = item.itemImage;
		this.itemQuantity = item.itemQuantity;
		this.itemInfo = item.itemInfo;
		this.itemName = item.itemName;
		this.itemID = item.itemID;
		this.itemType = item.itemType;
	}
}

public enum ItemID
{
	PurpleBall,
	BlueBall,
	YellowBall,
	BasicHealth,
	InterMedHealth,
	AdvancedHealth,
	SuperHealth,
	SpeedBuff,
	SlowMotionBuff,
	TeleportationBuff,
	ImmortalityBuff,
	Null,
}

public enum ItemType
{
	Projectile,
	Collectible,
	Buff,
}
