using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class nav : MonoBehaviour {
    NavMeshAgent agent;
	// Use this for initialization
	void Start () {
        agent = GetComponent<NavMeshAgent>();
       
	}
	
	// Update is called once per frame
	public void NavigateTo (Vector3 position) {
        agent.SetDestination(position);

	}
    void Update()
    {
        GetComponent<Animator>().SetFloat("Distince",agent.remainingDistance);

    }
}
