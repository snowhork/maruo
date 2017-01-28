using UnityEngine;
using System.Collections;

public class Stone : MonoBehaviour {

    [SerializeField]
    GameObject Left;

    [SerializeField]
    GameObject Right;

    [SerializeField]
    GameObject Center;

    [SerializeField]
    GameObject LeftCenter;

    [SerializeField]
    GameObject RightCenter;

    [SerializeField]
    int price = 100;


    public IEnumerator StoneBreak()
    {
        SoundController.instance.Play("MineSound");
        GetComponent<BoxCollider>().enabled = false;
        GetComponent<Rigidbody>().useGravity = false;
        Coin.SpawnCoin(Center, 250f*Vector3.up, price);
        float MoveSpeed = 0.0f;
        float AngleVolume = 4f;
        for (float angle = 0; angle < 90f; angle += AngleVolume) {
            Left.transform.RotateAround(LeftCenter.transform.position, new Vector3(1f, 0f, 1f), AngleVolume);
            Left.transform.localPosition += new Vector3(-1f, -1f, 0f) * MoveSpeed;
            Right.transform.RotateAround(RightCenter.transform.position, new Vector3(1f, 0f, 1f), -AngleVolume);
            Right.transform.localPosition += new Vector3(1f, 1f, 0f) * MoveSpeed;
            yield return null;
        }
        Destroy(gameObject, 1f);
        yield break;
    }
}
