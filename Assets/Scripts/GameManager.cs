using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {
    public static GameManager instance;
    public RightController right_controller;
    public LeftController left_controller;
    public Camera camera;
    public Inventory inventory;
    public GameObject field;
    public GameObject Camera;
    public GameObject CameraRig;

    Vector3 startCameraRig;

    // Use this for initialization
    void Start () {
        if(instance == null)
        {
            instance = this;
            startCameraRig = instance.CameraRig.transform.position;
        }
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    public Const.ItemName SelectedItem
    {
        get
        {
            return inventory.SelectedItem;
        }
    }

    public GameObject SelectedGameObject
    {
        get
        {
            return right_controller.SelectedItem;
        }
    }

    public Vector3 StartCameraRig
    {
        get
        {
            return startCameraRig;
        }
    }

    public Const.ItemName RemoveSelctedItem()
    {

        Const.ItemName selected_item = inventory.remove_select_item();
        Destroy(right_controller.SelectedItem);
        right_controller.change_selected_item((int)instance.inventory.select_next_item());
        return selected_item;
    }
}
