using StarterAssets;
using UnityEngine;
using UnityEngine.InputSystem;

public class Crouch : MonoBehaviour
{
    private StarterAssetsInputs _input;

    public float crouchStepSize = 0.05f;             //increments bt which it will shift between the values
    public float HeightStanding = 1f;                   //same size as usual player height
    public float HeightCrouching = 0.5f;              //50% shorter
    public float rayDistance = 1.5f;                //distance for raycast to check
    public LayerMask crouchableLayer;               //crouch layer
    
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


            newYScale = Mathf.Lerp(transform.localScale.y, HeightCrouching, crouchStepSize);                   //lerp into new size when crouching, 
            transform.localScale = new Vector3(transform.localScale.x, newYScale, transform.localScale.z);         
            Debug.Log("Crouching");
        }
        else
        {


            newYScale = Mathf.Lerp(transform.localScale.y, HeightStanding, crouchStepSize);                          //go back to original size
            transform.localScale = new Vector3(transform.localScale.x, newYScale, transform.localScale.z);
        }
    }

    void CheckAbove()
    {

    }
    
    
   
}
