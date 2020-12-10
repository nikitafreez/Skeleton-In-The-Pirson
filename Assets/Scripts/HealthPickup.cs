using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    public int healAmount;
    public bool isFullHeal;

    public GameObject healthEffect;

    public ParticleSystem particle;

    void Start()
    {

    }

    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Destroy(gameObject);
            
            var main = particle.main;
            main.playOnAwake = true;
            Instantiate(healthEffect, PlayerController.instance.transform.position + new Vector3(0f, 0f, 0f), PlayerController.instance.transform.rotation);
            if (isFullHeal)
            {
                HelthManager.instance.ResetHealth();
            }
            else
            {
                HelthManager.instance.AddHealth(healAmount);
            }
            HelthManager.instance.UpdateUI();

            int rnd = Random.Range(4, 6);
            AudioManager.instance.PlaySFX(rnd);
        }
    }
}