using UnityEngine;
using System.Collections;
using Valve.VR.InteractionSystem;
using System;

public class StickyFinger_Crafting : MonoBehaviour
{
    GameObject _parent;
    public GameObject HoldItem = null;

    public float minScalePer = 0.1f;

    //public bool ChangeedHoldItem=false;

    //public CraftingSystem craftingSystem = null;

    void Start()
    {

    }

    void Update()
    {



    }

    private void OnTriggerEnter(Collider other)
    {
        
    }

    private void OnTriggerExit(Collider other)
    {
        
    }

    private void OnTriggerStay(Collider other)
    {

        if (!HoldItem)
        {
            if (other.transform.parent.gameObject.GetComponent<Interactable>())
            {
                _parent = other.transform.parent.gameObject;

                if (!_parent.gameObject.GetComponent<Interactable>().attachedToHand)
                {

                    _parent.gameObject.transform.parent = transform.parent;


                    _parent.gameObject.transform.position = this.transform.position;
                    _parent.gameObject.transform.rotation = this.transform.rotation;
                    _parent.gameObject.transform.localScale = _parent.gameObject.transform.localScale * minScalePer;

                    _parent.gameObject.GetComponent<Rigidbody>().isKinematic = true;

                    HoldItem = _parent.gameObject;

                    //ChangeedHoldItem = true;

                    //craftingSystem.canCraft();

                }
            }
        }
        else// Holditem
        {
            if (HoldItem.GetComponent<Interactable>())
            {
                

                if (HoldItem.GetComponent<Interactable>().attachedToHand)
                {
                    HoldItem.transform.parent = null;
                    HoldItem.GetComponent<Rigidbody>().isKinematic = false;
                    HoldItem.transform.localScale = HoldItem.transform.localScale / minScalePer;

                    HoldItem = null;
                    //ChangeedHoldItem = true;

                    //craftingSystem.canCraft();
                }
            }


        }




    }
}
