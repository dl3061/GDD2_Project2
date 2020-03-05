using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPlatform : MonoBehaviour
{
    [Tooltip("How long this platform will exist before it is unmade.")]
    public float existenceTime;
    private float existenceTimer = 0f;

    [Tooltip("The tiles under this platform to activate/deactive.")]
    public List<GameObject> tiles;
    private bool isPlatformActive;

    // Start is called before the first frame update
    void Start()
    {
        existenceTimer = 0f;
        isPlatformActive = true;
    }

    // Update is called once per frame
    void Update()
    {
        // Update whetehr or not isPlatformActive
        if (isPlatformActive)
        {
            // If we run out of time or if the player jumps, get rid of the platform
            existenceTimer += Time.deltaTime;
            if (existenceTimer > existenceTime)
                isPlatformActive = false;

            if (GameManager.Singleton.ActivePlayer?.GetComponent<PlayerMovement>() != null
                && (GameManager.Singleton.ActivePlayer.GetComponent<PlayerMovement>().IsWaiting() == false))
            {
                isPlatformActive = false;
            }
        }

        // Update tile existence
        foreach (GameObject tile in tiles)
        {
            tile.SetActive(isPlatformActive);
        }
    }


    /// <summary>
    /// OnValidate is run when inspector changes are made.
    /// Populate tiles with child components assumed to be tiles.
    /// </summary>
    private void OnValidate()
    {
        tiles = new List<GameObject>();
        for (int i = 0; i < transform.childCount; i++)
        {
            GameObject child = transform.GetChild(i).gameObject;

            if (child.name.Contains("Tile"))    // Might change this. This is hacky as heck.
                tiles.Add(child);
        }
    }


    /// <summary>
    /// Reset Event Handler
    /// Reactive the spawn platform and let the timer loose.
    /// </summary>
    public void ResetEventHandler()
    {
        existenceTimer = 0f;
        isPlatformActive = true;
    }
}
