using UnityEngine;
using System.Collections;

public class EventStart : MonoBehaviour {

    [SerializeField]
    Hukidashi hukidashi;
    [SerializeField]
    string[] contents;

    void Start()
    {
        hukidashi = GetComponentInChildren<Hukidashi>();
        hukidashi.enabled = true;
        StartCoroutine(start());
        
    }

    void Update()
    {
        if (hukidashi.State == Hukidashi.HukidashiState.flowing)
        {
            if (GameManager.instance.left_controller.warpflag)
            {
                hukidashi.EndText();
            }
        }
    }

    string[] set_contents()
    {
        return contents;
    }

    IEnumerator start()
    {
        yield return new WaitForSeconds(3f);
        hukidashi.StartContents(set_contents());
        yield return null;
    }
}
