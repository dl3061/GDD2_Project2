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

    private void Update()
    {
        Exit.onClick.AddListener(endGame);
        HTP.onClick.AddListener(help);
        Play.onClick.AddListener(play);
    }

    void endGame()
    {
        Application.Quit();   
    }
    void help()
    {
        SceneManager.LoadScene("Help");
    }

    void play()
    {
        SceneManager.LoadScene("TestScene");
    }

}
