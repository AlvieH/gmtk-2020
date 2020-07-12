using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Rotation controller doesn't allow rotation of other 
public class RotationController : MonoBehaviour
{
    [SerializeField] private GameObject target;

    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    void Start()
    {
        target = null;
    }

    // Set a new target for rotation
    public void SetTarget(GameObject newTarget) {
        target = newTarget;
    }

    // Get the current target of rotation
    public GameObject GetTarget() {
        return target;
    }

    // Nullifies target on mouse up after grab
    public void NullifyTarget() {
        target = null;
    }
}
