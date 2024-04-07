using System.Collections;
using System.Collections.Generic;
using UnityEditor.Presets;
using UnityEngine;

public class InitNewCard : MonoBehaviour
{
    private string DayTracker;

    List<GameObject> allCardForToday;

    public void InitCard(string CardTag)
    {

        //reset allCardForToday
        allCardForToday = new List<GameObject>();
        //Debug.Log("card before" + allCardForToday.Count);

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
        DayTracker = "one";
    }

    private void GetAllCardForToday()
    {
        //assign all card for today to allcardforToday

        GameObject[] allObjGame = GameObject.FindObjectsOfType<GameObject>();

        foreach (GameObject obj in allObjGame)
        {
            string objTag = obj.tag;

            if(objTag.Contains(DayTracker))
            {
                //add gameobject to allCardsforToday
                allCardForToday.Add(obj);
            }
        }

        //Debug.Log("Card after add" + allCardForToday.Count);
    }

    private void InitCardAsWork()
    {
        //in allcardForToday find the one that are work related 
        List<GameObject> workCard = new List<GameObject>();

        Vector3 instancedPosition = new Vector3(transform.position.x, transform.position.y, transform.position.z - 0.5f);

        foreach (GameObject obj in allCardForToday)
        {
            if (obj.tag.Contains("work"))
            {
                Instantiate(obj, instancedPosition, obj.transform.rotation);
                workCard.Add(obj);
            }
        }
    }

    private void InitCardAsMood()
    {
        //in allcardForToday find the one that are mood related 
        List<GameObject> moodCard = new List<GameObject>();

        foreach (GameObject obj in allCardForToday)
        {
            if (obj.tag.Contains("mood"))
            {
                Instantiate(obj, transform.position, obj.transform.rotation);
                moodCard.Add(obj);
            }
        }
    }

    private void InitCardAsLeisure()
    {
        //in allcardForToday find the one that are leisure related 
        List<GameObject> leisureCard = new List<GameObject>();

        foreach (GameObject obj in allCardForToday)
        {
            if (obj.tag.Contains("leisure"))
            {
                Instantiate(obj, transform.position, obj.transform.rotation);
                leisureCard.Add(obj);
            }
        }
    }

    private void InstanceCard(GameObject[] cards, GameObject[] Positions)
    {
        //instance first card at the first position

        //instance the second card if there is at pos 2 
    }
}
