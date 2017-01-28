using UnityEngine;
using System.Collections;

public class ViveController : RayCaster {
    SteamVR_TrackedObject trackedObject;
    SteamVR_Controller.Device device;


    public Vector3 Velocity { get
        {
            return device.velocity;
        }
    }

    protected bool PressDown { get
        {
            return device.GetPressDown(SteamVR_Controller.ButtonMask.Trigger);
        }
    }

    protected bool Pressing
    {
        get
        {
            return device.GetPress(SteamVR_Controller.ButtonMask.Trigger);
        }
    }

    protected bool PressUp
    {
        get
        {
            return device.GetPressUp(SteamVR_Controller.ButtonMask.Trigger);
        }
    }

    protected virtual void Start()
    {
        trackedObject = GetComponent<SteamVR_TrackedObject>();
        device = SteamVR_Controller.Input((int)trackedObject.index);
    }

    public void Vibration(ushort p, float vibration_time)
    {
        StartCoroutine(vibration_coroutine(p, vibration_time));
    }

    /* private */

    private void vibration(ushort p)
    {
        device.TriggerHapticPulse(p);
    }
    private IEnumerator vibration_coroutine(ushort p, float vibration_time)
    {
        for (float time = 0; time < vibration_time; time += Time.deltaTime)
        {
            vibration(p);
            yield return null;
        }
        yield break;
    }
}
