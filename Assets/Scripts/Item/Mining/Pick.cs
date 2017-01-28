using UnityEngine;
using System.Collections;

public class Pick : MonoBehaviour {

    void OnTriggerEnter(Collider other)
    {
        Stone stone = other.gameObject.GetComponent<Stone>();
        if (stone != null)
        {
            StartCoroutine(stone.StoneBreak());
        }
    }
}
