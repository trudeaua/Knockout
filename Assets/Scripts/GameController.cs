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
    private float p1Health;
    private float p2Health;
    private bool isGameOver;
    private bool restart;

    void Start()
    {
        p1Health = 0;
        p2Health = 0;
        gameOverTextFg.text = "";
        gameOverTextBg.text = "";
        UpdateHealth();
    }

    // Update is called once per frame
    void Update()
    {
        if (restart)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                //Application.LoadLevel(Application.loadedLevel);
                UnityEngine.SceneManagement.SceneManager.LoadScene("Main");
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
        gameOverTextFg.text = "Player " + player + " wins!";
        gameOverTextBg.text = "Player " + player + " wins!";
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
    }

}
