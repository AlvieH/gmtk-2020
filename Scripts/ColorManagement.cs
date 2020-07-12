using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorManagement : MonoBehaviour
{
    [SerializeField] private Color currentColor;
    private bool paintMode;

    public void SetPaintColor(float red, float green, float blue) {
        currentColor = new Color(red, green, blue, 1.0f);
    }

    // Paint mode disables chair dragging and changes the cursor to a paintbrush
    public void TogglePaintMode() {
        paintMode = paintMode ? false : true;
    }

    // Returns current value of paint color
    public Color GetPaintColor() {
        return currentColor;
    }

    // Returns whether or not paint mode is currently enabled
    public bool PaintModeEnabled() {
        return paintMode;
    }

    // Sets paint color to red
    public void PaintRed() {
        currentColor = Color.red;
    }

    // Sets paint color to blue
    public void PaintBlue() {
        currentColor = Color.blue;
    }
}
