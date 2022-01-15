using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class PhotonManager : MonoBehaviourPunCallbacks
{
    [SerializeField] private string region;
    [SerializeField] private string nicName;
    [SerializeField] private InputField roomName;
    [SerializeField] private ListItems itemPrefab;
    [SerializeField] private Transform content;
    [SerializeField] private Transform spawnPoint;

    List<RoomInfo> allRoomInfo = new List<RoomInfo>();
    private GameObject player;
    [SerializeField] private GameObject playerPref;

    void Start()
    {
        nicName = Random.Range(1, 500).ToString();
        PhotonNetwork.ConnectUsingSettings();
        PhotonNetwork.ConnectToRegion(region);

        if(SceneManager.GetActiveScene().name == "FPS")
        {
            player = PhotonNetwork.Instantiate(playerPref.name, spawnPoint.position, Quaternion.identity);
        }
    }

    //при конекте к мастер серверу подключаемся к лобби
    public override void OnConnectedToMaster()
    {
        Debug.Log("Вы пдключины к " + region);
        PhotonNetwork.NickName = nicName;

        if(!PhotonNetwork.InLobby)PhotonNetwork.JoinLobby();
    }

    //при отключении 
    public override void OnDisconnected(DisconnectCause cause)
    {
        Debug.Log("Вы отключены от сервера");
    }

    //по кнопке содаем комнату
    public void CreateRoomButton()
    {
        if(!PhotonNetwork.IsConnected)return;

        if(PhotonNetwork.NickName == "")PhotonNetwork.NickName = "User";

        RoomOptions roomOptions  = new RoomOptions();
        roomOptions.MaxPlayers = 2;
        PhotonNetwork.CreateRoom(roomName.text, roomOptions, TypedLobby.Default);
    }

    //когда комната создана, выдается сообщение о создании
    public override void OnCreatedRoom()
    {
        Debug.Log("Создана комната, имя комнаты: " + PhotonNetwork.CurrentRoom.Name);  
    }

    //если создаь комнату не получилось то отработает этот метод
    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        Debug.Log("Не удалось создать ");
    }

    //обновить лист комнат, создать префаб комнаты в контейнере
    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        foreach (var info in roomList)
        {
            for (int i = 0; i < allRoomInfo.Count; i++)
            {
                if(allRoomInfo[i].masterClientId == info.masterClientId)return;
            }

            ListItems listItems = Instantiate(itemPrefab, content);
            
            if(listItems)
            {
                listItems.SetInfo(info);
                allRoomInfo.Add(info);
            }
        }
    }

    // подключиться к комнате
    public override void OnJoinedRoom()
    {
        PhotonNetwork.LoadLevel("FPS");
    }

    //на кнопку найти комнат со свободным местом и подключиться
    public void JoinRandomRoom()
    {
        PhotonNetwork.JoinRandomRoom();
    }

    //подключиться к комнате
    public void JoinButton()
    {
        PhotonNetwork.JoinRoom(roomName.text);
    }

    //выйти из комноты
    public void LeaveButton()
    {
        PhotonNetwork.LeaveRoom();
    }

    //после выхода из комнаты загружаемся в гл меню
    public override void OnLeftRoom()
    {
        PhotonNetwork.Destroy(player);
        PhotonNetwork.LoadLevel("Главное Меню");
    }
}
