using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
enum GameState
{
    playing,
    paused,
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
    public AudioMixer audioMixer;
    bool willRestart = false;
    public float score = 0f;
    GameState gameState = GameState.idle;
    public bool gameIsRunning() => gameState == GameState.playing;
    public float fogIncreaseDensity = 0.0f;
    public GameObject pause;
    public Slider volumeSlider;
    public float speedMultiplier = 1.0f;
    public void SetSpeedMultiplier(float speed)
    {
        speedMultiplier = speed;
    }
    public void PauseGame()
    {
        pause.SetActive(true);

        player.rb.velocity = Vector3.zero;
        player.rb.angularVelocity = Vector3.zero;
        player.enabled = false;
        gameState = GameState.paused;
        bgMusic.Pause();
    }
    public void ResumeGame()
    {
        pause.SetActive(false);
        player.enabled = true;
        gameState = GameState.playing;
        bgMusic.Play();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.D))
        {
            if (gameState == GameState.idle)
            {
                StartGame();
            }
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (gameState == GameState.playing)
            {

                PauseGame();
            }
            else if (gameState == GameState.paused)
            {
                ResumeGame();
            }
        }

        if (gameState == GameState.playing)
        {

            RenderSettings.fogDensity += Mathf.Clamp(Time.deltaTime * fogIncreaseDensity, 0.0f, 0.2f);

        }
    }
    void Start()
    {
        finalScoreText.text = "";
        scoreText.text = "";
        player.enabled = false;
        playButton.gameObject.SetActive(true);
        playButton.onClick.AddListener(() =>
        {
            if (!willRestart)
            {
                StartGame();
            }
            else
            {
                RestartGame();
            }
        });
        float volume = 0.0f;
        audioMixer.GetFloat("volume", out volume);
        volumeSlider.value = volume;
    }
    float Remap(float value, float min1, float max1, float min2, float max2)
    {
        return min2 + (value - min1) * (max2 - min2) / (max1 - min1);
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
        RenderSettings.fog = true;
        RenderSettings.fogDensity = 0.0f;
        ObjectSpawner spawner = FindObjectOfType<ObjectSpawner>();
        spawner.Spawn();
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
    public void UpdateScore(float newScore)
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

    public void SetVolume(float volume)
    {
        audioMixer.SetFloat("volume", volume);
    }
}
