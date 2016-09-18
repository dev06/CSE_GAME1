using UnityEngine;
using System.Collections;
using System.Collections.Generic;
//Responsible for updating the state of the inventory item (occupied or empty)
//Responsible for return next un-occupied slot
//Responsible for adding items to the inventory upon player collision
public class InventoryManager {

	public List<InventorySlot> inventorySlots = new List<InventorySlot>();

	public void AddItem(Item item)
	{
		//check if item is already in the inventory
		//if yes the increment item quantity
		//else
		//check for the next un-occupied slot
		//add item to that slot
		int itemIndexInInventory;
		if (DoesItemExits(item, out itemIndexInInventory))
		{
			inventorySlots[itemIndexInInventory].UpdateItem(item);
		} else
		{
			inventorySlots[GetNextAvailableSlot()].SetItem(item);
		}
	}


	public bool DoesItemExits(Item item, out int itemIndexInInventory)
	{
		itemIndexInInventory = 0;
		for (int i = 0; i < inventorySlots.Count; i++)
		{
			InventorySlot currentSlot = inventorySlots[i];
			if (currentSlot.item != null)
			{
				if (currentSlot.item.itemID == item.itemID)
				{
					itemIndexInInventory = i;
					return true;
				}
			}
		}

		itemIndexInInventory = -1;
		return false;
	}

	public int GetNextAvailableSlot()
	{
		for (int i = 0; i < inventorySlots.Count; i++)
		{
			Item currentSlotItem = inventorySlots[i].item;
			if (currentSlotItem == null)
			{
				return i;
			}
		}

		return -1;
	}
}
