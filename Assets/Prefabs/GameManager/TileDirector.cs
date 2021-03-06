﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileDirector : MonoBehaviour
{
    [SerializeField]
    GameObject powerupPrefab;

    private GameObject powerup;

    [SerializeField]
    GameObject tilePrefab;
    [SerializeField]
    List<GameObject> tiles;
    [SerializeField]
    Formation currentFormation;
    [SerializeField]
    Formation starter;
    Object[] formations;

    private float powerUpTimer = 10;
    private float powerUPDelay = 10;
    private float formationDistanceTravelled;

    private float formationDistanceToNext;

    //Should be same z value as camera
    float formationDespawnThreshold = -40.0f;

    private void Start()
    {
        formations = Resources.LoadAll("Formations/base", typeof(Formation));

        formationDistanceTravelled = 0f;
        formationDistanceToNext = 0f;

        powerup = Instantiate(powerupPrefab);
        powerup.SetActive(false);
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
            List<Polarity> polarityIndexes = new List<Polarity>();
            
            Random rng = new Random();
            List<int> xCoords = new List<int> { -2, 0, 2 };
            int n = xCoords.Count;
            while (n > 1)
            {
                n--;
                int k = Random.Range(0,n+1);
                int v = xCoords[k];
                xCoords[k] = xCoords[n];
                xCoords[n] = v;
            }

            foreach (Formation.formationDetail f in currentFormation.formationDetails)
            {
                GameObject tile = getReadyTile();
                tile.transform.position = f.formationPositionDetails;
                tile.transform.localScale = f.formationScaleDetails;
                if (currentFormation.randomizeLanes && tile.transform.localScale.x == 2)
                {
                    tile.transform.position = new Vector3(xCoords[((int)tile.transform.position.x/2) + 1], tile.transform.position.y, tile.transform.position.z);
                }
                if (f.syncPolarity != 0)
                {
                    switch (f.syncPolarity)
                    {
                        case 1:
                            tile.GetComponent<PolarityToggle>().defaultPolarity = polarityIndexes[f.copyIndex];
                            break;
                        case -1:
                            if(polarityIndexes[f.copyIndex] == Polarity.Light)
                            {
                                tile.GetComponent<PolarityToggle>().defaultPolarity = Polarity.Dark;
                            } else if(polarityIndexes[f.copyIndex] == Polarity.Dark)
                            {
                                tile.GetComponent<PolarityToggle>().defaultPolarity = Polarity.Light;
                            } else
                            {
                                Polarity[] p = new Polarity[2] { Polarity.Light, Polarity.Dark };
                                tile.GetComponent<PolarityToggle>().defaultPolarity = p[Random.Range(0, 2)];
                            }              
                            break;
                    }
                    
                } else { 
                 
                    tile.GetComponent<PolarityToggle>().defaultPolarity = f.polarities[Random.Range(0, f.polarities.Count)];
                }
                polarityIndexes.Add(tile.GetComponent<PolarityToggle>().defaultPolarity);
                tile.GetComponent<PolarityToggle>().ResetEvent();
                tile.SetActive(true);
                if(powerUpTimer < Time.timeSinceLevelLoad)
                {
                    if(tile.transform.localScale.y <= 2 && tile.GetComponent<PolarityToggle>().defaultPolarity != Polarity.Neutral && tile.GetComponent<PolarityToggle>().defaultPolarity != GetComponent<GameManager>().ActivePlayer.GetComponent<PlayerPolarity>().CurrentPolarity)
                    {
                        powerUpTimer = Time.timeSinceLevelLoad + powerUPDelay;
                        powerup.transform.position = new Vector3(tile.transform.position.x, tile.transform.position.y+2, tile.transform.position.z);
                        powerup.SetActive(true);
                    }
                }
            }
            Debug.Log(currentFormation);
            formationDistanceToNext = currentFormation.GetLength();
            if (GetComponent<GameManager>().ActivePlayer.transform.position.y > 5)
            {
                currentFormation = starter;
            } else
            {
                currentFormation = (Formation)formations[Random.Range(0, formations.Length)];
            }

            formationDistanceTravelled = 0f;
            
        } 
    }

}
