﻿using System.Collections;
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

    Rigidbody body;


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

        // When in dark mode, ignore light mask collisions. When in light mode or neutral, don't ignore.
        Physics.IgnoreLayerCollision(lightMask.value, playerMask.value, currentPolarity == Polarity.Dark);

        // When in light mode, ignore dark mask collisions. When in dark mode or neutral, don't ignore.
        Physics.IgnoreLayerCollision(darkMask.value, playerMask.value, currentPolarity == Polarity.Light);
    }

}
