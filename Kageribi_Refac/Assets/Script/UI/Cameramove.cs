using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cameramove : MonoBehaviour
{
    public GameObject gold;
    public GameObject silver;
    public GameObject camera_move;
    

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Vector3 playerPos;
        if (gold.activeSelf == true)
        {
            playerPos = gold.transform.position;
            transform.position = new Vector3(playerPos.x, playerPos.y+2.6f, transform.position.z);
            playerPos = transform.position;
            if (transform.position.y < 0) 
            {
                playerPos.y = 0.1f;
                //playerPos.x = 0.1f;
                transform.position = playerPos;
            }
            if (transform.position.x < 0)
            {
                playerPos.x = 0.1f;
                transform.position = playerPos;
            }
        }
        else if (silver.activeSelf == true)
        {
            playerPos = silver.transform.position;
            transform.position = new Vector3(playerPos.x, playerPos.y+2.6f, transform.position.z);
            playerPos = transform.position;
            if (transform.position.y < 0 ) 
            {
                playerPos.y = 0.1f;
                transform.position = playerPos;
            }
            if(transform.position.x < 0)
            {
                playerPos.x = 0;
                transform.position = playerPos;
            }
        }

        
    }
}
