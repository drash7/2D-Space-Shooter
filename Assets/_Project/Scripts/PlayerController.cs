using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : Singleton<PlayerController>
{

    public int maxLives = 3;

    private int currentLife;

    public Text livesText;
    private string initLivesText = "Lives: ";

    void Start()
    {
        currentLife = maxLives;
        livesText.text = initLivesText + currentLife.ToString();
    }

    public void PlayerKilled()
    {
        currentLife--;
        if (currentLife == 0)
        {
            GameController.Instance.GameOver();
        }
        else
        {
            // Player.instance.Respawn();
            StartCoroutine(routine: DoSpawnAfterDelay());
            livesText.text = initLivesText + currentLife.ToString();
        }
    }

    public void ResetLives()
    {
        currentLife = maxLives;
        livesText.text = initLivesText + currentLife.ToString();
    }

    IEnumerator DoSpawnAfterDelay()
    {
        Player.instance.gameObject.SetActive(false);
        yield return new WaitForSeconds(3f);
        Player.instance.gameObject.SetActive(true);
        Player.instance.Respawn();
    }

}
