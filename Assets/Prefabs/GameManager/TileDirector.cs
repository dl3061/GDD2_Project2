using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileDirector : MonoBehaviour
{
    public GameObject tilePrefab;
    [SerializeField]
    List<GameObject> tiles;
    Formation currentFormation;

    private void Start()
    {
        for(int i = 0; i < 10; i++)
        {
            GameObject tile = Instantiate(tilePrefab);
            tile.SetActive(false);
            tiles.Add(tile);
        }
    }

    GameObject getReadyTile()
    {
        GameObject returnObj = null;
        foreach(GameObject t in tiles)
        {
            if (!t.activeSelf)
            {
                returnObj = t;
            }
        }
        return returnObj;
        
    }

    private void Update()
    {
        if (currentFormation)
        {
            for(int i = 0; i < currentFormation.formationDetails.Count; i++)
            {
                //Loop through formation details and re-create tiles based on instructions 
            }
        }
    }

}
