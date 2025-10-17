using StarterAssets;
using UnityEngine;
using UnityEngine.InputSystem;

public class Crouch : MonoBehaviour
{
    private StarterAssetsInputs _input;

    public float crouchStepSize = 0.05f;             //increments bt which it will shift between the values
    public float HeightStanding = 1f;                   //same size as usual player height
    public float HeightCrouching = 0.5f;              //50% shorter
    public float rayDistance = 20f;                //distance for raycast to check
    public LayerMask crouchableLayer;               //crouch layer
    public bool isCrouching = false;

    
    void Start()
    {
        _input = GetComponent<StarterAssetsInputs>();
    }

    // Update is called once per frame
    void Update()
    {
        Crouching();
        CheckAbove();
    }
     private void Crouching()
    {
        
        float newYScale;

        if (_input.crouch)                   //reference it in starter assets 
        {
            Debug.Log("Crouching");
            isCrouching =true;

            newYScale = Mathf.Lerp(transform.localScale.y, HeightCrouching, crouchStepSize);                   //lerp into new size when crouching, 
            transform.localScale = new Vector3(transform.localScale.x, newYScale, transform.localScale.z);         
            Debug.Log("Crouching");
        }
        else
        {

            isCrouching=false;
            newYScale = Mathf.Lerp(transform.localScale.y, HeightStanding, crouchStepSize);                          //go back to original size
            transform.localScale = new Vector3(transform.localScale.x, newYScale, transform.localScale.z);
        }
    }

    void CheckAbove()
    {
        RaycastHit hit;
        Vector3 rayOrigin = transform.position + Vector3.up * 0.5f;        //add offset, might help detect collider if it overlaps with player collider before the crouch collider

        if (Physics.Raycast(rayOrigin, Vector3.up, out hit, rayDistance, crouchableLayer))                   //when doing this for deflate add Vector3.left and right separate
        {
            Debug.Log("Hitting something");                             //also not even showing

            if(hit.collider.CompareTag("Crouchable"))
            {
                Debug.Log("Can't stand up, object above me");                      //not logging when colliding with tag
                HeightCrouching = 0.5f;
            }
        }

        else
        {
            Debug.Log("[Raycast] didn't hit anything above");
        }
    }
    
    
   
}
