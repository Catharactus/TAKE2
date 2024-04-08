using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelKing : MonoBehaviour
{
    public string currentDay = "one";
    
    string dayOne = "one";
    string dayTwo = "two";
    string dayThree = "three";
    string dayFour = "four";
    string dayFive = "five";

    public string GetCurrentDay()
    {
        return currentDay;
    }

    public void UpdateCurrentDay(int day)
    {
        switch(day)
        {
            case 1:
                currentDay = dayOne;
                break;
            case 2:
                currentDay = dayTwo;
                break;
            case 3:
                currentDay = dayThree;
                break;
            case 4:
                currentDay = dayFour;
                break;
            case 5: 
                currentDay = dayFive;
                break;
        }
    }

    public void DealWithYesterdayCards(List<GameObject> yesterdaysCards)
    {
        if(yesterdaysCards.Count != 0)
        {
            foreach(GameObject yesterday in yesterdaysCards)
            {

                yesterday.SetActive(false);

            }
        }
    }

}
