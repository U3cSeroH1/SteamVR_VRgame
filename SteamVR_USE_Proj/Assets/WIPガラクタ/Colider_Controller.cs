//======= Copyright (c) Valve Corporation, All rights reserved. ===============
//
// Purpose: Collider dangling from the player's head
//
//=============================================================================

using UnityEngine;
using System.Collections;

namespace Valve.VR.InteractionSystem
{
    //-------------------------------------------------------------------------
    [RequireComponent(typeof(CharacterController))]
    public class Colider_Controller : MonoBehaviour
    {
        public Transform head;

        public CharacterController CharacterController;

        //-------------------------------------------------


        //-------------------------------------------------
        void Update()
        {
            float distanceFromFloor = Mathf.Clamp(head.localPosition.y, 0.5f, 2);
            CharacterController.height = distanceFromFloor;

            Vector3 newCenter = Vector3.zero;
            newCenter.y = CharacterController.height / 2;
            newCenter.y += CharacterController.skinWidth;

            newCenter.x = head.localPosition.x;
            newCenter.z = head.localPosition.z;

            newCenter = Quaternion.Euler(0, -transform.eulerAngles.y, 0) * newCenter;

            CharacterController.center = newCenter;
        

        }
    }
}
