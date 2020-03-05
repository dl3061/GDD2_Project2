using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Singleton
    {
        get; private set;
    }

    [Tooltip("The Player itself, so other scripts can reference it statically")]
    public GameObject ActivePlayer;

    [Tooltip("The default scroll speed of the game.")]
    public float defaultScrollSpeed = -2f;

    [Tooltip("How many lanes are in the game. Should be 3.")]
    public int NumberOfLanes = 3;

    [Tooltip("How wide is a lane.")]
    public float LaneWidth = 1;

    [Tooltip("The event to throw ")]
    public GameEvent ResetEvent;

    [Tooltip("The event to throw ")]
    public GameEvent PauseEvent;

    [Tooltip("The event to throw when toggling polarity")]
    public GameEvent TogglePolarityEvent;

    // The current scroll speed
    [SerializeField]
    private float currScrollSpeed = 0f; 


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
        currScrollSpeed = defaultScrollSpeed;
    }


    /// <summary>
    /// Update is called once per frame
    /// </summary>
    void Update()
    {
        // Update the values 

        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (ResetEvent != null)
                ResetEvent.Raise();
        }

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            if (TogglePolarityEvent != null)
                TogglePolarityEvent.Raise();
        }
    }


    private void OnValidate()
    {
        if (defaultScrollSpeed > 0f)
        {
            Debug.LogWarning("Scroll speed is positive, which is away from the camera by default. Are you sure you want this?");
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
}
