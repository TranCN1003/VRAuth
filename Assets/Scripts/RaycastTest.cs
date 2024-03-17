using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class RaycastTest : MonoBehaviour
{
    public XRRayInteractor interactor; // Reference to the XR Ray Interactor

private void Update()
{
    // Check if there's a valid interactor
    if (interactor != null)
    {
        // Check if the interactor is hitting something
        RaycastHit hit;
        if (interactor.TryGetCurrent3DRaycastHit(out hit))
        {
            // Check if the hit object has a Canvas component
            Canvas canvas = hit.collider.GetComponent<Canvas>();
            if (canvas != null)
            {
                // Get the point on the canvas where the ray hits
                Vector2 canvasPoint = hit.textureCoord; 
                Debug.Log("Canvas hit at: " + canvasPoint);
            }
            else
            {
                Debug.Log("No Canvas component found on the hit object: " + hit.collider.gameObject.name);
            }
        }
        else
        {
            Debug.Log("Interactor is not hitting anything.");
        }
    }
    else
    {
        Debug.Log("Interactor reference is null.");
    }
}
}

