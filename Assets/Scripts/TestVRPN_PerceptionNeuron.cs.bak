/* TestVRPN_PerceptionNeuron.cs
 * Written by Pablo Figueroa, Ph.D.
 * Universidad de los Andes, Colombia
 * Visiting Scholar at Institute for Creative Technologies
 * Email: pfiguero@uniandes.edu.co
 */

using UnityEngine;
using System.Collections;

public class TestVRPN_PerceptionNeuron : MonoBehaviour {
    public string tracker;
    public string server;
    public GameObject refCube;
    public float distance;

    // Use when working with a Perception Neuron and you want not to send position and a reference point.
    //@HideInInspector
    public static bool referenceAndDisplacement = true;

    // Fixed displacements in meters. From NeuronDataReader Runtime API Documentation_D16
    //@HideInInspector
    public static Vector3[] displ = new Vector3[] {
                          new Vector3(0,104.19f,0),
                          new Vector3(-11.5f,0,0),
                          new Vector3(0,-48f,0),
                          new Vector3(0,-48f,0),
                          new Vector3(0,-1.81f,18.6f),
                          new Vector3(11.5f,0,0),
                          new Vector3(0,-48f,0),
                          new Vector3(0,-48f,0),
                          new Vector3(0,-1.81f,18.6f),
                          new Vector3(0,13.88f,0),
                          new Vector3(0,11.31f,0),
                          new Vector3(0,11.78f,0),
                          new Vector3(0,11.31f,0),
                          new Vector3(0,12.09f,0),
                          new Vector3(0,9f,0),
                          new Vector3(0,18f,0),
                          new Vector3(-3.5f,8.06f,0),
                          new Vector3(-17.5f,0,0),
                          new Vector3(-29f,0,0),
                          new Vector3(-28f,0,0),
                          new Vector3(-2.7f,0.21f,3.39f),
                          new Vector3(-2.75f,-0.64f,2.83f),
                          new Vector3(-2.13f,-0.81f,1.59f),
                          new Vector3(-1.8f,-0.9f,1.8f),
                          new Vector3(-3.5f,0.55f,2.15f),
                          new Vector3(-5.67f,-0.1f,1.09f),
                          new Vector3(-3.92f,-0.19f,0.2f),
                          new Vector3(-2.22f,-0.14f,-0.08f),
                          new Vector3(-2.28f,0,0),
                          new Vector3(-3.67f,0.56f,0.82f),
                          new Vector3(-5.62f,-0.09f,0.34f),
                          new Vector3(-4.27f,-0.29f,-0.2f),
                          new Vector3(-2.67f,-0.21f,-0.24f),
                          new Vector3(-2.28f,0,0),
                          new Vector3(-3.65f,0.59f,-0.14f),
                          new Vector3(-5f,-0.02f,-0.52f),
                          new Vector3(-3.65f,-0.29f,-0.74f),
                          new Vector3(-2.55f,-0.19f,-0.44f),
                          new Vector3(-2.16f,0,0),
                          new Vector3(-3.43f,0.51f,-1.3f),
                          new Vector3(-4.49f,-0.02f,-1.18f),
                          new Vector3(-2.85f,-0.16f,-0.9f),
                          new Vector3(-1.77f,-0.14f,-0.66f),
                          new Vector3(-1.68f,0,0),
                          new Vector3(3.5f,8.06f,0),
                          new Vector3(17.5f,0,0),
                          new Vector3(29f,0,0),
                          new Vector3(28f,0,0),
                          new Vector3(2.7f,0.21f,3.39f),
                          new Vector3(2.75f,-0.64f,2.83f),
                          new Vector3(2.13f,-0.81f,1.59f),
                          new Vector3(1.8f,-0.9f,1.8f),
                          new Vector3(3.5f,0.55f,2.15f),
                          new Vector3(5.67f,-0.1f,1.09f),
                          new Vector3(3.92f,-0.19f,0.2f),
                          new Vector3(2.22f,-0.14f,-0.08f),
                          new Vector3(2.28f,0,0),
                          new Vector3(3.67f,0.56f,0.82f),
                          new Vector3(5.62f,-0.09f,0.34f),
                          new Vector3(4.27f,-0.29f,-0.2f),
                          new Vector3(2.67f,-0.21f,-0.24f),
                          new Vector3(2.28f,0,0),
                          new Vector3(3.65f,0.59f,-0.14f),
                          new Vector3(5f,-0.02f,-0.52f),
                          new Vector3(3.65f,-0.29f,-0.74f),
                          new Vector3(2.55f,-0.19f,-0.44f),
                          new Vector3(2.16f,0,0),
                          new Vector3(3.43f,0.51f,-1.3f),
                          new Vector3(4.49f,-0.02f,-1.18f),
                          new Vector3(2.85f,-0.16f,-0.9f),
                          new Vector3(1.77f,-0.14f,-0.66f),
                          new Vector3(1.68f,0,0),
                    };


