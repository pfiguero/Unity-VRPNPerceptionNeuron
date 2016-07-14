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
        if (origin != null)
        {
            var msg = origin.getLastMessage();
            if(msg.Length > 0)
                dest.text = msg;
        }
	}
}
