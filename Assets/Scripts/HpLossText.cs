using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HpLossText : MonoBehaviour
{
    public float duration = 2f;

    private float _lastTime = 0f;

    // Update is called once per frame
    void Update()
    {
        _lastTime += Time.deltaTime;
        if (_lastTime >= duration)
        {
            Destroy(gameObject);
        }
    }
}
