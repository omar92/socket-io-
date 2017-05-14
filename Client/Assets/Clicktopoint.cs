using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clicktopoint : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetButtonDown("Fire1"))
        {
            clicked();
        }
	}

     void clicked()
    {
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit = new RaycastHit();
        if (Physics.Raycast(ray, out hit))
        {
            Debug.Log(hit.collider.gameObject.name);
            var ClickMove = hit.collider.gameObject.GetComponent<Clickmove>();
            ClickMove.OnClick(hit.point);
        }

    }
}
