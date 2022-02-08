using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManagerScript : MonoBehaviour
{
    public PlayerScript Player;
    public AlienScript Alien;
    public int PlayerLives = 3;
    public float TimeToRespawn = 3.0f;
    public int Score;
    public Text ScoreText;
    public Text GameOverText;
    public Text TotalScore;
    public List<Image> LivesIcon;
    public Text Winner;

    private void Start()
    {
        foreach (var icon in LivesIcon)
        {
            icon.enabled = true;
        }

        TotalScore.text = "";
        GameOverText.text = "";
        Winner.text = "";
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    private void Update()
    {
        ShowSprites();
        Alien.gameObject.SetActive(Score >= 1000);

        if (Score >= 1000 && Alien.AlienLives <= 0)
        {
            NextLevel();
            Alien.gameObject.SetActive(false);
            FindObjectOfType<AudioManager>().Play("MonsterDeath");
            ScoreText.text = "";
        }
    }

    public void ShowSprites()
    {
        for (var i = 0; i < 3 - PlayerLives; i++)
        {
            LivesIcon[i].enabled = false;
        }
    }

    public void PlayerDied(PlayerScript player)
    {
        PlayerLives--;

        if (PlayerLives <= 0)
            GameOver();
        else
            Invoke(nameof(Respawn), TimeToRespawn);
    }

    public void AlienHit(AlienScript alien)
    {
        if (alien.AlienLives == 50)
        {
            FindObjectOfType<AudioManager>().Play("MonsterAppear");
        }

        alien.AlienLives--;
        FindObjectOfType<AudioManager>().Play("AlienHit");

        if (alien.AlienLives <= 0)
        {
            alien.gameObject.SetActive(false);
            NextLevel();
        }
    }

    private void Respawn()
    {
        Player.transform.position = Vector3.zero;
        Player.gameObject.layer = LayerMask.NameToLayer("Ignore Collisions");
        Player.gameObject.SetActive(true);
        Invoke(nameof(TurnOnCollisions), 3.0f);
    }

    private void TurnOnCollisions()
    {
        Player.gameObject.layer = LayerMask.NameToLayer("Player");
    }

    public void PlayerScore(AsteroidScript asteroid)
    {
        if (asteroid.Size < 0.7f)
            Score += 20;
        else if (asteroid.Size < 1.4f)
            Score += 15;
        else
            Score += 10;

        ScoreText.text = $"Score : {Score}";
    }

    private void GameOver()
    {
        FindObjectOfType<AudioManager>().Play("GameOver");
        GameOverText.text = "GAME OVER!";
        TotalScore.text = $"TOTAL SCORE : {Score}";

        if (Input.GetKey(KeyCode.R))
        {
            Start();
            PlayerLives = 3;
            Score = 0;
        }
    }

    public void NextLevel()
    {
        FindObjectOfType<AudioManager>().Play("WinSound");
        Winner.text = "YOU WIN !";
        TotalScore.text = $"TOTAL SCORE : {Score}";
    }
}
