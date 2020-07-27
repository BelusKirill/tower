using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Config", menuName = "Config", order = 51)]
public class Config : ScriptableObject
{
    private const int MaxIdImagePac = 4;
    private const int MinIdImagePac = 2;

    [SerializeField] private int _idImagePac;

    public int GetIdImagePac()
    {
        return _idImagePac;
    }

    public void AddIdImagePac()
    {
        if (_idImagePac + 1 > MaxIdImagePac)
        {
            _idImagePac = MinIdImagePac;
        }
        else
        {
            _idImagePac++;
        }
    }

    public void DefIdImagePac()
    {
        if (_idImagePac - 1 < MinIdImagePac)
        {
            _idImagePac = MaxIdImagePac;
        }
        else
        {
            _idImagePac--;
        }
    }
}
