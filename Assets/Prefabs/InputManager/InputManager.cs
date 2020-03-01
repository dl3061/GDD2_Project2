using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public static InputManager Singleton
    {
        get; private set;
    }


    // Variables
    private Vector2 move;
    private bool moveLeft;
    private bool moveLeftDown;
    private bool moveRight;
    private bool moveRightDown;
    private bool jump;
    private bool jumpDown;


    /// <summary>
    /// Awake is called before start. 
    /// Initialize references here.
    /// </summary>
    private void Awake()
    {
        InputManager.Singleton = this;
    }


    /// <summary>
    /// Start is called before the first frame update.
    /// Initialize values here.
    /// </summary>
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Update the values 
    }


    #region Accessors

    public Vector2 GetMove()
    {
        return move;
    }

    public bool GetMoveLeft()
    {
        return moveLeft;
    }

    public bool GetMoveLeftDown()
    {
        return moveLeftDown;
    }

    public bool GetMoveRight()
    {
        return moveRight;
    }

    public bool GetMoveRightDown()
    {
        return moveRightDown;
    }

    public bool GetJump()
    {
        return jump;
    }

    public bool GetJumpDown()
    {
        return jumpDown;
    }

    #endregion
}
