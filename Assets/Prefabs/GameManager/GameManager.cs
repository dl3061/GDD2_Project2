using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Singleton
    {
        get; private set;
    }



    /// <summary>
    /// Awake is called before start. 
    /// Initialize references here.
    /// </summary>
    private void Awake()
    {
        GameManager.Singleton = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
