using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoButton : MonoBehaviour
{

    Vector3 mousePositionOffset;
    int currentDay;

    LevelKing level;

    private void Start()
    {
        currentDay = 1;

        GameObject levelHolder = GameObject.FindGameObjectWithTag("level");
        level = levelHolder.GetComponent<LevelKing>();
        level.UpdateCurrentDay(currentDay);
        
    }

    private void OnMouseDown()
    {
        currentDay++;

        level.UpdateCurrentDay(currentDay);

        GameObject Onglet = GameObject.FindGameObjectWithTag("onglet");
        OngletDelaer scriptOnglet = Onglet.GetComponent<OngletDelaer>();
        scriptOnglet.InitOnglets();
    }

}
