using System.Collections;
using System.Collections.Generic;
using UnityEditor.Presets;
using UnityEngine;

public class InitNewCard : MonoBehaviour
{
    private string DayTracker;

    List<GameObject> allCardForToday;
    List<GameObject> todaysCard;

    public void InitCard(string CardTag)
    {
        //Debug.Log("init for tag " + CardTag);

        //reset allCardForToday
        allCardForToday = new List<GameObject>();

        GameObject level = GameObject.FindGameObjectWithTag("level");
        LevelKing levelKing = level.GetComponent<LevelKing>();
        if(todaysCard != null )
        {
            levelKing.DealWithYesterdayCards(todaysCard);
        }

        todaysCard = new List<GameObject>();
        //Debug.Log("card before" + allCardForToday.Count);

        //Debug.Log(FindCorrectInitPreset(Tag));
        int preset = FindCorrectInitPreset(CardTag);

        UpdateDaytracker();
        //Debug.Log("dayTracker equal after update " + DayTracker);

        GetAllCardForToday();
        //Debug.Log("Card after add" + allCardForToday.Count);

        switch (preset)
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
        //Debug.Log(CardTag);

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
        GameObject level = GameObject.FindGameObjectWithTag("level");
        LevelKing levelKing = level.GetComponent<LevelKing>();

        //get the day by day tracker 
        DayTracker = levelKing.GetCurrentDay();
    }

    private void GetAllCardForToday()
    {
        //assign all card for today to allcardforToday

        GameObject[] allObjGame = GameObject.FindObjectsOfType<GameObject>();

        //Debug.Log("todays day " + DayTracker);

        foreach (GameObject obj in allObjGame)
        {
            string objTag = obj.tag;

            if(objTag.Contains(DayTracker))
            {
                //add gameobject to allCardsforToday
                allCardForToday.Add(obj);
            }
        }

    }

    private void InitCardAsWork()
    {
        Vector3 instancedPosition = new Vector3(transform.position.x, transform.position.y, transform.position.z - 0.5f);

        foreach (GameObject obj in allCardForToday)
        {
            if (obj.tag.Contains("_work"))
            {
                obj.transform.position = instancedPosition;
                todaysCard.Add(obj);

                //Debug.Log(obj);

                Transform first_child = obj.transform.GetChild(0);
                Transform drag_helper = first_child.transform.GetChild(0);
                Debug.Log(drag_helper);

                dragin_object script = drag_helper.GetComponent<dragin_object>();
                script.InitBlok();
            }
        }
    }

    private void InitCardAsMood()
    {
        Vector3 instancedPosition = new Vector3(transform.position.x, transform.position.y, transform.position.z - 0.5f);

        foreach (GameObject obj in allCardForToday)
        {
            if (obj.tag.Contains("_mood"))
            {
                obj.transform.position = instancedPosition;
                todaysCard.Add(obj);

                Transform first_child = obj.transform.GetChild(0);
                Transform drag_helper = first_child.transform.GetChild(0);

                dragin_object script = drag_helper.GetComponent<dragin_object>();
                script.InitBlok();
            }
        }
    }

    private void InitCardAsLeisure()
    {
        Vector3 instancedPosition = new Vector3(transform.position.x, transform.position.y, transform.position.z - 0.5f);

        foreach (GameObject obj in allCardForToday)
        {
            if (obj.tag.Contains("_leisure"))
            {
                obj.transform.position = instancedPosition;
                todaysCard.Add(obj);

                Transform first_child = obj.transform.GetChild(0);
                Transform drag_helper = first_child.transform.GetChild(0);

                dragin_object script = drag_helper.GetComponent<dragin_object>();
                script.InitBlok();
            }
        }
    }

    private void InstanceCard(GameObject[] cards, GameObject[] Positions)
    {
        //instance first card at the first position

        //instance the second card if there is at pos 2 
    }
}
