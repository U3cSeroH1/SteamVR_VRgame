using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class move : MonoBehaviour
{
    public float Sensitivity = 0.1f;



    public float MaxSpeed = 1.0f;

    float moveX = 0f;
    float moveZ = 0f;


    public SteamVR_Action_Boolean MovePress = null;
    public SteamVR_Action_Vector2 MoveValue = null;



    private float Speed = 0.0f;

    public CharacterController CharacterController = null;

    public Transform Head = null;

    [SerializeField]
    private GameObject leftcontroller;

    private void Update()
    {

        CalculateMovement();
        HandleHeight();
    }





    private void CalculateMovement()
    {

        moveX = MoveValue.axis.x * MaxSpeed;
        moveZ = MoveValue.axis.y * MaxSpeed;


        //Debug.Log(MoveValue.axis.x);

        //左


        if (MoveValue.axis != Vector2.zero)
        {
            CharacterController.SimpleMove(leftcontroller.transform.rotation * new Vector3(moveX, 0, moveZ));
        }

    }

    private void HandleHeight()
    {
        float distanceFromFloor = Mathf.Clamp(Head.localPosition.y, 0.5f, 2);
        CharacterController.height = distanceFromFloor;

        Vector3 newCenter = Vector3.zero;
        newCenter.y = CharacterController.height / 2;
        newCenter.y += CharacterController.skinWidth;

        newCenter.x = Head.localPosition.x;
        newCenter.z = Head.localPosition.z;

        newCenter = Quaternion.Euler(0, -transform.eulerAngles.y, 0) * newCenter;

        CharacterController.center = newCenter;


    }

}