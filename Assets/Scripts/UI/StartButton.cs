using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartButton : MonoBehaviour
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
            GameEvents.StartScreenTransition();
            Invoke(nameof(LoadMainGame), 1.5f);
        }
    }
    private void LoadMainGame() {
        SceneManager.LoadScene("Main Game");
    }
    private void OnMouseExit() {
        GetComponent<SpriteRenderer>().sprite = originalSprite;
    }
}
