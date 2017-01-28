using UnityEngine;
using System.Collections;

public class RayCaster : MonoBehaviour {

    protected RayReciever Reciever = null;

    private void RayOn(RayReciever reciever)
    {
        if (reciever == null)
        {
            return;
        }
        reciever.RayOn(this);
    }

    private void RayOff(RayReciever reciever)
    {
        if(reciever == null)
        {
            return;
        }
        reciever.RayOff(this);
    }

    protected RaycastHit RayCast(Ray ray)
    {
        RaycastHit hit;
        if(Physics.Raycast(ray, out hit))
        {
            var reciever = hit.collider.GetComponent(typeof(RayReciever)) as RayReciever;
            if(Reciever == reciever)
            {
                RayOn(reciever);
                return hit;
            }
            RayOff(Reciever);
            RayOn(reciever);
            Reciever = reciever;
        }
        else
        {
            RayOff(Reciever);
            Reciever = null;
        }
        return hit;
    }
}
