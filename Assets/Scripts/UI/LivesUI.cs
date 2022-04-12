using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LivesUI : MonoBehaviour
{
    private Text playerLivesText;
    void Awake()
    {
        playerLivesText = GetComponent<Text>();
        GameEvents.onUpdatePlayerLives += UpdatePlayerLives;
    }
    private void OnDisable() {
        GameEvents.onUpdatePlayerLives -= UpdatePlayerLives;
    }

    private void UpdatePlayerLives(int currLives) {
        playerLivesText.text = currLives.ToString("D2");
    }
}
