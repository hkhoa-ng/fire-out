using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoverButton : MonoBehaviour
{
    public Sprite hoverSprite;
    private Sprite originalSprite;
    // Start is called before the first frame update
    void Start()
    {
        originalSprite = GetComponent<SpriteRenderer>().sprite;
    }

    // Update is called once per frame
    private void OnMouseOver() {
        GetComponent<SpriteRenderer>().sprite = hoverSprite;
    }
    private void OnMouseExit() {
        GetComponent<SpriteRenderer>().sprite = originalSprite;
    }
}
