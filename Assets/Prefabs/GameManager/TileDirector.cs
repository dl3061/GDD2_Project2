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
    Object[] formations;


    private float formationDistanceTravelled;

    private float formationDistanceToNext;

    //Should be same z value as camera
    float formationDespawnThreshold = -30.0f;

    private void Start()
    {
        formations = Resources.LoadAll("Formations",typeof(Formation));

        formationDistanceTravelled = 0f;
        formationDistanceToNext = 0f;
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
        if(returnObj == null)
        {
            GameObject i = Instantiate(tilePrefab);
            tiles.Add(i);
            returnObj = i;
        }
        return returnObj;
        
    }

    private void Update()
    {
        formationDistanceTravelled += Mathf.Abs(GameManager.Singleton.GetCurrentScrollSpeed() * Time.deltaTime);

        foreach (GameObject t in tiles)
        {
           if(t.transform.position.z < formationDespawnThreshold)
            {
                t.SetActive(false);
            }
        }


        if (currentFormation && formationDistanceTravelled >= formationDistanceToNext)
        {
            //2.68 base
            //As scroll speed is multiplied by x, delay is divided by x
            
            
            foreach (Formation.formationDetail f in currentFormation.formationDetails)
            {
                GameObject tile = getReadyTile();
                tile.transform.position = f.formationPositionDetails;
                tile.transform.localScale = f.formationScaleDetails;
                tile.GetComponent<PolarityToggle>().defaultPolarity = f.polarities[Random.Range(0,f.polarities.Count)];
                tile.GetComponent<PolarityToggle>().ResetEvent();
                tile.SetActive(true);
            }

            currentFormation = (Formation) formations[Random.Range(0, formations.Length)];

            formationDistanceTravelled = 0f;
            formationDistanceToNext = currentFormation.GetLength();
        } 
    }

}
