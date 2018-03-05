using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{

    // Use this for initialization
    public UnityEngine.UI.Text p1HealthText;
    public UnityEngine.UI.Text p2HealthText;
    public UnityEngine.UI.Text gameOverTextFg;
    public UnityEngine.UI.Text gameOverTextBg;
    public UnityEngine.UI.Text countdownText;
    private Player1BodyController player1BodyController;
    private Player2BodyController player2BodyController;
    private float p1Health;
    private float p2Health;
    private bool isGameOver;
    private bool restart;
    private int timeLeft = 3;
    private bool isGameStarted;
    private AudioSource[] sounds;
    private AudioSource audio1;
    private AudioSource audio2;
    private AudioSource audio3;

    void Start()
    {
        p1Health = 0;
        p2Health = 0;
        gameOverTextFg.text = "";
        gameOverTextBg.text = "";
        isGameStarted = false;
        player1BodyController = FindObjectOfType<Player1BodyController>();
        player2BodyController = FindObjectOfType<Player2BodyController>();
        sounds = GetComponents<AudioSource>();
        audio1 = sounds[0];
        audio2 = sounds[1];
        audio3 = sounds[2];
        UpdateHealth();
        StartCoroutine(Countdown());
        audio2.Play();
    }

    // Update is called once per frame
    void Update()
    {
        if (restart)
        {
            if (Input.GetKeyDown(KeyCode.M))
            {
                //Application.LoadLevel(Application.loadedLevel);
                UnityEngine.SceneManagement.SceneManager.LoadScene("StageSelect");
            }
        }
        else if (!isGameStarted)
        {
            countdownText.text = "" + timeLeft;
            if (timeLeft <= 0)
            {
                StopCoroutine(Countdown());
                countdownText.text = "Go!";
                audio3.Play();
                StartCoroutine(ShowGoText());
                StopCoroutine(ShowGoText());
                player1BodyController.EnableMovement();
                player2BodyController.EnableMovement();
                isGameStarted = true;
            }
        }
    }

    public void DamageP1(float val)
    {
        p1Health = val;
        UpdateHealth();
    }
    public void DamageP2(float val)
    {
        p2Health = val;
        UpdateHealth();
    }
    void UpdateHealth()
    {
        p1HealthText.text = p1Health + "%";
        p2HealthText.text = p2Health + "%";
    }

    public void GameOver(int player)
    {
        if (!isGameOver)
        {
            gameOverTextFg.text = "Player " + player + " wins!\n Press 'M' to Return to Menu";
            gameOverTextBg.text = "Player " + player + " wins!\n Press 'M' to Return to Menu";
            /*switch (player)
            {
                case 1:
                    gameOverText.color = new UnityEngine.Color(255, 0, 0, 0);
                    break;
                case 2:
                    gameOverText.color = new UnityEngine.Color(0, 255, 0, 0);
                    break;
            }*/
            isGameOver = true;
            restart = true;
            audio1.Play();
        }
    }
    IEnumerator Countdown()
    {
        while (timeLeft > 0)
        {
            audio2.Play();
            yield return new WaitForSeconds(1);
            timeLeft--;
        }
    }
    IEnumerator ShowGoText()
    {
        yield return new WaitForSeconds(1);
        countdownText.text = "";
    }

}
