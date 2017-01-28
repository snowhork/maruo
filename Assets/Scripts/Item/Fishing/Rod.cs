using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Rod : MonoBehaviour {

    const int rod_line_index = 50;
    int end_index = -1;
    LineRenderer line_renderer;
    Vector3[] vertexes = new Vector3[rod_line_index+1];
    bool pointing_flag;
    Vector3 pointed_position;

    GameObject bobber;
    Fish fish;
    bool is_bobber_down = false;
    Vector3 bobber_down_position = 0.3f * Vector3.down;

    // Use this for initialization
    void Start () {
        line_renderer = GetComponent<LineRenderer>();
        line_renderer.SetColors(Color.white, Color.white);
        line_renderer.SetWidth(0.02f, 0.02f);
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (GameManager.instance.SelectedGameObject == gameObject)
        {
            float forward_distance = 0.80f;
            line_renderer.enabled = true;
            Vector3 start_position = transform.position;
            Vector3 controller_forward = GameManager.instance.right_controller.transform.forward;
            vertexes[0] = start_position + controller_forward * forward_distance;
            line_renderer.SetPosition(0, vertexes[0]);

            Destroy(bobber);
            if (is_bobber_down)
            {
                line_renderer.SetVertexCount(2);
                line_renderer.SetPosition(1, vertexes[1]);
                GameObject resource = (GameObject)Resources.Load("Item/10_bobber");
                
                bobber = (GameObject)Instantiate(resource);
                bobber.transform.FindChild("bobber02").transform.position += bobber_down_position;

                bobber.transform.position = vertexes[1];

                fishing_process();
            }
            else
            {       
                float start_velocity = 10.0f;               
                pointing_flag = false;
                
                for (int i = 0; i < rod_line_index; i++)
                {
                    vertexes[i + 1] = calc_free_fall(vertexes[0], controller_forward, start_velocity, 0.08f * i);

                    Vector3 direction = vertexes[i + 1] - vertexes[i];

                    Ray ray = new Ray(vertexes[i], direction);
                    RaycastHit hit;

                    line_renderer.SetVertexCount(i + 1 + 1);
                    line_renderer.SetPosition(i + 1, vertexes[i + 1]);
                    if (Physics.Raycast(ray, out hit, direction.magnitude) && hit.collider.tag == "Water")
                    {
                        GameObject resource = (GameObject)Resources.Load("Item/10_bobber");
                        bobber = (GameObject)Instantiate(resource);
                        bobber.transform.position = pointed_position;
                        bobber.transform.parent = gameObject.transform;
                        //bobber.transform.Rotate(Vector3.right, resource.transform.rotation.eulerAngles.x);
                        //bobber.transform.Rotate(Vector3.up, resource.transform.rotation.eulerAngles.y);
                        //bobber.transform.Rotate(Vector3.forward, resource.transform.rotation.eulerAngles.z);
                        //bobber.transform.localPosition += resource.transform.position;
                        pointing_flag = true;
                        pointed_position = hit.point;
                        break;
                    }
                }
            }
        }
        else
        {
            line_renderer.enabled = false;
            Destroy(bobber);
        }
    }

    public bool isPointing
    {
        get
        {
            return pointing_flag;
        }
    }

    public Vector3 PointedPosition
    {
        get
        {
            return pointed_position;
        }
    }

    private Vector3 calc_free_fall(Vector3 start_position, Vector3 forward, float start_speed, float time)
    {
        float forward_angle = Vector3.Angle(forward, Vector3.up) * Mathf.Deg2Rad;
        float vertical_start_speed = Mathf.Cos(forward_angle) * start_speed;
        float horizontal_start_speed = Mathf.Sin(forward_angle) * start_speed;

        start_position += (horizontal_start_speed * time) * Vector3.ProjectOnPlane(forward, Vector3.up);

        start_position += (vertical_start_speed * time - 0.5f * 8f * time * time) * Vector3.up;

        return start_position;
    }

    public void bobber_down(Fish fish)
    {
        this.fish = fish;
        is_bobber_down = true;
        vertexes[1] = bobber.transform.position;
    }

    public void fishing_process()
    {
        if(GameManager.instance.right_controller.velocity.y > 2f)
        {
            is_bobber_down = false;
            fish.fishing();
            fish = null;
            GameManager.instance.right_controller.vibration(1500, 0.65f);
        }
    }

    void OnDestroy()
    {
        Destroy(bobber);
    }

    public Fish Fish
    {
        get
        {
            return fish;
        }
    }
    
}
