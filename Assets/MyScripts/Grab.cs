using StarterAssets;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.XR;

public class PickUp : MonoBehaviour
{
    public FirstPersonController FirstPersonController;
    public ArmController armController;
    public Transform handTransform;
    public float grabRadius;
    private GameObject grabbedObject = null;
    public float throwingForce;
    private PlayerInput playerInput;
    private InputAction throwAction;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (armController == null)
        {
            armController = GetComponent<ArmController>();                       //add as safety measure if its not set in inspector
        }

        playerInput = GetComponent<PlayerInput>();
        if (playerInput != null )
        {
            throwAction = playerInput.actions["Throw"];
            
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (armController == null || handTransform == null)                    //add null cuz im dumb so i gotta notice if its not actually assigned
        {
            return;
        }

        if (armController.isRaising)              //reference arm controller cuz i gotta know when its actually up to be able to grab, dont grab just when close
        {
            if (grabbedObject == null)               //grab only if i didnt already grab something
            {
                GrabObject();
            }
        }

        else
        {
            if (grabbedObject != null)                   //drop object if arm isnt raised anymore and if we are still holding it
            {
                ReleaseObject();
            }
        }

        if (grabbedObject != null && throwAction != null && throwAction.triggered)
        {
            ThrowObject();
        }
    }

    void GrabObject()
    {
        Collider[] hits = Physics.OverlapSphere(handTransform.position, grabRadius);                                      //get the collider thats within the collider i set around arm transform

        foreach (Collider hit in hits)
        {
            if (hit.CompareTag("Grabbable"))                                                  //only grab grabbable
            {
                grabbedObject = hit.gameObject;                                              //i think this stores the grabbed object, ask john or google

                Rigidbody rb = grabbedObject.GetComponent<Rigidbody>();                      //stop it reacting weird to the physics whil i hold it      

                if (rb != null) rb.isKinematic = true;

                grabbedObject.transform.SetParent(handTransform);                          //parent to hand transform so it can move with arm
                grabbedObject.transform.localPosition = Vector3.zero;                      //it goes to centre of hand, avoid weird snaps
                grabbedObject.transform.localRotation = Quaternion.identity;                    //makes the rotation match the hand so it doesn't overlap
                break;
            }
        }
    }

    void ReleaseObject()
    {
        Rigidbody rb = grabbedObject.GetComponent<Rigidbody>();

        if (rb != null) rb.isKinematic = false;                                          //make physics normal again so it can fall when released
        {
            grabbedObject.transform.SetParent(null);
            grabbedObject = null;                                          //shouldn't be attached to parent anymore since its dropped
        }
    }

    void ThrowObject()
    {
        Rigidbody rb = grabbedObject.GetComponent<Rigidbody>();

        if (rb != null) {

            rb.isKinematic = false;                                                       //not working, stays in air
            rb.linearVelocity = handTransform.forward * throwingForce;
            grabbedObject.transform.SetParent(null);                                 //shouldn't be attached to parent anymore since its dropped
        }

        grabbedObject = null;
        

    }

}