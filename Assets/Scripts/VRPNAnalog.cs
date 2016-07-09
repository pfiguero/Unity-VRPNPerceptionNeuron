/* VRPNAnalog.cs
 * Written by Evan A. Suma, Ph.D.
 * Institute for Creative Technologies
 * University of Southern California
 * Email: suma@ict.usc.edu
 */

using UnityEngine;
using System;
using System.Collections;
using System.Runtime.InteropServices;

public class VRPNAnalog : MonoBehaviour
{
    // the name of the VRPN button
    // standard default is Button0
    public string device = "Analog0";

    // the address of the server
    // leave as localhost if running on same computer
    public string server = "localhost";

    // the channel number
    public int channel = 0;

    IntPtr analogDataPointer;
    AnalogData analogData;

    // the movement effect to apply to the game object's transform
    public enum MovementType { TRANSLATE, ROTATE, SCALE };
    public MovementType movementType = MovementType.TRANSLATE;

    // the axis to apply the movement on
    public enum Axis { X, Y, Z, ALL };
    public Axis axis = Axis.X;

    public float speed = 1.0f;

    static int lastAnalogUpdateFrame = -1;

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct AnalogData
    {
        public double state;
    }

    [DllImport("vrpn-wwa")]
    static extern IntPtr initializeAnalog(string serverName, int channelNumber);
    [DllImport("vrpn-wwa")]
    static extern void updateAnalogs();
    [DllImport("vrpn-wwa")]
    static extern void endVRPNService();

    // Use this for initialization
    void Start()
    {
        analogDataPointer = initializeAnalog(device + "@" + server, channel);
    }

    // Update is called once per frame
    void Update()
    {
        // update the trackers only once per frame
        if (lastAnalogUpdateFrame != Time.frameCount)
        {
            updateAnalogs();
            lastAnalogUpdateFrame = Time.frameCount;
        }
        analogData = (AnalogData)Marshal.PtrToStructure(analogDataPointer, typeof(AnalogData));


        float speedThisFrame = speed * Time.deltaTime * (float)analogData.state;

        if (movementType == MovementType.TRANSLATE)
        {
            Vector3 position = transform.localPosition;
            if (axis == Axis.X)
                position.x += speedThisFrame;
            else if (axis == Axis.Y)
                position.y += speedThisFrame;
            else if (axis == Axis.Z)
                position.z += speedThisFrame;
            else
            {
                position.x += speedThisFrame;
                position.y += speedThisFrame;
                position.z += speedThisFrame;
            }

            transform.localPosition = position;
        }
        else if (movementType == MovementType.ROTATE)
        {
            Quaternion newRotation = Quaternion.identity;
            Vector3 newAngles = newRotation.eulerAngles;

            if (axis == Axis.X)
                newAngles.x += speedThisFrame;
            else if (axis == Axis.Y)
                newAngles.y += speedThisFrame;
            else if (axis == Axis.Z)
                newAngles.z += speedThisFrame;
            else
            {
                newAngles.x += speedThisFrame;
                newAngles.y += speedThisFrame;
                newAngles.z += speedThisFrame;
            }

            if (newAngles.x > 360)
                newAngles.x %= 360;

            if (newAngles.y > 360)
                newAngles.y %= 360;

            if (newAngles.z > 360)
                newAngles.z %= 360;

            newRotation.eulerAngles = newAngles;


            Quaternion rotation = transform.localRotation;
            rotation = rotation * newRotation;
            transform.localRotation = rotation;
        }
        else
        {
            Vector3 scale = transform.localScale;
            if (axis == Axis.X)
                scale.x += speedThisFrame;
            else if (axis == Axis.Y)
                scale.y += speedThisFrame;
            else if (axis == Axis.Z)
                scale.z += speedThisFrame;
            else
            {
                scale.x += speedThisFrame;
                scale.y += speedThisFrame;
                scale.z += speedThisFrame;
            }

            transform.localScale = scale;
        }

        
    }

    void OnApplicationQuit()
    {
        endVRPNService();
    }

}
