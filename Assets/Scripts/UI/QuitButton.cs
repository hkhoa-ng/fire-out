using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuitButton : MonoBehaviour
{
    public Sprite hoverSprite;
    private Sprite originalSprite;
    void Start()
    {
        originalSprite = GetComponent<SpriteRenderer>().sprite;
    }
    private void OnMouseOver() {
        GetComponent<SpriteRenderer>().sprite = hoverSprite;
        if (Input.GetMouseButtonDown(0)) {
            Application.Quit();
        }
    }
    
    private void OnMouseExit() {
        GetComponent<SpriteRenderer>().sprite = originalSprite;
    }
}
