using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ItemMenuController : MonoBehaviour {

    [SerializeField]
    Canvas canvas;
    [SerializeField]
    Image image;
    public static ItemMenuController instance;
    [SerializeField]
    GameObject[] itemObjects;

    [SerializeField]
    Text item_num;

    [SerializeField]
    Text money_num;

    // Use this for initialization
    void Start()
    {
        if (instance == null)
        {
            instance = this;
            itemObjects = new GameObject[Inventory.MaxItemNum];
        }
    }

    void reset_item_menu()
    {
        for (int i = 0; i < Inventory.MaxItemNum; i++)
        {
            Destroy(itemObjects[i]);
        }

        int index = 0;
        foreach (Transform child in image.transform)
        {
            ItemMenuCell cell = child.GetComponent<ItemMenuCell>();
            if (index + 1 == GameManager.instance.inventory.selected_item_index)
            {
                cell = child.GetComponent<ItemMenuCell>();
                cell.select();
            }
            else {
                cell.detach();
            }
            index++;
        }
        item_num.text = (GameManager.instance.inventory.ItemListCount()-1) + "/" + Inventory.MaxItemNum;
        money_num.text = MoneyController.instance.money + " ＄";
    }

    public void remove_item_menu()
    {
        reset_item_menu();
        canvas.enabled = false;
    }

    public void set_item_contents()
    {
        reset_item_menu();
        int index = 0;
        foreach (Transform child in image.transform)
        {
            Const.ItemName item = GameManager.instance.inventory.get_item_with_index(index+1);
            if (item != Const.ItemName.Invalid)
            {
               
                itemObjects[index] = ItemObject.InstantiateFromResources((int)item, child.transform);
                itemObjects[index].transform.localScale *= 0.5f;
                itemObjects[index].transform.localPosition += Vector3.back * 0.1f;
                itemObjects[index].transform.Rotate(Vector3.right, -90f);
                Destroy(itemObjects[index].GetComponent<ItemObject>());
                Destroy(itemObjects[index].GetComponent<BoxCollider>());
                Destroy(itemObjects[index].GetComponent<SphereCollider>());
                Destroy(itemObjects[index].GetComponent<CapsuleCollider>());
                Destroy(itemObjects[index].GetComponent<butterfly>());
            }

            
            if(index+1 == GameManager.instance.inventory.selected_item_index)
            {
                ItemMenuCell cell = child.GetComponent<ItemMenuCell>();
                cell.select();
            }
           
            index++;
        }
    }

    public void set_item_menu(Vector3 position, Transform target)
    {
        canvas.enabled = true;
        transform.position = new Vector3(position.x, GameManager.instance.CameraRig.transform.position.y + 1.4f, position.z);
        transform.LookAt(new Vector3(GameManager.instance.camera.transform.position.x, GameManager.instance.CameraRig.transform.position.y + 1.4f, GameManager.instance.camera.transform.position.z ));
        set_item_contents();
    }
}
