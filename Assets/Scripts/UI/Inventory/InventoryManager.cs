﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
//Responsible for updating the state of the inventory item (occupied or empty)
//Responsible for return next un-occupied slot
//Responsible for adding items to the inventory upon player collision
public class InventoryManager {

	public List<InventorySlot> inventorySlots = new List<InventorySlot>();
	public List<InventorySlot> quickItemSlots = new List<InventorySlot>();
	public Item hoverItem; // item that is currently being hovered on.



	public void AddItem(Item item)
	{
		int itemIndexInInventory;
		int itemIndexInQuickInventory;
		if (DoesItemExits(inventorySlots, item, out itemIndexInInventory))
		{

			inventorySlots[itemIndexInInventory].UpdateItem(item);

			if (DoesItemExits(quickItemSlots, item, out itemIndexInQuickInventory))
			{
				quickItemSlots[itemIndexInQuickInventory].SetItem(inventorySlots[itemIndexInInventory].item);
			}
		} else
		{
			inventorySlots[GetNextAvailableSlot()].SetItem(item);
		}
	}

	public void AddToQuickItem(Item item, InventorySlot slot)
	{
		if (slot.inventoryType == InventoryType.QuickItem)
		{
			if (slot.item == null)
			{
				for (int i = 0; i < quickItemSlots.Count; i++)
				{
					if (quickItemSlots[i].item != null)
					{
						if (quickItemSlots[i].item.itemID == item.itemID)
						{
							quickItemSlots[i].RemoveSlotItem();
							quickItemSlots[i].SetItem(null);
						}
					}
				}
				slot.SetItem(item, GetSlotIndex(quickItemSlots, slot));

			} else
			{
				if (slot.item.itemID != item.itemID)
				{
					Item existingItem = slot.item;
					for (int i = 0; i < quickItemSlots.Count; i++)
					{
						if (quickItemSlots[i].item != null)
						{
							if (quickItemSlots[i].item.itemID == item.itemID)
							{
								quickItemSlots[i].RemoveSlotItem();
								quickItemSlots[i].SetItem(null);
							}
						}

					}
					slot.SetItem(item, GetSlotIndex(quickItemSlots, slot));
					for (int i = 0; i < quickItemSlots.Count; i++)
					{
						if (quickItemSlots[i].item == null)
						{
							quickItemSlots[i].SetItem(existingItem, i);
							break;
						}
					}
				}
			}
		}
	}

	public int GetSlotIndex(List<InventorySlot> collection, InventorySlot slot)
	{
		for (int i = 0; i < collection.Count; i++)
		{
			if (collection[i].GetHashCode() == slot.GetHashCode())
			{
				return i;
			}
		}

		return -1;
	}

/// <summary>
/// Returns whether an item exits in a collection
/// </summary>

	public bool DoesItemExits(List<InventorySlot> collection, Item item, out int itemIndexInInventory)
	{
		itemIndexInInventory = 0;
		for (int i = 0; i < collection.Count; i++)
		{
			InventorySlot currentSlot = collection[i];
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

	public bool isOccupied(InventorySlot slot)
	{
		return slot.item != null;
	}

	public bool isOfType(Item item, Item targetItem)
	{
		return item.itemID == targetItem.itemID;
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