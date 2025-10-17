using StarterAssets;
using System.Collections;
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
    private InputAction throwAction;
    public bool throwing;
    public Transform cameraTransform;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (armController == null)
        {
            armController = GetComponent<ArmController>();                       //add as safety measure if its not set in inspector
        }

            throwAction = InputSystem.actions.FindAction("Throw");

        if(throwAction == null) 
        {
            Debug.Log("oH NO");
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        if (armController == null || handTransform == null)                    //add null cuz im dumb so i gotta notice if its not actually assigned
        {
            return;
        }

        if (armController.isRaising && grabbedObject == null && !throwing)            //if arm is raising and nothing is grabbed and ur not throwing
        {
            
                GrabObject();
            
        }

        else if (grabbedObject != null && throwAction != null && throwAction.triggered)       //if holding object and throwing by clicking T
        {
            ThrowObject();
        }

        else if (!armController.isRaising && grabbedObject != null)                          //stopped raising arm and not grabbing object anymore
        {
            ReleaseObject();
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
            Debug.Log("Throwing object");

            rb.isKinematic = false;                                                       //not working, stays in air
            rb.linearVelocity = cameraTransform.forward * throwingForce;
            grabbedObject.transform.SetParent(null);                                 //shouldn't be attached to parent anymore since its dropped
            StartCoroutine(ThrowCooldown(2f));
        }

        grabbedObject = null;
        

    }

    IEnumerator ThrowCooldown(float delay)                   //cooldown so it doesnt snap back to grab while throwing
    {
        throwing = true;
        yield return new WaitForSeconds(delay);
        throwing = false;
    }

}