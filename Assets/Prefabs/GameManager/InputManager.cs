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
    private bool speedIncrease;
    private bool speedIncreaseDown;
    private bool speedDecrease;
    private bool speedDecreaseDown;


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
            speedIncrease = false;
            speedIncreaseDown = false;
            speedDecrease = false;
            speedDecreaseDown = false;
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
        moveLeft = Input.GetKey(KeyCode.LeftArrow);
        moveLeftDown = Input.GetKeyDown(KeyCode.LeftArrow);

        moveRight = Input.GetKey(KeyCode.RightArrow);
        moveRightDown = Input.GetKeyDown(KeyCode.RightArrow);

        jump = Input.GetKey(KeyCode.Space);
        jumpDown = Input.GetKey(KeyCode.Space);

        speedIncrease = Input.GetKey(KeyCode.UpArrow);
        speedIncreaseDown = Input.GetKeyDown(KeyCode.UpArrow);

        speedDecrease = Input.GetKey(KeyCode.DownArrow);
        speedDecreaseDown = Input.GetKeyDown(KeyCode.DownArrow);
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

    #endregion
}
