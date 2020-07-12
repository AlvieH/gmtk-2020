using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroupingBonus : MonoBehaviour
{
    const string CHAIR_COLLIDER_TAG = "Chair";

    [SerializeField] private List<GameObject> symmetryTargets;
    private int chairsInGroup;
    private bool symmetry;

    // Start is called before the first frame update
    void Start()
    {
        chairsInGroup = 0;
        symmetry = false;
        symmetryTargets = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
       if (symmetryTargets.Count > 0 && !symmetry) {
           CheckTargetsForSymmetry();
       }
    }

    /// <summary>
    /// OnTriggerEnter is called when the Collider other enters the trigger.\
    /// Increment chairsInGroup when a new chair enters the group
    /// </summary>
    /// <param name="other">The other Collider involved in this collision.</param>
    void OnTriggerEnter(Collider other)
    {   
        
        if (other.gameObject.CompareTag(CHAIR_COLLIDER_TAG)) { 
            Debug.Log("Enter");
            ++chairsInGroup;
            symmetryTargets.Add(other.gameObject);
        } 
    }

    /// <summary>
    /// OnTriggerExit is called when the Collider other has stopped touching the trigger.
    /// </summary>
    /// <param name="other">The other Collider involved in this collision.</param>
    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag(CHAIR_COLLIDER_TAG)) {
            Debug.Log("Exit");
            --chairsInGroup;
            symmetryTargets.Remove(other.gameObject);
        }
    }

    // Checks targets in symmetry List for symmetry
    private void CheckTargetsForSymmetry() {
        foreach (GameObject target in symmetryTargets) {
            // Perform check for symmetry
            float dot = Vector3.Dot(target.transform.right, 
                (transform.position - target.transform.position).normalized);
        
            Debug.Log(dot);
        
            if (dot < -0.8f) {
                symmetry = true;
                Debug.Log("Symmetry!");
                return;
            }
            else {
                symmetry = false;
            }
        }
    }
}
