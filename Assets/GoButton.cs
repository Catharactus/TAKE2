using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoButton : MonoBehaviour
{

    Vector3 mousePositionOffset;
    int currentDay = 0;

    LevelKing level;

    private void Start()
    {
        currentDay = 1;

        GameObject levelHolder = GameObject.FindGameObjectWithTag("level");
        Debug.Log(levelHolder);
        level = levelHolder.GetComponent<LevelKing>();
        
    }

    private void OnMouseDown()
    {
        currentDay++;

        level.UpdateCurrentDay(currentDay);
    }

}
