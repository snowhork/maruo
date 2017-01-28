using UnityEngine;
using System.Collections;

public class TerrainTree : MonoBehaviour {
    
	void Start () {
        transform.Rotate(Vector3.up, Random.Range(0f, 180f));
        transform.localScale *= Random.Range(1.0f, 2.0f);
    }
}
