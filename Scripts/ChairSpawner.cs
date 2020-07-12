using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChairSpawner : MonoBehaviour
{   
    [SerializeField] GameObject chair;
    [SerializeField] GameObject chairParent;
    [SerializeField] float chairSpawnHeight = 2.5f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Spawns chair at origin and height chairSpawnHeight
    public void SpawnChairAtOrigin() {
        GameObject newChair = Instantiate( 
            chair,
            new Vector3(0, chairSpawnHeight, 0),
            Quaternion.identity) as GameObject;

        newChair.transform.parent = chairParent.transform;
    }
}
