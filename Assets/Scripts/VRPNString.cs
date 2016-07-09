/* VRPNString.cs
 * Written by Pablo Figueroa, Ph.D.
 * Visiting Scholar, Institute for Creative Technologies
 * Associate Professor, Universidad de los Andes, Colombia
 * Email: pfiguero@uniandes.edu.co
 */

using UnityEngine;
using System;
using System.Collections;
using System.Text;
using System.Runtime.InteropServices;

public class VRPNString : MonoBehaviour
{
    // the name of the VRPN string receiver
    public string device = "StrGenerator";

    // the address of the server
    // leave as localhost if running on same computer
    public string server = "localhost";

    // the string number
    // assuming there is only one string stream per device
    // public int strNumber = 0;

    static int lastStringUpdateFrame = -1;

    // This number should be the same as MAX_MSG_LENGTH in C++
    private const int MAX_MSG_LENGTH = 256;

    // Pointer to the string
    private IntPtr msg;

    private String curMsg;
    private bool hasBeenRead = true;

    [DllImport("vrpn-wwa")]
    static extern IntPtr initializeStrings(string serverName);
    [DllImport("vrpn-wwa")]
    static extern void updateStrings();
    [DllImport("vrpn-wwa")]
    static extern void endVRPNService();

        // Use this for initialization
    void Start()
    {
        msg = initializeStrings(device + "@" + server);
    }

    // Update is called once per frame
    void Update()
    {
        // update the trackers only once per frame
        if (lastStringUpdateFrame != Time.frameCount)
        {
            updateStrings();
            // update the local string

            curMsg = (String)Marshal.PtrToStringAnsi(msg);

            hasBeenRead = false;

            lastStringUpdateFrame = Time.frameCount;
        }
        // It is assumed the data is already in the strData

    }
    public String getLastMessage()
    {
        hasBeenRead = true;
        return curMsg;
    }
    public bool isBeenRead()
    {
        return hasBeenRead;
    }
    
	void OnApplicationQuit()
    {
        endVRPNService();
    }

}
