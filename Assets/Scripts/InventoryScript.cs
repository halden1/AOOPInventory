#region[Code]

#region[Namespaces]
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
#endregion

#region[Custom DataTypes]
public class Slot
{
    public GameObject gameObject;
    public Item item;
    public Slot(GameObject g, Item i)
    {
        this.item = i;
        this.gameObject = g;
    }
}
#endregion

#region[Custom Classes]
public class InventoryScript : MonoBehaviour
{

    #region[Enum Definitions]
    enum items { sword, hand, fist };
    #endregion

    #region[Public Variables]
    [Header("Slots")]
    public int slotAmount;
    [Header("Prefabs")]
    public GameObject slotPanel;
    public GameObject slotPrefab;
    public GameObject itemPrefab;
    [Header("Items//Slots")]
    public List<Item> itemList = new List<Item>();
    public List<Slot> slotList = new List<Slot>();
    #endregion

    #region[Private Variables]
    private ItemDatabase itemDataBase;
    #endregion

    #region[Unity Events]
    void Start()
    {
        itemDataBase = GetComponent<ItemDatabase>();
        for (int i = 0; i < slotAmount; i++)
        {
            GameObject clone = Instantiate(slotPrefab);
            clone.transform.SetParent(slotPanel.transform);
            Slot slot = new Slot(clone, null);
            SlotData sData = clone.GetComponent<SlotData>();
            sData.slot = slot;
            slotList.Add(slot);
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            AddItem("cupboard door");
        }
    }
    #endregion

    #region[Custom Functions]
    public void AddItem(string itemName, int amount = 1)
    {
        Item newItem = ItemDatabase.GetItem(itemName);
        Slot newSlot = GetEmptySlot();
        if (newItem != null && newSlot != null)
        {
            if (hasStack(newItem, amount))
            {
                return;
            }
            newSlot.item = newItem;

            GameObject item = Instantiate(itemPrefab);
            item.transform.position = newSlot.gameObject.transform.position;
            item.transform.SetParent(newSlot.gameObject.transform);
            item.name = newItem.Title;
            newItem.gameObject = item;

            Image immaagge = item.GetComponent<Image>();
            immaagge.sprite = newItem.sprite;
            ItemData iData = item.GetComponent<ItemData>();
            iData.item = newItem;
            iData.slot = newSlot;
        }
    }
    public Slot GetEmptySlot()
    {
        for (int i = 0; i < slotAmount; i++)
        {
            if (slotList[i].item == null)
            {
                return slotList[i];
            }
        }
        print("No Vacancies");
        return null;
    }

    bool hasStack(Item toadd, int itemAmount)
    {
        if (toadd.Stackable)
        {
            Slot occupied = getsLotWithItem(toadd);
            if (occupied != null)
            {
                Item itm = occupied.item;
                ItemData dat = itm.gameObject.GetComponent<ItemData>();
                dat.Amount += itemAmount;
                Text elem = itm.gameObject.GetComponentInChildren<Text>();
                elem.text = dat.Amount.ToString();
                return true;
            }
        }
        return false;
    }

    Slot getsLotWithItem(Item it)
    {
        for (int i = 0; i < slotList.Count; i++)
        {
            Item currentit = slotList[i].item;
            if (currentit != null && currentit.Title == it.Title)
            {
                return slotList[i];
            }
        }
        return null;
    }
    #endregion
}
#endregion

#endregion
