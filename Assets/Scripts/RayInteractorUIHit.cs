using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.InputSystem;

public class RayInteractorUIHit : MonoBehaviour
{
    public InputActionProperty LeftTriggerRef;  //fio
    public InputActionProperty RightTriggerRef;
    public XRRayInteractor RrayInteractor; // Reference to the XR Ray Interactor
    public XRRayInteractor LrayInteractor; // Reference to the XR Ray Interactor
    public GameObject uiElement; // Reference to the UI element to detect hits on
  

    private void Update()
    {
        //left trigger reference
        float LTriggerValue = LeftTriggerRef.action.ReadValue<float>();
        bool LTriggerPressed = LTriggerValue > 0;
        //Debug.Log(LTriggerPressed);

        //right trigger reference
        float RTriggerValue = RightTriggerRef.action.ReadValue<float>();
        bool RTriggerPressed = RTriggerValue > 0;
        //Debug.Log(RTriggerPressed);

        // Check if there's a valid ray interactor and UI element
        if (RrayInteractor && LrayInteractor != null && uiElement != null)
        {
            // Check if the ray interactor is hitting anything
            RaycastHit hit;
            if (RrayInteractor.TryGetCurrent3DRaycastHit(out hit) && RTriggerPressed)
            {
                // Debug log to check what object the ray interactor is hitting
                //Debug.Log("Ray interactor hit: " + hit.collider.gameObject.name);

                // Check if the ray interactor is hitting the specified UI element
                if (hit.collider.gameObject == uiElement)
                {
                    
                    Debug.Log("Right hand Ray interactor hit: " + uiElement.name + " @ x: " + hit.point.x.ToString("F3") + " @ y: " + hit.point.y.ToString("F3"));
                }
            }
            else if (LrayInteractor.TryGetCurrent3DRaycastHit(out hit) && LTriggerPressed)
            {
                // Debug log to check what object the ray interactor is hitting
                //Debug.Log("Ray interactor hit: " + hit.collider.gameObject.name);

                // Check if the ray interactor is hitting the specified UI element
                if (hit.collider.gameObject == uiElement)
                {
                    
                    Debug.Log("Left hand Ray interactor hit: " + uiElement.name + " @ x: " + hit.point.x.ToString("F3") + " @ y: " + hit.point.y.ToString("F3"));
                }
            }
            else
            {
                //Debug.Log("Ray interactor is not hitting anything.");
            }
        }
        else
        {
            //Debug.LogWarning("Ray interactor or UI element not set.");
        }
    }
}
