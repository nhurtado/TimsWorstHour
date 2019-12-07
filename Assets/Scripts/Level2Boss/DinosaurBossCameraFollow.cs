using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DinosaurBossCameraFollow : MonoBehaviour
{
    public GameObject dinosaur;
    public GameObject BlueFilter;

    void Start()
    {
        BlueFilter.transform.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 0f);
        BlueFilter.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (dinosaur)
        {
            transform.position = dinosaur.transform.position + new Vector3(15f, 2.5f, -10);
        }
    }


}
