using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VR;
using Valve.VR;

public class GripMove : MonoBehaviour
{

    public float gravity = 45.0f;


    public SteamVR_Action_Boolean isGrip = null;

    public GameObject leftcontroller;

    void Update()
    {



        //"Grip"Moveなのでグリップボタンを入力したときに移動処理を実行
        if (isGrip.state)
        {
            Debug.Log("fuckin");


        }


    }
}