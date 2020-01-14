using UnityEngine;
using Valve.VR;

public class MovementVR : MonoBehaviour
{
    public float moveForceMultiplier = 0.1f;



    public float MaxSpeed = 1.0f;



    float moveX = 0f;
    float moveZ = 0f;


    public SteamVR_Action_Boolean MovePress = null;
    public SteamVR_Action_Vector2 MoveValue = null;



    private float Speed = 0.0f;


    public Transform Head = null;

    [SerializeField]
    private GameObject leftcontroller;
    private int GroundCount;

    private CapsuleCollider CapCollider;
    private new Rigidbody rigidbody;

    public PhysicMaterial NoFrictionMaterial;
    public PhysicMaterial FrictionMaterial;

    //public LayerMask collisionMask;

    private void Start()
    {
        CapCollider = GetComponent<CapsuleCollider>();
        rigidbody = GetComponent<Rigidbody>();

        int layer1 = LayerMask.NameToLayer("MyBody");
        int layer2 = LayerMask.NameToLayer("MyHand");

        Physics.IgnoreLayerCollision(layer1, layer2);

    }

    private void FixedUpdate()
    {

        CalculateMovement(false);
        HandleHeight();

    }


    public void CalculateMovement(bool called)
    {
        Vector3 moveVector = Vector3.zero;


        moveX = MoveValue.axis.x;
        moveZ = MoveValue.axis.y;

        if (MovePress.state || called)
        {
            moveVector = MaxSpeed * ( Quaternion.Euler(0, leftcontroller.transform.eulerAngles.y, 0) * new Vector3(moveX, 0, moveZ));

            rigidbody.AddForce(moveForceMultiplier * (moveVector - rigidbody.velocity), ForceMode.Force);

            Debug.Log("ああああああああああああああああああああああああああああああああああああ！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！");

        }

    }

    private void HandleHeight()
    {
        float distanceFromFloor = Mathf.Clamp(Head.localPosition.y, 0.5f, 2);
        CapCollider.height = distanceFromFloor;

        Vector3 newCenter = Vector3.zero;
        newCenter.y = CapCollider.height / 2;
        //newCenter.y += CapCollider.skinWidth;

        newCenter.x = Head.localPosition.x;
        newCenter.z = Head.localPosition.z;

        //newCenter = Quaternion.Euler(0, -transform.eulerAngles.y, 0) * newCenter;

        CapCollider.center = newCenter;


    }

    private void OnCollisionEnter(Collision collision)
    {
        GroundCount++;
    }
    private void OnCollisionExit(Collision collision)
    {
        GroundCount--;
    }
}