using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraEffect : MonoBehaviour
{
    private Coroutine shakeCoroutine;

    public IEnumerator Shake(float ShakeIntensity,float ShakeDuration)
    {
        Vector3 originalPos = transform.localPosition;
        float elapsed = 0.0f;
        while (elapsed<ShakeDuration)
        {
            float x = Random.Range(-1f, 1f) * ShakeIntensity;
            float y = Random.Range(-1f, 1f) * ShakeIntensity;
            
            transform.localPosition = new Vector3(x,y,originalPos.z);

            elapsed += Time.deltaTime;
            
            yield return null;
        }

        transform.localPosition = originalPos;

    }

    public void StartShaking(float duration = 0.4f,float intensity=0.15f)
    {
        shakeCoroutine = StartCoroutine(Shake(intensity, duration));
    }

    public void StopShaking()
    {
        StopCoroutine(shakeCoroutine);
    }
}
