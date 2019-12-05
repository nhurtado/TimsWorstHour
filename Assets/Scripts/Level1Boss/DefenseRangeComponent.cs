using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefenseRangeComponent : MonoBehaviour
{
    WizardMasterScript wizardMasterScript;

    void Start()
    {
        wizardMasterScript = transform.parent.GetComponent<WizardMasterScript>();
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" || collision.gameObject.tag == "IceBall")
        {
            wizardMasterScript.playerEnteredDefenseRange = true;
        }
    }
}
