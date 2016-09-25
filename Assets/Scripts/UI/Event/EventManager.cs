using UnityEngine;
using System.Collections;

public class EventManager : MonoBehaviour {

	public delegate void InventoryActive(int direction);
	public static  InventoryActive OnInventoryActive;
	public static  InventoryActive OnInventoryUnActive;


	public delegate void ItemAddedOrRemoved();
	public static ItemAddedOrRemoved OnItemAddedOrRemoved;

}
