using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Singleton
    {
        get; private set;
    }

    [Tooltip("The default scroll speed of the game.")]
    public float defaultScrollSpeed = 2f;

    // The current scroll speed
    private float currScrollSpeed = 0f; 


    /// <summary>
    /// Awake is called before start. 
    /// Initialize references here.
    /// </summary>
    private void Awake()
    {
        GameManager.Singleton = this;
    }


    /// <summary>
    /// Start is called before the first frame update.
    /// Initialize values here.
    /// </summary>
    void Start()
    {

    }


    /// <summary>
    /// Update is called once per frame
    /// </summary>
    void Update()
    {
        // Update the values 
    }
}
