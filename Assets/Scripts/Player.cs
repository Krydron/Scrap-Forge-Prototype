using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] int playerHealth;
    private int playerMaxHealth;
    private float playerHealthPercent;
    [SerializeField] GameObject healthBar;
    [SerializeField] GameObject gameOverMenu;

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1;
        playerMaxHealth = playerHealth;
        playerHealthPercent = 1;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayerDamage(int value) //When the player takes damage decrement health and run death code if health drops below zero
    {
        playerHealth -= value;
        if (playerHealth <= 0 )
        {
            //Death
            gameOverMenu.SetActive(true);
            this.GetComponent<PlayerMovement>().TogglePause();
        }
        UpdateHealthBar();
    }

    public void PlayerHeal(int value) //When the player is healed increment health but not above the maximum
    {
        playerHealth += value;
        playerHealth = Mathf.Clamp(playerHealth, 0, playerMaxHealth);
        UpdateHealthBar();
    }

    public bool isHealthMax()
    {
        return playerHealth==playerMaxHealth;
    }

    private void UpdateHealthBar()
    {
        playerHealthPercent = ((float)playerHealth) / ((float)playerMaxHealth); //Updates the health percent
        healthBar.GetComponent<HealthBar>().UpdateHealthBar(playerHealthPercent); //Updates health bar
    }
}
