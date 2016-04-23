/* VRPNAnimatorDriver.cs
 * Written by Pablo Figueroa, Ph.D.
 * Universidad de los Andes, Colombia
 * Visiting Scholar at Institute for Creative Technologies
 * Email: pfiguero@uniandes.edu.co
 */

using UnityEngine;
using System.Collections;
using Neuron;


public class VRPNAnimatorDriver : MonoBehaviour {
    
    // VRPN Connection
    public string tracker;
    public string server;

    public bool remoteDisplacements = false;
    public bool takeHipsDisplacement = false;
    public bool secondSuit = false;

    public Animator animator = null;                                // The animator component which receives the mocap data
    /*
    // Assuming there is a reference node.
    enum NeuronBones
    {
        // Reference = 0, No se envia!!!
        Hips = 0,
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
    */

    private GameObject[] objs;
    private Vector3[] bonePositionOffsets;
    private Vector3[] boneRotationOffsets;

    // HumanBodyBones displacements
    // Old hips
    //                      new Vector3(-1.250471f,0.921503f,-1.918202f),

    public static Vector3[] displ = new Vector3[] {
                      new Vector3(0.0f,0.0f,0.0f),
                      new Vector3(-0.105532f,-0.088063f,-0.008584f),
                      new Vector3(0.105532f,-0.088063f,-0.008584f),
                      new Vector3(-0.012004f,-0.456507f,-0.015162f),
                      new Vector3(0.012004f,-0.456507f,-0.015162f),
                      new Vector3(0.004438f,-0.467116f,-0.002509f),
                      new Vector3(-0.004438f,-0.467116f,-0.002509f),
                      new Vector3(0.000000f,0.096093f,0.000000f),
                      new Vector3(-0.000000f,0.117231f,0.012485f),
                      new Vector3(0.000000f,0.120702f,-0.012485f),
                      new Vector3(-0.000000f,0.100041f,0.000000f),
                      new Vector3(-0.049049f,0.040631f,-0.012485f),
                      new Vector3(0.049049f,0.040628f,-0.012485f),
                      new Vector3(-0.156488f,0.000000f,0.000000f),
                      new Vector3(0.156489f,0.000000f,0.000000f),
                      new Vector3(-0.258760f,0.000000f,0.000000f),
                      new Vector3(0.258760f,0.000000f,0.000000f),
                      new Vector3(-0.284020f,0.000000f,0.000000f),
                      new Vector3(0.284020f,0.000000f,0.000000f),
                      new Vector3(-0.000000f,-0.080176f,0.121593f),
                      new Vector3(0.000000f,-0.080176f,0.121593f),
                      new Vector3(0.000000f,0.000000f,0.000000f),
                      new Vector3(0.000000f,0.000000f,0.000000f),
                      new Vector3(0.000000f,0.000000f,0.000000f),
                      new Vector3(-0.033787f,0.002575f,0.042356f),
                      new Vector3(-0.049988f,0.000127f,-0.000126f),
                      new Vector3(-0.034725f,-0.000080f,0.000101f),
                      new Vector3(-0.070869f,-0.001237f,0.013565f),
                      new Vector3(-0.049062f,-0.002391f,-0.000123f),
                      new Vector3(-0.027794f,-0.001731f,0.000110f),
                      new Vector3(-0.070228f,-0.001142f,0.004264f),
                      new Vector3(-0.053482f,-0.003643f,-0.000157f),
                      new Vector3(-0.033494f,-0.002587f,0.000143f),
                      new Vector3(-0.062954f,-0.000303f,-0.006521f),
                      new Vector3(-0.046582f,-0.003573f,0.000023f),
                      new Vector3(-0.032328f,-0.002325f,0.000019f),
                      new Vector3(-0.056182f,-0.000366f,-0.014798f),
                      new Vector3(-0.037361f,-0.002047f,0.000107f),
                      new Vector3(-0.023572f,-0.001779f,-0.000119f),
                      new Vector3(0.033787f,0.002580f,0.042356f),
                      new Vector3(0.049988f,-0.000058f,-0.000224f),
                      new Vector3(0.034724f,0.000029f,-0.000202f),
                      new Vector3(0.070869f,-0.001240f,0.013565f),
                      new Vector3(0.049062f,-0.002390f,0.000088f),
                      new Vector3(0.027794f,-0.001730f,-0.000013f),
                      new Vector3(0.070228f,-0.001140f,0.004264f),
                      new Vector3(0.053483f,-0.003640f,-0.000140f),
                      new Vector3(0.033493f,-0.002590f,0.000086f),
                      new Vector3(0.062955f,-0.000300f,-0.006521f),
                      new Vector3(0.046513f,-0.004378f,0.000126f),
                      new Vector3(0.032290f,-0.002809f,-0.000155f),
                      new Vector3(0.056183f,-0.000360f,-0.014798f),
                      new Vector3(0.037362f,-0.002039f,-0.000172f),
                      new Vector3(0.023572f,-0.001777f,0.000090f),
                    };

