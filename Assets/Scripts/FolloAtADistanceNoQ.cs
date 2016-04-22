/* FolloAtADistanceNoQ.cs
 * Written by Pablo Figueroa, Ph.D.
 * Universidad de los Andes, Colombia
 * Visiting Scholar at Institute for Creative Technologies
 * Email: pfiguero@uniandes.edu.co
 */

using UnityEngine;
using System.Collections;

// without rotation, just translation

public class FolloAtADistanceNoQ : MonoBehaviour {

    public GameObject follower;
    public GameObject target;
    public Vector3 distance;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 pos = target.transform.position;
        pos -= distance;
        follower.transform.position = pos;
	}
}
