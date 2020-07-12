using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paintable : MonoBehaviour
{
    [SerializeField] GameObject seatCloth;

    private ColorManagement colorManager;
    private Renderer renderer;
    const string colorName = "_Color";

    public bool HasBeenPainted = false;

    public Color ChairColor
    {
        get => renderer.material.GetColor(colorName);
    }

    // Start is called before the first frame update
    void Awake()
    {
        colorManager = FindObjectOfType<ColorManagement>();
        renderer = seatCloth.GetComponent<Renderer>();
    }

    // Paint object when clicked on in edit mode
    void OnMouseDown()
    {
        if (colorManager.PaintModeEnabled())
        {
            var currentColor = renderer.material.GetColor(colorName);
            var selectedColor = colorManager.GetPaintColor();
            if (!HasBeenPainted)
            {
                AchievementManager.instance.ChairsPaintedCount += 1;
            }
            HasBeenPainted = true;
            if (currentColor != selectedColor)
            {
                renderer.material.SetColor(colorName, selectedColor);
                WorldManager.instance.OnChairPainted();
            }
        }
    }
}
