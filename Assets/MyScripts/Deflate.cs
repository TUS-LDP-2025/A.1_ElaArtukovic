using UnityEngine;
using StarterAssets;
using UnityEngine.InputSystem;

public class Deflate : MonoBehaviour
{

    private StarterAssetsInputs _input;

    public float deflatingSize = 0.05f;              //increments like in crouch
    public float UsualRadius = 1f;                   //the usual width of the player
    public float DeflatedRadius = 0.5f;                 //skinnier version for squeezing through

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _input = GetComponent<StarterAssetsInputs>();
    }

    // Update is called once per frame
    void Update()
    {
        Deflating();
    }

    private void Deflating()
    {
        float newXZScale;

        if (_input.deflate)                            //reference in starter assets
        {
            Debug.Log("Deflating");

            newXZScale = Mathf.Lerp(transform.localScale.x, DeflatedRadius, deflatingSize);                              //same as crouch, make it slimmer when deflating
            transform.localScale = new Vector3(newXZScale, transform.localScale.y, newXZScale);              

        }

        else
        {
            newXZScale = Mathf.Lerp(transform.localScale.x, UsualRadius, deflatingSize);                                   //turn it back when not deflating
            transform.localScale = new Vector3(newXZScale, transform.localScale.y, newXZScale);
        }
    }
}
