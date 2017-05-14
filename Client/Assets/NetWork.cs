using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SocketIO;
using System;

public class NetWork : MonoBehaviour {
    static SocketIOComponent socket;
    public GameObject playerPrefs;
    public GameObject myplayer;

    Dictionary<string, GameObject> players;
    // Use this for initialization
    void Start () {
        socket = GetComponent<SocketIOComponent>();
        socket.On("open",OnConnected);
        socket.On("spawn", OnSpawn);
        socket.On("move", OnMove);


        socket.On("disconnected", OnDisconnected);
        socket.On("requestpostion", Onrequestpostion);
        socket.On("updatePostion", OnUpdatepostion);


        players = new Dictionary<string, GameObject>();
    }


    // Update is called once per frame
    void OnConnected(SocketIOEvent e) {
        Debug.Log("connected");
        //socket.Emit("move");
	}
    void OnSpawn(SocketIOEvent e)
    {
        Debug.Log("spawn "+e.data);
        var player=Instantiate(playerPrefs);
        players.Add(e.data["id"].ToString(), player);
        Debug.Log("count:"+ players.Count);
    }
     void OnMove(SocketIOEvent e)
    {
        Debug.Log("player is moved "+e.data);
        
        var pos = new Vector3(GetFloatFromJeson(e.data, "x"), GetFloatFromJeson(e.data, "y"), GetFloatFromJeson(e.data, "z"));
    
        var player = players[e.data["id"].ToString()];
        Debug.Log(player.name);

       
        var navigatepos = player.GetComponent<nav>();
        navigatepos.NavigateTo(pos);
    }
  
    private void Onrequestpostion(SocketIOEvent e)
    {
        Debug.Log("server request postion");
        socket.Emit("updatePostion", new JSONObject(VectorToJeson(myplayer.transform.position)));
    }
    void OnUpdatepostion(SocketIOEvent e)
    {
        Debug.Log("updatpostionold player"+ e.data);
      var positions = new Vector3(GetFloatFromJeson(e.data, "x"), GetFloatFromJeson(e.data, "y"), GetFloatFromJeson(e.data, "z"));
       var player = players[e.data["id"].ToString()];
       player.transform.position = positions;
    }
    void OnDisconnected(SocketIOEvent e)
    {
        Debug.Log("disconnected"+e.data);
        var id = e.data["id"].ToString();
        var player = players[id];
        Destroy(player);
        players.Remove(id);
    }

  


    float GetFloatFromJeson(JSONObject data, string key)
    {
        return float.Parse(data[key].ToString().Replace("\"", ""));
    }
   public static string VectorToJeson(Vector3 vector)
    {
        return string.Format(@"{{""x"":""{0}"",""y"":""{1}"",""z"":""{2}""}}", vector.x, vector.y, vector.z);
    }
}
