using UnityEngine;
using System.Collections;

public class LeftController : MonoBehaviour {

    SteamVR_TrackedObject trackedObject;
    Vector3[] vertexes = new Vector3[warpball_index];
    GameObject[] warp_traces;
    GameObject warp_target;
    GameObject warp_target_arrow;

    bool warpable = false;
    const int warpball_index = 40;
    int end_index = - 1;

    public GameObject warpable_trace_resource;
    public GameObject unwarpable_trace_resource;

    public GameObject warp_target_resource;
    public bool warpflag;



    // Use this for initialization
    void Start()
    {
        trackedObject = GetComponent<SteamVR_TrackedObject>();
        warp_traces = new GameObject[warpball_index];
        warp_target_arrow = Instantiate((GameObject)Resources.Load("UI/WarpTargetArrow"));
        warp_target_arrow.SetActive(false);

    }


    // Update is called once per frame
    void Update()
    {
        var device = SteamVR_Controller.Input((int)trackedObject.index);
        
        Vector3 start_position = transform.position + transform.forward;
        Vector3 prev_position = start_position;
        Vector3 next_position;
        
        for (int i = 0; i < warpball_index; i++)
        {
            Destroy(warp_traces[i]);
        }
        Destroy(warp_target);
        warp_target_arrow.SetActive(false);



        end_index = warpball_index - 1;
        for (int i = 0; i < warpball_index; i++)
        {
            if (Timer.instance.IsRest)
            {
                break;
            }
            vertexes[i] = prev_position;
            next_position = calc_free_fall(start_position, transform.forward, 15f, 0.055f * i);
 
            Vector3 direction = next_position - prev_position;
            Ray ray = new Ray(prev_position, direction);
            RaycastHit hit;
            //Debug.DrawLine(prev_position, next_position);
            warpflag = false;
            warpable = false;
            if (Physics.Raycast(ray, out hit, direction.magnitude))
            {
                end_index = i;
                if (hit.collider.tag != "Field")
                {
                    break;
                }
                
                // warp
                if (Mathf.Abs(GameManager.instance.CameraRig.transform.position.y - hit.point.y) <= 1.2f)
                {
                    warpable = true;
                    warp_target = Instantiate(warp_target_resource);
                    warp_target.transform.position = hit.point + Vector3.up * 0.001f;

                    warp_target_arrow.SetActive(true);
                    warp_target_arrow.transform.position = hit.point + Vector3.up * 1f;

                    
                    if (device.GetPressDown(SteamVR_Controller.ButtonMask.Trigger))
                    {
                        warpflag = true;
                        GameManager.instance.right_controller.ChangeMode(RightController.Mode.NormalMode);
                        GameManager.instance.CameraRig.transform.position = hit.point;

                    }
                }
                    break; 
            }
            prev_position = next_position;
        }

        for (int i = 0; i <= end_index; i++)
        {
            if (warpable)
            {
                warp_traces[i] = Instantiate(warpable_trace_resource);
            }
            else
            {
                warp_traces[i] = Instantiate(unwarpable_trace_resource);
            }
            warp_traces[i].transform.position = vertexes[i];
        }

            //// 自由移動のスクリプト

            //if (device.GetPress(SteamVR_Controller.ButtonMask.Touchpad))
            //{
            //    if (device.GetTouch(SteamVR_Controller.ButtonMask.Touchpad))
            //    {
            //        float speed = 0.075f;
            //        Vector2 touchaxis = device.GetAxis();
            //        Vector3 forward_axis = Vector3.ProjectOnPlane(GameManager.instance.Camera.transform.forward, Vector3.up).normalized;
            //        Vector3 right_axis = Vector3.Cross(Vector3.up, forward_axis);

            //        if(touchaxis.y > 0 )
            //        {
            //            GameManager.instance.CameraRig.transform.position += speed * forward_axis;
            //        }
            //        if (touchaxis.y < 0)
            //        {
            //            GameManager.instance.CameraRig.transform.position += -speed * forward_axis;
            //        }
            //        //GameManager.instance.CameraRig.transform.position += speed* forward_axis * touchaxis.y + speed *right_axis * touchaxis.x;
            //    }
            //}

            //if (device.GetPressDown(SteamVR_Controller.ButtonMask.Trigger))
            //{
            //    if (Physics.Raycast(ray, out hit))
            //    {
            //        pointingObj = hit.transform.gameObject;
            //        grabObject(pointingObj);
            //    }
            //}
            //if (device.GetPressUp(SteamVR_Controller.ButtonMask.Trigger))
            //{
            //    if (targetObj != null)
            //    {
            //        //targetObj.GetComponent<Rigidbody>().velocity = device.velocity*hit.distance;
            //        //targetObj.GetComponent<Rigidbody>().angularVelocity = device.angularVelocity;
            //        //releaseObject();
            //    }
            //}
            //if (device.GetTouchDown(SteamVR_Controller.ButtonMask.Trigger))
            //{
            //    print("トリガーを浅く引いた");
            //}
            //if (device.GetTouch(SteamVR_Controller.ButtonMask.Trigger))
            //{
            //    print("トリガーを浅く引いている");
            //}

            //if (device.GetPressDown(SteamVR_Controller.ButtonMask.Trigger))
            //{
            //    print("トリガーを深く引いた");
            //}
            //if (device.GetPress(SteamVR_Controller.ButtonMask.Trigger))
            //{
            //    print("トリガーを深く引いている");
            //}
            //if (device.GetPressUp(SteamVR_Controller.ButtonMask.Trigger))
            //{
            //    print("トリガーを離した");
            //}
            //if (device.GetTouchUp(SteamVR_Controller.ButtonMask.Trigger))
            //{
            //    print("トリガーを離した");
            //}

        }

    private Vector3 calc_free_fall(Vector3 start_position, Vector3 forward, float start_speed, float time)
    {
        float forward_angle = Vector3.Angle(forward, Vector3.up) * Mathf.Deg2Rad;
        float vertical_start_speed = Mathf.Cos(forward_angle)* start_speed;
        float horizontal_start_speed = Mathf.Sin(forward_angle) * start_speed;

        start_position += (horizontal_start_speed * time) * Vector3.ProjectOnPlane(forward, Vector3.up);
        //start_position += (horizontal_start_speed * time) * Vector3.ProjectOnPlane(forward, Vector3.up);

        start_position += (vertical_start_speed* time - 0.5f * 9.81f * time * time)* Vector3.up;

        return start_position;
    }

}
