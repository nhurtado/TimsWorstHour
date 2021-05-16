using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MuerteSonidos : MonoBehaviour
{
    public float tiempoMuerte;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject,tiempoMuerte);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
