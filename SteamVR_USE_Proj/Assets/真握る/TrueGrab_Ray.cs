using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class TrueGrab_Ray : MonoBehaviour
{

    public GameObject TargetThumb = null;
    public GameObject HintThumb = null;
    public GameObject TargetIndex = null;
    public GameObject HintIndex = null;
    public GameObject TargetMiddle = null;
    public GameObject HintMiddle = null;
    public GameObject TargetRing = null;
    public GameObject HinttRing = null;
    public GameObject TargetPinky = null;
    public GameObject HintPinky = null;



    public SteamVR_Behaviour_Skeleton Behaviour_Skeleton = null;

    public Collider colid = null;

    public GameObject Thumb = null;
    public GameObject Thumb1 = null;
    public GameObject Index = null;
    public GameObject Index1 = null;
    public GameObject Middle = null;
    public GameObject Middle1 = null;
    public GameObject Ring = null;
    public GameObject Ring1 = null;
    public GameObject Pinky = null;
    public GameObject Pinky1 = null;

    public Vector3 Indexbuf;

    public GameObject RootPosRef = null;
    public GameObject RigPosRef = null;


    private GameObject holder;
    private GameObject pointer;

    // Start is called before the first frame update
    void Start()
    {

    }

    private void Awake()
    {
        //Indexbuf = Index.transform.position;


        holder = new GameObject();
        holder.transform.parent = Index.transform;
        holder.transform.localPosition = Vector3.zero;
        holder.transform.localRotation = Quaternion.Euler(0, 90, 0);

        pointer = GameObject.CreatePrimitive(PrimitiveType.Cube);
        pointer.transform.parent = holder.transform;
        pointer.transform.localScale = new Vector3(0.03f, 0.03f, 0.03f);
        pointer.transform.localPosition = new Vector3(0f, 0f, 50f);
        pointer.transform.localRotation = Quaternion.Euler(0, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        //RigPosRef.transform.position = RootPosRef.transform.position;
        //RigPosRef.transform.rotation = RootPosRef.transform.rotation;

        //Debug.Log(Indexbuf.x + Indexbuf.y + Indexbuf.z);



        Ray raycastIndex = new Ray(Index.transform.position, -Index.transform.up);
        RaycastHit hitIndex;

        Ray raycastThumb = new Ray(Thumb.transform.position, -Thumb.transform.up);
        RaycastHit hitThumb;

        Ray raycastMiddle = new Ray(Middle.transform.position, -Middle.transform.up);
        RaycastHit hitMiddle;

        Ray raycastRing = new Ray(Ring.transform.position, -Ring.transform.up);
        RaycastHit hitRing;

        Ray raycastPinky = new Ray(Pinky.transform.position, -Pinky.transform.up);
        RaycastHit hitPinky;



        //人差し指

        if (Physics.Raycast(raycastIndex, out hitIndex, 0.05f))
        {
            Debug.Log(hitIndex.collider.gameObject.name);


            Behaviour_Skeleton.skeletonBlend = 0;
            TargetIndex.transform.position = hitIndex.transform.position;
            TargetIndex.transform.rotation = hitIndex.transform.rotation;


            Debug.Log("当たってる");

        }
        else
        {
            Behaviour_Skeleton.skeletonBlend = 1;
            TargetIndex.transform.position = Index.transform.position;
            TargetIndex.transform.rotation = Index.transform.rotation;

            Debug.Log("当たってない！！！！！！！！！！！！！！");

        }

        //親指

        if (Physics.Raycast(raycastThumb, out hitThumb, 0.05f))
        {
            Debug.Log(hitThumb.collider.gameObject.name);


            Behaviour_Skeleton.skeletonBlend = 0;
            TargetThumb.transform.position = hitThumb.transform.position;
            TargetThumb.transform.rotation = hitThumb.transform.rotation;


            Debug.Log("当たってる");

        }
        else
        {
            Behaviour_Skeleton.skeletonBlend = 1;
            TargetThumb.transform.position = Thumb.transform.position;
            TargetThumb.transform.rotation = Thumb.transform.rotation;

            Debug.Log("当たってない！！！！！！！！！！！！！！");

        }

        //中指

        if (Physics.Raycast(raycastMiddle, out hitMiddle, 0.05f))
        {
            Debug.Log(hitMiddle.collider.gameObject.name);


            Behaviour_Skeleton.skeletonBlend = 0;
            TargetMiddle.transform.position = hitMiddle.transform.position;
            TargetMiddle.transform.rotation = hitMiddle.transform.rotation;


            Debug.Log("当たってる");

        }
        else
        {
            Behaviour_Skeleton.skeletonBlend = 1;
            TargetMiddle.transform.position = Middle.transform.position;
            TargetMiddle.transform.rotation = Middle.transform.rotation;

            Debug.Log("当たってない！！！！！！！！！！！！！！");

        }

        //薬指

        if (Physics.Raycast(raycastRing, out hitRing, 0.05f))
        {
            Debug.Log(hitRing.collider.gameObject.name);


            Behaviour_Skeleton.skeletonBlend = 0;
            TargetRing.transform.position = hitRing.transform.position;
            TargetRing.transform.rotation = hitRing.transform.rotation;


            Debug.Log("当たってる");

        }
        else
        {
            Behaviour_Skeleton.skeletonBlend = 1;
            TargetRing.transform.position = Ring.transform.position;
            TargetRing.transform.rotation = Ring.transform.rotation;

            Debug.Log("当たってない！！！！！！！！！！！！！！");

        }

        //小指

        if (Physics.Raycast(raycastPinky, out hitPinky, 0.05f))
        {
            Debug.Log(hitPinky.collider.gameObject.name);


            Behaviour_Skeleton.skeletonBlend = 0;
            TargetPinky.transform.position = hitPinky.transform.position;
            TargetPinky.transform.rotation = hitPinky.transform.rotation;


            Debug.Log("当たってる");

        }
        else
        {
            Behaviour_Skeleton.skeletonBlend = 1;
            TargetPinky.transform.position = Pinky.transform.position;
            TargetPinky.transform.rotation = Pinky.transform.rotation;

            Debug.Log("当たってない！！！！！！！！！！！！！！");

        }



        Debug.DrawRay(raycastThumb.origin, raycastThumb.direction * 0.03f, Color.yellow, 2, false);
        Debug.DrawRay(raycastIndex.origin, raycastIndex.direction * 0.03f, Color.yellow, 2, false);
        Debug.DrawRay(raycastMiddle.origin, raycastMiddle.direction * 0.03f, Color.yellow, 2, false);
        Debug.DrawRay(raycastRing.origin, raycastRing.direction * 0.03f, Color.yellow, 2, false);
        Debug.DrawRay(raycastPinky.origin, raycastPinky.direction * 0.03f, Color.yellow, 2, false);


        //bool bHit = Physics.Raycast(raycast, out hit);

    }


}
