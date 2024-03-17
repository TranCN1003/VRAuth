using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TraceScript : MonoBehaviour
{
    public InputActionProperty LeftTriggerRef;
    public InputActionProperty RightTriggerRef;
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float LTriggerValue = LeftTriggerRef.action.ReadValue<float>();
        bool LTriggerPressed = LTriggerValue > 0;
        //Debug.Log(LTriggerPressed);

        float RTriggerValue = RightTriggerRef.action.ReadValue<float>();
        bool RTriggerPressed = RTriggerValue > 0;
        //Debug.Log(RTriggerPressed);
        
        
    }
}
