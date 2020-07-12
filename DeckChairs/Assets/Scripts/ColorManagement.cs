using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorManagement : MonoBehaviour
{
    //[SerializeField] private Color currentColor;

    Color currentColor => GetComponent<PaintPalette>().SelectedColor;

    private bool paintMode;

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
        return GetComponent<PaintPalette>().IsActive;
    }
}
