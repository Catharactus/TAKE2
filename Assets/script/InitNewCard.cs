using System.Collections;
using System.Collections.Generic;
using UnityEditor.Presets;
using UnityEngine;

public class InitNewCard : MonoBehaviour
{
    public void InitCard(string CardTag)
    {
        //Debug.Log(FindCorrectInitPreset(Tag));
        int preset = FindCorrectInitPreset(CardTag);

        switch(preset)
        {
            default:
                Debug.Log("cardFailled");
                break;

            case 1:
                InitCardAsWork();
                break;


            case 2:
                InitCardAsMood();
                break;

            case 3:
                InitCardAsLeisure();
                break;
        }

    }

    int FindCorrectInitPreset(string CardTag)
    {

        int preset = 0;

        if(CardTag == "work")
        {
            preset = 1;
            return preset;
        }

        if(CardTag == "mood")
        {
            preset = 2;
            return preset;
        }

        if (CardTag == "leisure")
        { 
            preset = 3;
            return preset;
        }

        return preset;
    }

    private void InitCardAsWork()
    {
        return;
    }

    private void InitCardAsMood()
    {
        return;
    }

    private void InitCardAsLeisure()
    {
        return;
    }
}
