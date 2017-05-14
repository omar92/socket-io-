using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SocketIO;
public class NetworkMove : MonoBehaviour {
    public SocketIOComponent socket;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void OnMove(Vector3 postion)
    {
        Debug.Log("send postion to node js" + NetWork.VectorToJeson( postion));
        socket.Emit("move",new JSONObject (NetWork.VectorToJeson(postion)));
    }
   
}
