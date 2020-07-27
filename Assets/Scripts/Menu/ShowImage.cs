using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowImage : MonoBehaviour
{
    [SerializeField] private Config config;
    void Update()
    {
        GetComponent<Image>().sprite = Resources.LoadAll("Images/Sprite-Tetris" + config.GetIdImagePac(), typeof(Sprite))[0] as Sprite;
    }
}
