using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Singleton
    {
        get; private set;
    }

    [Tooltip("The Player itself, so other scripts can reference it statically")]
    public GameObject ActivePlayer;

    [Header("Game Speed Control")]

    [Tooltip("The default scroll speed of the game.")]
    public float defaultScrollSpeed = -6f;

    [Tooltip("The scroll speed to go at when the player presses Up.")]
    public float fastScrollSpeed = -12f;

    [Tooltip("The scroll speed to go at when the player presses Down.")]
    public float slowScrollSpeed = -2f;

    [Tooltip("Max change in scroll speed per second")]
    public float scrollAcceleration = 3f;

    [Tooltip("The scroll speed to go at when the player is toggling.")]
    public float midtoggleScrollSpeed = 0.01f;

    [Tooltip("How long the toggle delay exists when toggling.")]
    public float midtoggleDelayTime = 0.25f;
    private float midtoggleDelayTimer = 0f;


    [Header("Game Configuration")]

    [Tooltip("How many lanes are in the game. Should be 3.")]
    public int NumberOfLanes = 3;

    [Tooltip("How wide is a lane.")]
    public float LaneWidth = 1;


    [Header("Game Events")]

    [Tooltip("The event to throw ")]
    public GameEvent ResetEvent;

    [Tooltip("The event to throw ")]
    public GameEvent PauseEvent;

    [Tooltip("The event to throw when toggling polarity")]
    public GameEvent TogglePolarityEvent;

    public Text gameOverText;

    // Score system- based on time
    public Text scoreText;
    private float startTime;

    [Header("Serialized Fields")]

    // The current scroll speed
    [SerializeField]
    private float currScrollSpeed = 0f;

    private float speedIncreaseTimer = 6f;

    private float speedIncreaseDelay = 8f;

    private float speedIncreaseAmount = 0.65f;


    /// <summary>
    /// Awake is called before start. 
    /// Initialize references here.
    /// </summary>
    private void Awake()
    {
        GameManager.Singleton = this;

        if (ActivePlayer == null)
        {
            // This should be avoided at all costs but it's safer than something that needs a static reference to the player breaking.
            ActivePlayer = GameObject.Find("Player");
        }
    }


    /// <summary>
    /// Start is called before the first frame update.
    /// Initialize values here.
    /// </summary>
    void Start()
    {
        // Initialize scroll speed to current scroll speed.
        currScrollSpeed = defaultScrollSpeed;

        midtoggleDelayTimer = 0f;

        startTime = Time.time;
    }


    /// <summary>
    /// Update is called once per frame
    /// </summary>
    void Update()
    {
        // Check for events

        // Score system- based on time
        float t = Time.time - startTime;
        string seconds = (t % 60).ToString("F2");

        // Debug.Log("t: " + t + " seconds: " + seconds);

        if (scoreText != null)
            scoreText.text = "Score: " + seconds;

        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (ResetEvent != null)
            {
                ResetEvent.Raise();
                startTime = Time.time;
            }
        }

        if (InputManager.Singleton.GetTransitionDown())
        {
            if (TogglePolarityEvent != null)
                TogglePolarityEvent.Raise();
        }


        // Control the scroll speed
        /*
        if (midtoggleDelayTimer > 0f)
        {
            currScrollSpeed = midtoggleScrollSpeed;

            midtoggleDelayTimer -= Time.deltaTime;
        }
        else
        {
            if (InputManager.Singleton.GetSpeedIncrease())
            {
                currScrollSpeed = fastScrollSpeed;
            }
            else if (InputManager.Singleton.GetSpeedDecrease())
            {
                currScrollSpeed = slowScrollSpeed;
            }
            else
            {
                currScrollSpeed = defaultScrollSpeed;
            }
        }
        */
        if(speedIncreaseTimer < Time.timeSinceLevelLoad)
        {
            currScrollSpeed -= speedIncreaseAmount;
            speedIncreaseTimer = Time.timeSinceLevelLoad + speedIncreaseDelay;
        }
        CheckDeath();
    }


    private void OnValidate()
    {
        if (defaultScrollSpeed > 0f)
        {
            Debug.LogWarning("Scroll speed is positive, which is away from the camera by default. Are you sure you want this?");
        }
    }

    void CheckDeath()
    {
        if(ActivePlayer.transform.position.y < -6.0f)
        {
            // Debug.Log("Game Over");

            Time.timeScale = 0.0f;

            if (gameOverText != null)
                gameOverText.text = "You Died!";
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public float GetCurrentScrollSpeed()
    {
        return currScrollSpeed;
    }


    public void ResetEventHandler()
    {
        // Reset scroll speed to default scroll speed.
        currScrollSpeed = defaultScrollSpeed;

        // Reset timers
        midtoggleDelayTimer = 0f;

        // Unpause if necessary
        Time.timeScale = 1.0f;

        if (gameOverText != null)
            gameOverText.text = "";

        SceneManager.LoadScene("TestScene");
    }


    public void PauseEventHandler()
    {
        // Pause
        Time.timeScale = 0f;
    }


    public void UnpauseEventHandler()
    {
        // Unpause
        Time.timeScale = 1f;
    }


    public void TogglePolarityEventHandler()
    {
        midtoggleDelayTimer = midtoggleDelayTime;
    }
}
