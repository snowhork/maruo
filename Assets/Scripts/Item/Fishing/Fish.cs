using UnityEngine;
using System.Collections;
using UnityEngine.AI;
using UniRx;
using UniRx.Triggers;

public class Fish : MonoBehaviour {

    [SerializeField]
    Transform[] goals = new Transform[goals_num];
    const int goals_num = 5; 
    NavMeshAgent agent;

    [SerializeField]int old_destination_index;
    [SerializeField]
    int destination_index;

    [SerializeField]
    float random_swim_speed = 1.5f;

    [SerializeField]
    float approach_swim_speed = 0.8f;

    [SerializeField]
    float approach_distance = 8f;

    [SerializeField]
    float catching_distance = 0.8f;

    float catching_time = 0f;

    [SerializeField]
    Const.ItemName itemtype = Const.ItemName.Fish;

    void set_destination(int index)
    {
        if(index >= destination_index)
        {
            index += 1;
        }
        old_destination_index = destination_index;
        destination_index = index;
        agent.destination = goals[index].position;
    }


    void reset_swim()
    {
        agent.speed = random_swim_speed;
        agent.destination = goals[destination_index].position;
    }

    bool is_approaching()
    {
        if (GameManager.instance.SelectedItem == Const.ItemName.Rod)
        {
            Rod rod = GameManager.instance.SelectedGameObject.GetComponent<Rod>();
            
            if (rod.Fish == this || (rod.isPointing && rod.Fish == null)) 
            {
                if (Vector3.Distance(rod.PointedPosition, transform.position) <= approach_distance)
                {
                    
                    return true;
                }
            }
        }
        return false;
    }

    bool is_catching()
    {
       
        if (GameManager.instance.SelectedItem == Const.ItemName.Rod)
        {
            Rod rod = GameManager.instance.SelectedGameObject.GetComponent<Rod>();

            if (rod.Fish == this || (rod.isPointing && rod.Fish == null))
            {
                if (Vector3.Distance(rod.PointedPosition, transform.position) <= catching_distance)
                {
                    GameManager.instance.right_controller.vibration(2000, 0.01f);
                    return true;
                }
            }
        }
        return false;
    }

    public void fishing()
    {
        GameObject item = (GameObject)Instantiate(Resources.Load("Item/" + (int)itemtype));
        item.transform.position = transform.position + Vector3.up*0.5f;
        Vector3 force = Vector3.up * 5f + GameManager.instance.camera.transform.position - transform.position;
        SoundController.instance.Play("WaterSound");


        item.transform.GetComponent<Rigidbody>().useGravity = true;
        item.transform.GetComponent<Rigidbody>().AddForce(force*80f);
        Destroy(gameObject);
    }

    void bobber_down()
    {
        catching_time = 0f;
        Rod rod = GameManager.instance.SelectedGameObject.GetComponent<Rod>();
        rod.bobber_down(this);
    }

    // Update is called once per frame
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.speed = random_swim_speed;
        old_destination_index = Random.Range(0, goals_num);
        destination_index = Random.Range(0, goals_num);
        set_destination(Random.Range(0, goals_num-1));

        //Observable.
        this.UpdateAsObservable()
            .Select(_ => is_catching())
            .DistinctUntilChanged()
            .Where(x => x)
            .Subscribe(_ => bobber_down());


        reset_swim();
    }


	void Update () {
        agent.enabled = true;
        if (is_approaching())
        {
            Rod rod = GameManager.instance.SelectedGameObject.GetComponent<Rod>();
            agent.speed = approach_swim_speed;
            agent.destination = rod.PointedPosition;  
            if (is_catching())
            {
                agent.enabled = false;
                catching_time += Time.deltaTime;
                if(catching_time >= 1f)
                {

                }
                //GameManager.instance.right_controller.vibration(1000);
            }
        }
        else
        {
            reset_swim();
            float max_distance = (agent.destination - goals[old_destination_index].position).magnitude;
            float distance = (agent.destination - transform.position).magnitude;
            if (Random.Range(0f, 1f) * Random.Range(0f, 1f) < 1f - distance / max_distance)
            {
                set_destination(Random.Range(0, goals_num - 1));

            }
        }
    }
}
