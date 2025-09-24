using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager GameplayManager;
    int playerHealth;
    public int maxHealth = 3;
    public TextMeshProUGUI healthUI;

    bool spikeDamageOnCooldown = false;
    
    void Awake()
    {
        GameplayManager = this;
    }

    void Start()
    {
        playerHealth = maxHealth;    
    }

    void Update()
    {
        
    }

    public void winGame()
    {
        Cursor.lockState = CursorLockMode.None; 
        Cursor.visible = true;
        PlayerPrefs.SetInt("PlayerWon", 1); // this needs to be replaced once i get a handle on scriptable objects for data persistance
        SceneManager.LoadScene("GameOver");

    }

    public void gameOver()
    {
        Cursor.lockState = CursorLockMode.None; 
        Cursor.visible = true;
        PlayerPrefs.SetInt("PlayerWon", 0);
        SceneManager.LoadScene("GameOver");
    }

    public void damagePlayer()
    {
        if (spikeDamageOnCooldown) { return; } // don't double-fire damage events by triggering a cooldown
        StartCoroutine(spikeTrapCooldown());
        playerHealth -= 1;
        healthUI.text = "HEALTH: " + playerHealth + "/" + maxHealth;
        if (playerHealth <= 0) // when out of health, game over
        {
            healthUI.enabled = false;
            gameOver();
        }
    }

    IEnumerator spikeTrapCooldown() // triggers a short cooldown after hitting a spike trap so damage instances can't be stacked (I know this as debouncing, I'm not sure if this is an official term)
    {
        spikeDamageOnCooldown = true;
        yield return new WaitForSeconds(0.5f);
        spikeDamageOnCooldown = false;
    }

}
