using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine;

public class helpManager : MonoBehaviour
{
    [SerializeField]
    Button Home;

    // Start is called before the first frame update
    void Start()
    {
        Home.onClick.AddListener(home);   
    }

    void home()
    {
        SceneManager.LoadScene("Home");
    }
}
