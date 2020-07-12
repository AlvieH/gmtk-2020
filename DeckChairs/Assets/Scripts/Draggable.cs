using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Draggable : MonoBehaviour
{
    const string CHAIR_TAG = "Chair";

    [SerializeField] float clickHeight;
    [SerializeField] float clickLerpTime;
    [SerializeField] Vector3 centerOfMass;


    private Rigidbody rigidbody;
    private ColorManagement colorManager;
    private float restingPosition;
    private Camera mainCamera;
    private Vector3 screenPoint;
    private Vector3 offset;
    private Vector3 previousLocation;
    private int chairLayer;
    Vector3 distance;

    private bool hasBeenMoved = false;

    // Start is called before the first frame update
    void Start()
    {
        mainCamera = Camera.main;
        rigidbody = GetComponent<Rigidbody>();
        restingPosition = 0f;
        chairLayer = LayerMask.GetMask(CHAIR_TAG);
        colorManager = FindObjectOfType<ColorManagement>();
        rigidbody.centerOfMass = centerOfMass;
    }

    void OnMouseDown()
    {
        if (colorManager.PaintModeEnabled()) return;

        if (!hasBeenMoved)
        {
            AchievementManager.instance.ChairsMovedCount += 1;
            hasBeenMoved = true;
        }

        GameManager.instance.IsPickingObject = true;

        distance = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.WorldToScreenPoint(transform.position).z)) - transform.position;
        WorldManager.instance.OnChairPickedUp();
    }

    void OnMouseDrag()
    {
        if (colorManager.PaintModeEnabled()) return;

        Vector3 distance_to_screen = Camera.main.WorldToScreenPoint(transform.position);
        Vector3 pos_move = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, distance_to_screen.z));
        transform.position = new Vector3(pos_move.x - distance.x, transform.position.y, pos_move.z - distance.z);
    }

    /// <summary>
    /// OnMouseUp is called when the user has released the mouse button.
    /// </summary>
    void OnMouseUp()
    {
        if (colorManager.PaintModeEnabled()) return;

        GameManager.instance.IsPickingObject = false;

        rigidbody.useGravity = true;
    }
}
