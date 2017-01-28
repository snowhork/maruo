using UnityEngine;
using System.Collections;

public class RightViveController : ViveController {
    public Item HoldItem { get; set; }
    public IRightControllerState State { get; set; }
    new public RightRayReciever Reciever { get
        {
            if (base.Reciever == null)
            {
                return null;
            }
            return base.Reciever.GetComponent(typeof(RightRayReciever)) as RightRayReciever;
        } }

	protected override void Start () {
        base.Start();
        State = RightControllerState.FreeState.Instance;
	}

    void Update()
    {
        State.BeforeUpdateAction(this);
        var ray = new Ray(transform.position, transform.forward);
        RayCast(ray);
        if (PressDown)
        {
            State.PressDownAction(this);
        }
        if (PressUp)
        {
            State.PressUpAction(this);
        }
        State.UpdateAction(this);
    }
}
