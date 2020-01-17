using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class TrueGrab : MonoBehaviour
{
    public GameObject TargetIndex = null;
    public GameObject HintIndex = null;


    public SteamVR_Behaviour_Skeleton Behaviour_Skeleton = null;



    public GameObject Indexend = null;
    public GameObject Index2 = null;
    public GameObject Index0 = null;


    public Transform RootPosRef;
    public Transform RigPosRef;

    private GameObject holder;
    private GameObject pointer;

    // Start is called before the first frame update
    void Start()
    {

        float DistA = Vector3.Distance(Indexend.transform.position, Index0.transform.position);

    }

    private void Awake()
    {
        //Indexbuf = Index.transform.position;

        
    }

    // Update is called once per frame
    void Update()
    {

        float DistA = Vector3.Distance(Indexend.transform.position, Index0.transform.position);

        Ray raycastIndex0 = new Ray(Index0.transform.position, -Indexend.transform.up);



        Ray raycastIndex = new Ray(Indexend.transform.position, -Indexend.transform.up);



        RaycastHit hitIndex;




        //人差し指

        if (Physics.Raycast(raycastIndex, out hitIndex, 0.05f))
        {
            Debug.Log(hitIndex.collider.gameObject.name);


            //Behaviour_Skeleton.skeletonBlend = 0;
            //TargetIndex.transform.position = hitIndex.transform.position;
            //TargetIndex.transform.rotation = hitIndex.transform.rotation;

            


        }
        else
        {
            Behaviour_Skeleton.skeletonBlend = 1;
            TargetIndex.transform.position = Indexend.transform.position;
            TargetIndex.transform.rotation = Indexend.transform.rotation;



        }

        HintIndex.transform.position = Index2.transform.position;



        Debug.DrawRay(raycastIndex.origin, raycastIndex.direction * 0.03f, Color.yellow, 2, false);

        Debug.DrawRay(raycastIndex0.origin, raycastIndex.direction * DistA, Color.red, 0.1f, false);


        //bool bHit = Physics.Raycast(raycast, out hit);

    }


}
