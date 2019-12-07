using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EraChangeWorldComponent : MonoBehaviour
{
    public static event UnityAction EraChangeEvent;
    public static EraChangeWorldComponent instance;
    public float eraChangeCooldown = 0.35f;
    float currentTime;
    float lastEraChange = float.MinValue;

    void Start()
    {
        instance = this;
    }

    public void TriggerEraChange()
    {
        currentTime = Time.time;
        if (lastEraChange + eraChangeCooldown <= currentTime)
        {
            EraChangeEvent.Invoke();
            lastEraChange = currentTime;
        }
    }

    public void ResetWorld()
    {
        EraChangeEvent = null;
    }
}
