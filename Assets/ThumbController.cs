using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThumbController : MonoBehaviour
{
    [SerializeField] private Transform rootBone; // Reference to the root bone
    [SerializeField] private Transform midBone;  // Reference to the mid bone
    [SerializeField] private float rotationSpeed = 1000f;
    [SerializeField] private string inputName;

    private Quaternion initialRootRotation;
    private Quaternion initialMidRotation;

    void Start()
    {
        initialRootRotation = rootBone.localRotation;
        initialMidRotation = midBone.localRotation;
    }

    void FixedUpdate()
    {
        HandleFingerInput();
    }

    void HandleFingerInput()
    {
        //Debug.Log("Midbone rotation " + midBone.rotation.eulerAngles.ToString());
        //Debug.Log("Rootbone rotation " + rootBone.localRotation.eulerAngles.ToString());
        if (Input.GetButton(inputName))
        {
            RotateBone(midBone, initialMidRotation, rotationSpeed, 0f, 90f);
            if (midBone.localRotation.eulerAngles.z >= initialMidRotation.eulerAngles.z + 60f)
            {
                RotateBone(rootBone, initialRootRotation, 0.5f * rotationSpeed, 0f, 45f); // Root bone only has to rotate half as much so it does so at half the speed
            }
        }
        else
        {
            RotateBone(rootBone, initialRootRotation, -0.5f * rotationSpeed, 0f, 45f); // Root bone only has to rotate half as much so it does so at half the speed

            if (rootBone.localRotation.eulerAngles.z <= initialRootRotation.eulerAngles.z + 15f)
            {
                RotateBone(midBone, initialMidRotation, -rotationSpeed, 0f, 90f);
            }
        }
    }

    void RotateBone(Transform bone, Quaternion initialRotation, float rotationAmount, float minAngle, float maxAngle)
    {
        float newRotation = Mathf.Clamp(bone.localRotation.eulerAngles.z + rotationAmount * Time.fixedDeltaTime, initialRotation.eulerAngles.z + minAngle, initialRotation.eulerAngles.z + maxAngle);
        bone.localRotation = Quaternion.Euler(initialRotation.eulerAngles.x, initialRotation.eulerAngles.y, newRotation);
    }
}
