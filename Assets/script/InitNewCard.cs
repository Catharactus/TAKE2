using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitNewCard : MonoBehaviour
{
    public void InitCard(string Tag)
    {
        Debug.Log(FindCorrectInitPreset(Tag));
    }

    int FindCorrectInitPreset(string Tag)
    {

        int preset = 0;

        if(Tag == "work")
        {
            preset = 1;
            return preset;
        }

        if(Tag == "mood")
        {
            preset = 2;
            return preset;
        }

        if (Tag == "leisure")
        { 
            preset = 3;
            return preset;
        }

        return preset;

    }
}
