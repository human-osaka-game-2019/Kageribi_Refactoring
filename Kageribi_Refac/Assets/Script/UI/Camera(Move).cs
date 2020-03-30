using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    public Camera Main_camera;//カメラを格納する変数。
    [SerializeField] GameObject Main_Camera_Move;//カメラの移動に関するあれこれをやるためにGameObjectとしてカメラを格納する。
    public GameObject[] Player;
    public GameObject PlayerCollider;
    public GameObject empty;
    public GameObject[] enemy;
    public Vector3 Player_pos;
    Vector3 kotei_pos;
    

    [SerializeField, Range(0.1f, 10.0f)] float Camera_aspet;//カメラのアスペクトの量を変更できるようにしたもの。

    bool move_side;
    bool move_height;
    bool kotei;
    bool size;
    public bool camera_return;
    public float speed;
    public int enemycount;
    void Start()
    {
        kotei_pos = new Vector3(55f, 5f, -10f);
        Main_camera = GameObject.FindWithTag("MainCamera").GetComponent<Camera>();
        enemycount = enemy.Length;
    }

    // Update is called once per frame
    void Update()
    {
        
        float step = speed * Time.deltaTime;

        
        if (kotei != false)
        {

            Main_Camera_Move.transform.position = Vector3.MoveTowards(Main_Camera_Move.transform.position, kotei_pos, step);
            if (Main_Camera_Move.transform.position == kotei_pos)
            {
                kotei = false;
            }

        }
        
        //if (move_side != false)
        //{
        //    if (Main_Camera_Move.transform.position.x > 55f)
        //    {
        //        move_side = true;
        //    }
        //    else
        //    {
        //        //Main_camera.orthographicSize += Camera_aspet;
        //        Main_Camera_Move.transform.Translate(0.1f, 0, 0);
        //    }
        //}
        //if (move_height != false)
        //{
        //    if (Main_Camera_Move.transform.position.y > 5f)
        //    {
        //        move_height = true;
        //    }
        //    else
        //    {
        //        Main_Camera_Move.transform.Translate(0, 0.1f, 0);
        //    }
        //}

        if (size == true)
        {
            if (Main_camera.orthographicSize >= 10)
            {
                size = false;
            }
            else
            {
                Main_camera.orthographicSize += 0.1f;

            }
        }

        if (enemycount == 0)
        {
            CameraReturn();
        }

        if (camera_return == true)
        {
            Vector3 Playerpos;
            if (Player[0].activeSelf == true)
            {
                
                Playerpos = new Vector3(Player[0].transform.position.x, Player[0].transform.position.y + 2.6f, Player[0].transform.position.z);
                Main_Camera_Move.transform.position = Vector3.MoveTowards(Main_Camera_Move.transform.position, Playerpos, step);
                Main_Camera_Move.transform.position = new Vector3(Main_Camera_Move.transform.position.x, Main_Camera_Move.transform.position.y, -10f);
            }
            else
            {
                Playerpos = new Vector3(Player[1].transform.position.x, Player[1].transform.position.y + 2.6f, Player[1].transform.position.z);
                Main_Camera_Move.transform.position = Vector3.MoveTowards(Main_Camera_Move.transform.position, Playerpos, step);
                Main_Camera_Move.transform.position = new Vector3(Main_Camera_Move.transform.position.x, Main_Camera_Move.transform.position.y, -10f);
            }

            //if (Main_Camera_Move.transform.position.x <= Player.transform.position.x)
            //{
            //    Main_Camera_Move.transform.Translate(0.1f, 0, 0);
            //}
            //if (Main_Camera_Move.transform.position.x >= Player.transform.position.x)
            //{
            //    Main_Camera_Move.transform.Translate(-0.1f, 0, 0);
            //}

            //if (Main_Camera_Move.transform.position.y <= Player.transform.position.y + 3)
            //{
            //    Main_Camera_Move.transform.Translate(0, 0.1f, 0);
            //}
            //if (Main_Camera_Move.transform.position.y >= Player.transform.position.y + 3)
            //{
            //    Main_Camera_Move.transform.Translate(0, -0.1f, 0);
            //}

            if (Main_camera.orthographicSize >= 5)
            {
                Main_camera.orthographicSize -= 0.12f;
            }
            else
            {
                camera_return = false;
                Main_camera.GetComponent<Cameramove>().enabled = true;
                GetComponent<BoxCollider2D>().enabled = false;
                empty.GetComponent<BoxCollider2D>().enabled = false;
            }
            
            //Main_camera.transform.parent = Player.transform;
            PlayerCollider.SetActive(false);

        }



    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Main_camera.GetComponent<Cameramove>().enabled = false;
            move_side = true;
            move_height = true;
            kotei = true;
            size = true;
            //Main_camera.transform.parent = null;
            PlayerCollider.SetActive(true);
        }
    }

    public void CameraReturn()
    {
        camera_return = true;

    }
    public void destroycount()
    {
        enemycount -= 1;
    }
}