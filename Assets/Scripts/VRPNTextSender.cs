/* VRPNTextSender.cs
 * Written by Pablo Figueroa, Ph.D.
 * Universidad de los Andes, Colombia
 * Email: pfiguero@uniandes.edu.co
 */

using UnityEngine;
using System;
using System.Collections;
using System.Runtime.InteropServices;

public class VRPNTextSender : MonoBehaviour 
{
    public string deviceName = "TextSender";
    public int port=4500;

    [DllImport("vrpn-wwa")]
    static extern void vts_createServer(string dName, int portNumber);
    [DllImport("vrpn-wwa")]
    static extern void vts_sendMsg(string msg);
    [DllImport("vrpn-wwa")]
    static extern void vts_destroyServer();


    // Use this for initialization
    void Awake ()
    {
        vts_createServer(deviceName, port);
	}

    public void SendMsg(string msg)
    {
        vts_sendMsg(msg);
        //Debug.Log("Queue message: " + msg);
    }

    public void pause()
    {
        vts_sendMsg("PAUSE");
    }

    public void play()
    {
        vts_sendMsg("CONTINUE");
    }

    public void replay()
    {
        vts_sendMsg("REPLAY");
    }

    // Update is called once per frame
    void Update()
    {
        //vts_update();
	}


    void OnApplicationQuit()
    {
        vts_destroyServer();
    }
}
