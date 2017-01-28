using UnityEngine;
using System.Collections;

public class Coin : MonoBehaviour {

    Vector3 default_force = 250 * Vector3.up;

    public static void SpawnCoin(GameObject Center,Vector3 force, int price=100)
    {
        var coin = Instantiate(Resources.Load<GameObject>("Item/4"));
        var itemobject = coin.GetComponent<ItemObject>();
        itemobject.price = price;
        coin.transform.position = Center.transform.position + Vector3.up * 0.5f;
        coin.GetComponent<Rigidbody>().useGravity = true;
        coin.GetComponent<Rigidbody>().AddForce(force);
    }
}
