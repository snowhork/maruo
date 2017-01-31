using UnityEngine;
using System.Collections;

public class MoveCaptain : MonoBehaviour {
    public Transform target;
    UnityEngine.AI.NavMeshAgent captain;

    //public GameObject target;
    //NavMeshAgent captain;
    //Animator animator;
    // public Transform goal;
    //Vector3 start;
    // Use this for initialization
    void Start () {
        captain = GetComponent<UnityEngine.AI.NavMeshAgent>();


        //start = transform.position;
        //var captain = GetComponent<NavMeshAgent>();
        // captain.destination = goal.position;
        //captain.SetDestination(goal.position);
        // NavMeshAgent captain = GetComponent<NavMeshAgent>();
        // captain.speed = 1;
        //captain.SetDestination(target.transform.position);
        // GetComponent<NavMeshAgent>().SetDestination(goal.transform.position);
    }
	
	// Update is called once per frame
	void Update () {
        captain.SetDestination(target.position);

       // if(Input.GetMouseButtonDown(0))
       // {
         //   transform.position = start;
        //}
        //captain.destination = target.transform.position;
        //animator.SetFloat("Speed", captain.velocity.magnitude);
	}
}
