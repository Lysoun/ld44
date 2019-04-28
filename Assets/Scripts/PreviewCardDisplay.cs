using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreviewCardDisplay : MonoBehaviour
{
    public Canvas canvas;

    public CombatManager manager;

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Display()
    {
        canvas.enabled = true;
    }

    public void Hide()
    {
        canvas.enabled = false;
    }

}
