using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ActiveCardDisplay : MonoBehaviour
{

    public Canvas canvas;
    public TextMeshProUGUI text;

    private ActiveCard controller;


    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<ActiveCard>();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateTexts();
    }

    public void Display()
    {
        canvas.enabled = true;
    }

    public void Hide()
    {
        canvas.enabled = false;
    }

    private void UpdateTexts()
    {
        text.text = "Invocation needs " + 
            controller.RemainingCost().ToString() + 
            "HP. Click here to pay with your life.";
        // using controller to have the card data;
    }
}
