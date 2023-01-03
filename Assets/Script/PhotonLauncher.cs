using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PhotonLauncher : MonoBehaviourPunCallbacks
{
    // Start is called before the first frame update
    void Start()
    {
        RoomOption();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void RoomOption()
    {
        PhotonNetwork.CreateRoom(null, new Photon.Realtime.RoomOptions());
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("OnJoinedRoom");
    }
}
