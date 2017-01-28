using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Inventory : MonoBehaviour
{
    List<Const.ItemName> itemList = new List<Const.ItemName>();
    public int selected_item_index = 0;
    Const.ItemName selected_item = Const.ItemName.None;
    public static int MaxItemNum = 12;

    public int SumPrice()
    {
        int sum = 0;
        foreach(Const.ItemName item in itemList){
            var resource = Resources.Load<GameObject>("Item/" + (int)item);
            var itemobj = resource.GetComponent<ItemObject>();
            if(itemobj != null)
            {
                sum += itemobj.price;
            }
        }
        return sum;
    }


    public Const.ItemName SelectedItem
    {
        get
        {
            return selected_item;
        }
    }

    // Use this for initialization
    void Start()
    {
        itemList.Add(Const.ItemName.None);
        
        itemList.Add(Const.ItemName.Rod);
        itemList.Add(Const.ItemName.Pick);
        
        select_item(0);


    }

    // Update is called once per frame
    void Update()
    {

    }

    public Const.ItemName select_next_item()
    {
        return select_item((selected_item_index + 1) % itemList.Count);
    }
    public Const.ItemName select_prev_item()
    {
        int index = selected_item_index - 1;
        if (index < 0)
        {
            return select_item(itemList.Count - 1);
        }
        else
        {
            return select_item(index);
        }
    }

    public Const.ItemName select_item(int index)
    {
        selected_item_index = index;
        selected_item = itemList[index];
        return selected_item;
    }

    public Const.ItemName remove_select_item()
    {
        int prev_selected_item_index = selected_item_index;
        select_next_item();
        itemList.RemoveAt(prev_selected_item_index);
        return select_item(prev_selected_item_index % itemList.Count);
    }

    public void get_item(ItemObject item)
    {
        SoundController.instance.Play("ItemGetSound");
        switch (item.item)
        {
            case Const.ItemName.Coin:
                MoneyController.instance.get_money(item.price);
                break;
            case Const.ItemName.GoldBar:
                MoneyController.instance.get_money(item.price);
                break;
            default:
                itemList.Add(item.item);
                break;
        }
    }

    public int ItemListCount()
    {
        return itemList.Count;
    }

    public Const.ItemName get_item_with_index(int index)
    {
        if(index >= itemList.Count)
        {
            return Const.ItemName.Invalid;
        }
        return itemList[index];
    }
    
}
