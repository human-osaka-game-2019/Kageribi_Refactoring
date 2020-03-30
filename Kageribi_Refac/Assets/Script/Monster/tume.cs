using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tume : MonoBehaviour
{
    GameObject Boss;
    public GameObject bothHP;
    bossAI script;
    bothHP Damage;
    public float attacktime;
    public Vector3 playerpos;
    public double attackjuge;
    public BoxCollider2D col;
    // Start is called before the first frame update
    void Start()
    {
        Boss = transform.root.gameObject;
        script = Boss.GetComponent<bossAI>();
        Damage = bothHP.GetComponent<bothHP>();
        col = GetComponent<BoxCollider2D>();
        col.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        attacktime += Time.deltaTime;
        if (attacktime >= 2)
        {
            Attack();
            
        }
        else
        {
            col.enabled = false;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            script.shortattack();            
        }
    }

    void Attack()
    {
        playerpos = script.playerpos;
        attackjuge = Boss.transform.position.x - playerpos.x;

        if (attackjuge <= 10 && attackjuge >= -10) 
        {
            if (attacktime >= 5)
            {
                script.shortrange();
                col.enabled = true;

            }
            
        }
        else
        {
            if (attacktime >= 5)
            {
                script.longrange();
                attacktime = 0;
                
            }
        }
    }

    public void RangeTime()
    {
        attacktime = 0;
        
    }
}
