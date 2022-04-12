using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    public Animator transition;
    public float transitionTime = 1f;
    private void Awake() {
        GameEvents.onStartScreenTransition += LoadStartAnimation;
        GameEvents.onEndScreenTransition += LoadEndAnimation;
    }
    private void OnDisable() {
        GameEvents.onStartScreenTransition -= LoadStartAnimation;
        GameEvents.onEndScreenTransition -= LoadEndAnimation;
    }
    public void LoadStartAnimation() {

        // StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex + 1));
        transition.SetTrigger("Start");

    }
    public void LoadEndAnimation() {
        transition.SetTrigger("End");
    }

    // IEnumerator LoadLevel(int levelIndex) {
    //     // Player animation
    //     transition.SetTrigger("Start");

    //     // Wait for animation to stop playing
    //     yield return new WaitForSeconds(transitionTime);

    //     // Load the new scene
    //     SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    // }
}
