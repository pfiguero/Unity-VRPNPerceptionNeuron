using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UpdateTextClient : MonoBehaviour {

    public Text dest;
    public Text timeText;
    private VRPNString origin;
	// Use this for initialization
	void Start () {
        origin = GetComponent<VRPNString>();
	}
	
	// Update is called once per frame
	void Update () {
        if (origin != null && !origin.isBeenRead())
        {
            var msg = origin.getLastMessage();
            /*
            if (timeText != null && msg.StartsWith("CURTIME "))
                timeText.text = msg;
            else
             */
                dest.text = msg;
        }
	}
}
