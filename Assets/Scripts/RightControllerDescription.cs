using UnityEngine;
using System.Collections;

public class RightControllerDescription : MonoBehaviour {

    bool set_text = false;

	void Update () {
        string ComponentName = "trackpad";

        if (set_text)
        {
            return;
        }
        var component = transform.FindChild(ComponentName).gameObject;
        if(component == null)
        {
            return;
        }
        var description = new GameObject("description");

        description.transform.parent = component.transform;
        description.transform.localPosition = Vector3.zero;
        description.transform.localRotation = Quaternion.identity;
        description.transform.Rotate(Vector3.right, 90f);

        var text = description.AddComponent<TextMesh>();
        text.fontSize = 148;
        text.lineSpacing = 0.8f;
        text.anchor = TextAnchor.UpperCenter;
        text.text = "\n\n　メニュー\n\n 　\n\n左右切り替え";
        text.color = Color.white;
        text.characterSize = 0.0005f;

        ComponentName = "trigger";

      
        component = transform.FindChild(ComponentName).gameObject;
        if (component == null)
        {
            return;
        }
        description = new GameObject("description");

        description.transform.parent = component.transform;
        description.transform.localPosition = Vector3.zero;
        description.transform.localRotation = Quaternion.identity;

        text = description.AddComponent<TextMesh>();
        text.fontSize = 148;
        text.lineSpacing = 0.8f;
        text.offsetZ = -0.035f;
        text.anchor = TextAnchor.UpperCenter;
        text.text = "\n\n\n\n調べる";
        text.color = Color.white;
        text.characterSize = 0.0005f;

        set_text = true;


	}


}
