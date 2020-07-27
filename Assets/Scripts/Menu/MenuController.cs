using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    [SerializeField] private Config config;
    public void ButtonLeft()
    {
        config.DefIdImagePac();
    }

    public void ButtonRight()
    {
        config.AddIdImagePac();
    }

    public void ButtonStart()
    {
        SceneManager.LoadScene(1);
    }
}