    // For Update
    // Define them as attributes... not as temporary variables.
    private static Transform tempTr = null;
    private static Vector3 vectorTr = new Vector3();

    void Awake()
    {
        tempTr = new GameObject().transform;
        // If no animator was assigned, try to get one
        if (animator == null)
        {
            animator = GetComponent<Animator>();
        }

    }

    // Use this for initialization
    void Start()
    {
        CreateTrackers();
        bonePositionOffsets = new Vector3[(int)HumanBodyBones.LastBone];
        boneRotationOffsets = new Vector3[(int)HumanBodyBones.LastBone];
    }

    void CreateTrackers()
    {
        // Assuming sensors for a body, not including a reference
        if (takeHipsDisplacement)
        {
            objs = new GameObject[(int)NeuronBones.NumOfBones+1];
        }
        else
        {
            objs = new GameObject[(int)NeuronBones.NumOfBones];
        }
        GameObject prev;

        for (int i = 0; i < (int)NeuronBones.NumOfBones; i++)
        {
            objs[i] = new GameObject();
            objs[i].name = ((NeuronBones)i).ToString();
            VRPNTracker tr = objs[i].AddComponent<VRPNTracker>();
            tr.tracker = tracker;
            tr.server = server;
            if( secondSuit )
            {
                tr.sensor = i+60;
            }
            else
            {
                tr.sensor = i;
            }
            // Me parece que no es necesario, porque el esqueleto debe tener la jerarquia...
            /*
            if (i == (int)BoneNames.RightUpLeg || i == (int)BoneNames.LeftUpLeg || i == (int)BoneNames.Spine)
            {
                prev = objs[(int)BoneNames.Hips];
            }
            else if (i == (int)BoneNames.RightShoulder || i == (int)BoneNames.LeftShoulder)
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
                if (i != 0)
                    prev = objs[i - 1];
                else
                    prev = gameObject;
            }

            objs[i].transform.parent = prev.transform;
            */
        }
        if (takeHipsDisplacement)
        {
            objs[(int)NeuronBones.NumOfBones] = new GameObject();
            objs[(int)NeuronBones.NumOfBones].name = "HipsDisplacement";
            VRPNTracker tr = objs[(int)NeuronBones.NumOfBones].AddComponent<VRPNTracker>();
            tr.tracker = tracker;
            tr.server = server;
            tr.sensor = (int)NeuronBones.NumOfBones; // Assuming 59
        }
    }

    void UpdateOffset( )
    {
        // we do some adjustment for the bones here which would replaced by our model retargeting later

        // initiate values
        for (int i = 0; i < (int)HumanBodyBones.LastBone; ++i)
        {
            bonePositionOffsets[i] = Vector3.zero;
            boneRotationOffsets[i] = Vector3.zero;
        }
        /*
        if (animator != null)
        {
            Transform leftLegTransform = animator.GetBoneTransform(HumanBodyBones.LeftUpperLeg);
            Transform rightLegTransform = animator.GetBoneTransform(HumanBodyBones.RightUpperLeg);
            if (leftLegTransform != null)
            {
                bonePositionOffsets[(int)HumanBodyBones.LeftUpperLeg] = new Vector3(0.0f, leftLegTransform.localPosition.y, 0.0f);
                bonePositionOffsets[(int)HumanBodyBones.RightUpperLeg] = new Vector3(0.0f, rightLegTransform.localPosition.y, 0.0f);
                bonePositionOffsets[(int)HumanBodyBones.Hips] = new Vector3(0.0f, -(leftLegTransform.localPosition.y + rightLegTransform.localPosition.y) * 0.5f, 0.0f);
            }
        }
        */
    }

