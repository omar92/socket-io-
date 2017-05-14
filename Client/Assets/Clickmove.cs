using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class Clickmove : MonoBehaviour {
    public GameObject player;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	public void OnClick (Vector3 position1) {
        var navpos = player.GetComponent<nav>();
        var netmove = player.GetComponent<NetworkMove>();

        navpos.NavigateTo(position1);
        netmove.OnMove(position1);
    }

}