    // Assuming there is a reference node.
    enum BoneNames {
        Hips = 1,
        RightUpLeg,
        RightLeg,
        RightFoot,
        LeftUpLeg,
        LeftLeg,
        LeftFoot,
        Spine,
        Spine1,
        Spine2,
        Spine3,
        Neck,
        Head,
        RightShoulder,
        RightArm,
        RightForeArm,
        RightHand,
        RightHandThumb1,
        RightHandThumb2,
        RightHandThumb3,
        RightInHandIndex,
        RightHandIndex1,
        RightHandIndex2,
        RightHandIndex3,
        RightInHandMiddle,
        RightHandMiddle1,
        RightHandMiddle2,
        RightHandMiddle3,
        RightInHandRing,
        RightHandRing1,
        RightHandRing2,
        RightHandRing3,
        RightInHandPinky,
        RightHandPinky1,
        RightHandPinky2,
        RightHandPinky3,
        LeftShoulder,
        LeftArm,
        LeftForeArm,
        LeftHand,
        LeftHandThumb1,
        LeftHandThumb2,
        LeftHandThumb3,
        LeftInHandIndex,
        LeftHandIndex1,
        LeftHandIndex2,
        LeftHandIndex3,
        LeftInHandMiddle,
        LeftHandMiddle1,
        LeftHandMiddle2,
        LeftHandMiddle3,
        LeftInHandRing,
        LeftHandRing1,
        LeftHandRing2,
        LeftHandRing3,
        LeftInHandPinky,
        LeftHandPinky1,
        LeftHandPinky2,
        LeftHandPinky3,
        NumBones
    }

	// Use this for initialization
	void Start () {
        if (referenceAndDisplacement)
            CreateBodyWithRD();	
        else
            CreateBodyWithoutRD();	
	}
	
	// Update is called once per frame
	void Update () {
	}

