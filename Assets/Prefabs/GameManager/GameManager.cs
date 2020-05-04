using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Singleton
    {
        get; private set;
    }

    [Tooltip("Background video clips")]
    [SerializeField]
    VideoClip[] backgrounds = new VideoClip[2];
    [SerializeField]
    VideoPlayer background;
    int currentVideo = 0;


    [Tooltip("The Player itself, so other scripts can reference it statically")]
    public GameObject ActivePlayer;
    public bool playerDead;

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

    [Tooltip("The event to throw ")]
    public GameEvent UnpauseEvent;

    [Tooltip("The event to throw when toggling polarity")]
    public GameEvent TogglePolarityEvent;

    [Header("Text")]

    [Tooltip("The Game Over text to display")]
    public Text centerText;

    [Tooltip("The Score text to display. Score system- based on time")]
    public Text scoreText;
    public Text multiplerText;

    private float scoreMultiplier;
    public int score;

    private float scoreTimer;

    [Tooltip("The time of session to display")]
    public Text TimerText;


    [Header("Serialized Fields")]

    // The current scroll speed
    [SerializeField]
    private float currScrollSpeed = 0f;

    private float speedIncreaseTimer = 6f;

    private float speedIncreaseDelay = 8f;

    private float speedIncreaseAmount = 0.65f;
    private bool gamePaused = false;



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
        background.clip = backgrounds[currentVideo];
        // Initialize scroll speed to current scroll speed.
        currScrollSpeed = defaultScrollSpeed;

        // Init timers
        midtoggleDelayTimer = 0f;

        // Init start time
        scoreTimer = 0f;
        score = 0;
        scoreMultiplier = 1f;

        // Init flags
        gamePaused = false;
    }


    /// <summary>
    /// Update is called once per frame
    /// </summary>
    void Update()
    {
        if (!gamePaused)
        {
            if (CheckDeath())
            {
                // Debug.Log("Game Over");
                Time.timeScale = 0.0f;
                gamePaused = true;
                scoreMultiplier = 0f;
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

            // Update score
            scoreTimer += Time.deltaTime;
            if (scoreTimer > 1f)
            {
                scoreTimer -= 1f;
                if (scoreMultiplier < 5) { 
                    scoreMultiplier += .1f;
                    if (scoreMultiplier >= 5)
                    {
                        scoreMultiplier = 5;
                    }
                }
                score += (int)Mathf.Ceil(10 * scoreMultiplier);
            }


            // Update UI
            if (scoreText != null)
                scoreText.text = "Score: " + Mathf.Ceil(score); //scoreText.text = "(x" + scoreMultiplier + ") " + score;

            if (multiplerText != null)
                multiplerText.text = "Bonus Multiplier: x" + scoreMultiplier;

            if (centerText != null)
            {
                if (CheckDeath())
                    centerText.text = "You Died!\nPress tab to restart or H to go back to the main menu";
                else
                    centerText.text = "";
            }
        }
        else
        {
            // If paused, hide UI

            if (centerText != null)
            {
                if (CheckDeath())
                    centerText.text = "You Died!\nPress tab to restart or H to go back to the main menu";
                else
                    centerText.text = "Paused";
            }
        }


        // Check for events
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (ResetEvent != null)
            {
                ResetEvent.Raise();
            }
        }

        if(Input.GetKeyDown(KeyCode.H) && CheckDeath())
        {
            SceneManager.LoadScene("Home");
        }

        if (InputManager.Singleton.GetTransitionDown())
        {
            if (TogglePolarityEvent != null)
                TogglePolarityEvent.Raise();
        }

        if (InputManager.Singleton.GetPauseDown())
        {
            if (gamePaused)
            {
                if (UnpauseEvent != null)
                    UnpauseEvent.Raise();
            }
            else
            {
                if (PauseEvent != null)
                    PauseEvent.Raise();
            }
        }

        //Update Timer
        TimerText.text = "Time: " + Mathf.Round(Time.timeSinceLevelLoad);

        //Increase speed over time
        if(speedIncreaseTimer < Time.timeSinceLevelLoad)
        {
            currScrollSpeed -= speedIncreaseAmount;
            ActivePlayer.GetComponent<PlayerMovement>().animator.speed += speedIncreaseAmount / 20;
            ActivePlayer.GetComponent<PlayerMovement>().gravityScale += 0.225f;
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

    private bool CheckDeath()
    {
        return (ActivePlayer.transform.position.y < -6.0f || playerDead) ;
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

        // Reset score
        scoreTimer = 0f;
        score = 0;
        scoreMultiplier = 1f;

        // Unpause if necessary
        Time.timeScale = 1.0f;
        gamePaused = false;

        if (centerText != null)
            centerText.text = "";

        SceneManager.LoadScene("TestScene");
    }


    public void PauseEventHandler()
    {
        // Pause
        gamePaused = true;
        Time.timeScale = 0f;
    }


    public void UnpauseEventHandler()
    {
        // Unpause
        gamePaused = false;
        Time.timeScale = 1f;
    }


    public void TogglePolarityEventHandler()
    {
        if(currentVideo == 1)
        {
            currentVideo = 0;
        } else
        {
            currentVideo = 1;
        }
        background.clip = backgrounds[currentVideo];
        scoreMultiplier = 1f;
        midtoggleDelayTimer = midtoggleDelayTime;
    }
}
