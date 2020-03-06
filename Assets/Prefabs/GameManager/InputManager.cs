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
    private bool jumpUp;
    private bool speedIncrease;
    private bool speedIncreaseDown;
    private bool speedDecrease;
    private bool speedDecreaseDown;
    private bool transition;
    private bool transitionDown;


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
        move = Vector2.zero;
        moveLeft = false;
        moveLeftDown = false;
        moveRight = false;
        moveRightDown = false;
        jump = false;
        jumpDown = false;
        jumpUp = false;
        speedIncrease = false;
        speedIncreaseDown = false;
        speedDecrease = false;
        speedDecreaseDown = false;
        transition = false;
    }


    /// <summary>
    /// Update is called once per frame
    /// </summary>
    void Update()
    {
        // Update the values 
        move.x = Input.GetAxis("Horizontal");
        move.y = Input.GetAxis("Vertical");

        // Update the values
        moveLeft = Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A);
        moveLeftDown = Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A);

        moveRight = Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D);
        moveRightDown = Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D);

        jump = Input.GetKey(KeyCode.Space);
        jumpDown = Input.GetKeyDown(KeyCode.Space);
        jumpUp = Input.GetKeyUp(KeyCode.Space);

        speedIncrease = Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W);
        speedIncreaseDown = Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W);

        speedDecrease = Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S);
        speedDecreaseDown = Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S);

        transition = Input.GetKey(KeyCode.Tab) || Input.GetKey(KeyCode.CapsLock);
        transitionDown = Input.GetKeyDown(KeyCode.Tab) || Input.GetKeyDown(KeyCode.CapsLock);
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

    public bool GetJumpUp()
    {
        return jumpUp;
    }

    public bool GetSpeedIncrease()
    {
        return speedIncrease;
    }

    public bool GetSpeedIncreaseDown()
    {
        return speedIncreaseDown;
    }

    public bool GetSpeedDecrease()
    {
        return speedDecrease;
    }

    public bool GetSpeedDecreaseDown()
    {
        return speedDecreaseDown;
    }

    public bool GetTransitionDown()
    {
        return transitionDown;
    }

    #endregion
}
