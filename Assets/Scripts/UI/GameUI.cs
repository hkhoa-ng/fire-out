using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameUI : MonoBehaviour
{
    private void Start() {
        GameEvents.GameStartGetUI(new onEventData(gameObject));
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
