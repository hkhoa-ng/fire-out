using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenShake : MonoBehaviour
{
    public Camera mainCam;
    public float strengthModifier = 1f;
    public float shakeDuration = 0.5f;
    public AnimationCurve animCurve;
    public bool start = false;
    private Vector3 startPos;
    private void Awake() {
        GameEvents.onScreenshake += StartShake;
    }
    private void OnDisable() {
        GameEvents.onScreenshake -= StartShake;
    }
    private void StartShake() {
        start = true;
    }
    public void StartEarthQuake() {
        InvokeRepeating(nameof(StartShake), 0, shakeDuration);
    }
    void Update() {
        if (start) {
            start = false;
            StartCoroutine(Shaking());
        }
    }

    IEnumerator Shaking() {
        Vector3 startPos = mainCam.transform.position;
        float elapseTime = 0f;

        while (elapseTime < shakeDuration) {
            elapseTime += Time.deltaTime;
            float strength = animCurve.Evaluate(elapseTime / shakeDuration);
            mainCam.transform.position = startPos + (Random.insideUnitSphere * strength * strengthModifier);
            yield return null;
        }
        mainCam.transform.position = startPos;
    }
}
