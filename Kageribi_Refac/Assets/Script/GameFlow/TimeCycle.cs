using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeCycle : MonoBehaviour
{
    public Camera MainCamera;
    public GameObject Clock;
    public GameObject ClockHands;

    public float cycle_second;
    private float width, height;
    private float elapsed_seconds;

    // Start is called before the first elapsed_time update
    void Start()
    {
        // メインカメラのサイズを取得
        width = Screen.width * Camera.main.orthographicSize / (Screen.height / 2);
        height = Screen.height * Camera.main.orthographicSize / (Screen.height / 2);

        // オブジェクトの初期座標を設定
        //Clock.transform.position = new Vector3(
        //    MainCamera.transform.position.x + (width  / 2.0f) - (Clock.GetComponent<SpriteRenderer>().bounds.size.x / 2.0f),
        //    MainCamera.transform.position.y + (height  / 2.0f) - (Clock.GetComponent<SpriteRenderer>().bounds.size.y / 2.0f),
        //    2.0f);

        //ClockHands.transform.position = new Vector3(
        //   MainCamera.transform.position.x + (width / 2.0f) - (ClockHands.GetComponent<SpriteRenderer>().bounds.size.x / 2.0f),
        //   MainCamera.transform.position.y + (height / 2.0f) - (ClockHands.GetComponent<SpriteRenderer>().bounds.size.y / 2.0f),
        //   1.0f);

        // 時計を朝が上に来るように設定
        Clock.transform.eulerAngles = new Vector3(0.0f, 0.0f, 90.0f);
    }

    // Update is called once per elapsed_time
    void Update()
    {
        // フレームカウント
        elapsed_seconds += 1.0f * Time.deltaTime;

        // 時計を回す
        Clock.transform.Rotate(new Vector3(0.0f, 0.0f, 360.0f / -cycle_second * Time.deltaTime));

        // 誤差を修正
        if (elapsed_seconds >= cycle_second)
        {
            Clock.transform.eulerAngles = new Vector3(0.0f, 0.0f, 90.0f);

            elapsed_seconds = 0;
        }
    }
}
