using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class PaintPalette : MonoBehaviour
{
    public Color[] Colors;
    public Image BrushTip;
    public Image BackgroundBorder;
    public GameObject Circles;
    [Min(0)]
    public int SelectedColorIndex;
    public Texture2D NormalCursorTexture;
    public Texture2D PaintCursorTexture;
    public Color SelectedColor => Colors[SelectedColorIndex];

    public bool IsActive
    {
        get => WorldManager.instance.InteractionMode == InteractionMode.Paint;
        private set
        {
            WorldManager.instance.InteractionMode = value ? InteractionMode.Paint : InteractionMode.Normal;
            UpdateCursor();
        }
    }
    Image[] CircleImages;

    void Start()
    {
        CircleImages = Circles.GetComponentsInChildren<Image>().ToArray();
        for (int i = 0; i < CircleImages.Length; i++)
        {
            var index = i;
            var CircleImage = CircleImages[i];
            CircleImage.GetComponent<Button>().onClick.AddListener(() => OnCircleClicked(index));
        }
        UpdateCursor();
    }

    void UpdateCursor()
    {
        Debug.Log("SetCursor");
        if (IsActive)
        {
            Texture2D ColorizedPaintCursorTexture = new Texture2D(PaintCursorTexture.width, PaintCursorTexture.height, PaintCursorTexture.format, false);
            Graphics.CopyTexture(PaintCursorTexture, ColorizedPaintCursorTexture);
            ColorizeTexture(ColorizedPaintCursorTexture, Colors[SelectedColorIndex]);
            Cursor.SetCursor(ColorizedPaintCursorTexture, Vector2.zero, CursorMode.ForceSoftware);
        }
        else 
        {
            Cursor.SetCursor(NormalCursorTexture, Vector2.zero, CursorMode.ForceSoftware);
        }
    }

    static void ColorizeTexture(Texture2D texture, Color color)
    {
        var fillColorArray = texture.GetPixels();

        for (var i = 0; i < fillColorArray.Length; ++i)
        {
            if (fillColorArray[i] == Color.white)
            {
                fillColorArray[i] = color;
            }
        }

        texture.SetPixels(fillColorArray);
        texture.Apply();
    }

    public void SetInactive()
    {
        IsActive = false;
    }

    void OnCircleClicked(int index)
    {
        SelectedColorIndex = index;
        IsActive = true;
    }

    void Update()
    {
        BackgroundBorder.gameObject.SetActive(IsActive);
        BrushTip.gameObject.SetActive(IsActive);
        if (SelectedColorIndex >= Colors.Length) return;
        var SelectedColor = Colors[SelectedColorIndex];
        BrushTip.color = SelectedColor;
        BackgroundBorder.color = SelectedColor;

        for (int i = 0; i < Colors.Length; i++)
        {
            CircleImages[i].color = Colors[i];
        }

    }
}
