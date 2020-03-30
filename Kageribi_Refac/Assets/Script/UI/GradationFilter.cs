using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GradationFilter : MonoBehaviour
{
    [System.Serializable]

    public class BGFilter
    {
        public GameObject Filter;
        public bool isStart;
        public bool isMax;
        public float alpha;

        public void DrawTexture(float seconds)
        {
            Filter.GetComponent<Renderer>().material.color = new Color(1.0f, 1.0f, 1.0f, alpha);
            Filter.SetActive(true);

            ChangeAlphaValue(seconds);
        }

        void ChangeAlphaValue(float seconds)
        {
            if (isMax == false)
            {
                alpha += 1.0f / seconds * Time.deltaTime;
            }
            else
            {
                alpha -= 1.0f / seconds * Time.deltaTime;
            }

            CompareAlphaSize();
        }

        void CompareAlphaSize()
        {
            if (alpha >= 1.0f)
            {
                isMax = true;
                alpha = 1.0f;
            }

            if (alpha <= 0.0f)
            {
                isMax = false;
                isStart = false;
                alpha = 0.0f;
            }
        }
    }

    public float seconds;
    public float frame = 0;

    public BGFilter Morning = new BGFilter();
    public BGFilter Evening = new BGFilter();
    public BGFilter Night = new BGFilter();

    // Start is called before the first frame update
    void Start()
    {
        Morning.Filter.SetActive(false);
        Evening.Filter.SetActive(false);
        Night.Filter.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(Morning.isStart == true)
        {
            if(Morning.isMax == true)
            {
                Morning.DrawTexture(seconds);
            }
            else
            {
                frame += 1.0f * Time.deltaTime;
                Morning.DrawTexture(seconds * 1.25f);
            }

            if(frame >= seconds)
            {
                Morning.alpha = 1.0f;
                Morning.isMax = true;
            }

            if(Morning.alpha <= 0.0f)
            {
                Evening.isStart = true;
            }
        }

        if (Evening.isStart == true)
        {
            if(Evening.isMax == true)
            {
                Evening.DrawTexture(seconds);
            }
            else
            {
                frame += 1.0f * Time.deltaTime;
                Evening.DrawTexture(seconds * 1.25f);
            }

            if(frame >= seconds)
            {
                Evening.alpha = 1.0f;
                Evening.isMax = true;
                Night.isStart = true;
            }
        }

        if (Night.isStart == true)
        {
            if(Night.isMax == true)
            {
                Night.DrawTexture(seconds * 1.25f);
            }
            else
            {
                Night.DrawTexture(seconds);
            }

            if(frame >= seconds)
            {
                Night.alpha = 0.0f;
            }
            
            if(Night.alpha >= 1.0f)
            {
                Night.isMax = true;
                Morning.isStart = true;
            }
        }

        if(frame >= seconds)
        {
            frame = 0.0f;
        }
    }
}
