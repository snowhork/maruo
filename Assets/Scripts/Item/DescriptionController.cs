using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DescriptionController : MonoBehaviour {

    [SerializeField] private Canvas canvas;
    [SerializeField] private Text text;




	// Update is called once per frame
	void Update () {


        if (text != null)
        {
            text.text = "";
        }
        canvas.enabled = false;
        if (GameManager.instance.right_controller.targetObj == null)
        {
            return;
        }
        ItemObject target_item_object = GameManager.instance.right_controller.targetObj.GetComponent<ItemObject>();
        if (target_item_object == null)
        {
            return;
        }
        canvas.enabled = true;
        transform.LookAt(GameManager.instance.right_controller.transform);
        transform.position = GameManager.instance.right_controller.targetObj.transform.position;

        if(target_item_object.item == Const.ItemName.Coin)
        {
            text.text = target_item_object.price + " $";
            return;
        }
        text.text = target_item_object.description;

    }
}
