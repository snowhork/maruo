using UnityEngine;
using System.Collections;
using UniRx;
using UniRx.Triggers;

public class butterfly : MonoBehaviour {
    bool StartedCoroutine = false;
    [SerializeField] float AngleVolume = 0.04f;
    [SerializeField]
    float speed = 0.4f;
    // Use this for initialization
    void Start () {

    }
	
	// Update is called once per frame
	void Update () {
        if(!IsSelected() && !StartedCoroutine)
        {
            StartedCoroutine = true;
            StartCoroutine(RandomMove());
        }
	
	}

    bool IsSelected()
    {
        return GameManager.instance.SelectedGameObject == this.gameObject;
    }

    IEnumerator RandomMove()
    {
        while (true)
        {
            Vector3 u = new Vector3(Random.Range(0f, 0.8f), Random.Range(0f, 0.4f), Random.Range(0f, 0.8f));
            Vector3 v = new Vector3(Random.Range(0f, 0.8f), Random.Range(0f, 0.4f), Random.Range(0f, 0.8f));

            for (float angle = 0; angle < Mathf.PI * 2f; angle += AngleVolume)
            {
                if (IsSelected())
                {
                    yield break; 
                }
                transform.position += speed * (u * Mathf.Cos(angle) + v * Mathf.Sin(angle));
                transform.rotation = Quaternion.LookRotation((u * Mathf.Cos(angle) + v * Mathf.Sin(angle)), Vector3.up);
                yield return null;
            }
        }
    }
}
