using System.Collections;
using System.Collections.Generic;
using UnityEditor.Presets;
using UnityEngine;

public class InitNewCard : MonoBehaviour
{
    private int DayTracker;

    GameObject[] allCardForToday;

    public void InitCard(string CardTag)
    {
        //Debug.Log(FindCorrectInitPreset(Tag));
        int preset = FindCorrectInitPreset(CardTag);

        UpdateDaytracker();

        GetAllCardForToday();

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

    private void UpdateDaytracker()
    {
        //ref the high level script

        //get the day by day tracker 

        //asign daytracker
    }

    private void GetAllCardForToday()
    {
        //assign all card for today to allcardforToday
    }

    private void InitCardAsWork()
    {
        //in allcardForToday find the one that are work related 

        //instance the first one 

        //instance the second one if there is 
        return;
    }

    private void InitCardAsMood()
    {
        //in allcardForToday find the one that are work related 

        //instance the first one 

        //instance the second one if there is 
        return;
    }

    private void InitCardAsLeisure()
    {
        //in allcardForToday find the one that are work related 

        //instance the first one 

        //instance the second one if there is 
        return;
    }

    private void InstanceCard(GameObject cards)
    {

    }
}
