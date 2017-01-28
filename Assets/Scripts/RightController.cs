using UnityEngine;
using System.Collections;

public class RightController : MonoBehaviour {
    SteamVR_TrackedObject trackedObject;
    static ItemMenuCell selected_cell = null;
    public GameObject targetObj = null;
    public GameObject SelectedItem = null;

    public Action action = Action.None;
    
    public enum Mode
    {
        NormalMode,
        menuMode,
        ShopMode,
    }

    public enum Action
    {
        None,
        NextItem,
        PrevItem,
        ThrowItem,
        ModeChange,
        Trigger,
        GetItem
    }

    Mode mode = Mode.NormalMode;
    string ray_hit_tag;
    
    
	void Start () {
        trackedObject = GetComponent<SteamVR_TrackedObject>();
    }

    public void change_selected_item(int item_id)
    {
        MenuRender();
        SelectedItem = ItemObject.InstantiateFromResources(item_id, transform);
    }

    void SelectItemFromMenu(ItemMenuCell cell)
    {

        if (cell == null)
        {
            return;
        }
        int selected_index = int.Parse(cell.name);
        if (selected_index >= GameManager.instance.inventory.ItemListCount())
        {
            return;
        }

        Destroy(SelectedItem);
        SoundController.instance.Play("ItemGetSound");
        int item_id = (int)GameManager.instance.inventory.select_item(selected_index);
        change_selected_item(item_id);
        
        
    }

    public Const.ItemName throw_selected_item()
    {
        float throw_speed = 1200f;
        if(GameManager.instance.inventory.SelectedItem == Const.ItemName.None)
        {
            return Const.ItemName.None;
        }
        SoundController.instance.Play("ThrowSound");
        action = Action.ThrowItem;
        SelectedItem.transform.parent = GameManager.instance.field.transform;
        SelectedItem.GetComponent<Collider>().isTrigger = false;
        SelectedItem.GetComponent<Collider>().enabled = true;
        SelectedItem.GetComponent<Rigidbody>().useGravity = true;
        SelectedItem.GetComponent<Rigidbody>().AddForce(transform.forward * throw_speed);
        SelectedItem = null;
        return  GameManager.instance.inventory.remove_select_item();

    }

    void TouchPadAction(Vector2 touchaxis)
    {
        //if (touchaxis.y >= 0.65)
        //{
        //    throw_selected_item();
        //    Item.ItemName selected_item = GameManager.instance.inventory.select_next_item();
        //    change_selected_item((int)selected_item);
        //    return;
        //}
        if (touchaxis.x >= 0.7)
        {
            Const.ItemName selected_item = GameManager.instance.inventory.select_next_item();
            action = Action.NextItem;
            Destroy(SelectedItem);
            change_selected_item((int)selected_item);
            return;
        }
        if (touchaxis.x <= -0.7)
        {
            Const.ItemName selected_item = GameManager.instance.inventory.select_prev_item();
            action = Action.PrevItem;
            Destroy(SelectedItem);
            change_selected_item((int)selected_item);
            return;
        }
    }

    public void ChangeMode(Mode mode)
    {
        switch (this.mode) //現在のモードの取り消し処理
        {
            case Mode.NormalMode:
                break;
            case Mode.menuMode:
                ItemMenuController.instance.remove_item_menu();
                break;
            case Mode.ShopMode:
                break;
        }
        switch (mode) //次のモードの設定処理
        {
            case Mode.NormalMode:
                this.mode = Mode.NormalMode;
                ItemMenuController.instance.remove_item_menu();

                break;
            case Mode.menuMode:
                this.mode = Mode.menuMode;
                ItemMenuController.instance.set_item_menu(transform.position, transform);
                break;
            case Mode.ShopMode:
                this.mode = Mode.ShopMode;
                break;
        }
    }
    public void SwitchMode()
    {
        switch (mode)
        {
            case Mode.NormalMode:
                ChangeMode(Mode.menuMode);
                break;
            case Mode.menuMode:
                ChangeMode(Mode.NormalMode);

                break;
        }
    }

    void MenuRender()
    {
        switch (mode)
        {
            case Mode.NormalMode:         
                break;
            case Mode.menuMode:
                ItemMenuController.instance.set_item_contents();

                break;
        }
    }

