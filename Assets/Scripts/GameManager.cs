using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    public GameObject[] itemList;
    public static GameManager instance;
    private Vector3 respawnPosition, camSpawnPosition;

    public GameObject deathEffect;

    public ParticleSystem particle;
    public bool isRespawning;

    public int currentCoins;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        respawnPosition = PlayerController.instance.transform.position;
        UIManager.instance.healthText.text = HelthManager.instance.maxHealth.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseUnpause();
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    public void Respawn()
    {
        StartCoroutine(RespawnCo());
    }
    public IEnumerator RespawnCo()
    {
        PlayerController.instance.gameObject.SetActive(false);
        CameraController.instance.theCMBrains.enabled = false;
        UIManager.instance.fadeToBlack = true;

        var main = particle.main;
        main.playOnAwake = true;
        Instantiate(deathEffect, PlayerController.instance.transform.position + new Vector3(0f, 1f, 0f), PlayerController.instance.transform.rotation);

        yield return new WaitForSeconds(1.5f);

        isRespawning = true;
        UIManager.instance.fadeFromBlack = true;
        PlayerController.instance.transform.position = respawnPosition;
        CameraController.instance.theCMBrains.enabled = true;
        PlayerController.instance.gameObject.SetActive(true);
        HelthManager.instance.currentHealth = HelthManager.instance.maxHealth;

        HelthManager.instance.UpdateUI();
    }

    public void SetSpawnPoint(Vector3 newSpawnPoint)
    {
        respawnPosition = newSpawnPoint;
    }

    public void AddCoins(int coinsToAdd)
    {
        currentCoins += coinsToAdd;
        if (currentCoins < 10)
        {
            UIManager.instance.coinText.text = "0" + currentCoins.ToString();
        }
        else
        {
            UIManager.instance.coinText.text = currentCoins.ToString();
        }
        Debug.Log($"Было получено {coinsToAdd}");
    }

    public void PauseUnpause()
    {
        if (UIManager.instance.pauseScreen.activeInHierarchy)
        {
            UIManager.instance.pauseScreen.SetActive(false);
            Time.timeScale = 1f;

            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
        else
        {
            UIManager.instance.pauseScreen.SetActive(true);
            Time.timeScale = 0f;
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
    }

    public IEnumerator LevelEndCo()
    {
        Scene scene = SceneManager.GetActiveScene();

        if (scene.name == "Boss1")
        {
            AudioManager.instance.PlayMusic(8);
        }
        else
        {
            AudioManager.instance.PlayMusic(2);
        }
        PlayerController.instance.stopMove = true;

        yield return new WaitForSeconds(2f);

        UIManager.instance.fadeToBlack = true;

        yield return new WaitForSeconds(2f);
        Debug.Log("Level Ended");


        if (scene.name == "Skeleton In Prison")
        {
            SceneManager.LoadScene("DanganMaze");
        }
        else if (scene.name == "DanganMaze")
        {
            SceneManager.LoadScene("Boss1");
        }
        else if (scene.name == "Boss1")
        {

        }
    }
}
