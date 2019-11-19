﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class move : MonoBehaviour
{

    public SteamVR_Input_Sources leftHand;
    public SteamVR_Input_Sources rightHand;
    [SerializeField]
    private GameObject leftcontroller;

    void Start()
    {

    }

    void Update()
    {
        if (SteamVR_Input.GetState("touching", leftHand))
        {
            this.transform.position += leftcontroller.transform.forward * Time.deltaTime * 3f;
        }
    }
}