    void ItemCellHit(ItemMenuCell cell)
    {
        if (cell == null)
        {
            return;
        }
        int selected_index = int.Parse(cell.name);
        if (selected_cell != null)
        {
            if (int.Parse(selected_cell.name) == GameManager.instance.inventory.selected_item_index)
            {
                selected_cell.select();
            }
            else
            {
                selected_cell.detach();
            }
        }
        selected_cell = cell;
            
        if(selected_index == GameManager.instance.inventory.selected_item_index)
        {
            cell.select();
        }
        else
        {
            cell.target();
        }
            
    }

    void GetItem()
    {
        ItemObject itemObject = targetObj.GetComponent<ItemObject>();
        if (itemObject == null || itemObject.item == Const.ItemName.Invalid || itemObject.item == Const.ItemName.None)
        {
            return;
        }
        if (GameManager.instance.inventory.ItemListCount() > Inventory.MaxItemNum)
        {
            print("XXXXXX");
            SoundController.instance.Play("ItemBadSound");
            return;
        }
        action = Action.GetItem;
        GameManager.instance.inventory.get_item(itemObject);
        MenuRender();
        Destroy(targetObj);
    }

    void Conversation()
    {
        Residents residents = targetObj.GetComponent<Residents>();
        if (residents == null)
        {
            return;
        }
        residents.StartConversation();
        
    }

    
    void Update () {
        Ray ray = new Ray(transform.position, transform.forward);
        var device = SteamVR_Controller.Input((int)trackedObject.index);
        RaycastHit hit;

        if (Timer.instance.IsRest)
        {
            return;
        }

        action = Action.None;
        if (device.GetPressDown(SteamVR_Controller.ButtonMask.Touchpad))
        {
            if (device.GetTouch(SteamVR_Controller.ButtonMask.Touchpad))
            {
                TouchPadAction(device.GetAxis());
            }
        }

        if (device.GetPressUp(SteamVR_Controller.ButtonMask.ApplicationMenu))
        {
            SwitchMode();
        }

        if (Physics.Raycast(ray, out hit))
        {
            targetObj = hit.transform.gameObject;
            ItemCellHit(hit.collider.GetComponent<ItemMenuCell>());
            if (device.GetPressDown(SteamVR_Controller.ButtonMask.Trigger))
            {
                action = Action.Trigger;
                GetItem();
                switch (mode)
                {
                    case Mode.NormalMode:
                        break;
                    case Mode.menuMode:
                        SelectItemFromMenu(hit.collider.GetComponent<ItemMenuCell>());
                        break;
                    default:
                        break;
                }
                Conversation();
                
            }
        }
        else
        {
            targetObj = null;
        }
        if (device.GetPressUp(SteamVR_Controller.ButtonMask.Trigger))
        {
            if (targetObj != null)
            {
                //targetObj.GetComponent<Rigidbody>().velocity = device.velocity*hit.distance;
                //targetObj.GetComponent<Rigidbody>().angularVelocity = device.angularVelocity;
                //releaseObject();
            }  
        }
        //if (device.GetTouchDown(SteamVR_Controller.ButtonMask.Trigger))
        //{
        //    print("トリガーを浅く引いた");
        //}
        //if (device.GetTouch(SteamVR_Controller.ButtonMask.Trigger))
        //{
        //    print("トリガーを浅く引いている");
        //}

        //if (device.GetPressDown(SteamVR_Controller.ButtonMask.Trigger))
        //{
        //    print("トリガーを深く引いた");
        //}
        //if (device.GetPress(SteamVR_Controller.ButtonMask.Trigger))
        //{
        //    print("トリガーを深く引いている");
        //}
        //if (device.GetPressUp(SteamVR_Controller.ButtonMask.Trigger))
        //{
        //    print("トリガーを離した");
        //}
        //if (device.GetTouchUp(SteamVR_Controller.ButtonMask.Trigger))
        //{
        //    print("トリガーを離した");
        //}

    }

    public Vector3 velocity
    {
        get
        {
            var device = SteamVR_Controller.Input((int)trackedObject.index);
            return device.velocity;
        }
    }

    public void vibration(ushort p)
    {
        var device = SteamVR_Controller.Input((int)trackedObject.index);
        device.TriggerHapticPulse(p);
    }

    public void vibration(ushort p, float vibration_time)
    {
        StartCoroutine(vibration_coroutine(p, vibration_time));
    }

    IEnumerator vibration_coroutine(ushort p, float vibration_time)
    {
        for (float time = 0; time < vibration_time; time += Time.deltaTime)
        {
            vibration(p);
            yield return null;
        }
        yield break;
    }


}

