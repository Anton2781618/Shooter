using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using TMPro;

public class GameManagerNew : MonoBehaviour
{
    [SerializeField] private Text textLastMessage; 
    [SerializeField] private InputField textMessageField;

    private PhotonView photonView;

    private void Start() 
    {
        photonView = GetComponent<PhotonView>();
    }

    public void SendButton()
    {
        photonView.RPC("Send_Data", RpcTarget.AllBuffered, PhotonNetwork.NickName, textMessageField.text);
    }

    [PunRPC]
    private void Send_Data(string nick, string message)
    {
        textLastMessage.text = nick + " " + message;
    }

}
