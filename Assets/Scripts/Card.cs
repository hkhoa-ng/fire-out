using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Card : MonoBehaviour
{
    public string cardEffect;
    public int id;
    private Text cardEffectDisplay;
    private CurseSelector curseSelector;
    private GameObject cursor;
    private GameObject gameManager;
    private void Awake() {
        cardEffectDisplay = GameObject.Find("Card Effects").GetComponent<Text>();
        curseSelector = GameObject.Find("CurseSelector").GetComponent<CurseSelector>();
        cursor = GameObject.Find("Cursor");
        gameManager = GameObject.Find("GameManager");
    }
    private void OnMouseOver() {
        cardEffectDisplay.text = cardEffect;

        if (Input.GetMouseButtonDown(0)) {
            GameEvents.SelectACardGetCardID(id);
            if (gameObject.name == "Flipped Card(Clone)") {
                // Debug.Log("Inverted!");
                curseSelector.invertedColor = true;
                // GameEvents.ChangeAllColor();
            }
            if (gameObject.name == "Meteor Card(Clone)") {
                ObstacleSpawner obstacleSpawner = GameObject.Find("Obstacle Spawner").GetComponent<ObstacleSpawner>();
                obstacleSpawner.spawnDebris = true;
            }
            if (gameObject.name == "Quake Card(Clone)") {
                curseSelector.earthQuake = true;
                gameManager.GetComponent<ScreenShake>().StartEarthQuake();
            }
            if (gameObject.name == "Rain Card(Clone)") {
                curseSelector.raining = true;
            }
            if (gameObject.name == "Soot Card(Clone)") {
                ObstacleSpawner obstacleSpawner = GameObject.Find("Obstacle Spawner").GetComponent<ObstacleSpawner>();
                obstacleSpawner.spawnViewSmoke = true;
            }
            if (gameObject.name == "Smoke Card(Clone)") {
                ObstacleSpawner obstacleSpawner = GameObject.Find("Obstacle Spawner").GetComponent<ObstacleSpawner>();
                obstacleSpawner.spawnFloorSmoke = true;
            }
            if (gameObject.name == "Colors Card(Clone)") {
                curseSelector.randomColor = true;
            }
            if (gameObject.name == "Blinded Card(Clone)") {
                curseSelector.blinded = true;
                cursor.SetActive(false);
            }
            if (gameObject.name == "Ghosted Card(Clone)") {
                gameManager.GetComponent<GameManager>().playerIsGhosted = true;
                curseSelector.ghosted = true;
            }
            if (gameObject.name == "Shakes Card(Clone)") {
                curseSelector.massiveScreenShake = true;
                GameEvents.MassiveScreenshake(3f);
            }
            if (gameObject.name == "Frozen Card(Clone)") {
                gameManager.GetComponent<GameManager>().timeStopOccasionally = true;
            }
            if (gameObject.name == "Giant Card(Clone)") {
                GameObject player = GameObject.Find("Player");
                player.transform.localScale = new Vector3(1.3f, 1.3f, 1.3f);
                // player.GetComponent<Player>().moveSpeed = (player.GetComponent<Player>().moveSpeed)/1.3f;
            }
            if (gameObject.name == "Rush Card(Clone)") {
                gameManager.GetComponent<GameManager>().defaultGameSpeed = 1.2f;
            }
            if (gameObject.name == "Critical Card(Clone)") {
                gameManager.GetComponent<GameManager>().criticalEnergy = true;
            }

            GameEvents.ResumeGameAfterCards();
            Invoke(nameof(SelfDestruct), 1f);
        }
    }
    private void SelfDestruct() {
        Destroy(gameObject);
    }
}
