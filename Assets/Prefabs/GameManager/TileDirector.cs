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

    float formationTimer;


    //Should be same z value as camera
    float formationDespawnThreshold = -30.0f;
    private void Start()
    {
        for(int i = 0; i < 30; i++)
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
        foreach(GameObject t in tiles)
        {
           if(t.transform.position.z < formationDespawnThreshold)
            {
                t.SetActive(false);
            }
        }
        if (currentFormation && formationTimer < Time.realtimeSinceStartup)
        {
            //2.68 base
            //As scroll speed is multiplied by x, delay is divided by x
            float delay = 1.34f;
            formationTimer = Time.realtimeSinceStartup + delay;
            foreach (Formation.formationDetail f in currentFormation.formationDetails)
            {
                GameObject tile = getReadyTile();
                tile.transform.position = f.formationPositionDetails;
                tile.transform.localScale = f.formationScaleDetails;
                tile.GetComponent<PolarityToggle>().defaultPolarity = f.polarities[Random.Range(0,f.polarities.Count)];
                tile.SetActive(true);
            }
        }
    }

}
