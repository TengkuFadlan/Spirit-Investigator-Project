using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryItemSlotSelect : MonoBehaviour
{
    public Item item;
    public void ClickButton()
    {
        InventoryManager.instance.ReadItem(item);
    }
}
