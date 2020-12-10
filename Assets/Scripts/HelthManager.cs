using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelthManager : MonoBehaviour
{
    public static HelthManager instance;
    public int currentHealth, maxHealth;

    public GameObject deathEffect;

    public ParticleSystem particle;

    public float invincibleLenght = 2f;
    private float invincCounter;

    public Sprite[] healthBarSprites;
    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if (invincCounter > 0)
        {
            invincCounter -= Time.deltaTime;

            for (int i = 0; i < PlayerController.instance.playerPieces.Length; i++)
            {
                if (Mathf.Floor(invincCounter * 5f) % 2 == 0)
                {
                    PlayerController.instance.playerPieces[i].SetActive(true);
                }
                else
                {
                    PlayerController.instance.playerPieces[i].SetActive(false);
                }
                if (invincCounter <= 0)
                {
                    PlayerController.instance.playerPieces[i].SetActive(true);
                }
            }
        }

    }

    public void Hurt()
    {
        if (invincCounter <= 0)
        {

            currentHealth--;
            AudioManager.instance.PlaySFX(3);
            if (currentHealth <= 0)
            {
                currentHealth = 0;
                GameManager.instance.Respawn();

            }
            else
            {
                PlayerController.instance.Knockback();
                invincCounter = invincibleLenght;
            }
        }
        UpdateUI();
    }

    public void ResetHealth()
    {
        currentHealth = maxHealth;
        UIManager.instance.healthImage.sprite = healthBarSprites[5];
    }
    public void AddHealth(int amountToHeal)
    {
        currentHealth += amountToHeal;
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
        Debug.Log($"Было вылечено {amountToHeal}");
    }

    public void UpdateUI()
    {
        UIManager.instance.healthText.text = currentHealth.ToString();

        switch (currentHealth)
        {
            case 0:
                UIManager.instance.healthImage.sprite = healthBarSprites[0];
                break;
            case 1:
                UIManager.instance.healthImage.sprite = healthBarSprites[1];
                break;
            case 2:
                UIManager.instance.healthImage.sprite = healthBarSprites[2];
                break;
            case 3:
                UIManager.instance.healthImage.sprite = healthBarSprites[3];
                break;
            case 4:
                UIManager.instance.healthImage.sprite = healthBarSprites[4];
                break;
            case 5:
                UIManager.instance.healthImage.sprite = healthBarSprites[5];
                break;
        }
    }

}
