using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager instance;

    public GameObject inventoryItemSlotPrefab;
    public GameObject inventoryContents;
    public TextMeshProUGUI selectedItemNameText;
    public TextMeshProUGUI selectedItemDescriptionText;
    public RawImage selectedItemImage;
    public Texture emptyItemImage;
    public bool isOpen = false;

    List<Item> inventory;
    Animator animator;

    void Awake()
    {
        if (instance != null)
            Debug.LogError("Instance already existed");
        instance = this;

        inventory = new List<Item>();
        animator = GetComponent<Animator>();
    }

    void Start()
    {
        RefreshUI();
    }

    void RefreshUI()
    {
        selectedItemNameText.text = "";
        selectedItemDescriptionText.text = "";
        selectedItemImage.texture = emptyItemImage;
        foreach (Transform child in inventoryContents.transform)
        {
            Destroy(child.gameObject);
        }
        foreach (Item item in inventory)
        {
            GameObject newItemSlot = Instantiate(inventoryItemSlotPrefab, inventoryContents.transform);

            InventoryItemSlotSelect itemSlotSelector = newItemSlot.GetComponent<InventoryItemSlotSelect>();
            itemSlotSelector.item = item;

            Transform itemImage = newItemSlot.transform.Find("ItemImage");
            RawImage itemSlotImage = itemImage.GetComponent<RawImage>();

            itemSlotImage.texture = item.itemImage;

            newItemSlot.transform.SetParent(inventoryContents.transform);
        }
    }

    public void ReadItem(Item item)
    {
        if (!inventory.Contains(item))
            return;

        selectedItemNameText.text = item.itemName;
        selectedItemDescriptionText.text = item.itemDescription;
        selectedItemImage.texture = item.itemImage;
    }

    public void AddItem(Item item)
    {
        if (inventory.Contains(item))
            return;

        inventory.Add(item);
        RefreshUI();
    }

    public void RemoveItem(Item item)
    {
        if (!inventory.Contains(item))
            return;

        inventory.Remove(item);
        RefreshUI();
    }
    public void RemoveItem(string name)
    {
        Item searchItem = FindItem(name);
        RemoveItem(searchItem);
    }

    public Item FindItem(string name)
    {
        foreach (Item item in inventory)
        {
            if (item.itemName == name)
            {
                return item;
            }
        }
        return null;
    }

    public void SetInventoryUI(bool value)
    {
        animator.SetBool("Open", value);
        isOpen = value;
    }

    void Update()
    {
        if (DialogueManager.instance.isDialogueActive)
            return;

        if (Time.timeScale == 0)
            return;

        if (Input.GetKeyDown(KeyCode.E))
        {
            SetInventoryUI(!isOpen);
        }
    }
}
