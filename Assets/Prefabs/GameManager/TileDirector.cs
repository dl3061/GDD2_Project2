using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileDirector : MonoBehaviour
{
    [SerializeField]
    GameObject tilePrefab;
    [SerializeField]
    List<GameObject> tiles;
    [SerializeField]
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
            foreach(Formation.formationDetail f in currentFormation.formationDetails)
            {
                GameObject tile = getReadyTile();
                tile.transform.position = f.formationPositionDetails;
                tile.transform.localScale = f.formationScaleDetails;
                tile.GetComponent<PolarityToggle>().defaultPolarity = f.polarity;
                tile.SetActive(true);
            }
            currentFormation = null;
        }
    }

}
