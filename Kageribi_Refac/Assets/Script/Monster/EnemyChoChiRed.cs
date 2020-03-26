using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyChoChiRed : MonoBehaviour
{
    int attackFlag = 0;
    int movementFlag = 0;

    public bool isTracing;
    //bool isAttacking;

    public float movePower;

    Animator animator;
    Rigidbody2D rigidbody;

    public RuntimeAnimatorController Thung;
    public RuntimeAnimatorController Fireball;
    public RuntimeAnimatorController isMoving;

    public GameObject player;
    public GameObject enemy;

    public GameObject bulletPLeft;
    public GameObject bulletPRight;

    public GameObject count;

    private float timer;
    private float waitingTime;
    string dist = "";

    public int hp = 15;

    public int great_damage = 4;
    public int normal_damage = 2;
    public int poor_damage = 1;

    /////////////////////////////////
    ///  Start, Update, Functions 
    /////////////////////////////////



    //////////////////////////   MOVE     ////////////////////////////




    public void MoveRoutine()
    {
        Vector3 moveVelocity = Vector3.zero;

        if (isTracing)
        {
            Vector3 playerPos = player.transform.position;

            if (playerPos.x < transform.position.x)
                dist = "Right";
            else if (playerPos.x > transform.position.x)
                dist = "Left";
        }
        else
        {
            if (movementFlag == 1)
                dist = "Right";
            else if (movementFlag == 2)
                dist = "Left";
        }

        if (dist == "Right")
        {
            moveVelocity = Vector3.left;
            transform.localScale = new Vector3(1, 1, 1);
        }
        else if (dist == "Left")
        {
            moveVelocity = Vector3.right;
            transform.localScale = new Vector3(-1, 1, 1);
        }

        transform.position += moveVelocity * movePower * Time.deltaTime;

    }


    public IEnumerator E_Movement()
    {
        movementFlag = Random.Range(0, 3);

        if (movementFlag == 0)
        {
            animator.SetBool("isMoving", false);
        }

        else if (movementFlag == 1)
        {
            animator.SetBool("isMoving", true);
        }

        else if (movementFlag == 2)
        {
            animator.SetBool("isMoving", true);
        }

        yield return new WaitForSeconds(3f);

        StartCoroutine(E_Movement());
    }

    //////////////////////////   ATTACK     ////////////////////////////


    void Attack()
    {
        Vector3 playerPos = player.transform.position;
        Vector3 enemyPos = enemy.transform.position;
        Vector3 moveVelocity = Vector3.zero;
        timer += Time.deltaTime;
        waitingTime = 0.2f;

        if (Mathf.Abs(playerPos.x - enemyPos.x) < 7 &&
            Mathf.Abs(playerPos.y - enemyPos.y) < 7)
        {
            if (timer > waitingTime)
            {
                if (Mathf.Abs(playerPos.x - enemyPos.x) <= 4 &&
                Mathf.Abs(playerPos.y - enemyPos.y) <= 4)
                {
                    attackFlag = 1;

                    animator.runtimeAnimatorController = Thung;
                }

                //if (Mathf.Abs(playerPos.x - transform.position.x) > 4 &&
                //    Mathf.Abs(playerPos.y - transform.position.y) > 4)
                else
                {
                    attackFlag = 2;

                    animator.runtimeAnimatorController = Fireball;
                }

                //GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
                timer = 0;

            }
        }
        else
        {
            animator.runtimeAnimatorController = isMoving;
            MoveRoutine();
        }
    }

    void BulletPref()
    {
        if (dist == "Right")
        {
            Instantiate(bulletPLeft, transform.position, Quaternion.identity);
        }
        else if (dist == "Left")
        {
            Instantiate(bulletPRight, transform.position, Quaternion.identity);
        }
    }

    public IEnumerator E_Attack()
    {

        if (attackFlag == 0)
        {
            animator.SetBool("isMoving", true);
        }
        else if (attackFlag == 1)
        {
            animator.SetBool("isMoving", false);

            animator.SetTrigger("Thung");
        }
        else if (attackFlag == 2)
        {
            animator.SetBool("isMoving", false);

            animator.SetTrigger("Fireball");
        }

        yield return new WaitForSeconds(2);

        StartCoroutine(E_Movement());
    }

    void Start()
    {
        animator = gameObject.GetComponentInChildren<Animator>();
        rigidbody = GetComponent<Rigidbody2D>();
        StartCoroutine(E_Movement());
    }

    void FixedUpdate()
    {
        MoveRoutine();

        Attack();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        //if (other.gameObject.tag == "Player")
        //{
        //    player = other.gameObject;

        //    StopCoroutine(E_Movement());

        //    StartCoroutine(E_Attack());
        //}
        if (other.gameObject.tag == ("AttackRange_Fire"))
        {
            GameObject prefab = Resources.Load("prefabs/DamageEffect_fire") as GameObject;
            Instantiate(prefab, this.gameObject.transform.position, Quaternion.identity);
            hp = hp - normal_damage;
        }
        else if (other.gameObject.tag == ("AttackRange_Water"))
        {
            GameObject prefab = Resources.Load("prefabs/DamageEffect_water") as GameObject;
            Instantiate(prefab, this.gameObject.transform.position, Quaternion.identity);
            hp = hp - great_damage;
        }
        else if (other.gameObject.tag == ("AttackRange_Grass"))
        {
            GameObject prefab = Resources.Load("prefabs/DamageEffect_grass") as GameObject;
            Instantiate(prefab, this.gameObject.transform.position, Quaternion.identity);
            hp = hp - poor_damage;
        }
    }
    public void tracking()
    {
        isTracing = true;
    }
    public void Nottracking()
    {
        isTracing = false;
    }
    void Update()
    {
        if (hp <= 0)
        {
            count.GetComponent<CameraMove>().destroycount();
            Destroy(gameObject);
        }
    }
}