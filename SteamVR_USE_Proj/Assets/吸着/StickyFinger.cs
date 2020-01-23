using UnityEngine;
using System.Collections;
using Valve.VR.InteractionSystem;
using System;

public class StickyFinger : MonoBehaviour
{
    //[Flags]
    public enum SticyType
    {
        bottle = 1 << 0,    // グー
        consent = 1 << 1,  // チョキ
        Key = 1 << 2,    // パー
    }

    public SticyType sticyType;

    void Start()
    {
        StartCoroutine("OnCollisionStay");
    }

    void Update()
    {
    }

    IEnumerator OnCollisionStay(Collision col)
    {
        if (col.gameObject.CompareTag("PlugMan"))
        {

            if (! col.gameObject.GetComponent<Interactable>().attachedToHand)
            {
                this.gameObject.AddComponent<FixedJoint>();


                this.GetComponent<FixedJoint>().connectedBody = col.gameObject.GetComponent<Rigidbody>();

                col.gameObject.transform.position = this.transform.position + new Vector3(0, 0, col.transform.localScale.z);
                col.gameObject.transform.rotation = this.transform.rotation;

                yield return new WaitForSeconds(0.001f);

                this.GetComponent<FixedJoint>().breakForce = 100;
                this.GetComponent<FixedJoint>().breakTorque = 100;
            }


        }


    }
}