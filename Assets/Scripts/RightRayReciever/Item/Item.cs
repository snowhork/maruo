using UnityEngine;
using System.Collections;

public class Item : RightRayReciever {
    public Rigidbody Rigidbody => gameObject.GetComponent<Rigidbody>();
    public IItemState State = ItemState.StaticState.Instance;

    void Start()
    {
        gameObject.AddComponent<Rigidbody>();

    }

    void Update()
    {
        State.MoveOnUpdate(this);
    }

    void OnCollisionEnter(Collision collision)
    {
        State.OnCollisionEnter(this, collision);
    }

    void OnTriggerEnter(Collider collider)
    {
        State.OnTriggerEnter(this, collider);
    }

    public override void Select(RightViveController controller)
    {
        controller.HoldItem = this;
		controller.State = RightControllerState.HoldState.Instance;
        State = ItemState.DrawState.Instance;
    }

    public void Release(RightViveController controller)
    {
        State = ItemState.DropState.Instance;
        Rigidbody.isKinematic = false;
        Rigidbody.AddForce(controller.Velocity * 300f);
        Rigidbody.AddForce(Vector3.down * 50f);
    }
}
