using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Experimental.GlobalIllumination;
using UnityEngine.UI;

public class UIButtonGlow : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private Image image;
    public Color glowColor = Color.cyan;
    private Color originalColor;
    
    void Start()
    {
        image = GetComponent<Image>();
        originalColor = image.color;
        
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        image.color = glowColor;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        image.color = originalColor;
    }
}
