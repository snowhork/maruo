using UnityEngine;
using System.Collections;

public class Residents : MonoBehaviour {

    public enum Type
    {
        None,
        Shop,
        Conversation
    }
    
    [SerializeField]
    Type type;
	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
        
    }

    public void StartConversation()
    {
        switch(type)
        {
            case Type.Shop:
                GetComponent<Shop>().EventStart();
                break;
            case Type.Conversation:
                GetComponent<Conversation>().EventStart();
                break;
            default:
                break;
        }

        
    }
}