    void Update()
    {
        if (animator != null)
        {
            UpdateOffset();

            // apply Hips position
            SetPosition(animator, HumanBodyBones.Hips, objs[(int)NeuronBones.Hips].transform.position + bonePositionOffsets[(int)HumanBodyBones.Hips]);
            SetRotation(animator, HumanBodyBones.Hips, objs[(int)NeuronBones.Hips].transform);

            // apply positions
            if (true) //actor.withDisplacement)
            {
                
                // legs
                SetPosition(animator, HumanBodyBones.RightUpperLeg, objs[(int)NeuronBones.RightUpLeg].transform.position + bonePositionOffsets[(int)HumanBodyBones.RightUpperLeg]);
                SetPosition(animator, HumanBodyBones.RightLowerLeg, objs[(int)NeuronBones.RightLeg].transform.position );
                SetPosition(animator, HumanBodyBones.RightFoot, objs[(int)NeuronBones.RightFoot].transform.position );
                SetPosition(animator, HumanBodyBones.LeftUpperLeg, objs[(int)NeuronBones.LeftUpLeg].transform.position + bonePositionOffsets[(int)HumanBodyBones.LeftUpperLeg]);
                SetPosition(animator, HumanBodyBones.LeftLowerLeg, objs[(int)NeuronBones.LeftLeg].transform.position );
                SetPosition(animator, HumanBodyBones.LeftFoot, objs[(int)NeuronBones.LeftFoot].transform.position );

                // spine
                SetPosition(animator, HumanBodyBones.Spine, objs[(int)NeuronBones.Spine].transform.position );
                SetPosition(animator, HumanBodyBones.Chest, objs[(int)NeuronBones.Spine3].transform.position );
                SetPosition(animator, HumanBodyBones.Neck, objs[(int)NeuronBones.Neck].transform.position );
                SetPosition(animator, HumanBodyBones.Head, objs[(int)NeuronBones.Head].transform.position );

                // right arm
                SetPosition(animator, HumanBodyBones.RightShoulder, objs[(int)NeuronBones.RightShoulder].transform.position );
                SetPosition(animator, HumanBodyBones.RightUpperArm, objs[(int)NeuronBones.RightArm].transform.position );
                SetPosition(animator, HumanBodyBones.RightLowerArm, objs[(int)NeuronBones.RightForeArm].transform.position );

                // right hand
                SetPosition(animator, HumanBodyBones.RightHand, objs[(int)NeuronBones.RightHand].transform.position );
                SetPosition(animator, HumanBodyBones.RightThumbProximal, objs[(int)NeuronBones.RightHandThumb1].transform.position  );
                SetPosition(animator, HumanBodyBones.RightThumbIntermediate, objs[(int)NeuronBones.RightHandThumb2].transform.position );
                SetPosition(animator, HumanBodyBones.RightThumbDistal, objs[(int)NeuronBones.RightHandThumb3].transform.position );

                SetPosition(animator, HumanBodyBones.RightIndexProximal, objs[(int)NeuronBones.RightHandIndex1].transform.position);
                SetPosition(animator, HumanBodyBones.RightIndexIntermediate, objs[(int)NeuronBones.RightHandIndex2].transform.position);
                SetPosition(animator, HumanBodyBones.RightIndexDistal, objs[(int)NeuronBones.RightHandIndex3].transform.position);

                SetPosition(animator, HumanBodyBones.RightMiddleProximal, objs[(int)NeuronBones.RightHandMiddle1].transform.position);
                SetPosition(animator, HumanBodyBones.RightMiddleIntermediate, objs[(int)NeuronBones.RightHandMiddle2].transform.position);
                SetPosition(animator, HumanBodyBones.RightMiddleDistal, objs[(int)NeuronBones.RightHandMiddle3].transform.position);

                SetPosition(animator, HumanBodyBones.RightRingProximal, objs[(int)NeuronBones.RightHandRing1].transform.position);
                SetPosition(animator, HumanBodyBones.RightRingIntermediate, objs[(int)NeuronBones.RightHandRing2].transform.position);
                SetPosition(animator, HumanBodyBones.RightRingDistal, objs[(int)NeuronBones.RightHandRing3].transform.position);

                SetPosition(animator, HumanBodyBones.RightLittleProximal, objs[(int)NeuronBones.RightHandPinky1].transform.position);
                SetPosition(animator, HumanBodyBones.RightLittleIntermediate, objs[(int)NeuronBones.RightHandPinky2].transform.position);
                SetPosition(animator, HumanBodyBones.RightLittleDistal, objs[(int)NeuronBones.RightHandPinky3].transform.position);

                // Left arm
                SetPosition(animator, HumanBodyBones.LeftShoulder, objs[(int)NeuronBones.LeftShoulder].transform.position);
                SetPosition(animator, HumanBodyBones.LeftUpperArm, objs[(int)NeuronBones.LeftArm].transform.position);
                SetPosition(animator, HumanBodyBones.LeftLowerArm, objs[(int)NeuronBones.LeftForeArm].transform.position);

                // Left hand
                SetPosition(animator, HumanBodyBones.LeftHand, objs[(int)NeuronBones.LeftHand].transform.position);
                SetPosition(animator, HumanBodyBones.LeftThumbProximal, objs[(int)NeuronBones.LeftHandThumb1].transform.position);
                SetPosition(animator, HumanBodyBones.LeftThumbIntermediate, objs[(int)NeuronBones.LeftHandThumb2].transform.position);
                SetPosition(animator, HumanBodyBones.LeftThumbDistal, objs[(int)NeuronBones.LeftHandThumb3].transform.position);

                SetPosition(animator, HumanBodyBones.LeftIndexProximal, objs[(int)NeuronBones.LeftHandIndex1].transform.position);
                SetPosition(animator, HumanBodyBones.LeftIndexIntermediate, objs[(int)NeuronBones.LeftHandIndex2].transform.position);
                SetPosition(animator, HumanBodyBones.LeftIndexDistal, objs[(int)NeuronBones.LeftHandIndex3].transform.position);

                SetPosition(animator, HumanBodyBones.LeftMiddleProximal, objs[(int)NeuronBones.LeftHandMiddle1].transform.position);
                SetPosition(animator, HumanBodyBones.LeftMiddleIntermediate, objs[(int)NeuronBones.LeftHandMiddle2].transform.position);
                SetPosition(animator, HumanBodyBones.LeftMiddleDistal, objs[(int)NeuronBones.LeftHandMiddle3].transform.position);

                SetPosition(animator, HumanBodyBones.LeftRingProximal, objs[(int)NeuronBones.LeftHandRing1].transform.position);
                SetPosition(animator, HumanBodyBones.LeftRingIntermediate, objs[(int)NeuronBones.LeftHandRing2].transform.position);
                SetPosition(animator, HumanBodyBones.LeftRingDistal, objs[(int)NeuronBones.LeftHandRing3].transform.position);

                SetPosition(animator, HumanBodyBones.LeftLittleProximal, objs[(int)NeuronBones.LeftHandPinky1].transform.position);
                SetPosition(animator, HumanBodyBones.LeftLittleIntermediate, objs[(int)NeuronBones.LeftHandPinky2].transform.position);
                SetPosition(animator, HumanBodyBones.LeftLittleDistal, objs[(int)NeuronBones.LeftHandPinky3].transform.position);
                
            }

            // apply rotations

            // legs
            SetRotation(animator, HumanBodyBones.RightUpperLeg, objs[(int)NeuronBones.RightUpLeg].transform );
            SetRotation(animator, HumanBodyBones.RightLowerLeg, objs[(int)NeuronBones.RightLeg].transform);
            SetRotation(animator, HumanBodyBones.RightFoot, objs[(int)NeuronBones.RightFoot].transform);
            SetRotation(animator, HumanBodyBones.LeftUpperLeg, objs[(int)NeuronBones.LeftUpLeg].transform );
            SetRotation(animator, HumanBodyBones.LeftLowerLeg, objs[(int)NeuronBones.LeftLeg].transform);
            SetRotation(animator, HumanBodyBones.LeftFoot, objs[(int)NeuronBones.LeftFoot].transform);

            // spine
            SetRotation(animator, HumanBodyBones.Spine, objs[(int)NeuronBones.Spine].transform);

            if (!remoteDisplacements)
            { 
                vectorTr.x = objs[(int)NeuronBones.Spine1].transform.position.x + objs[(int)NeuronBones.Spine2].transform.position.x + objs[(int)NeuronBones.Spine3].transform.position.x;
                vectorTr.y = objs[(int)NeuronBones.Spine1].transform.position.y + objs[(int)NeuronBones.Spine2].transform.position.y + objs[(int)NeuronBones.Spine3].transform.position.y;
                vectorTr.z = objs[(int)NeuronBones.Spine1].transform.position.z + objs[(int)NeuronBones.Spine2].transform.position.z + objs[(int)NeuronBones.Spine3].transform.position.z;
                tempTr.position = vectorTr;
                SetRotation(animator, HumanBodyBones.Chest, tempTr);
            }
            else
            {
                tempTr.rotation = objs[(int)NeuronBones.Spine1].transform.rotation * objs[(int)NeuronBones.Spine2].transform.rotation * objs[(int)NeuronBones.Spine3].transform.rotation;
                SetRotation(animator, HumanBodyBones.Chest, tempTr);
            }

            SetRotation(animator, HumanBodyBones.Neck, objs[(int)NeuronBones.Neck].transform);
            SetRotation(animator, HumanBodyBones.Head, objs[(int)NeuronBones.Head].transform);


            // right arm
            SetRotation(animator, HumanBodyBones.RightShoulder, objs[(int)NeuronBones.RightShoulder].transform);
            SetRotation(animator, HumanBodyBones.RightUpperArm, objs[(int)NeuronBones.RightArm].transform);
            SetRotation(animator, HumanBodyBones.RightLowerArm, objs[(int)NeuronBones.RightForeArm].transform);

            // right hand
            SetRotation(animator, HumanBodyBones.RightHand, objs[(int)NeuronBones.RightHand].transform);
            SetRotation(animator, HumanBodyBones.RightThumbProximal, objs[(int)NeuronBones.RightHandThumb1].transform);
            SetRotation(animator, HumanBodyBones.RightThumbIntermediate, objs[(int)NeuronBones.RightHandThumb2].transform);
            SetRotation(animator, HumanBodyBones.RightThumbDistal, objs[(int)NeuronBones.RightHandThumb3].transform);

            if (!remoteDisplacements)
            {
                vectorTr.x = objs[(int)NeuronBones.RightHandIndex1].transform.position.x + objs[(int)NeuronBones.RightInHandIndex].transform.position.x;
                vectorTr.y = objs[(int)NeuronBones.RightHandIndex1].transform.position.y + objs[(int)NeuronBones.RightInHandIndex].transform.position.y;
                vectorTr.z = objs[(int)NeuronBones.RightHandIndex1].transform.position.z + objs[(int)NeuronBones.RightInHandIndex].transform.position.z;
                tempTr.position = vectorTr;
                SetRotation(animator, HumanBodyBones.RightIndexProximal, tempTr);
            }
            else
            {
                tempTr.rotation = objs[(int)NeuronBones.RightHandIndex1].transform.rotation * objs[(int)NeuronBones.RightInHandIndex].transform.rotation;
                SetRotation(animator, HumanBodyBones.RightIndexProximal, tempTr);
            }

            SetRotation(animator, HumanBodyBones.RightIndexIntermediate, objs[(int)NeuronBones.RightHandIndex2].transform);
            SetRotation(animator, HumanBodyBones.RightIndexDistal, objs[(int)NeuronBones.RightHandIndex3].transform);

            if (!remoteDisplacements)
            {
                vectorTr.x = objs[(int)NeuronBones.RightHandMiddle1].transform.position.x + objs[(int)NeuronBones.RightInHandMiddle].transform.position.x;
                vectorTr.y = objs[(int)NeuronBones.RightHandMiddle1].transform.position.y + objs[(int)NeuronBones.RightInHandMiddle].transform.position.y;
                vectorTr.z = objs[(int)NeuronBones.RightHandMiddle1].transform.position.z + objs[(int)NeuronBones.RightInHandMiddle].transform.position.z;
                tempTr.position = vectorTr;
                SetRotation(animator, HumanBodyBones.RightMiddleProximal, tempTr);
            }
            else
            {
                tempTr.rotation = objs[(int)NeuronBones.RightHandMiddle1].transform.rotation * objs[(int)NeuronBones.RightInHandMiddle].transform.rotation;
                SetRotation(animator, HumanBodyBones.RightMiddleProximal, tempTr);
            }

            SetRotation(animator, HumanBodyBones.RightMiddleIntermediate, objs[(int)NeuronBones.RightHandMiddle2].transform);
            SetRotation(animator, HumanBodyBones.RightMiddleDistal, objs[(int)NeuronBones.RightHandMiddle3].transform);

            if (!remoteDisplacements)
            {
                vectorTr.x = objs[(int)NeuronBones.RightHandRing1].transform.position.x + objs[(int)NeuronBones.RightInHandRing].transform.position.x;
                vectorTr.y = objs[(int)NeuronBones.RightHandRing1].transform.position.y + objs[(int)NeuronBones.RightInHandRing].transform.position.y;
                vectorTr.z = objs[(int)NeuronBones.RightHandRing1].transform.position.z + objs[(int)NeuronBones.RightInHandRing].transform.position.z;
                tempTr.position = vectorTr;
                SetRotation(animator, HumanBodyBones.RightRingProximal, tempTr);
            }
            else
            {
                tempTr.rotation = objs[(int)NeuronBones.RightHandRing1].transform.rotation * objs[(int)NeuronBones.RightInHandRing].transform.rotation;
                SetRotation(animator, HumanBodyBones.RightRingProximal, tempTr);
            }

            SetRotation(animator, HumanBodyBones.RightRingIntermediate, objs[(int)NeuronBones.RightHandRing2].transform);
            SetRotation(animator, HumanBodyBones.RightRingDistal, objs[(int)NeuronBones.RightHandRing3].transform);

            if (!remoteDisplacements)
            {
                vectorTr.x = objs[(int)NeuronBones.RightHandPinky1].transform.position.x + objs[(int)NeuronBones.RightInHandPinky].transform.position.x;
                vectorTr.y = objs[(int)NeuronBones.RightHandPinky1].transform.position.y + objs[(int)NeuronBones.RightInHandPinky].transform.position.y;
                vectorTr.z = objs[(int)NeuronBones.RightHandPinky1].transform.position.z + objs[(int)NeuronBones.RightInHandPinky].transform.position.z;
                tempTr.position = vectorTr;
                SetRotation(animator, HumanBodyBones.RightLittleProximal, tempTr);
            }
            else
            {
                tempTr.rotation = objs[(int)NeuronBones.RightHandPinky1].transform.rotation * objs[(int)NeuronBones.RightInHandPinky].transform.rotation;
                SetRotation(animator, HumanBodyBones.RightLittleProximal, tempTr);
            }

            SetRotation(animator, HumanBodyBones.RightLittleIntermediate, objs[(int)NeuronBones.RightHandPinky2].transform);
            SetRotation(animator, HumanBodyBones.RightLittleDistal, objs[(int)NeuronBones.RightHandPinky3].transform);

            // Left arm
            SetRotation(animator, HumanBodyBones.LeftShoulder, objs[(int)NeuronBones.LeftShoulder].transform);
            SetRotation(animator, HumanBodyBones.LeftUpperArm, objs[(int)NeuronBones.LeftArm].transform);
            SetRotation(animator, HumanBodyBones.LeftLowerArm, objs[(int)NeuronBones.LeftForeArm].transform);

            // Left hand
            SetRotation(animator, HumanBodyBones.LeftHand, objs[(int)NeuronBones.LeftHand].transform);
            SetRotation(animator, HumanBodyBones.LeftThumbProximal, objs[(int)NeuronBones.LeftHandThumb1].transform);
            SetRotation(animator, HumanBodyBones.LeftThumbIntermediate, objs[(int)NeuronBones.LeftHandThumb2].transform);
            SetRotation(animator, HumanBodyBones.LeftThumbDistal, objs[(int)NeuronBones.LeftHandThumb3].transform);

            // ADD Changes as in Right
            //SetRotation(animator, HumanBodyBones.LeftIndexProximal, objs[(int)NeuronBones.LeftHandIndex1].transform * objs[(int)NeuronBones.LeftInHandIndex].transform);
            SetRotation(animator, HumanBodyBones.LeftIndexIntermediate, objs[(int)NeuronBones.LeftHandIndex2].transform);
            SetRotation(animator, HumanBodyBones.LeftIndexDistal, objs[(int)NeuronBones.LeftHandIndex3].transform);

            //SetRotation(animator, HumanBodyBones.LeftMiddleProximal, objs[(int)NeuronBones.LeftHandMiddle1].transform * objs[(int)NeuronBones.LeftInHandMiddle].transform);
            SetRotation(animator, HumanBodyBones.LeftMiddleIntermediate, objs[(int)NeuronBones.LeftHandMiddle2].transform);
            SetRotation(animator, HumanBodyBones.LeftMiddleDistal, objs[(int)NeuronBones.LeftHandMiddle3].transform);

            //SetRotation(animator, HumanBodyBones.LeftRingProximal, objs[(int)NeuronBones.LeftHandRing1].transform * objs[(int)NeuronBones.LeftInHandRing].transform);
            SetRotation(animator, HumanBodyBones.LeftRingIntermediate, objs[(int)NeuronBones.LeftHandRing2].transform);
            SetRotation(animator, HumanBodyBones.LeftRingDistal, objs[(int)NeuronBones.LeftHandRing3].transform);

            //SetRotation(animator, HumanBodyBones.LeftLittleProximal, objs[(int)NeuronBones.LeftHandPinky1].transform * objs[(int)NeuronBones.LeftInHandPinky].transform);
            SetRotation(animator, HumanBodyBones.LeftLittleIntermediate, objs[(int)NeuronBones.LeftHandPinky2].transform);
            SetRotation(animator, HumanBodyBones.LeftLittleDistal, objs[(int)NeuronBones.LeftHandPinky3].transform);

            // If you want to do some customization and set bones transforms in your own way, use the following methods.

            // Update Hips
            // Transform hips_t = animator.GetBoneTransform( HumanBodyBones.Hips );
            // hips_t.position = actor.GetReceivedPosition( NeuronBones.Hips );
            // hips_t.root = actor.GetReceivedRotation( NeuronBones.Hips );

            // Update RightUpLeg
            // Transform rightUpperLeg_t = animator.GetBoneTransform( HumanBodyBones.RightUpperLeg );
            // rightUpperLeg_t.position = actor.GetReceivedPosition( NeuronBones.RightUpLeg );
            // rightUpperLeg_t.rotation = actor.GetReceivedRotation( NeuronBones.RightUpLeg );

            // ... and so on for the other bones.

            // In this example we traverse the HumanBodyBones to get transforms components referenced in the animator component.
            // If you don't want to use the Animator component, you can assign the bones transforms references in your own way.
            // You can check NeuronHelper.Bind() to see how we bind bones using a naming convention with prefixs.
            // Just keep in mind that the Neuron default model might be rigged differently then yours.
            // For further info on this subject check the included documentation.
        }
    }

