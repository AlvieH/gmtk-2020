using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Draggable : MonoBehaviour
{
    const string CHAIR_TAG = "Chair";

    [SerializeField] float clickHeight;
    [SerializeField] float clickLerpTime;
    

    private Rigidbody rigidbody;
    private ColorManagement colorManager;
    private float restingPosition;
    private Camera mainCamera;
    private Vector3 screenPoint;
    private Vector3 offset;
    private Vector3 previousLocation;
    private int chairLayer;

    // Start is called before the first frame update
    void Start()
    {
        mainCamera = Camera.main;
        rigidbody = GetComponent<Rigidbody>();
        restingPosition = 0f;
        chairLayer = LayerMask.GetMask(CHAIR_TAG);
        colorManager = FindObjectOfType<ColorManagement>();
    }

    /// <summary>
    /// OnMouseDown is called when the user has pressed the mouse button while
    /// over the GUIElement or Collider.
    /// </summary>
    void OnMouseDown()
    {   
        // Mouse control for movement disabled if paint mode is enabled
        if (colorManager.PaintModeEnabled()) { return; }

        screenPoint = Camera.main.WorldToScreenPoint(transform.position);
        offset = transform.position - Camera.main.ScreenToWorldPoint(
            new Vector3(Input.mousePosition.x, clickHeight + transform.position.y, screenPoint.z));
    }

    /// <summary>
    /// OnMouseDrag is called when the user has clicked on a GUIElement or Collider
    /// and is still holding down the mouse.
    /// </summary>
    void OnMouseDrag()
    {
        // Mouse control for movement disabled if paint mode is enabled
        if (colorManager.PaintModeEnabled()) { return; }
        
        float planeY = restingPosition;
        Transform draggingObject = transform;
     
        Plane plane = new Plane(Vector3.up, Vector3.up * planeY); // ground plane
     
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
     
        float distance; // the distance from the ray origin to the ray intersection of the plane
        if(plane.Raycast(ray, out distance))
        {
            rigidbody.useGravity = false;
            Vector3 clickPosition = ray.GetPoint(distance); // distance along the ray
            draggingObject.position = new Vector3(clickPosition.x, draggingObject.position.y, clickPosition.z);

            // Handle slopes by shooting ray from gameObject down
            RaycastHit hit;
            // Don't adjust height if raycast hit was against another chair (looks janky)
            if (Physics.Raycast(transform.position, Vector3.down, out hit, clickHeight + 30f, ~chairLayer)) {
                draggingObject.position += new Vector3(0, clickHeight - hit.distance, 0);
            }
        }

        previousLocation = draggingObject.position;
    }

    /// <summary>
    /// OnMouseUp is called when the user has released the mouse button.
    /// </summary>
    void OnMouseUp()
    {
        rigidbody.useGravity = true;
        restingPosition = transform.position.y;
    }
}
