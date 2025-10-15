using UnityEngine;
using StarterAssets;
using UnityEngine.InputSystem;

public class Deflate : MonoBehaviour
{

    private StarterAssetsInputs _input;

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
        
    }
}
