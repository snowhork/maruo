using UnityEngine;
using System.Collections;

public class Crave : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void OnTriggerEnter(Collider other) {
        print("hoge");
        print(other.name);
        ItemObject itemobject = other.GetComponent<ItemObject>();
        if (GameManager.instance.SelectedItem == Const.ItemName.Pick && itemobject != null && 
            itemobject.item == Const.ItemName.Pick)
        {
            GameObject item = (GameObject)Instantiate(Resources.Load("Item/4"));
            item.transform.position = transform.position;
            Vector3 force = Vector3.up * 2f + GameManager.instance.camera.transform.position - transform.position;

            item.transform.GetComponent<Rigidbody>().useGravity = true;
            item.transform.GetComponent<Rigidbody>().AddForce(force * 10f);
        }

    }
}
