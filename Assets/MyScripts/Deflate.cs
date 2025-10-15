using UnityEngine;
using StarterAssets;
using UnityEngine.InputSystem;

public class Deflate : MonoBehaviour
{

    private StarterAssetsInputs _input;

    public float deflatingSize = 0.05f;
    public float UsualRadius = 1f;
    public float DeflatedRadius = 0.5f;

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
        float newXScale;

        if (_input.deflate)
        {
            Debug.Log("Deflating");

            newXScale = Mathf.Lerp(transform.localScale.x, 0.5f, deflatingSize);
            transform.localScale = new Vector3(newXScale, transform.localScale.y, transform.localScale.z);

        }

        else
        {
            newXScale = Mathf.Lerp(transform.localScale.x, 1f, deflatingSize);
            transform.localScale = new Vector3(newXScale, transform.localScale.y, transform.localScale.z);
        }
    }
}
