using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
public class ClimberHand : MonoBehaviour
{
    public Climber climber = null;
    public SteamVR_Action_Boolean isGrab = null;

    public SteamVR_Input_Sources ControllerHand;

    public Vector3 Delta { private set; get; } = Vector3.zero;

    private Vector3 lastPosition = Vector3.zero;

    private GameObject currentPoint = null;
    private List<GameObject> contactPoints = new List<GameObject>();
    private MeshRenderer meshRenderer = null;

    private void Awake()
    {
        meshRenderer = GetComponentInChildren<MeshRenderer>();
    }

    private void Start()
    {
        climber = GetComponentInParent<Climber>();

        lastPosition = transform.localPosition;
    }

    private void Update()
    {
        if (isGrab.GetStateDown(ControllerHand))
        {


            GrabPoint();


        }

        if (isGrab.GetStateUp(ControllerHand))
        {
            ReleasePoint();



        }

    }

    private void FixedUpdate()
    {
        lastPosition = transform.localPosition;
    }

    private void LateUpdate()
    {
        Delta = lastPosition - transform.localPosition;
    }


    private void GrabPoint()
    {
        currentPoint = Utility.GetNearest(transform.localPosition, contactPoints);

        if (currentPoint)
        {
            climber.SetHand(this);
            //meshRenderer.enabled = false;
        }

    }

    public void ReleasePoint()
    {
        if (currentPoint)
        {
            climber.ClearHand();
            //meshRenderer.enabled = true;
        }

        currentPoint = null;

    }

    private void OnTriggerEnter(Collider other)
    {
        AddPoint(other.gameObject);
    }

    private void AddPoint(GameObject newObject)
    {
        if (newObject.CompareTag("Climbable"))
        {
            contactPoints.Add(newObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        RemovePoint(other.gameObject);
    }

    private void RemovePoint(GameObject newObject)
    {
        contactPoints.Remove(newObject);
    }






}