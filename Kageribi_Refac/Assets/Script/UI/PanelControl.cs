using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelControl : MonoBehaviour
{
    public Image ImagePanel;

    private float alpha = 0.0f;
    public float speed;

    // Start is called before the first frame update
    void Start()
    {
        // パネルの表示を有効にする
        ImagePanel.enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (alpha < 0.5)
        {
            StartFadeOut();
        }
    }
    void StartFadeOut()
    {
        alpha += speed;
        ImagePanel.color = new Color(0.0f, 0.0f, 0.0f, alpha);
    }
}
