using UnityEngine;
using System.Collections;

public class Axe : MonoBehaviour {

    void OnTriggerEnter(Collider other)
    {
        Tree tree = other.gameObject.GetComponent<Tree>();
        if (tree != null)
        {
            if (GameManager.instance.right_controller.velocity.magnitude >= 2f)
            {
                Vector3 rotate_axis = Vector3.Cross(Vector3.up, Vector3.ProjectOnPlane(GameManager.instance.right_controller.velocity, Vector3.up));
                StartCoroutine(tree.TreeFall(rotate_axis));
            }
        }
    }
}
