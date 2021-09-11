using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using System;

enum GameState
{
    playing,
    paused,
    gameOver,
    idle,
}

public class GameManager : MonoBehaviour
{
    private String _volumeKey = "volume";
    private String _speedMultiplyerKey = "speed_multiplyer";
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
    public Slider speedSlider;
    public float speedMultiplier = 1.0f;

    private bool isMute = false;
    public void SetSpeedMultiplier(float speed)
    {
        speedMultiplier = speed;
        PlayerPrefs.SetFloat(_speedMultiplyerKey, speed);
    }

    void getSpeedMultiplier()
    {
        speedMultiplier = PlayerPrefs.GetFloat(_speedMultiplyerKey, 1.0f);
        speedSlider.value = speedMultiplier;
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

    void keyPressEvents()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.D))
        {
            if (gameState == GameState.idle)
            {
                StartGame();
            }
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            RestartGame();
        }
        if (Input.GetKeyDown(KeyCode.M))
        {
            Mute();
        }
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P))
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
    }
    void Mute()
    {
        isMute = !isMute;
        if (isMute)
        {
            volumeSlider.value = -80f;
            SetVolume(-80f);
        }
        else
        {
            volumeSlider.value = 0f;
            SetVolume(0f);
        }

    }

    void Update()
    {
        keyPressEvents();
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
        SetVolume(PlayerPrefs.GetFloat(_volumeKey, 0f));
        getSpeedMultiplier();
        getVolumn();
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
        PlayerPrefs.SetFloat(_volumeKey, volume);
    }

    void getVolumn()
    {
        float volume = 0.0f;
        audioMixer.GetFloat("volume", out volume);
        volumeSlider.value = volume;
    }
}
