using UnityEngine;
using System.Collections;

public class ItemObject : MonoBehaviour {
    public Const.ItemName item;
    public string description;
    public int price;
    

    static public GameObject InstantiateFromResources(int item_id, Transform transform)
    {
        GameObject resource = (GameObject)Resources.Load("Item/" + item_id);
        GameObject item = (GameObject)Instantiate(resource);
        item.name = item_id.ToString();
        item.GetComponent<Collider>().isTrigger = true;
        item.transform.position = transform.position;
        item.transform.rotation = transform.rotation;
        item.transform.parent = transform;

        item.transform.Rotate(Vector3.right, resource.transform.rotation.eulerAngles.x);
        item.transform.Rotate(Vector3.up, resource.transform.rotation.eulerAngles.y);
        item.transform.Rotate(Vector3.forward, resource.transform.rotation.eulerAngles.z);
        item.transform.localPosition += resource.transform.position;
        return item;

    }
}
