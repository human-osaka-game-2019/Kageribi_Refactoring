using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotation : MonoBehaviour
{
    [System.Serializable]
    public class CameraObject
    {
        public Camera Camera;
        float width, half_width;

        public void Start()
        {
            // 画面の幅のサイズを取得
            width = Screen.width * Camera.main.orthographicSize / (Screen.height / 2);
            half_width = width / 2;
        }

        public float GetHalfWidth() { return half_width; }
    }

    [System.Serializable]
    public class RotationObject
    {
        public GameObject Texture;
        float half_width;

        public void ObtainWidth() // オブジェクトの幅の半分を格納
        {
            half_width = Texture.GetComponent<SpriteRenderer>().bounds.size.x / 2.0f;
        }

        public void SetPosition(CameraObject Main) // オブジェクトの座標を設定
        {
            Texture.transform.position = new Vector3(
                Main.Camera.transform.position.x - Main.GetHalfWidth() - half_width,
                Main.Camera.transform.position.y - 1.0f,
                10.0f);
        }

        public void Rotate(CameraObject Main, float seconds) // オブジェクトを回転させる
        {
            Texture.transform.RotateAround(
                new Vector3(
                    Main.Camera.transform.position.x,
                    Main.Camera.transform.position.y - Main.GetHalfWidth() - half_width - 1.0f,
                    0.0f),
                new Vector3(0.0f, 0.0f, 1.0f),
                45.0f / -seconds * Time.deltaTime);

            Texture.transform.Rotate(new Vector3(0.0f, 0.0f, 45.0f / seconds * Time.deltaTime));
        }

        public bool is_InTheScreen(CameraObject Main) // 画面上にオブジェクトがいるか
        {
            if (Texture.transform.position.x - half_width < Main.Camera.transform.position.x + Main.GetHalfWidth())
            {
                return true;
            }

            return false;
        }
    }

    int current_number = 0;
    public float seconds;

    public CameraObject Main = new CameraObject();
    public RotationObject Sun = new RotationObject();
    public RotationObject Moon = new RotationObject();
    RotationObject[] ObjectList;

    // Start is called before the first frame update
    void Start()
    {
        Main.Start();

        ObjectList = new RotationObject[2] { Sun, Moon };
        for (int i = 0; i < 2; i++)
        {
            ObjectList[i].ObtainWidth();
            ObjectList[i].SetPosition(Main);
        }
    }

    // Update is called once per frame
    void Update()
    {
        ObjectList[current_number].Rotate(Main, seconds);

        if (ObjectList[current_number].is_InTheScreen(Main) == false)
        {
            ObjectList[current_number].SetPosition(Main);
            if (++current_number > 1)
            {
                current_number = 0;
            }
        }
    }
}
