using UnityEngine;
using System.Collections;


public class Shop : MonoBehaviour
{

    public GameObject yes = null;
    public GameObject no = null;
    public Hukidashi hukidashi;

    int price;
    void Start()
    {
        hukidashi = GetComponentInChildren<Hukidashi>(); 
    }

    void Update()
    {

        if(hukidashi.State == Hukidashi.HukidashiState.flowing)
        {
            if(GameManager.instance.left_controller.warpflag)
            {
                hukidashi.EndText();
            }
            switch (GameManager.instance.right_controller.action)
            {
                case RightController.Action.NextItem:
                case RightController.Action.PrevItem:
                case RightController.Action.ThrowItem:
                    hukidashi.StartContents(set_contents(), choose_flag: true);
                    break;
            }

            if(GameManager.instance.right_controller.targetObj == null)
            {
                return;
            }
            if (GameManager.instance.right_controller.action != RightController.Action.Trigger)
            {
                return;
            }
            if (GameManager.instance.right_controller.targetObj.tag == "Yes")
            {
                Const.ItemName item = GameManager.instance.RemoveSelctedItem();
                hukidashi.StartContents(new string[] {"まいどあり。"});
                SoundController.instance.Play("GoldSound");
                //MoneyController.instance.money += price;
                Vector3 force = GameManager.instance.camera.transform.position - gameObject.transform.position;
                Coin.SpawnCoin(gameObject, 70f* force, price: price);
            }
            if (GameManager.instance.right_controller.targetObj.tag == "No")
            {
                hukidashi.EndText();
            }
        }
    }

    string[] set_contents()
    {
        if (GameManager.instance.inventory.SelectedItem != Const.ItemName.None)
        {
            string[] contents = new string[2];

            string item_name = GameManager.instance.SelectedGameObject.GetComponent<ItemObject>().description;
            price = GameManager.instance.SelectedGameObject.GetComponent<ItemObject>().price;
            contents[0] = item_name + "は" + price + " ＄です。";
            contents[1] = "売りますか？";
            
            return contents;
        }
        else
        {
            string[] contents = new string[1];
            contents[0] = "なにかアイテムを見せてくれたら私が買い取ってあげよう。";
            return contents;
        }
    }

    public void EventStart()
    {
        
        hukidashi.enabled = true;
        hukidashi.StartContents(set_contents(), choose_flag: true);
    }
}
