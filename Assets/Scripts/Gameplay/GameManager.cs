using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public TextMeshProUGUI healthUI;
    public PlayerData playerData;
    public TimeManager timeManager;

    bool spikeDamageOnCooldown = false; // set to true while player is immune to damage from traps
    
    void Awake()
    {
        instance = this;
        
    }

    void Start()
    {
        playerData.currentHealth = playerData.maxHealth; // set player health correctly based on their max health
        timeManager.startNewRun(); // time manager begins with a fresh time
    }

    void Update()
    {
        
    }

    public void endGame(bool runVictory) // ends the game, bool is true if player won, false if they died
    {
        Cursor.lockState = CursorLockMode.None; // returns control of mouse to player
        Cursor.visible = true;
        
        timeManager.concludeRun();
        SceneManager.LoadScene("GameOver");
    }

    public void damagePlayer()
    {
        if (spikeDamageOnCooldown) { return; } // don't double-fire damage events by triggering a cooldown
        StartCoroutine(spikeTrapCooldown());
        playerData.currentHealth -= 1;
        healthUI.text = "HEALTH: " + playerData.currentHealth + "/" + playerData.maxHealth;
        if (playerData.currentHealth <= 0) // when out of health, game over
        {
            healthUI.enabled = false;
            endGame(false);
        }
    }

    IEnumerator spikeTrapCooldown() // triggers a short cooldown after hitting a spike trap so damage instances can't be stacked (I know this as debouncing, I'm not sure if this is an official term)
    {
        spikeDamageOnCooldown = true;
        yield return new WaitForSeconds(0.5f);
        spikeDamageOnCooldown = false;
    }

}