using UnityEngine;
using System.Collections;
using Valve.VR.InteractionSystem;
using System;

public class StickyFinger_Crafting : MonoBehaviour
{
    GameObject _parent;
    public GameObject HoldItem = null;

    //public bool ChangeedHoldItem=false;

    //public CraftingSystem craftingSystem = null;

    void Start()
    {

    }

    void Update()
    {



    }

    private void OnTriggerStay(Collider other)
    {
        if (other.transform.parent.gameObject.GetComponent<Interactable>())
        {
            _parent = other.transform.parent.gameObject;

            if (!_parent.gameObject.GetComponent<Interactable>().attachedToHand && !HoldItem)
            {

                _parent.gameObject.transform.parent = transform.parent;


                _parent.gameObject.transform.position = this.transform.position;
                _parent.gameObject.transform.rotation = this.transform.rotation;


                _parent.gameObject.GetComponent<Rigidbody>().isKinematic = true;

                HoldItem = _parent.gameObject;

                //ChangeedHoldItem = true;

                //craftingSystem.canCraft();

            }
            else if (_parent.gameObject.GetComponent<Interactable>().attachedToHand && HoldItem)
            {
                _parent.gameObject.transform.parent = null;
                _parent.gameObject.GetComponent<Rigidbody>().isKinematic = false;

                HoldItem = null;
                //ChangeedHoldItem = true;

                //craftingSystem.canCraft();
            }


        }

    }
}
