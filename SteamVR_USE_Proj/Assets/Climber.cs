using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VR;
using Valve.VR;

public class Climber : MonoBehaviour
{

    public float gravity = 45.0f;
    public float sensitivity = 45.0f;

    public ClimberHand currentHand = null;
    private CharacterController controller = null;

    private void Awake()
    {
        controller = GetComponent<CharacterController>();
    }


    void Update()
    {
        CalculateMovement();
    }

    private void CalculateMovement()
    {
        Vector3 movement = Vector3.zero;

        if (currentHand)
            movement += currentHand.Delta * sensitivity;

        if (movement == Vector3.zero)
            movement.y -= gravity * Time.deltaTime;


        controller.Move(movement * Time.deltaTime);


    }

    public void SetHand(ClimberHand hand)
    {
        if (currentHand)
            currentHand.ReleasePoint();


        

        currentHand = hand;
        //Debug.Log("セカンド");

    }

    public void ClearHand()
    {
        //Debug.Log("ファースト");
        currentHand = null;
    }

}