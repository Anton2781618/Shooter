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
        IPHostEntry host1 = Dns.GetHostEntry("www.microsoft.com");
        Debug.Log(host1.HostName);

        foreach (var ip in host1.AddressList)
        {
            Debug.Log(ip.ToString());
        }

        IPHostEntry host2 = Dns.GetHostEntry("www.google.com");
        Debug.Log(host2.HostName);

        foreach (var ip in host2.AddressList)
        {
            Debug.Log(ip.ToString());
        }
    }

    [ContextMenu("DownloadFile")]
    public async Task DownloadFile()
    {
        WebRequest request = WebRequest.Create("http://somesite.com/myfile.txt");
        WebResponse responce = request.GetResponse();

        using (Stream stream = responce.GetResponseStream())
        {
            using (StreamReader reader = new StreamReader(stream))
            {
                string line = "";
                while ((line = reader.ReadLine()) != null)
                {
                    Debug.Log(line);
                }
            }
        }
        responce.Close();
        Debug.Log("Выполнено");
    }
}


