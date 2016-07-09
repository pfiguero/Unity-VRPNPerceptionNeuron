/* VRPNTracker.cs
 * Written by Evan A. Suma, Ph.D.
 * Institute for Creative Technologies
 * University of Southern California
 * Email: suma@ict.usc.edu
 */

using UnityEngine;
using System;
using System.Collections;
using System.Runtime.InteropServices;

public class VRPNTracker : MonoBehaviour
{
    // the name of the VRPN tracker
    // standard default is Tracker0
    public string tracker = "Tracker0";

    // the address of the server
    // leave as localhost if running on same computer
    public string server = "localhost";

    // the sensor number to track
    public int sensor = 0;

    // the origin of the Unity tracker coordinate space
    // the tracker transform will be relative to this transform
    // can be left as null if using (0, 0, 0)
    public Transform origin = null;

    // update position, orientation, or both?
    public enum TrackingMode { SIX_DOF, POSITION_ONLY, ORIENTATION_ONLY };
    public TrackingMode trackingMode = TrackingMode.SIX_DOF;

    public enum ReportType { MOST_RECENT, AVERAGE };
    public ReportType reportType = ReportType.MOST_RECENT;


    public enum ThreadingMode {SINGLE_THREADED, MULTI_THREADED};
    public ThreadingMode threadingMode = ThreadingMode.MULTI_THREADED;

    // for making manual adjustments to tracker position
    // (e.g. if there is an offset between tracker location and eyepoint)
    public Vector3 positionAdjustment = Vector3.zero;

    // for making manual adjustments to tracker rotation
    // (e.g. if the tracker is mounted in the wrong direction on a rigid object)
    public Vector3 rotationAdjustment = Vector3.zero;

    // for making manual adjustments to tracker rotation
    // (e.g. if the tracker is mounted in the wrong direction on a rigid object)
    public Vector3 scaleAdjustment = Vector3.one;
    
	public bool useOculusOrientation = false;
	public Transform OVROrientationReference;

    IntPtr trackerDataPointer;
    TrackerData trackerData;
    static int lastTrackerUpdateFrame = -1;

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct TrackerData
    {
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
        public double[] position;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
        public double[] rotation;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
        public double[] positionSum;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
        public double[] rotationSum;

        public int numReports;
    }
    
    [DllImport("vrpn-wwa")]
    static extern IntPtr initializeTracker(string serverName, int sensorNumber);
    [DllImport("vrpn-wwa")]
    static extern void endVRPNService();

    // for single threading
    [DllImport("vrpn-wwa")]
    static extern void updateTrackers();

    // for multithreading
    [DllImport("vrpn-wwa")]
    static extern void updateTrackersMultiThreaded();
    [DllImport("vrpn-wwa")]
    static extern void lockTrackerData(IntPtr trackerData);
    [DllImport("vrpn-wwa")]
    static extern void unlockTrackerData(IntPtr trackerData);

    // Use this for initialization
    void Start()
    {
        trackerDataPointer = initializeTracker(tracker + "@" + server, sensor);
    }

    // Update is called once per frame
    void Update()
    {
        if (threadingMode == ThreadingMode.SINGLE_THREADED)
        {
            // update the trackers only once per frame
            if (lastTrackerUpdateFrame != Time.frameCount)
            {
                updateTrackers();
                lastTrackerUpdateFrame = Time.frameCount;
            }

            trackerData = (TrackerData)Marshal.PtrToStructure(trackerDataPointer, typeof(TrackerData));
        }
        else
        {
            // update the trackers only once per frame
            if (lastTrackerUpdateFrame != Time.frameCount)
            {
                updateTrackersMultiThreaded();
                lastTrackerUpdateFrame = Time.frameCount;
            }

            lockTrackerData(trackerDataPointer);
            trackerData = (TrackerData)Marshal.PtrToStructure(trackerDataPointer, typeof(TrackerData));
            unlockTrackerData(trackerDataPointer);
        }

        if (reportType == ReportType.AVERAGE && trackerData.numReports > 0)
            getAverageReport();
        else
            getMostRecentReport();

        // if a custom origin has been specified
        // then update the transform coordinate space
        if (origin != null)
        {
            if (trackingMode != TrackingMode.POSITION_ONLY)
                transform.rotation = origin.rotation * transform.rotation;
            else
                transform.rotation = origin.rotation;

            if (trackingMode != TrackingMode.ORIENTATION_ONLY)
                transform.position = origin.position + origin.rotation * transform.position;
            else
                transform.position = origin.position;
        }
    }

    void OnApplicationQuit()
    {
        endVRPNService();
    }

    void getMostRecentReport()
    {
        Quaternion rotation = Quaternion.identity;
        if (trackingMode != TrackingMode.POSITION_ONLY)
        {
			if (useOculusOrientation) {
				rotation = OVROrientationReference.rotation;//OVRManager.display.GetEyePose (OVREye.Left).orientation;
			} else {
				// convert Phasespace VRPN rotation into Unity coordinate system
				rotation = transform.rotation;
				rotation.x = -(float)trackerData.rotation [0];
				rotation.y = -(float)trackerData.rotation [1];
				rotation.z = (float)trackerData.rotation [2];
				rotation.w = (float)trackerData.rotation [3];
			}
            // apply tracker rotation and adjustment
            transform.rotation = rotation * Quaternion.Euler(rotationAdjustment);
        }

		if (trackingMode != TrackingMode.ORIENTATION_ONLY) {
			// convert Phasespace VRPN position into Unity coordinate system
			Vector3 position = transform.position;
			position.x = (float)trackerData.position [0] * scaleAdjustment.x;
			position.y = (float)trackerData.position [1] * scaleAdjustment.y;
			position.z = -(float)trackerData.position [2] * scaleAdjustment.z;

			if (useOculusOrientation) {
				transform.position = position + OVROrientationReference.rotation * positionAdjustment;

			} else {
				
				// apply tracker position and adjustment
				transform.position = position + rotation * positionAdjustment;
			}
		}

        
    }

    void getAverageReport()
    {
        Quaternion rotation = Quaternion.identity;
        if (trackingMode != TrackingMode.POSITION_ONLY)
        {
			if (useOculusOrientation) {
				rotation = OVROrientationReference.rotation;
			} else {

				// convert VRPN rotation into Unity coordinate system
				rotation = transform.rotation;
				rotation.x = -(float)trackerData.rotationSum [0] / trackerData.numReports;
				rotation.y = -(float)trackerData.rotationSum [1] / trackerData.numReports;
				rotation.z = (float)trackerData.rotationSum [2] / trackerData.numReports;
				rotation.w = (float)trackerData.rotationSum [3] / trackerData.numReports;
			}
            // apply tracker rotation and adjustment
            transform.rotation = rotation * Quaternion.Euler(rotationAdjustment);
        }

		if (trackingMode != TrackingMode.ORIENTATION_ONLY) {
			// convert VRPN position into Unity coordinate system
			Vector3 position = transform.position;
			position.x = ((float)trackerData.positionSum [0] / trackerData.numReports) * scaleAdjustment.x;
			position.y = ((float)trackerData.positionSum [1] / trackerData.numReports) * scaleAdjustment.y;
			position.z = -((float)trackerData.positionSum [2] / trackerData.numReports) * scaleAdjustment.z;

			if (useOculusOrientation) {
				transform.position = position + OVROrientationReference.rotation * positionAdjustment;
			} else {
				
				// apply tracker position and adjustment
				transform.position = position + rotation * positionAdjustment;
			}
		}
    }

    
}
