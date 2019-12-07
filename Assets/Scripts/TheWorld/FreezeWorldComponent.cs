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
    public float lastTimeFreeze = float.MinValue;
    public float currentTime;
    public bool isTimeFrozen = false;
    public AudioSource levelMusic;

    void Start()
    {
        if (PlayerPrefs.HasKey("Time Freeze Limit"))
        {
            timeFreezeLimit = PlayerPrefs.GetFloat("Time Freeze Limit");
            timeFreezeCooldown = PlayerPrefs.GetFloat("Time Freeze Cooldown");
        }
        instance = this;
    }

    public void FreezeTime()
    {
        currentTime = Time.time;
        if (lastTimeFreeze + timeFreezeCooldown + timeFreezeLimit <= currentTime)
        {
            isTimeFrozen = true;
            lastTimeFreeze = currentTime;
            FreezeEvent.Invoke();
            levelMusic.pitch = 0.2f;
            StartCoroutine("UnfreezeTime");
        }
    }
    
    IEnumerator UnfreezeTime()
    {
        yield return new WaitForSeconds(timeFreezeLimit);
        isTimeFrozen = false;
        UnfreezeEvent.Invoke();
        levelMusic.pitch = 1f;
    }

    public void UpgradeFreeze()
    {
        timeFreezeLimit += timeFreezeLimitUpgrade;
    }

    public void UpgradeCooldown()
    {
        if (timeFreezeCooldown - timeFreezeCooldownUpgrade > 0)
        {
            timeFreezeCooldown -= timeFreezeCooldownUpgrade;
        }
    }

    public void ResetWorld()
    {
        FreezeEvent = null;
        UnfreezeEvent = null;
    }
}
