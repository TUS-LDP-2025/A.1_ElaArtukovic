/*using StarterAssets;
using UnityEngine;
using UnityEngine.InputSystem;

public class Crouch : MonoBehaviour
{
    private StarterAssetsInputs _input;

    public float crouchStepSize = 0.05f;
    public float HeightStanding = 1f;
    public float HeightCrouching = 0.5f;
    private bool isCrouching = false;
   
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _input = GetComponent<StarterAssetsInputs>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
     private void OnCrouch(InputAction.CallbackContext context)
    {
        isCrouching = context.ReadValueAsButton();
        float newYScale;

        if (_input.crouch)
        {
            Debug.Log("Crouching");


            newYScale = Mathf.Lerp(transform.localScale.y, 0.5f, crouchStepSize);
            transform.localScale = new Vector3(transform.localScale.x, newYScale, transform.localScale.z);
        }
        else
        {


            newYScale = Mathf.Lerp(transform.localScale.y, 1, crouchStepSize);
            transform.localScale = new Vector3(transform.localScale.x, newYScale, transform.localScale.z);
        }
    }
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
   
}
*/