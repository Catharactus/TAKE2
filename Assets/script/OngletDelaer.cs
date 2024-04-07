using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class OngletDelaer : MonoBehaviour
{
    GameObject workOnglet;
    GameObject moodOnglet;
    GameObject leisureOnglet;

    // Start is called before the first frame update
    void Start()
    {
        InitOnglets();
    }

    // Update is called once per frame
    public void InitOnglets()
    {
        workOnglet = GameObject.FindGameObjectWithTag("work");
        moodOnglet = GameObject.FindGameObjectWithTag("mood");
        leisureOnglet = GameObject.FindGameObjectWithTag("leisure");

        //Debug.Log(workOnglet + ", " + moodOnglet + ", " + leisureOnglet);

        GameObject[] onglets = new GameObject[] { workOnglet, moodOnglet, leisureOnglet };

        foreach(GameObject onglet in onglets)
        {
            InitNewCard script = onglet.GetComponent<InitNewCard>();
            script.InitCard(onglet.tag);
        }
    }
}
