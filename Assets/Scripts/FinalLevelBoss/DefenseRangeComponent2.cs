using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefenseRangeComponent2 : MonoBehaviour
{
    WizardMasterScript2 wizardMasterScript;

    void Start()
    {
        wizardMasterScript = transform.parent.GetComponent<WizardMasterScript2>();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            wizardMasterScript.playerEnteredDefenseRange = true;
        }
        else if (collision.gameObject.tag == "FireBall")
        {
            wizardMasterScript.healthPoints -= 1;
            wizardMasterScript.playerEnteredDefenseRange = true;
        }
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            wizardMasterScript.playerEnteredDefenseRange = true;
        }
        else if (collision.gameObject.tag == "FireBall")
        {
            wizardMasterScript.healthPoints -= 1;
            wizardMasterScript.playerEnteredDefenseRange = true;
        }
    }
}
