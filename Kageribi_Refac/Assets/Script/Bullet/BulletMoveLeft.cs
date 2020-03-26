using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMoveLeft : MonoBehaviour
{
    public float moveSpeed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        MoveControl();
    }

    void MoveControl()
    {
        transform.Translate(Vector2.left * moveSpeed);
        transform.Translate(Vector2.down * moveSpeed);
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player" || col.gameObject.tag == "floor")
        {
            //gameObject.SetActive(false);
            Destroy(gameObject);
            Debug.Log("destroyed");
        }
    }

}
