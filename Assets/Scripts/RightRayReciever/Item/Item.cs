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
		PositionTracking(controller.transform.position);
        controller.HoldItem = this;
		controller.State = RightControllerState.DrawState.Instance;
    }

    public override void Holding(RightViveController controller)
    {
		PositionHolding(controller.transform.position);
    }

	public override void Drawing(RightViveController controller)
	{
		PositionTracking(controller.transform.position);
		if (Vector3.Distance (transform.position, controller.transform.position) <= 0.01f) {
			controller.State = RightControllerState.HoldState.Instance;

		}
	}

    public override void Release(RightViveController controller)
    {
        Rigidbody.isKinematic = false;
        Rigidbody.AddForce(controller.Velocity * 300f);
        Rigidbody.AddForce(Vector3.down * 50f);
    }

    private void PositionTracking(Vector3 position)
    {
        transform.position = Vector3.Lerp(transform.position, position, 0.2f);
        Rigidbody.isKinematic = true;
    }

	private void PositionHolding(Vector3 position)
	{
		transform.position = position;
		Rigidbody.isKinematic = true;
	}

}
