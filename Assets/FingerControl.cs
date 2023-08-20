using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class FingerControl : MonoBehaviour
{
    // [SerializeField] private float rotationSpeed = 30f;
    [SerializeField] private TwoBoneIKConstraint rightPinky;

    void Start()
    {
    }

    // void Update()
    // {
    //     HandleFingerInput();
    // }

    // Check if the semicolon key is pressed
    public float easingSpeed = 2.0f; // Speed at which the weight transitions

    void Update()
    {
        if (Input.GetKey(KeyCode.Semicolon))
        {
            rightPinky.weight = Mathf.MoveTowards(rightPinky.weight, 1.0f, easingSpeed * Time.deltaTime);
        }
        else
        {
            rightPinky.weight = Mathf.MoveTowards(rightPinky.weight, 0.0f, easingSpeed * Time.deltaTime);
        }
    }

    void HandleFingerInput()
    {
        // Left Pinky
        if (Input.GetKey(KeyCode.L)) 
        {
            rightPinky.weight = 1;
        }

        
        if (Input.GetKeyUp(KeyCode.L))
        {
            rightPinky.weight = 0;
        }
    }
}