    // set position for bone in animator
    void SetPosition(Animator animator, HumanBodyBones bone, Vector3 pos)
    {
        Transform t = animator.GetBoneTransform(bone);
        // Assuming local displacements
        if (!remoteDisplacements )
        { 
            pos = new Vector3(displ[(int)bone].x, displ[(int)bone].y, displ[(int)bone].z);
            if (bone == HumanBodyBones.Hips && takeHipsDisplacement)
            {
                pos = objs[59].transform.position;
            }
        }
        else
        {
            pos.z = -pos.z;
        }
        if (t != null)
        {
            if (!float.IsNaN(pos.x) && !float.IsNaN(pos.y) && !float.IsNaN(pos.z))
            {
                t.localPosition = pos;
            }
        }
    }

    // set rotation for bone in animator
    void SetRotation(Animator animator, HumanBodyBones bone, Transform tr)
    {
        // Quaternion rot = tr.rotation;
        //Vector3 euler = new Vector3(-tr.position.z, -tr.position.x, tr.position.y);
        // Vector3( data[offset+1], -data[offset], -data[offset+2] );
        Transform t = animator.GetBoneTransform(bone);

        Quaternion q = tr.rotation;
        if (!remoteDisplacements)
        {
            // Assuming euler angles in the position of this tracker
            Vector3 euler = new Vector3(tr.position.y, -tr.position.x, -tr.position.z);
            q = Quaternion.Euler(euler);
        }
        
        if (t != null)
        {
            if (!float.IsNaN(q.x) && !float.IsNaN(q.y) && !float.IsNaN(q.z) && !float.IsNaN(q.w))
            {
                t.localRotation = q;
            }
        }
    }


}
