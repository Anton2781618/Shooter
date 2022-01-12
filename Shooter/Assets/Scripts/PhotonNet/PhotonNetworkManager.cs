using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PhotonNetworkManager : Photon.MonoBehaviour
{
    [SerializeField] private Text connectText;
    [SerializeField] private GameObject player;
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private GameObject lobbyCamera;

    private void Start()
    {
        PhotonNetwork.ConnectUsingSettings("0.1");
    }

    public virtual void OnJoinedLobby() 
    {
        Debug.Log("подключились к Лобби");

        PhotonNetwork.JoinOrCreateRoom("New", null, null);
    }

    //заходим в комнату 
    public virtual void OnJoinedRoom()
    {
        PhotonNetwork.Instantiate(player.name, spawnPoint.position, spawnPoint.rotation, 0);
        lobbyCamera.SetActive(false);
    }
    

    private void Update()
    {
        connectText.text = PhotonNetwork.connectionStateDetailed.ToString();
    }
}
