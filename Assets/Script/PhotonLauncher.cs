using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class PhotonLauncher : MonoBehaviourPunCallbacks
{
    [SerializeField] byte maxPlayer = 8;

    RoomOptions room = new RoomOptions();

    private void Awake()
    {
        // 룸 내의 로드된 레벨 동기화
        PhotonNetwork.AutomaticallySyncScene = true;
    }

    private void Start()
    {
        SetPhotonNetWork();
        Debug.Log(PhotonNetwork.ServerAddress);
    }

    private void Update()
    {
        //StatusServer();
    }

    public void SetPhotonNetWork()
    {
        // PhotonServer 연결
        PhotonNetwork.ConnectUsingSettings();

        if (PhotonNetwork.IsConnected)
        {
            Debug.Log("서버 연결 완료");
            room.CustomRoomProperties = new ExitGames.Client.Photon.Hashtable();
        }

        else
        {
            Debug.Log("서버 연결 실패");
            PhotonNetwork.ConnectUsingSettings();
        }
    }

    // MasterClient 연결 시 호출되는 Callback 함수
    public override void OnConnectedToMaster()
    {
        Debug.Log("OnMaster");
        //PhotonNetwork.JoinRandomRoom();
        //PhotonNetwork.JoinOrCreateRoom("test", new RoomOptions { MaxPlayers = maxPlayer }, null);
        PhotonNetwork.JoinRandomOrCreateRoom(null, maxPlayer);
    }

    public override void OnCreatedRoom()
    {
        Debug.Log("방 생성 성공");
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        Debug.Log("방 생성 실패");
    }

    // 방 참가 시 호출되는 CallBack 함수
    public override void OnJoinedRoom()
    {
        Debug.Log("방 입장 성공");
        Debug.Log(PhotonNetwork.CurrentRoom.PlayerCount);
    }

    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        Debug.Log("방 입장 실패");
        Debug.Log(message);
        Debug.Log("Returncode : " + returnCode);
        PhotonNetwork.JoinRandomOrCreateRoom(null, maxPlayer);
        //PhotonNetwork.CreateRoom(null, new RoomOptions { MaxPlayers = maxPlayer});
    }

    // 서버 상태 표시
    public void StatusServer()
    {
        Debug.Log(PhotonNetwork.NetworkClientState);
    }

    // 서버와 연결이 끊겼을 때 호출되는 함수
    public void OnDisconnectedFromServer(DisconnectCause Cause)
    {
        Debug.LogError("서버 연결 끊김");
    }
}
