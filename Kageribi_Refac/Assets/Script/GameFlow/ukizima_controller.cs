using System.Collections;

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

 class ukizima_controller : MonoBehaviour
{
    public GameObject gold;
    public GameObject silver;
    public float timeout;
    public float time;
    float[] red = new float[2];
    float[] blue = new float[2];
    float[] green = new float[2];
    float Galfa = 1;
    float Salfa = 0;
    public float fadespeed = 0.001f;
    TilemapCollider2D gold_juge;
    TilemapCollider2D silver_juge;
    

    // Start is called before the first frame update
    void Start()
    {
        red[0] = gold.GetComponent<Tilemap>().color.r;
        red[1] = silver.GetComponent<Tilemap>().color.r;
        blue[0] = gold.GetComponent<Tilemap>().color.b;
        blue[1] = silver.GetComponent<Tilemap>().color.b;
        green[0] = gold.GetComponent<Tilemap>().color.g;
        green[1] = silver.GetComponent<Tilemap>().color.g;
        gold_juge = gold.GetComponent<TilemapCollider2D>();
        silver_juge = silver.GetComponent<TilemapCollider2D>();
        silver_juge.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        //time += 1;
        if (time >= timeout)
        { 
            if (gold_juge.enabled==true)
            {
                if (Salfa <= 0)
                {
                    silver_juge.enabled = true;

                }
                gold.GetComponent<Tilemap>().color = new Color(red[0], blue[0], green[0], Galfa);
                silver.GetComponent<Tilemap>().color = new Color(red[1], blue[1], green[1], Salfa);
                Galfa -= fadespeed;
                Salfa += fadespeed;
                if (Galfa <= 0)
                {
                    gold_juge.enabled = false;
                    time = 0;
                }
            }
            else
            {
                if (Galfa >= 1)
                {
                    gold_juge.enabled = true;
                }
                silver.GetComponent<Tilemap>().color = new Color(red[1], blue[1], green[1], Salfa);
                gold.GetComponent<Tilemap>().color = new Color(red[0], blue[0], green[0], Galfa);
                Salfa -= fadespeed;
                Galfa += fadespeed;

                if (Salfa <= 0)
                {
                    silver_juge.enabled=false;
                    time = 0;
                }
            }
            
        }

    }

}

