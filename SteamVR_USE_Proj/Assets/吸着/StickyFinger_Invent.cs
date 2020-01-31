using UnityEngine;
using System.Collections;
using Valve.VR.InteractionSystem;
using System;

public class StickyFinger_Invent : MonoBehaviour
{

    GameObject _parent;

    void Start()
    {
        StartCoroutine("OnTriggerStay");
    }

    void Update()
    {
    }

    IEnumerator OnTriggerStay(Collider other)
    {

        if (other.transform.parent.gameObject.GetComponent<Interactable>())
        {
            _parent = other.transform.parent.gameObject;

            if (!_parent.gameObject.GetComponent<Interactable>().attachedToHand)
            {
                yield return new WaitForSeconds(0.001f);
                _parent.gameObject.transform.parent = transform.parent;


                _parent.gameObject.transform.position = this.transform.position;
                _parent.gameObject.transform.rotation = this.transform.rotation;


                _parent.gameObject.GetComponent<Rigidbody>().isKinematic = true;

            }
            else
            {
                _parent.gameObject.transform.parent = null;
                _parent.gameObject.GetComponent<Rigidbody>().isKinematic = false;
            }


        }

    }



}