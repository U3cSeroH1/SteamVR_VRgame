using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VR;
using Valve.VR;

public class Climber : MonoBehaviour
{

    public float gravity = 45.0f;
    public float sensitivity = 45.0f;

    public GameObject Body;

    public Vector3 prevPos;


    public new Rigidbody rigidbody;
    public ClimberHand currentHand = null;

    //private CharacterController controller = null;

    private void Awake()
    {
        //rigidbody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        CalculateMovement();
    }

    private void CalculateMovement()
    {
        //Vector3 movement = Vector3.zero;

        //if (currentHand)
        //    movement += currentHand.Delta * sensitivity;

        //if (movement == Vector3.zero)
        //    movement.y -= gravity * Time.deltaTime;


        //controller.Move(movement * Time.deltaTime);


        if (currentHand)
        {
            Body.transform.position +=  (Quaternion.Euler(0, 0, 0) * currentHand.Delta) ;
            rigidbody.useGravity = false;
        }

        else
        {
            rigidbody.useGravity = true;
        }
        //prevPos = currentHand.transform.localPosition;

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