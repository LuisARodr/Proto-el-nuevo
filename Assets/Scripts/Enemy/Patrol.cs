using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Patrol : MonoBehaviour {

    /*public Transform pos1;
    public Transform pos2;*/

    private Rigidbody rigidBody;
    //private NavMeshAgent agent;

    [SerializeField]
    float velocity;

    private void Start() {
        rigidBody = GetComponent<Rigidbody>();
        //agent = gameObject.GetComponent<NavMeshAgent>();
    }

    void Update() {
        rigidBody.velocity = -Vector3.forward * velocity * Time.deltaTime;
    }

    /*private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "1")
        {
            agent.SetDestination(pos2.position);
        }
        if (other.tag == "2")
        {
            agent.SetDestination(pos1.position);
        }
    }*/
}
