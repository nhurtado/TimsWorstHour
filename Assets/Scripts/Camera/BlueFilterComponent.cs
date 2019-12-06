using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueFilterComponent : MonoBehaviour
{
    void Start()
    {
        FreezeWorldComponent.FreezeEvent += Activate;
        FreezeWorldComponent.UnfreezeEvent += Deactivate;
    }

    void Activate()
    {
        transform.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 0.3f);
    }

    void Deactivate()
    {
        transform.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 0f);
    }
}
