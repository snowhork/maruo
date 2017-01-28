using UnityEngine;
using System.Collections;

public class Tree : MonoBehaviour {

    [SerializeField]
    Transform Pivot;

    [SerializeField]
    Transform falling_part;

    public IEnumerator TreeFall(Vector3 rotate_axis)
    {
        GetComponent<BoxCollider>().enabled = false;
        //SpawnWood();
        float AngleVolume = 1.8f;
        for (float angle = 0; angle < 90f; angle += AngleVolume)
        {
            falling_part.RotateAround(Pivot.position, rotate_axis, AngleVolume);
            yield return null;
        }
        Destroy(gameObject, 2f);
        yield break;
    }
}
