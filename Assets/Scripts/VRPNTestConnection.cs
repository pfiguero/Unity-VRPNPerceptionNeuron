/* VRPNTestConnection.cs
 * Written by Evan A. Suma, Ph.D.
 * Institute for Creative Technologies
 * University of Southern California
 * Email: suma@ict.usc.edu
 */

using UnityEngine;
using System;
using System.Collections;
using System.Runtime.InteropServices;

public class VRPNTestConnection : MonoBehaviour 
{

    // the address of the server
    // leave as localhost if running on same computer
    public string serverName = "Tracker0@localhost";

    // the axis to apply the movement on
    public enum Type { TRACKER, BUTTON, ANALOG };
    public Type serverType = Type.TRACKER;

    [DllImport("vrpnclient")]
    static extern bool isTrackerConnected(string serverName);
    [DllImport("vrpnclient")]
    static extern bool isButtonConnected(string serverName);
    [DllImport("vrpnclient")]
    static extern bool isAnalogConnected(string serverName);

	// Use this for initialization
	void Start ()
    {
	
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (serverType == Type.TRACKER)
        {
            if (isTrackerConnected(serverName))
            {
                ConnectionTestSuccessful();
            }
            else
            {
                ConnectionTestFailed();
            }
        }
        else if (serverType == Type.BUTTON)
        {
            if (isButtonConnected(serverName))
            {
                ConnectionTestSuccessful();
            }
            else
            {
                ConnectionTestFailed();
            }
        }
        else if (serverType == Type.ANALOG)
        {
            if (isAnalogConnected(serverName))
            {
                ConnectionTestSuccessful();
            }
            else
            {
                ConnectionTestFailed();
            }
        }
	}


    void ConnectionTestSuccessful()
    { 
        // fill in whatever action you want to perform when VRPN is connected
    }

    void ConnectionTestFailed()
    { 
        // fill in whatever action you want to perform when VRPN is disconnected
    }
}
