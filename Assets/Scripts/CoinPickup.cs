using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinPickup : MonoBehaviour
{
    public GameObject coinsEffect;
    public ParticleSystem particle;

    public int value;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Destroy(gameObject);
            GameManager.instance.AddCoins(value);

            var main = particle.main;
            main.playOnAwake = true;
            Instantiate(coinsEffect, PlayerController.instance.transform.position + new Vector3(0f, 0f, 0f), PlayerController.instance.transform.rotation);

            int rnd = Random.Range(0, 3);
            AudioManager.instance.PlaySFX(rnd);
        }
    }
}
