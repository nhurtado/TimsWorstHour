using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class FreezeWorldComponent : MonoBehaviour
{
    public static event UnityAction FreezeEvent;
    public static event UnityAction UnfreezeEvent;
    public static FreezeWorldComponent instance;
    public float timeFreezeLimit = 3f;
    public float timeFreezeCooldown = 2f;
    public float timeFreezeLimitUpgrade = 0.5f;
    public float timeFreezeCooldownUpgrade = 0.2f;
    float lastTimeFreeze = float.MinValue;
    float currentTime;

    void Start()
    {
        instance = this;
    }

    public void FreezeTime()
    {
        currentTime = Time.time;
        if (lastTimeFreeze + timeFreezeCooldown + timeFreezeLimit <= currentTime)
        {
            lastTimeFreeze = currentTime;
            FreezeEvent.Invoke();
            StartCoroutine("UnfreezeTime");
        }
    }
    
    IEnumerator UnfreezeTime()
    {
        yield return new WaitForSeconds(timeFreezeLimit);
        UnfreezeEvent.Invoke();
    }

    public void UpgradeFreeze()
    {
        timeFreezeLimit += timeFreezeLimitUpgrade;
    }

    public void UpgradeCooldown()
    {
        timeFreezeCooldown -= timeFreezeCooldownUpgrade;
    }
}
