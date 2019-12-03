using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlinkComponent : MonoBehaviour
{
    public bool isShowing = true;
    public GameObject pressEnterText;

    void Start()
    {
        StartCoroutine("ChangeState");
    }

    IEnumerator ChangeState()
    {
        while (true)
        {
            pressEnterText.SetActive(isShowing);
            yield return new WaitForSeconds(1f);
            isShowing = !isShowing;
        }
    }
}
