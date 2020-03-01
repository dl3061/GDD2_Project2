using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This monobehavior is responsible from toggling active/inactive gameobjects depending on polarity.
///     
/// </summary>
public class PolarityToggle : MonoBehaviour
{
    [Tooltip("The polarity to be initialized to. If Neutral, this script does nothing.")]
    public Polarity defaultPolarity = Polarity.Neutral;

    [Tooltip("The gameobject that appears when in lightside.")]
    public GameObject lightObject;

    [Tooltip("The gameobject that appears when in darkside.")]
    public GameObject darkObject;


    // The current polarity
    private Polarity currentPolarity;

    /// <summary>
    /// Start is called before the first frame update
    /// </summary>
    void Start()
    {
        currentPolarity = defaultPolarity;
    }


    /// <summary>
    /// Update is called once per frame
    /// </summary>
    void Update()
    {
        // Reinforce the current active polarity, unless neutral.
        if (currentPolarity != Polarity.Neutral)
        {
            if (lightObject != null)
                lightObject.SetActive(currentPolarity == Polarity.Light);

            if (darkObject != null)
                darkObject.SetActive(currentPolarity == Polarity.Dark);
        }
    }


    /// <summary>
    /// Reset Event Handler. Call when resetting (ie restarting) the game.
    /// </summary>
    public void ResetEvent()
    {
        currentPolarity = defaultPolarity;
    }


    /// <summary>
    /// Toggles the polarity to the opposite polarity.
    /// </summary>
    public void TogglePolarity()
    {
        if (currentPolarity == Polarity.Light)
        {
            currentPolarity = Polarity.Dark;
        }
        else if (currentPolarity == Polarity.Dark)
        {
            currentPolarity = Polarity.Light;
        }
        else
        {
            // Neutral case. Do nothing.
        }
    }


    /// <summary>
    /// Forcibly sets the polarity to light side.
    /// </summary>
    public void SetPolarityToLight()
    {
        currentPolarity = Polarity.Light;
    }


    /// <summary>
    /// Forcibly sets the polarity to dark side.
    /// </summary>
    public void SetPolarityToDark()
    {
        currentPolarity = Polarity.Dark;
    }
}
