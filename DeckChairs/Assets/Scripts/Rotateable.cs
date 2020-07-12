using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotateable : MonoBehaviour
{
    [SerializeField] float rotateStep = 10f;

    private Quaternion previousRotation;
    private ColorManagement colorManager;
    private bool rotating;
    private bool dragging;
    private Rigidbody rigidbody;
    private RotationController rotationController;

    bool didAnnounceRotation = false;

    // Start is called before the first frame update
    void Start()
    {
        transform.rotation = Quaternion.Euler(-90, 0, 0);
        previousRotation = transform.rotation; 
        rotating = false;
        dragging = false;
        colorManager = FindObjectOfType<ColorManagement>();
        rotationController = FindObjectOfType<RotationController>();
        Physics.queriesHitTriggers = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// OnMouseDown is called when the user has pressed the mouse button while
    /// over the GUIElement or Collider.
    /// </summary>
    void OnMouseDown()
    {
        // Maintain chair y rotation when picking up
        if (!colorManager.PaintModeEnabled()) {
            transform.rotation = Quaternion.Euler(-90, previousRotation.eulerAngles.y, 0);
        }
    }

    /// <summary>
    /// Called every frame while the mouse is over the GUIElement or Collider.
    /// </summary>
    void OnMouseOver()
    {  
        // Disqualify illegitimate rotations 
        if (!GameObject.ReferenceEquals(rotationController.GetTarget(), null) &&
            !GameObject.ReferenceEquals(rotationController.GetTarget(), gameObject) ) { return; }

        var scroll = Input.GetAxis("Mouse ScrollWheel");

        // User scrolling up
        if (scroll > 0 && !rotating) {
            rotating = true;
            transform.Rotate(0, rotateStep, 0, Space.World);
            Debug.Log("Rotated clockwise! Current rotation:" + transform.rotation);
            previousRotation = transform.rotation;
        }
        // User scrolling down
        else if (scroll < 0 && !rotating) {
            rotating = true;
            transform.Rotate(0, -rotateStep, 0, Space.World);
            Debug.Log("Rotated counterclockwise! Current rotation:" + transform.rotation);
            previousRotation = transform.rotation;
        }
        else if (scroll == 0) {
            rotating = false;
        }

        if (rotating && !didAnnounceRotation)
        {
            AchievementManager.instance.ChairsRotatedCount += 1;
            didAnnounceRotation = true;
        }
    }

    // Maintain rotation while being dragged
    void OnMouseDrag()
    {
        rotationController.SetTarget(gameObject);
        transform.rotation = previousRotation;
        dragging = true;
    }

    // Reset RotationController
    private void OnMouseUp() {
        if (dragging) {
            rotationController.NullifyTarget();
            dragging = false;
        }
    }
}
