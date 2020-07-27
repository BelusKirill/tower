using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class FigueConfig : MonoBehaviour
{
    public int Id;
    [SerializeField] private Config config;
    void Start()
    {
        GetComponent<SpriteRenderer>().sprite = Resources.LoadAll("Images/Sprite-Tetris" + config.GetIdImagePac(), typeof(Sprite))[Id] as Sprite;
    }
}
