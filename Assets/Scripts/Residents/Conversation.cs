using UnityEngine;
using System.Collections;

public class Conversation : MonoBehaviour {

    [SerializeField]
    Hukidashi hukidashi;
    [SerializeField]
    string[] contents;
    
    void Start()
    {
        hukidashi = GetComponentInChildren<Hukidashi>();
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

    public void EventStart()
    {

        hukidashi.enabled = true;
        hukidashi.StartContents(set_contents());
    }
}
