using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Clear : MonoBehaviour
{
    public GameObject boss;
    public GameObject image;
    Image fade;
    float alpha;
    bool fadein;
    bool fadeout = true;
    // Start is called before the first frame update
    void Start()
    {
        fade = image.GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        
        if (fadeout)
        {
            alpha += 0.005f;
            if (boss.GetComponent<bossAI>().BossHp <= 0)
            {
                fade.color = new Color(0.0f, 0.0f, 0.0f, alpha);
                if (alpha >= 1)
                {
                    fadeout=false;
                }
            }
        }
        else
        {
            SceneManager.LoadScene("result");
        }
        
        
    }
}
