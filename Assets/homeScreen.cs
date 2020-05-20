using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine;

public class homeScreen : MonoBehaviour
{
    [SerializeField]
    Button Exit;

    [SerializeField]
    Button HTP;

    [SerializeField]
    Button Play;

    public void endGame()
    {
        Application.Quit();   
    }
    public void help()
    {
        SceneManager.LoadScene("Help");
    }

    public void play()
    {
        SceneManager.LoadScene("TestScene");
    }

}
