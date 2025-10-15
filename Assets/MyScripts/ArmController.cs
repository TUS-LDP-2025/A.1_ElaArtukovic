using TMPro;
using Unity.Burst.Intrinsics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class ArmController : MonoBehaviour
{

    public float raiseSpeed;
    private Quaternion startRotation;                 //start rotation
    private Quaternion endRotation;                    //when raised at end angle
    public bool isRaising = false;                    //checking if its being raised
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        startRotation = transform.localRotation;                      //keep track of how i stored the rotation at start
        endRotation = Quaternion.Euler(-90f, 0, 0) * startRotation;                   //makes rotation i need of 90 degrees on x
    }

    // Update is called once per frame
    void Update()
    {
        isRaising = Mouse.current.leftButton.isPressed;                       //check if its pressed, if yes then it can raise it

        if (isRaising)
        {
            RaiseArm();
        }

        else
        {
            LowerArm();
        }

    }

    private void RaiseArm()
    {

        transform.localRotation = Quaternion.Lerp(transform.localRotation, endRotation, Time.deltaTime * raiseSpeed);      //go from start rot to end rot during the speed i set
    }

    private void LowerArm()
    {
        transform.localRotation = Quaternion.Lerp(transform.localRotation, startRotation, Time.deltaTime * raiseSpeed);          //the same just backwards

    }
}