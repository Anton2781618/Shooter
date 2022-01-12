using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PLayerNetwork : MonoBehaviour
{
    [SerializeField] private GameObject playerCamera;
    [SerializeField] private MonoBehaviour[] playerControlSkripts;

    private PhotonView photonView;

    private void Start()
    {
        photonView = GetComponent<PhotonView>();
        Initialize();
    }

    private void Initialize()
    {
        if(photonView.isMine)
        {

        }
        else
        {
            playerCamera.SetActive(false);

            foreach (MonoBehaviour item in playerControlSkripts)
            {
                item.enabled = false;
            }
        }
    }
}
