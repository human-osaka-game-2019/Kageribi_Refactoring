using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleControl : MonoBehaviour
{
    public GameObject TitleNoon, TitleNight;
    public GameObject UI_Start;

    public float UI_switch;
    public float Title_switch;

    private float UI_seconds;
    private float Title_seconds;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        UI_seconds += 1.0f * Time.deltaTime;
        Title_seconds += 1.0f * Time.deltaTime;

        // Uiの点滅
        if (UI_seconds >= UI_switch)
        {
            if (UI_Start.activeSelf == true)
            {
                UI_Start.SetActive(false);
            }
            else
            {
                UI_Start.SetActive(true);
            }

            UI_seconds = 0.0f;
        }
        
        // タイトルの切り替え
        if (Title_seconds >= Title_switch)
        {
            if (TitleNoon.activeSelf == true)
            {
                TitleNoon.SetActive(false);
            }
            else
            {
                TitleNoon.SetActive(true);
            }

            Title_seconds = 0.0f;
        }

        if (Input.GetButtonDown("XBOXA"))
        {
            SceneManager.LoadScene("Master Release");
        }

        if (Input.GetMouseButtonDown(0))
        {
            SceneManager.LoadScene("Master Release");
        }
    }
}
