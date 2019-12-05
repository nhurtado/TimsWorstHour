using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossCameraComponent : MonoBehaviour
{
    public GameObject BlueFilter;
    
    void Start()
    {
        BlueFilter.transform.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 0f);
        BlueFilter.SetActive(true);
    }
}
