using UnityEngine;
using System.Collections;

public class Item : RightRayReciever {
    public Rigidbody Rigidbody
    {
        get
        {
            return gameObject.GetComponent<Rigidbody>();
        }
    }

    void Start()
    {
        gameObject.AddComponent<Rigidbody>();
        
    }

	public override void Select(RightViveController controller)
    {
        Tracking(controller.transform.position);
        controller.HoldItem = this;
        controller.State = RightControllerState.HoldState.Instance;
    }

    public override void Holding(RightViveController controller)
    {
        Tracking(controller.transform.position);
    }

    public override void Release(RightViveController controller)
    {
        Rigidbody.isKinematic = false;
        Rigidbody.AddForce(controller.Velocity * 300f);
        Rigidbody.AddForce(Vector3.down * 50f);
    }

    private void Tracking(Vector3 position)
    {
        transform.position = Vector3.Lerp(transform.position, position, 0.2f);
        Rigidbody.isKinematic = true;
    }

}
