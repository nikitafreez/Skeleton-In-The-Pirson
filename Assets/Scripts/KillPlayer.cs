using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillPlayer : MonoBehaviour
{

    private void Awake()
    {

    }

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
            // Debug.Log("Скелет помер в адских муках в бездне геймдева");
            GameManager.instance.Respawn();
        }
    }
}
