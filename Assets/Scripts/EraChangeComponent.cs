using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EraChangeComponent : MonoBehaviour
{
    public bool present = true;

    void Start()
    {
        UpdateSetActive();
        EraChangeWorldComponent.EraChangeEvent += ChangeEra;
    }

    void ChangeEra()
    {
        UpdateSetActive();
    }

    void UpdateSetActive()
    {
        gameObject.SetActive(present);
        present = !present;
    }
}
