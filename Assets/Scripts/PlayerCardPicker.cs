using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCardPicker : MonoBehaviour
{
    public PlayerController player;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);

            RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);
            if (hit.collider != null)
            {
                Debug.Log(hit.collider.gameObject.name);

                if (hit.collider.gameObject.GetComponent<Card>() != null)
                {
                    player.PickedCard(hit.collider.gameObject);
                }
            }
        }

    }
}