    void CreateBodyWithRD()
    {
        // Assuming 60 sensors for a body, including a reference
        GameObject[] objs = new GameObject[(int)BoneNames.NumBones];
        GameObject prev;

        objs[0] = refCube;
        // sensor 0 comes in ref...
        for (int i = 1; i < (int)BoneNames.NumBones; i++)
        {
            objs[i] = GameObject.CreatePrimitive(PrimitiveType.Cube);
            objs[i].name = ((BoneNames)i).ToString();
            objs[i].transform.localScale = new Vector3(0.03f, 0.03f, 0.03f);
            VRPNTracker tr = objs[i].AddComponent<VRPNTracker>();
            tr.tracker = tracker;
            tr.server = server;
            tr.sensor = i;
            if (i == (int)BoneNames.RightUpLeg || i == (int)BoneNames.LeftUpLeg || i == (int)BoneNames.Spine)
            {
                prev = objs[(int)BoneNames.Hips];
            }
            else if( i== (int) BoneNames.RightShoulder || i== (int) BoneNames.LeftShoulder )
            {
                prev = objs[(int)BoneNames.Spine3];
            }
            else if (i == (int)BoneNames.RightHandThumb1 || i == (int)BoneNames.RightInHandIndex ||
                i == (int)BoneNames.RightInHandMiddle || i == (int)BoneNames.RightInHandRing || i == (int)BoneNames.RightInHandPinky)
            {
                prev = objs[(int)BoneNames.RightHand];
            }
            else if (i == (int)BoneNames.LeftHandThumb1 || i == (int)BoneNames.LeftInHandIndex ||
                i == (int)BoneNames.LeftInHandMiddle || i == (int)BoneNames.LeftInHandRing || i == (int)BoneNames.LeftInHandPinky)
            {
                prev = objs[(int)BoneNames.LeftHand];
            }
            else
            {
                prev = objs[i - 1];
            }

//            if( !(i>= (int)BoneNames.Head && i<= (int)BoneNames.Spine3) )
//            {
                objs[i].transform.parent = prev.transform;
//            }
        }
        objs[(int)BoneNames.Hips].transform.parent = objs[0].transform;
//        objs[(int)BoneNames.Spine].transform.parent = objs[(int)BoneNames.Hips].transform;

    }


    void CreateBodyWithoutRD()
    {
        // Assuming 58 sensors for a body, without reference
        GameObject[] objs = new GameObject[59];
        GameObject prev;

        // sensor 0 comes in ref...
        for (int i = 0; i < 59; i++)
        {
            objs[i] = GameObject.CreatePrimitive(PrimitiveType.Cube);
            objs[i].name = ((Neuron.NeuronBones)i).ToString();
            objs[i].transform.position = displ[i];
            objs[i].transform.localScale = new Vector3(0.03f, 0.03f, 0.03f);
            VRPNTracker tr = objs[i].AddComponent<VRPNTracker>();
            tr.tracker = tracker;
            tr.server = server;
            tr.sensor = i;
            if (i == (int)Neuron.NeuronBones.RightUpLeg || i == (int)Neuron.NeuronBones.LeftUpLeg || i == (int)Neuron.NeuronBones.Spine)
            {
                prev = objs[(int)Neuron.NeuronBones.Hips];
            }
            else if (i == (int)Neuron.NeuronBones.RightShoulder || i == (int)Neuron.NeuronBones.LeftShoulder)
            {
                prev = objs[(int)Neuron.NeuronBones.Spine3];
            }
            else if (i == (int)Neuron.NeuronBones.RightHandThumb1 || i == (int)Neuron.NeuronBones.RightInHandIndex ||
                i == (int)Neuron.NeuronBones.RightInHandMiddle || i == (int)Neuron.NeuronBones.RightInHandRing || i == (int)Neuron.NeuronBones.RightInHandPinky)
            {
                prev = objs[(int)Neuron.NeuronBones.RightHand];
            }
            else if (i == (int)Neuron.NeuronBones.LeftHandThumb1 || i == (int)Neuron.NeuronBones.LeftInHandIndex ||
                i == (int)Neuron.NeuronBones.LeftInHandMiddle || i == (int)Neuron.NeuronBones.LeftInHandRing || i == (int)Neuron.NeuronBones.LeftInHandPinky)
            {
                prev = objs[(int)Neuron.NeuronBones.LeftHand];
            }
            else
            {
                if(i!=0)
                    prev = objs[i - 1];
                else
                    prev = refCube;
            }

            //            if( !(i>= (int)Neuron.NeuronBones.Head && i<= (int)Neuron.NeuronBones.Spine3) )
            //            {
            objs[i].transform.parent = prev.transform;
            //            }
        }
        objs[(int)Neuron.NeuronBones.Hips].transform.parent = objs[0].transform;
        //        objs[(int)Neuron.NeuronBones.Spine].transform.parent = objs[(int)Neuron.NeuronBones.Hips].transform;

    }
}
