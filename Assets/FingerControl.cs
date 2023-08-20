using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FingerControl : MonoBehaviour
{
    public Transform[] pinkyBones; // Array of pinky finger's joint transforms
    public float rotationSpeed = 30f;

    private Quaternion[] initialRotations; // Store initial rotations for the pinky

    void Start()
    {
        StoreInitialRotations();
    }

    void Update()
    {
        HandleFingerInput();
    }

    void StoreInitialRotations()
    {
        initialRotations = new Quaternion[pinkyBones.Length];
        for (int i = 0; i < pinkyBones.Length; i++)
        {
            initialRotations[i] = pinkyBones[i].localRotation;
        }
    }

    void HandleFingerInput()
    {
        // Control pinky with the home row keys
        if (Input.GetKey(KeyCode.P)) // Pinky
        {
            RotateFinger(pinkyBones, Input.GetAxis("Vertical") * rotationSpeed, -90f, 0f);
        }

        // Release the key to return pinky to initial position
        if (Input.GetKeyUp(KeyCode.P))
        {
            ResetFinger(pinkyBones, initialRotations);
        }
    }

    void RotateFinger(Transform[] bones, float rotationAmount, float minAngle, float maxAngle)
    {
        foreach (Transform bone in bones)
        {
            // Calculate the new rotation angle
            float newAngle = bone.localRotation.eulerAngles.x + rotationAmount * Time.deltaTime;

            // Apply rotation within the specified limits
            newAngle = Mathf.Clamp(newAngle, minAngle, maxAngle);

            // Apply the new rotation
            bone.localRotation = Quaternion.Euler(newAngle, 0f, 0f);
        }
    }

    void ResetFinger(Transform[] bones, Quaternion[] initialRotations)
    {
        for (int i = 0; i < bones.Length; i++)
        {
            bones[i].localRotation = Quaternion.Lerp(bones[i].localRotation, initialRotations[i], Time.deltaTime * rotationSpeed);
        }
    }
}

