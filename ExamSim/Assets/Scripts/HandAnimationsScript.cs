using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class HandAnimationsScript : MonoBehaviour
{
   
   public InputActionProperty pinchAnimation;
   public InputActionProperty grabAnimation;

   public Animator handAnimation;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float pinchValue = pinchAnimation.action.ReadValue<float>();
        handAnimation.SetFloat("Pinch", pinchValue);

        float grabValue = grabAnimation.action.ReadValue<float>();
       handAnimation.SetFloat("Grab", grabValue);
    }
}
