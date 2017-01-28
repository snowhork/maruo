using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UniRx;

public class ItemMenuCell : MonoBehaviour {

    public ReactiveProperty<bool> selected_flag {
        get; private set;
    }
    
	void Start () {
        selected_flag = new ReactiveProperty<bool>(false);
	}

    public void target()
    {
        selected_flag.Value = true;
        GetComponent<Image>().color = new Color(1f, 1f, 0f, 0.6f);
    }

    public void select()
    {
        selected_flag.Value = true;
        GetComponent<Image>().color = new Color(0.8f, 0f, 0f, 0.6f);
    }


    public void detach()
    {
        selected_flag.Value = false;
        GetComponent<Image>().color = new Color(1f, 1f, 1f, 0.6f);
    }
}
