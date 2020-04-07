using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPolarity : PolarityToggle
{
    [Tooltip("The mask that the player exclusively exists in.")]
    public LayerMask playerMask = 10;

    [Tooltip("The mask for the light elements")]
    public LayerMask lightMask = 8;

    [Tooltip("The mask for the dark elements")]
    public LayerMask darkMask = 9;

    [Tooltip("How long does it take to shift.")]
    public float shiftTime = .25f;
    private float shiftTimer = 0f;

    private Rigidbody body;

    public Material material;
    
    private bool isTransitioning = false;


    private void Awake()
    {
        body = GetComponent<Rigidbody>();
    }

    /// <summary>
    /// Start is called before the first frame update
    /// </summary>
    protected override void Start()
    {
        base.Start();
    }


    /// <summary>
    /// Update is called once per frame
    /// </summary>
    protected override void Update()
    {
        // In ::PolarityToggle, it checks which gameobject to display for each polarity.
        // Comment this line out if you don't want to do that and do something else instead.
        base.Update();

        // Leo's code, under Player Polarity instead of Polarity Toggle so it only affects Player and not break everything else.
        {
            float alpha = 0f;

            if (isTransitioning)
            {
                shiftTimer += Time.deltaTime;
                if (shiftTimer > shiftTime)
                    shiftTimer = shiftTime;

                if (currentPolarity == Polarity.Light)
                {
                    alpha = 1.0f - shiftTimer / shiftTime;
                }
                else
                {
                    alpha = shiftTimer / shiftTime;
                }

                if (shiftTimer >= shiftTime)
                {
                    shiftTimer = 0f;
                    isTransitioning = false;
                }
            }
            else
            {
                shiftTimer = 0f;
                isTransitioning = false;

                if (currentPolarity == Polarity.Light)
                {
                    alpha = 0f;
                }
                else
                {
                    alpha = 1f;
                }
            }



            // Set the float
            material.SetFloat("Vector1_9152EDBB", alpha);
        }


        // Add collision checking from layers
        int playerLayer = (int)Mathf.Log(playerMask.value, 2);
        int lightLayer = (int)Mathf.Log(lightMask.value, 2);
        int darkLayer = (int)Mathf.Log(darkMask.value, 2);

        // When in dark mode, ignore light mask collisions. When in light mode or neutral, don't ignore.
        Physics.IgnoreLayerCollision(lightLayer, playerLayer, currentPolarity == Polarity.Dark);

        // When in light mode, ignore dark mask collisions. When in dark mode or neutral, don't ignore.
        Physics.IgnoreLayerCollision(darkLayer, playerLayer, currentPolarity == Polarity.Light);
    }


    public override void TogglePolarity()
    {
        if (isTransitioning == false)
        {
            base.   TogglePolarity();

            shiftTimer = 0f;
            isTransitioning = true;
        }
    }
}
