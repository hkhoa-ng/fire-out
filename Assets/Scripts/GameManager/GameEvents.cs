using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;

public class GameEvents : MonoBehaviour
{
    /* ------------EVENTS FOR PLAYER GETTING HIT------------
    1. UPDATE GAME MANAGER THAT THE PLAYER JUST GOT HIT
    2. UPDATE THE PLAYER'S CURRENT HEALTH ON THE UI 
    3. DESTROY ALL ENEMIES ON SCREEN AT CURRENT TIME
    4. STOP THE ENEMY SPAWNER
    */
    public static event Action<onEventData> onPlayerHit;
    public static event Action<int> onUpdatePlayerLives;
    public static event Action onPlayerDieDestroyObjects;
    public static event Action onPlayerDieStopEnemySpawner;
    public static void UpdatePlayerLives(int currentLives) {
        onUpdatePlayerLives?.Invoke(currentLives);
    }
    public static void PlayerHitEvent(onEventData data) {
        onPlayerHit?.Invoke(data);
    }
    public static void PlayerDieDestroyObjects() {
        onPlayerDieDestroyObjects?.Invoke();
    }
    public static void StopEnemySpawner() {
        onPlayerDieStopEnemySpawner?.Invoke();
    }
    public static void PlayerDieGetSpawner(onEventData spawner) {
        onPlayerHit?.Invoke(spawner);
    }
    /* ------------EVENTS FOR PLAYER RESPAWN------------
    1. REENABLE ENEMY SPAWNER */
    // public static event Action onPlayerRespawnEnableEnemySpawner;
    // public static void EnableEnemySpawner() {
    //     onPlayerRespawnEnableEnemySpawner?.Invoke();
    // }

    /* ------------SCREEN SHAKE EVENTS-----------*/
    public static event Action onScreenshake;

    public static void Screenshake() {
        onScreenshake?.Invoke();
    }
    /* ------------CURSES EVENTS------------ */
    public static event Action<float> onMassiveScreenshake;
    public static event Action onStartSpawnFloorSmoke;
    public static event Action onStartSpawnViewSmoke;
    public static event Action onStartSpawnDebris;
    public static void MassiveScreenshake(float strengthModifier) {
        onMassiveScreenshake?.Invoke(strengthModifier);
    }
    public static void StartSpawnFloorSmoke() {
        onStartSpawnFloorSmoke?.Invoke();
    }
    public static void StartSpawnViewSmoke() {
        onStartSpawnViewSmoke?.Invoke();
    }
    public static void StartSpawnDebris() {
        onStartSpawnDebris?.Invoke();
    }
    /* ---------CHANGE COLOR EVENTs--------- */
    public static event Action<onEventData> onChangeColor;
    public static void ChangeColor(onEventData senderGameObject) {
        onChangeColor?.Invoke(senderGameObject);
    }
    public static event Action onChangeAllColor;
    public static void ChangeAllColor() {
        onChangeAllColor?.Invoke();
    }

    public static event Action<float, float, float> onParticleSpawn;
    public static void ParticleStartColor(float red, float green, float blue) {
        onParticleSpawn?.Invoke(red, green, blue);
    }

    /* --------ALL THE STUFFS THAT REQUIRE A SCREEN TRANSITION-------- */
    public static event Action onStartScreenTransition;
    public static void StartScreenTransition() {
        onStartScreenTransition?.Invoke();
    }
    public static event Action onEndScreenTransition;
    public static void EndScreenTransition() {
        onEndScreenTransition?.Invoke();
    }
    /* --------ON GAME START REFERENCING THE PLAYER-------- */
    public static event Action<onEventData> onGameStartGetPlayer;
    public static void GameStartGetPlayer(onEventData data) {
        onGameStartGetPlayer?.Invoke(data);
    }
    /* -------- EVENTS FOR FINISH A STAGE -------- */
    public static event Action<onEventData> onGameStartGetUI;
    public static void GameStartGetUI(onEventData data) {
        onGameStartGetUI?.Invoke(data);
    }
    // public static event Action<onEventData> onChooseCardGetCardUI;
    // public static void ChooseCardGetCardUI(onEventData data) {
    //     onChooseCardGetCardUI?.Invoke(data);
    // }
    public static event Action onResumeGameAfterCards;
    public static void ResumeGameAfterCards() {
        onResumeGameAfterCards?.Invoke();
    }
    public static event Action<int> onSelectACardGetCardID;
    public static void SelectACardGetCardID(int id) {
        onSelectACardGetCardID?.Invoke(id);
    }
    /* -------- EVENTS FOR ENEMY DIES -------- */
    public static event Action onEnemyDie;
    public static void EnemyDie() 
    {
        onEnemyDie?.Invoke();
    }
    // Boss dies
    public static event Action onBossDie;
    public static void GameWin() {
        onBossDie?.Invoke();
    }

}

// -----NEW DATA TYPE FOR PASS IN DATA OF THE OBJECT SENDING THE EVENT-----
public class onEventData {
    public GameObject sender;
    // Constructor
    public onEventData(GameObject sender) {
        this.sender = sender;
    }
}

