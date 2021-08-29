using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
enum GameState
{
    playing,
    gameOver,
    idle,
}

public class GameManager : MonoBehaviour
{
    public Text scoreText;
    public Text finalScoreText;

    public PlayerMovement player;
    public AudioSource gameOverAudio;
    public AudioSource bgMusic;
    public Button playButton;

    bool willRestart = false;
    double score = 0;
    GameState gameState = GameState.idle;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.D))
        {
            if (gameState == GameState.idle)
            {
                StartGame();
            }
        }

    }
    void Start()
    {
        Debug.Log($"GameManager Start {willRestart}");
        if (willRestart)
        {
            StartGame();
        }
        player.enabled = false;
        playButton.gameObject.SetActive(true);
        playButton.onClick.AddListener(() =>
        {
            Debug.Log($"Playbutton Pressed. Restart: {willRestart}");
            if (!willRestart)
            {
                StartGame();
            }
            else
            {
                RestartGame();
            }
        });
    }
    void StartGame()
    {
        Debug.Log("Starting Game");
        willRestart = false;
        scoreText.text = "";
        player.enabled = true;
        gameState = GameState.playing;
        bgMusic.Play();
        playButton.gameObject.SetActive(false);
        scoreText.gameObject.SetActive(true);
        Debug.Log("Game Started");
    }

    public float restartDelay = 3f;
    public void GameOver()
    {
        Debug.Log("Game Over");
        bgMusic.Stop();
        gameOverAudio.Play();
        InitiallizeRestart();
    }
    public void UpdateScore(double newScore)
    {
        if (gameState == GameState.playing)
        {
            score = newScore;
            var _scoreText = $"Score: {score.ToString("0")}";
            scoreText.text = _scoreText;
        }
    }
    public void RestartGame()
    {
        Debug.Log("Restarting Game");
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Debug.Log("Game Restarted");
        StartGame();
    }

    void InitiallizeRestart(bool win = false)
    {
        finalScoreText.text = $"Final Score: {score.ToString("0")}";
        scoreText.gameObject.SetActive(false);
        Debug.Log("Initiallizing Restart");
        player.enabled = false;
        gameState = GameState.gameOver;
        playButton.gameObject.SetActive(true);
        playButton.GetComponentInChildren<Text>().text = win ? "Play Again" : "Restart";
        willRestart = true;
        Debug.Log("Restart Initialized");
    }
    public void WinGame()
    {
        Debug.Log("You Win");
        InitiallizeRestart(true);
    }
}
