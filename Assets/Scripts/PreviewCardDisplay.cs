using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreviewCardDisplay : MonoBehaviour
{
    public Canvas canvas;
    public Canvas preview_canvas;

    public Card card_to_preview;

    public CombatManager manager;

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Display()
    {
        card_to_preview.transform.SetParent(preview_canvas.transform);
        card_to_preview.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
        card_to_preview.GetComponent<RectTransform>().localScale = Vector3.one;
        canvas.enabled = true;
    }

    public void Hide()
    {
        if (card_to_preview != null)
        {
            Destroy(card_to_preview.gameObject);
        }
        canvas.enabled = false;
    }

}
