using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class HandAnimationsScript : MonoBehaviour
{

    public InputActionProperty pinchAnimation; // Listening for input from the VR controller's pinch action using the Unity Input System
    public InputActionProperty grabAnimation;  // Listening for input from the VR controller's grab action using the Unity Input System

    public Animator handAnimation; // Accessing the Animator to send float values and trigger the correct animations

    void Update()
    {
        // Listening for pinch input and mapping it to a float value
        float pinchValue = pinchAnimation.action.ReadValue<float>();
        handAnimation.SetFloat("Pinch", pinchValue); // Sending the pinch value to the Animator to control the "Pinch" animation

        // Listening for grab input and mapping it to a float value
        float grabValue = grabAnimation.action.ReadValue<float>();
        handAnimation.SetFloat("Grab", grabValue);  // Sending the grab value to the Animator to control the "Grab" animation
    }
}
