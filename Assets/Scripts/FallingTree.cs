using UnityEngine;
using System.Collections;

public class FallingTree : MonoBehaviour {
    float start_y;
    float falling_count = 0f;
    float falling_angle = 0f;
    bool falling = false;
   

    int hp = 3;
    Vector3 fall_direction;
    // Use this for initialization
    void Start()
    {
        start_y = transform.position.y;
        Mesh mesh = GetComponent<MeshFilter>().mesh;
        for (int i = 0; i < mesh.vertices.Length; i++)
        {

          //  print(i + "----------");
          //  print(mesh.vertices[i]);
        }
    
    }
	
	// Update is called once per frame
	void Update () {
 
        if (falling && falling_angle <= 90f)
        {
            float deltatime = Time.deltaTime;
            falling_count += deltatime;
            falling_angle += 1.5f*Mathf.Cos(falling_count*Mathf.Deg2Rad);
           Vector3 rotaion_axis = Vector3.Cross(Vector3.up, fall_direction);

            transform.Rotate(rotaion_axis, 1.5f*Mathf.Cos(falling_count * Mathf.Deg2Rad));
        }
	}

    void OnTriggerEnter(Collider other)
    {
        print(other.name);
        if (other.tag == "RightController")
        {
            StartCoroutine(vibration());
            hp--;
            if (hp == 0)
            {
                fall_direction = Vector3.ProjectOnPlane(
                 GameManager.instance.right_controller.velocity,
                 Vector3.up).normalized;
                
                falling = true;
                hp--;
            }
        }
    }

    IEnumerator vibration()
    {
        for(float t = 0f; t < 0.5f; t += Time.deltaTime)
        {
            GameManager.instance.right_controller.vibration(2000);
            yield return null;
        }
        
    }
}
