using UnityEngine;
using System.Collections;

public class LeftControllerDescription : MonoBehaviour {

    bool set_text = false;

	void Update () {
        string ComponentName = "trigger";

        if (set_text)
        {
            return;
        }
        var component = transform.FindChild(ComponentName).gameObject;
        if (component == null)
        {
            return;
        }
        var description = new GameObject("description");

        description.transform.parent = component.transform;
        description.transform.localPosition = Vector3.zero;
        description.transform.localRotation = Quaternion.identity;

        var text = description.AddComponent<TextMesh>();
        text.fontSize = 148;
        text.lineSpacing = 0.8f;
        text.offsetZ = -0.035f;
        text.anchor = TextAnchor.UpperCenter;
        text.text = "\n\n\n\nワープ";
        text.color = Color.white;
        text.characterSize = 0.0005f;

        set_text = true;


	}


}
