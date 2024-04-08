using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.InputSystem;
using System.Collections.Generic;

public class RayInteractorUIHit : MonoBehaviour
{
    public InputActionProperty LeftTriggerRef;  //fio
    public InputActionProperty RightTriggerRef;
    public XRRayInteractor RrayInteractor; // Reference to the XR Ray Interactor
    public XRRayInteractor LrayInteractor; // Reference to the XR Ray Interactor
    public GameObject uiElement; // Reference to the UI element to detect hits on
    public LineRenderer lineRenderer;  //tracing line reference

    private List<Vector3> hitPoints = new List<Vector3>();  //stores hit points
    

    private List<float> speeds = new List<float>(); // stores speed values
    private List<float> accelerations = new List<float>(); // stores acceleration values
    private List<float> jerks = new List<float>(); // stores jerk values

    private float lastTime = 0f;
    private Vector3 lastPosition = Vector3.zero;
  

    private void Update()
    {
        float currentTime = Time.time;
        float deltaTime = currentTime - lastTime;

        //left trigger reference
        float LTriggerValue = LeftTriggerRef.action.ReadValue<float>();
        bool LTriggerPressed = LTriggerValue > 0;

        //right trigger reference
        float RTriggerValue = RightTriggerRef.action.ReadValue<float>();
        bool RTriggerPressed = RTriggerValue > 0;

        // Check if there's a valid ray interactor and UI element
        if (RrayInteractor && LrayInteractor != null && uiElement != null)
        {
            
            // Check if the ray interactor is hitting anything
            RaycastHit hit;
            if (RrayInteractor.TryGetCurrent3DRaycastHit(out hit) && RTriggerPressed)
            {

                // Check if the ray interactor is hitting the specified UI element
                if (hit.collider.gameObject == uiElement)
                {

                    
                    hitPoints.Add(hit.point);

                    Vector3 velocity = (hit.point - lastPosition) / deltaTime;
                    float speed = velocity.magnitude;
                    float acceleration = speed / deltaTime;

                    float lastAcceleration = (accelerations.Count > 0) ? accelerations[accelerations.Count - 1] : 0f;
                    float jerk = (acceleration - lastAcceleration) / deltaTime;

                    speeds.Add(speed);
                    accelerations.Add(acceleration);
                    jerks.Add(jerk);

                    lastPosition = hit.point;
                    lastTime = currentTime;

                    //Debug.Log("Right hand Ray interactor hit: " + uiElement.name + " @ x: " + hit.point.x.ToString("F3") + " @ y: " + hit.point.y.ToString("F3"));
                }
            }
            else if (LrayInteractor.TryGetCurrent3DRaycastHit(out hit) && LTriggerPressed)
            {
                // Check if the ray interactor is hitting the specified UI element
                if (hit.collider.gameObject == uiElement)
                {
                    
                    hitPoints.Add(hit.point);

                    Vector3 velocity = (hit.point - lastPosition) / deltaTime;
                    float speed = velocity.magnitude;
                    float acceleration = speed / deltaTime;

                    float lastAcceleration = (accelerations.Count > 0) ? accelerations[accelerations.Count - 1] : 0f;
                    float jerk = (acceleration - lastAcceleration) / deltaTime;

                    speeds.Add(speed);
                    accelerations.Add(acceleration);
                    jerks.Add(jerk);

                    lastPosition = hit.point;
                    lastTime = currentTime;
                    
                    

                    //Debug.Log("Left hand Ray interactor hit: " + uiElement.name + " @ x: " + hit.point.x.ToString("F3") + " @ y: " + hit.point.y.ToString("F3"));
                }
            }
            else
            {
                UpdateLineRenderer();
            }
        }

    }

private void UpdateLineRenderer()
    {
        if (lineRenderer != null)
        {
            // Set the number of positions in the LineRenderer
            lineRenderer.positionCount = hitPoints.Count;

            // Set the positions of the LineRenderer
            for (int i = 0; i < hitPoints.Count; i++)
            {
                lineRenderer.SetPosition(i, hitPoints[i]);
            }
            
        }
    }
    public void ClearPoints()  //clear points function
    {
        hitPoints.Clear();
        speeds.Clear();
        accelerations.Clear();
        jerks.Clear();
    }

    public float CalculateAverageSpeed()
    {
        if (speeds.Count == 0)
            return 0f;

        float sum = 0f;
        foreach (float speed in speeds)
        {
            sum += speed;
        }
        return sum / speeds.Count;
    }
   
    public float CalculateAverageAcceleration()
    {
        if (accelerations.Count == 0)
            return 0f;

        float sum = 0f;
        foreach (float acceleration in accelerations)
        {
            sum += acceleration;
        }
        return sum / accelerations.Count;
    }

    public float CalculateAverageJerk()
    {
        if (jerks.Count == 0)
            return 0f;

        float sum = 0f;
        foreach (float jerk in jerks)
        {
            sum += jerk;
        }
        return sum / jerks.Count;
    }
    
    public void DisplayAvgAcc()
    {
        Debug.Log("Average speed, acceleration, and jerk: " + CalculateAverageSpeed() +" , "+CalculateAverageAcceleration() + " , " +CalculateAverageJerk());
    }
}
