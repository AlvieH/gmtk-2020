using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paintable : MonoBehaviour
{   
    [SerializeField] GameObject seatCloth;

    private ColorManagement colorManager;
    private Renderer renderer;
    
    // Start is called before the first frame update
    void Start()
    {
        colorManager = FindObjectOfType<ColorManagement>();
        renderer = seatCloth.GetComponent<Renderer>();
    }

    // Paint object when clicked on in edit mode
    void OnMouseDown()
    {
        if (colorManager.PaintModeEnabled()) { 
            renderer.material.SetColor("_Color", colorManager.GetPaintColor());
        }
    }
}
