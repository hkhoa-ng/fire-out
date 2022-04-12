using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Tilemaps;


public class ChangeColor : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private Text textRenderer;
    private Image imageRenderer;
    private Tilemap tilemapRenderer;
    private ParticleSystem particleRenderer;
    private void Awake() {
        GameEvents.onChangeAllColor += ChangeAllColor;
        GameEvents.ChangeColor(new onEventData(gameObject));
    }
    private void OnDisable() {
        GameEvents.onChangeAllColor -= ChangeAllColor;
    }
    private void Start() {
        GameEvents.ChangeColor(new onEventData(gameObject));
    }
    private void ChangeAllColor() {
        GameEvents.ChangeColor(new onEventData(gameObject));
    }
    private void FixedUpdate() {
        GameEvents.ChangeColor(new onEventData(gameObject));
    }

    
}
