using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Tilemaps;

public class CurseSelector : MonoBehaviour
{
    public bool raining = false;
    public ParticleSystem rain;
    public bool floorSmoke = false;
    public ParticleSystem smallSmoke;
    public bool smokeView = false;
    public ParticleSystem bigSmoke;
    public bool fallingDebris = false;
    public GameObject debris;
    public bool noCursor = false;
    public bool twoLivesPerDeath = false;
    public bool shakyWater = false;
    public bool noLight = false;
    public bool useMoreWater = false;
    public bool invertedColor = false;
    public bool randomColor = false;
    [SerializeField] Color lightColor = Color.white;
    [SerializeField] Color darkColor = Color.black;
    public bool earthQuake = false;
    public bool massiveScreenShake = false;
    public bool blinded = false;
    public bool ghosted = false;
    private void Awake() {
        if (randomColor) ChooseRandomColor();
        GameEvents.onChangeColor += ChangeRenderedColor;
        GameEvents.onChangeAllColor += ChooseRandomColor;
    }
    private void OnDisable() {
        GameEvents.onChangeColor -= ChangeRenderedColor;
        GameEvents.onChangeAllColor -= ChooseRandomColor;
    }
    public void ChooseRandomColor() {
        // Debug.Log("Start choosing random color...");
        if (randomColor) {
            // Debug.Log("Chose random colors!");
            lightColor = Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f);
            darkColor = Random.ColorHSV(0f, 1f, 1f, 1f, 0f, 0.5f);
        }
    }
    private void ChangeRenderedColor(onEventData senderGameObject) {
        Color colorToChange = new Color(0.753916f, 0.8113208f, 0.753916f, 1);
        Color backgroundToChange = new Color(0.09723212f, 0.1445429f, 0.1792453f, 1);

        SpriteRenderer spriteRenderer = senderGameObject.sender.GetComponent<SpriteRenderer>();
        Text textRenderer = senderGameObject.sender.GetComponent<Text>();
        Image imageRenderer = senderGameObject.sender.GetComponent<Image>();
        Tilemap tilemapRenderer = senderGameObject.sender.GetComponent<Tilemap>();
        ParticleSystem particleRenderer = senderGameObject.sender.GetComponent<ParticleSystem>();
        Camera cam = senderGameObject.sender.GetComponent<Camera>();

        if (randomColor) {
            colorToChange = new Color(lightColor.r, lightColor.g, lightColor.b, 1);
            backgroundToChange = darkColor;
        }
        if (invertedColor) {
            colorToChange = new Color(darkColor.r, darkColor.g, darkColor.b, 1);
            backgroundToChange = lightColor;
        }

        if (spriteRenderer) spriteRenderer.color = colorToChange;
        if (textRenderer) textRenderer.color = colorToChange;
        if (imageRenderer) {
            imageRenderer.color = colorToChange;
            if (imageRenderer.sprite.name == "Transition Diamond") {
                imageRenderer.color = new Color(backgroundToChange.r, backgroundToChange.g, backgroundToChange.b, 1);
                // imageRenderer.color = Color.white;
            }
            // imageRenderer.color = backgroundToChange;
        } 
        if (tilemapRenderer) tilemapRenderer.color = colorToChange;
        if (particleRenderer) {
            var main = particleRenderer.main;
            main.startColor = colorToChange;
        }
        if (cam) cam.backgroundColor = backgroundToChange;
    }

    void Start()
    {
        
    }
    
    void FixedUpdate()
    {
        if (earthQuake) {
            // GameEvents.Screenshake();
        }
        if (massiveScreenShake) {
            // earthQuake = false;
            // GameEvents.MassiveScreenshake(4f);
            // earthQuake = true;
        } else {
            GameEvents.MassiveScreenshake(1f);
        }
        if (raining) {
            raining = false;
            Instantiate(rain, new Vector3(-1, 7.5f, 0), Quaternion.identity);
        }
        if (floorSmoke) {
            floorSmoke = false;
            GameEvents.StartSpawnFloorSmoke();
        }
        if (fallingDebris) {
            fallingDebris = false;
            GameEvents.StartSpawnDebris();
        }
        if (smokeView) {
            smokeView = false;
            GameEvents.StartSpawnViewSmoke();
        }
    }
}
