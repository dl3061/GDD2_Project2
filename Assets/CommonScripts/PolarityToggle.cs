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
    public Polarity defaultPolarity = Polarity.Light;

    public GameObject gameObject;

    public Material material;

    public float alpha = 0; //controlls visual state
    bool isTransitioning = false;

    // The current polarity
    protected Polarity currentPolarity;

    /// <summary>
    /// Start is called before the first frame update
    /// </summary>
    protected virtual void Start()
    {
        // Initialize the game object activeness to the default polarity
        UpdateObjectActiveness(defaultPolarity);

        // Set current polarity to the default polarity
        currentPolarity = defaultPolarity;
    }


    /// <summary>
    /// Update is called once per frame
    /// </summary>
    protected virtual void Update()
    {
        // Reinforce the current active polarity, unless neutral (in which case do nothing).
        UpdateObjectActiveness(currentPolarity);

        if(isTransitioning)
        {
            if (currentPolarity == Polarity.Light)
            {
                alpha += 0.01f;
            }
            else
            {
                alpha -= 0.01f;
            }

            Debug.Log(alpha);

            //player material lerp
            material.SetFloat("Vector1_9152EDBB", Mathf.Lerp(0, 1, alpha));

            //platform material lerp
            if (currentPolarity != defaultPolarity)
            {
                material.SetFloat("Vector1_7E84E80", Mathf.Lerp(0.4f, 1, alpha));
            }
            else
            {
                material.SetFloat("Vector1_7E84E80", Mathf.Lerp(1, 0.4f, alpha));
            }

            

            if ((alpha > 1 && currentPolarity == Polarity.Light) || (alpha < 0 && currentPolarity == Polarity.Dark))
            {
                isTransitioning = false;
                if (currentPolarity == Polarity.Light)
                {
                    currentPolarity = Polarity.Dark;
                }
                else
                {
                    currentPolarity = Polarity.Light;
                }
            }
        }
    }


    /// <summary>
    /// OnValidate is called in editor. 
    /// </summary>
    private void OnValidate()
    {
        UpdateObjectActiveness(defaultPolarity);
    }


    /// <summary>
    /// Given input polarity, updates the three children using that polarity;
    /// </summary>
    /// <param name="polarity"></param>
    private void UpdateObjectActiveness(Polarity polarity)
    {
        //if (neutralObject != null)
            //neutralObject.SetActive(polarity == Polarity.Neutral);

       // if (lightObject != null)
           // lightObject.SetActive(polarity == Polarity.Light);

        //if (darkObject != null)
            //darkObject.SetActive(polarity == Polarity.Dark);
    }


    #region public functions

    /// <summary>
    /// Reset Event Handler. Call when resetting (ie restarting) the game.
    /// </summary>
    public virtual void ResetEvent()
    {
        currentPolarity = defaultPolarity;
    }


    /// <summary>
    /// Toggles the polarity to the opposite polarity.
    /// </summary>
    public void TogglePolarity()
    {
        if(isTransitioning == false)
        {
            isTransitioning = true;
        }

        //if (currentPolarity == Polarity.Light)
        //{
        //    currentPolarity = Polarity.Dark;
        //}
        //else if (currentPolarity == Polarity.Dark)
        //{
        //    currentPolarity = Polarity.Light;
        //}
        //else
        //{
        //    // Neutral case. Do nothing.
        //}
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

    #endregion
}
