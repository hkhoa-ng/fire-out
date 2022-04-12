using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDestructWhenPlayerDies : MonoBehaviour
{
    private void Awake() {
        GameEvents.onPlayerDieDestroyObjects += SelfDestruct;
    }
    private void OnDisable() {
        
        GameEvents.onPlayerDieDestroyObjects -= SelfDestruct;
    }
    private void SelfDestruct() {
        Destroy(gameObject);
    }
}
