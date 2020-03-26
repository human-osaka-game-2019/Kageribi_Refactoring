using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class bossAI : MonoBehaviour
{
    public RuntimeAnimatorController[] Boss;
    public float Change;
    public GameObject[] Player;
    public GameObject Attackjuge;
    public GameObject[] BossBulet;
    public GameObject BulletFactory;
    public GameObject tackle;
    public GameObject clear;
    tume script;
    goldfox Gscr;
    silverfox Sscr;

    public bothHP damage;
    Animator anim;
    public Animator[] Playeranim;

    bool isAnim;
    bool tacklestop;
    float movepatern;
    float movetime;
  
    float tacklespeed = 0.5f;
    public float tackletime;
    float ChangeTime;
    float ScaleX;    
    public double TacklePos;
    int bulletcount;

    public Vector3 playerpos;
    public Vector3 BulletPos;
    public Vector3 hitpos;
    public int BossHp;
    // Start is called before the first frame update
    void Start()
    {
        ScaleX = transform.localScale.x;
        anim = GetComponent<Animator>();
        Playeranim[0] = Player[0].GetComponent<Animator>();
        Playeranim[1] = Player[1].GetComponent<Animator>();
        script = Attackjuge.GetComponent<tume>();
        Gscr = Player[0].GetComponent<goldfox>();
        Sscr = Player[1].GetComponent<silverfox>();
    }

    // Update is called once per frame
    void Update()
    {
        float ChangeCount;

        BulletPos = BulletFactory.transform.position;
        ChangeTime += Time.deltaTime;
        tackletime += Time.deltaTime;
        movetime += Time.deltaTime;


        if (Player[0].activeSelf)
        {
            playerpos = Player[0].transform.position;
        }
        else if (Player[1].activeSelf)
        {
            playerpos = Player[1].transform.position;
        }
        if (BossHp <= 0)
        {
            anim.SetBool("dead", true);
            Gscr.enabled = false;
            Sscr.enabled = false;
            clear.SetActive(true);


        }

        if (movetime >= 10)
        {
            movepatern = Random.value;
            movetime = 0;
        }


        if (transform.position.x < playerpos.x || transform.position.x < playerpos.x)
        {
            Vector3 scale = transform.localScale;
            scale.x = ScaleX * -1.0f;
            transform.localScale = scale;
        }
        if (transform.position.x > playerpos.x || transform.position.x > playerpos.x)
        {
            Vector3 scale = transform.localScale;
            scale.x = ScaleX;
            transform.localScale = scale;
        }

        if (ChangeTime >= Change && !anim.GetCurrentAnimatorStateInfo(0).IsName("dead"))
        {
            ChangeCount = Random.value;
            if (ChangeCount <= 0.3)
            {
                anim.SetInteger("change", 1);
                anim.runtimeAnimatorController = Boss[0];
                ChangeTime = 0;
            }
            else if (ChangeCount >= 0.7)
            {
                anim.SetInteger("change", 1);
                anim.runtimeAnimatorController = Boss[2];
                ChangeTime = 0;
            }
            else
            {
                anim.SetInteger("change", 1);
                anim.runtimeAnimatorController = Boss[1];
                ChangeTime = 0;
            }


        }

        if (tackletime >= 30)
        {
            pos();
            if (transform.position.x > TacklePos && !anim.GetCurrentAnimatorStateInfo(0).IsName("tume") &&
                !anim.GetCurrentAnimatorStateInfo(0).IsName("longrange") && !anim.GetCurrentAnimatorStateInfo(0).IsName("dead"))
            {
                isAnim = true;
                anim.SetInteger("Attack", 2);
                transform.Translate(-tacklespeed, 0.0f, 0.0f);
                if (tackle.transform.position.x <= TacklePos || tacklestop == true)
                {
                    tackletime = 0;
                    anim.SetInteger("Attack", 0);
                    isAnim = false;
                    tacklestop = false;
                }
            }
            else if (transform.position.x < TacklePos && !anim.GetCurrentAnimatorStateInfo(0).IsName("tume") &&
                     !anim.GetCurrentAnimatorStateInfo(0).IsName("longrange") && !anim.GetCurrentAnimatorStateInfo(0).IsName("dead"))
            {
                Debug.Log("aaa");
                isAnim = true;
                anim.SetInteger("Attack", 2);
                transform.Translate(tacklespeed, 0.0f, 0.0f);
                if (tackle.transform.position.x >= TacklePos || tacklestop == true)
                {
                    tackletime = 0;
                    anim.SetInteger("Attack", 0);
                    isAnim = false;
                    tacklestop = false;
                }
            }

        }

        if (movepatern >= 0.6 && !anim.GetCurrentAnimatorStateInfo(0).IsName("tume") &&
                     !anim.GetCurrentAnimatorStateInfo(0).IsName("longrange") && !anim.GetCurrentAnimatorStateInfo(0).IsName("dead"))
        {
            anim.SetInteger("move", 1);
            if (transform.position.x < playerpos.x || transform.position.x < playerpos.x)
            {
                transform.Translate(0.1f, 0.0f, 0.0f);
            }
            if (transform.position.x > playerpos.x || transform.position.x > playerpos.x)
            {
                transform.Translate(-0.1f, 0.0f, 0.0f);
            }

            if (tackle.transform.position.x <= playerpos.x)
            {
                anim.SetInteger("move", 0);
                movepatern = 0;
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (anim.runtimeAnimatorController == Boss[0])
        {
            if (collision.gameObject.tag == ("AttackRange_Fire"))
            {
                GameObject prefab = Resources.Load("prefabs/DamageEffect_fire") as GameObject;
                Instantiate(prefab, this.gameObject.transform.position, Quaternion.identity);
                BossHp = BossHp - 2;
            }
            else if (collision.gameObject.tag == ("AttackRange_Water"))
            {
                GameObject prefab = Resources.Load("prefabs/DamageEffect_water") as GameObject;
                Instantiate(prefab, this.gameObject.transform.position, Quaternion.identity);
                BossHp = BossHp - 4;
            }
            else if (collision.gameObject.tag == ("AttackRange_Grass"))
            {
                GameObject prefab = Resources.Load("prefabs/DamageEffect_grass") as GameObject;
                Instantiate(prefab, this.gameObject.transform.position, Quaternion.identity);
                BossHp = BossHp - 1;
            }
        }

        else if (anim.runtimeAnimatorController == Boss[1])
        {
            if (collision.gameObject.tag == ("AttackRange_Fire"))
            {
                GameObject prefab = Resources.Load("prefabs/DamageEffect_fire") as GameObject;
                Instantiate(prefab, this.gameObject.transform.position, Quaternion.identity);
                BossHp = BossHp - 1;
            }
            else if (collision.gameObject.tag == ("AttackRange_Water"))
            {
                GameObject prefab = Resources.Load("prefabs/DamageEffect_water") as GameObject;
                Instantiate(prefab, this.gameObject.transform.position, Quaternion.identity);
                BossHp = BossHp - 2;
            }
            else if (collision.gameObject.tag == ("AttackRange_Grass"))
            {
                GameObject prefab = Resources.Load("prefabs/DamageEffect_grass") as GameObject;
                Instantiate(prefab, this.gameObject.transform.position, Quaternion.identity);
                BossHp = BossHp - 4;
            }
        }
        else if (anim.runtimeAnimatorController == Boss[2])
        {
            if (collision.gameObject.tag == ("AttackRange_Fire"))
            {
                GameObject prefab = Resources.Load("prefabs/DamageEffect_fire") as GameObject;
                Instantiate(prefab, this.gameObject.transform.position, Quaternion.identity);
                BossHp = BossHp - 4;
            }
            else if (collision.gameObject.tag == ("AttackRange_Water"))
            {
                    GameObject prefab = Resources.Load("prefabs/DamageEffect_water") as GameObject;
                    Instantiate(prefab, this.gameObject.transform.position, Quaternion.identity);
                    BossHp = BossHp - 1;
            }
            else if (collision.gameObject.tag == ("AttackRange_Grass"))
            {
                    GameObject prefab = Resources.Load("prefabs/DamageEffect_grass") as GameObject;
                    Instantiate(prefab, this.gameObject.transform.position, Quaternion.identity);
                    BossHp = BossHp - 2;
            }
            
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //foreach (ContactPoint2D point in collision.contacts)
        //{
        //    hitpos = point.point; 
        //}

        //if(collision.gameObject.tag == ("AttackRange_Fire")|| collision.gameObject.tag == ("AttackRange_Water")|| 
        //    collision.gameObject.tag == ("AttackRange_Grass"))
        //{
        //    Debug.Log("aaa");
        //    foreach (ContactPoint2D point in collision.contacts)
        //    {
        //        hitpos = point.point;
        //    }
        //}

        if (collision.gameObject.tag == "Player")
        {
            

            if (isAnim == true)
            {
                damage.BigDamage();
                tacklestop = true;
            }

            damage.NormalDamage();
        }
        

        

    }

    void pos()
    {
        TacklePos = playerpos.x;
    }

    public void shortrange()
    {
        anim.SetInteger("Attack", 1);
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("tume"))
        {   
            anim.SetInteger("Attack", 0);
            script.RangeTime();
        }
    }

    public void longrange()
    {
        anim.SetInteger("Attack", 3);
        bulletcount += 1;
        if (anim.runtimeAnimatorController == Boss[0] && !anim.GetCurrentAnimatorStateInfo(0).IsName("dead"))
        {
            Instantiate(BossBulet[0],BulletPos , Quaternion.identity);
            if (bulletcount < 3)
            {
                Invoke("longrange", 0.5f);
            }
            
            //if (bulletcount >= 3)
            else
            {
                anim.SetInteger("Attack", 0);                
                bulletcount = 0;
            }
            
        }
        else if (anim.runtimeAnimatorController == Boss[1] && !anim.GetCurrentAnimatorStateInfo(0).IsName("dead"))
        {
            Instantiate(BossBulet[1], BulletPos, Quaternion.identity);
            if (bulletcount < 3)
            {
                Invoke("longrange", 0.5f);
            }

            if (bulletcount >= 3)
            {
                anim.SetInteger("Attack", 0);
                bulletcount = 0;
            }
        }
        else if (anim.runtimeAnimatorController == Boss[2] && !anim.GetCurrentAnimatorStateInfo(0).IsName("dead"))
        {
            Instantiate(BossBulet[2], BulletPos, Quaternion.identity);
            if (bulletcount < 3)
            {
                Invoke("longrange", 0.5f);
            }

            if (bulletcount >= 3)
            {
                anim.SetInteger("Attack", 0);
                bulletcount = 0;
            }
        }
        
        
    }
    public void bulletdamege()
    {
        if (anim.runtimeAnimatorController==Boss[0])
        {
            if (Playeranim[0].runtimeAnimatorController == Gscr.Fire|| Playeranim[1].runtimeAnimatorController == Sscr.Fire)
            {
                damage.NormalDamage();
            }
            else if (Playeranim[0].runtimeAnimatorController == Gscr.Water|| Playeranim[1].runtimeAnimatorController == Sscr.Water)
            {
                damage.SmallDamage();
            }
            else if (Playeranim[0].runtimeAnimatorController == Gscr.Grass|| Playeranim[1].runtimeAnimatorController == Gscr.Grass)
            {
                damage.NormalDamage();
            }
        }
        else if (anim.runtimeAnimatorController == Boss[1])
        {
            if (Playeranim[0].runtimeAnimatorController == Gscr.Fire || Playeranim[1].runtimeAnimatorController == Sscr.Fire)
            {
                damage.NormalDamage();
            }
            else if (Playeranim[0].runtimeAnimatorController == Gscr.Water || Playeranim[1].runtimeAnimatorController == Sscr.Water)
            {
                damage.NormalDamage();
            }
            else if (Playeranim[0].runtimeAnimatorController == Gscr.Grass || Playeranim[1].runtimeAnimatorController == Gscr.Grass)
            {
                damage.SmallDamage();
            }
        }
        else if (anim.runtimeAnimatorController == Boss[2])
        {
            if (Playeranim[0].runtimeAnimatorController == Gscr.Fire || Playeranim[1].runtimeAnimatorController == Sscr.Fire)
            {
                damage.SmallDamage();
            }
            else if (Playeranim[0].runtimeAnimatorController == Gscr.Water || Playeranim[1].runtimeAnimatorController == Sscr.Water)
            {
                damage.NormalDamage();
            }
            else if (Playeranim[0].runtimeAnimatorController == Gscr.Grass || Playeranim[1].runtimeAnimatorController == Gscr.Grass)
            {
                damage.NormalDamage();
            }
        }
    }
    public void shortattack()
    {
        if (anim.runtimeAnimatorController == Boss[0])
        {
            if (Playeranim[0].runtimeAnimatorController == Gscr.Fire || Playeranim[1].runtimeAnimatorController == Sscr.Fire)
            {
                damage.NormalDamage();
            }
            else if (Playeranim[0].runtimeAnimatorController == Gscr.Water || Playeranim[1].runtimeAnimatorController == Sscr.Water)
            {
                damage.SmallDamage();
            }
            else if (Playeranim[0].runtimeAnimatorController == Gscr.Grass || Playeranim[1].runtimeAnimatorController == Gscr.Grass)
            {
                damage.BigDamage();
            }
        }
        else if (anim.runtimeAnimatorController == Boss[1])
        {
            if (Playeranim[0].runtimeAnimatorController == Gscr.Fire || Playeranim[1].runtimeAnimatorController == Sscr.Fire)
            {
                damage.BigDamage();
            }
            else if (Playeranim[0].runtimeAnimatorController == Gscr.Water || Playeranim[1].runtimeAnimatorController == Sscr.Water)
            {
                damage.NormalDamage();
            }
            else if (Playeranim[0].runtimeAnimatorController == Gscr.Grass || Playeranim[1].runtimeAnimatorController == Gscr.Grass)
            {
                damage.SmallDamage();
            }
        }
        else if (anim.runtimeAnimatorController == Boss[2])
        {
            if (Playeranim[0].runtimeAnimatorController == Gscr.Fire || Playeranim[1].runtimeAnimatorController == Sscr.Fire)
            {
                damage.SmallDamage();
            }
            else if (Playeranim[0].runtimeAnimatorController == Gscr.Water || Playeranim[1].runtimeAnimatorController == Sscr.Water)
            {
                damage.BigDamage();
            }
            else if (Playeranim[0].runtimeAnimatorController == Gscr.Grass || Playeranim[1].runtimeAnimatorController == Gscr.Grass)
            {
                damage.NormalDamage();
            }
        }
    }
}
