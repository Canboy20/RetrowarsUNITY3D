using UnityEngine;
using System;


public class BlinkerEffect : MonoBehaviour
{
    public float _blinkSpeed = 1.0f;        // How fast blinking on and off

    public void Start()
    {
        InvokeRepeating("Blink", _blinkSpeed, _blinkSpeed);
    }

    public void Blink()
    {
        if (gameObject.activeInHierarchy) gameObject.GetComponent<Renderer>().enabled = !gameObject.GetComponent<Renderer>().enabled;       // Simply turns on and off renderer
    }
}
