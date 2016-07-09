
/* VRPNCmdService.cs
 * Written by Pablo Figueroa, Ph.D.
 * Universidad de los Andes, Colombia
 * Email: pfiguero@uniandes.edu.co
 */

using UnityEngine;
using UnityEngine.UI;

using System;
using System.Collections;
using System.Text;
using System.Runtime.InteropServices;

public class VRPNCmdService : MonoBehaviour {

    private enum ConnectionState  {SYN, ACK, WAIT_FOR_OTHERS, CONNECTED_AND_READY};

    public string client_ID = "CS";
    public string VRPNCmdServiceSenderName = "Client";
    public int VRPNCmdServiceSenderPort = 3885 ;
    public string VRPNCmdServiceRemoteSenderName = "WWAMsg";
    public string VRPNServerIP = "192.168.1.100";

    private ConnectionState m_ConnectionState = ConnectionState.SYN;

    static int lastStringUpdateFrame = -1;

    // This number should be the same as MAX_MSG_LENGTH in C++
    private const int MAX_MSG_LENGTH = 256;

    // Pointer to the string
    private IntPtr msg;

    private String curMsg;
    private bool hasBeenRead = true;

    private bool isInitialized = false;
    private bool m_isConnected = false;

    public bool displayStatus = true;
    private Text status;

    //Receiver methods
    [DllImport("vrpn-wwa")]
    static extern IntPtr initializeStrings(string serverName);
    [DllImport("vrpn-wwa")]
    static extern void updateStrings();
    [DllImport("vrpn-wwa")]
    static extern void endVRPNService();

    //Sender methods
    [DllImport("vrpn-wwa")]
    static extern void vts_createServer(string dName, int portNumber);
    [DllImport("vrpn-wwa")]
    static extern void vts_sendMsg(string msg);
    [DllImport("vrpn-wwa")]
    static extern void vts_update();
    [DllImport("vrpn-wwa")]
    static extern void vts_destroyServer();


    // Use this for initialization
    public void Initialize()
    {
        if (!isInitialized)
        {
            vts_createServer(VRPNCmdServiceSenderName, VRPNCmdServiceSenderPort);
            DontDestroyOnLoad(this);
            isInitialized = true;
            msg = initializeStrings(VRPNCmdServiceRemoteSenderName + "@" + VRPNServerIP);
        }
    }

    // Use this for initialization
	void Start () {
        Initialize();
        m_isConnected = false;
        hasBeenRead = true;
        m_ConnectionState = ConnectionState.SYN;
        if (displayStatus)
        {
            status = GetComponentInChildren<Text>();
            status.text = "VPRNCmdService: Establishing connection to WWA server...";
        }
        
       
	}
	
	// Update is called once per frame
	void Update () {
        vts_update();

        // update the trackers only once per frame
        if (lastStringUpdateFrame != Time.frameCount)
        {
            updateStrings();
            // update the local string

            curMsg = (String)Marshal.PtrToStringAnsi(msg);
			Debug.Log (curMsg);
            //If conneaction is not established yet, read message here and suppress

            if (m_ConnectionState != ConnectionState.CONNECTED_AND_READY)
            {
                switch (m_ConnectionState)
                {
                    case ConnectionState.SYN:
                        if (curMsg != ("SYNACK SV " + client_ID))
                            SendMsg("SYN " + client_ID);
                        else
                            m_ConnectionState = ConnectionState.ACK;
                           
                        break;
                    case ConnectionState.ACK:
                        SendMsg("ACK " + client_ID);
						if (displayStatus)
							status.text = "VPRNCmdService: Acknowledgement received. Wainting for start command...";
                        m_ConnectionState = ConnectionState.WAIT_FOR_OTHERS;
                       
                        break;
                    case ConnectionState.WAIT_FOR_OTHERS:
                        if (curMsg == "CNTREADY SV")
                        {
                            m_ConnectionState = ConnectionState.CONNECTED_AND_READY;
                            m_isConnected = true;
                            if (displayStatus)
                                status.text = "VPRNCmdService: Connected and ready.";

                        }
                        break;
                }

            }
            else
            {
                hasBeenRead = false;
            }
            

            lastStringUpdateFrame = Time.frameCount;
        }
        // It is assumed the data is already in the strData

      
	}

    public String getLastMessage()
    {
        if (!hasBeenRead)
        {
            hasBeenRead = true;
            return curMsg;
        }
        else
        {
            return "";
        }
        
    }
    public bool isBeenRead()
    {
        return hasBeenRead;
    }

    void OnApplicationQuit()
    {
        endVRPNService();
        vts_destroyServer();
    }

    public void SendMsg(string msg)
    {
        vts_sendMsg(msg);
        //Debug.Log("Queue message: " + msg);
    }

    public bool isConnected()
    {
        return m_isConnected;
    }

    public void Reconnect()
    {
        m_isConnected = false;
        m_ConnectionState = ConnectionState.SYN;
    }
    

}
