using UnityEngine;

public class Highlightable : MonoBehaviour
{
    public Color highlightColor = Color.yellow;
    private Color originalColor;
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        if (spriteRenderer != null)
            originalColor = spriteRenderer.color;
    }

    public void Highlight(bool highlight)
    {
        if (spriteRenderer != null)
            spriteRenderer.color = highlight ? highlightColor : originalColor;
    }

    void OnDisable()
    {
        spriteRenderer.color = originalColor;
    }
}
