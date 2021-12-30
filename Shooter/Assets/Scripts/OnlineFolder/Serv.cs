using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.IO;
using System.Threading.Tasks;
using UnityEngine;
using System;

public class Serv : MonoBehaviour
{
    
    [ContextMenu("ServTest")]
    public void ServTest()
    {
        IPHostEntry host1 = Dns.GetHostEntry("yandex.ru");
        Debug.Log(host1.HostName);

        foreach (var ipObject in host1.AddressList)
        {
            Debug.Log(ipObject.ToString());
        }
    }

    [ContextMenu("DownloadFile")]
    public void DownloadFile()
    {
        WebClient client = new WebClient();
        client.DownloadFile("https://yandex.ru/images/search?text=rfhn&from=tabbar&pos=0&img_url=https%3A%2F%2Fpngimg.com%2Fuploads%2Fcards%2Fcards_PNG8471.png&rpt=simage", "2Fc.png");
        Debug.Log("ddd");
    }
}