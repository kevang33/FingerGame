using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FingerController : MonoBehaviour
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
            RotateBone(rootBone, initialRootRotation, -rotationSpeed, -100f, 0f);
            if (rootBone.localRotation.eulerAngles.z <= initialRootRotation.eulerAngles.z - 60f)
            {
                RotateBone(midBone, initialMidRotation, -rotationSpeed, -90f, 0f);
            }
        }
        else
        {
            RotateBone(midBone, initialMidRotation, rotationSpeed, -90f, 0f);
            if (midBone.localRotation.eulerAngles.z >= initialMidRotation.eulerAngles.z - 30f)
            {
                RotateBone(rootBone, initialRootRotation, rotationSpeed, -100f, 0f);
            }
        }
    }

    void RotateBone(Transform bone, Quaternion initialRotation, float rotationAmount, float minAngle, float maxAngle)
    {
        float newRotation = Mathf.Clamp(bone.localRotation.eulerAngles.z + rotationAmount * Time.fixedDeltaTime, initialRotation.eulerAngles.z + minAngle, initialRotation.eulerAngles.z + maxAngle);
        bone.localRotation = Quaternion.Euler(initialRotation.eulerAngles.x, initialRotation.eulerAngles.y, newRotation);
    }
}
