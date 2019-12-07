using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StateComponent : MonoBehaviour
{
    public bool isGrounded = false;
    public bool canFly = false;
    public int shields = 0;

    int orbs;
    int keys;

    GameObject shieldCounter;
    GameObject orbCounter;
    GameObject keyCounter;

    public Vector2 iniPosition;

    public bool cantDie = false;

    void Start()
    {
        shieldCounter = GameObject.Find("ShieldCounter");
        orbCounter = GameObject.Find("OrbCounter");
        keyCounter = GameObject.Find("KeyCounter");
        AddShield();
    }

    public void AddOrb()
    {
        orbs += 1;
        if (orbs > 9)
        {
            orbs = 0;
            AddShield();
        }
        orbCounter.GetComponent<Text>().text = "X "+ orbs;
    }

    public void AddKey()
    {
        keys += 1;
        keyCounter.GetComponent<Text>().text = "X " + keys;
    }

    public void RemoveKey()
    {
        keys -= 1;
        keyCounter.GetComponent<Text>().text = "X " + keys;
    }

    void AddShield()
    {
        shields += 1;
        shieldCounter.GetComponent<Text>().text = "X " + shields;
    }

    public void RecieveDamage(int damage) {
        if (shields < damage) {
            Die();
        }
        else {
            shields -= damage;
            shieldCounter.GetComponent<Text>().text = "X " + shields;
        }
    }
    public void Die() {

        if (cantDie)
        {
            shieldCounter.GetComponent<Text>().text = "X " + 0;
        }
        else
        {
            FreezeWorldComponent.instance.ResetWorld();
            EraChangeWorldComponent.instance.ResetWorld();
            Scene scene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(scene.name);
        }
    }

    public int GetKeys() {
        return keys;
    }
}
