using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // The initial '0' position of the player
    private Vector3 initPosition;

    // For lerping
    [Tooltip("How long does it take to move to a subsequent tile")]
    public float movementLerpTime = 0.3f;
    private float movementLerpTimer;

    [Range(0.0f, 1.0f)]
    [Tooltip("How far out before the player is nudged back in, as a ratio of a tile.")]
    public float invalidLerpNudgeRatio = 0.25f;

    [Tooltip("The initial upward force applied when jump.")]
    public float jumpInitialForce = 250f;

    [Tooltip("The multiplicative rate the applied force goes down per second.")]
    [Range(0f, 1f)]
    public float jumpDeterRate = .6f;

    [Tooltip("The maximum number of times a player may jump before landing.")]
    public int jumpLimit = 2;


    // Variables for lerp control
    private float lerpSource;               // The source of the lerp
    private float lerpDestination;          // The destination of the lerp
    private bool isLerping;                 // A flag to check if the player is lerping
    private bool isLerpingToInvalidTile;    // A flag to check, if the player is lerping, if the lerp is valid (ie off the tiles)

    // Variables for jumping
    private int jumpCount = 0;              // How many times the player has jumped.
    private float jumpTimer = 0f;           

    // Which lane is the user in
    private int initLanePosition;

    // 
    Rigidbody body;
    CollisionHelper collisionHelper;


    [SerializeField]
    private int lanePosition;
    private float laneWidth;

    private void Awake()
    {
        body = GetComponent<Rigidbody>();
        collisionHelper = GetComponent<CollisionHelper>();
    }


    void Start()
    {
        // Determine the init position
        initPosition = transform.position;

        // Set the user in the center position
        initLanePosition = 0;
        lanePosition = 0;

        // Initialize private variables
        lerpSource = 0f;
        lerpDestination = 0f;
        isLerping = false;
        isLerpingToInvalidTile = false;
        laneWidth = GameManager.Singleton.LaneWidth;

        // Reset timers
        movementLerpTimer = 0f;
        jumpTimer = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        // Check if we should play a lerp animation for the x-position
        if (isLerping)
        {
            float x = 0f;
            movementLerpTimer += Time.deltaTime;

            // If it's a valid movement (aka move from 0% to 100% of the way)
            if (!isLerpingToInvalidTile)
            {
                float percent = (movementLerpTimer / movementLerpTime);
                if (percent > 1.0f)
                    percent = 1.0f;

                x = lerpSource + percent * (lerpDestination - lerpSource);
            }
            else
            {
                if (movementLerpTimer < invalidLerpNudgeRatio * movementLerpTime)
                {
                    // First quarter of the time, move out to a quarter-ish
                    x = lerpSource + (movementLerpTimer / movementLerpTime) * (lerpDestination - lerpSource);
                }
                else
                {
                    // Remainder of the time, move back
                    // position = stating location + percentage moved * displacement
                    x = (lerpSource + invalidLerpNudgeRatio * (lerpDestination - lerpSource))
                        + ((movementLerpTimer - invalidLerpNudgeRatio * movementLerpTime) / ((1.0f - invalidLerpNudgeRatio) * movementLerpTime))
                        * (lerpSource - (lerpSource + invalidLerpNudgeRatio * (lerpDestination - lerpSource)));
                }
            }

            if (movementLerpTimer > movementLerpTime)
            {
                isLerping = false;
            }

            // Set our position
            Vector3 lerpPosition = transform.position;
            lerpPosition.x = x;
            transform.position = lerpPosition;
        }

        // Check for user input
        InputManager input = InputManager.Singleton;

        // Check for movements
        if (!isLerping && (input.GetMoveLeftDown() || input.GetMoveRightDown()))
        {
            // Calc which lane we want to move to.
            bool isValidMove = true;
            int nextLanePosition = lanePosition;
            int laneBounds = (GameManager.Singleton.NumberOfLanes - 1) / 2; // 1

            if (input.GetMoveLeftDown())
            {
                nextLanePosition -= 1;
                if (nextLanePosition < (-laneBounds))
                    isValidMove = false;
            }
            else if (input.GetMoveRightDown())
            {
                nextLanePosition += 1;
                if (nextLanePosition > laneBounds)
                    isValidMove = false;
            }

            // Prepare for the lerpation process
            isLerping = true;
            movementLerpTimer = 0f;
            isLerpingToInvalidTile = !isValidMove;
            lerpSource = laneWidth * lanePosition;
            lerpDestination = laneWidth * nextLanePosition;

            // Set new lane
            if (isValidMove)
            {
                lanePosition = nextLanePosition;
            }
        }

        // Check for jump
        {
            Vector2 jumpForce = Vector2.zero;
            if (input.GetJump())
            {
                if (input.GetJumpDown() && jumpCount < jumpLimit)
                {
                    // This is the first frame of jumping -> apply max force.
                    jumpForce.y = jumpInitialForce;
                    jumpTimer = 0f;

                    // Reset the vertical velocity to zero.
                    Vector2 newVelocity = body.velocity;
                    newVelocity.y = 0;
                    body.velocity = newVelocity;

                    // Increase jumpCount
                    jumpCount += 1;
                }
                else
                {
                    // Increment timer, and apply deterioated jump rate.
                    jumpTimer += Time.deltaTime;
                    jumpForce.y = jumpInitialForce * Mathf.Pow(jumpDeterRate, jumpTimer) * Time.deltaTime;
                }
            }
            body.AddForce(jumpForce);
        }

        if (collisionHelper.IsTouchingGround())
            jumpCount = 0;
    }


    public void ResetEventHandler()
    {
        // Init position
        transform.position = initPosition;
        lanePosition = initLanePosition;

        // Init variables
        lerpSource = 0f;
        lerpDestination = 0f;
        isLerping = false;
        isLerpingToInvalidTile = false;
        laneWidth = GameManager.Singleton.LaneWidth;

        // Reset timers
        movementLerpTimer = 0f;
    }

    private void OnCollisionEnter(Collision collision)
    {
        
    }
}
