using UnityEngine;

public class HighlightObject : MonoBehaviour
{
    [SerializeField] private Color normalColor;
    [SerializeField] private Color highlightColor;
    private Renderer objRenderer;
    private bool isHighlighted;
    public bool IsHighlighted => isHighlighted;

    private void Awake()
    {
        objRenderer = GetComponent<Renderer>();
    }

    public void SetHighlight(bool value)
    {
        isHighlighted = value;
        SetColor();
    }

    private void SetColor()
    {
        objRenderer.material.color = isHighlighted ? highlightColor : normalColor;
    }
}
