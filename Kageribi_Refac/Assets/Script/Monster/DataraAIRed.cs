using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataraAIRed : MonoBehaviour
{


    public int hp = 10;

    public int great_damage = 4;
    public int normal_damage = 2;
    public int poor_damage = 1;

    public float movePower = 1f;

    Animator animator;
    Vector3 movement;
    int movementFlag = 0;  // 0:　停止　1:　左へ　2:　右へ

    // Start is called before the first frame update
    void Start()
    {
        animator = gameObject.GetComponentInChildren<Animator>();

        StartCoroutine("ChangeMovement");
    }

    IEnumerator ChangeMovement()   // Coroutine
    {
        movementFlag = Random.Range(0, 3); // ランダムな動き

        if (movementFlag == 0)      //Animation Mapping
            animator.SetBool("isMoving", false);
        else
            animator.SetBool("isMoving", true);

        Debug.Log("Front Logic :" + Time.time);

        yield return new WaitForSeconds(3f);  // 3秒間待機

        Debug.Log("Behind Logic :" + Time.time);

        StartCoroutine("ChangeMovement");　// Logicの再スタート
    }

    void FixedUpdate()
    {
        Move();
    }

    // Update is called once per frame
    void Move()
    {
        Vector3 moveVelocity = Vector3.zero;

        if (movementFlag == 1)
        {
            moveVelocity = Vector3.left;
            transform.localScale = new Vector3(1, 1, 1);
        }
        else if (movementFlag == 2)
        {
            moveVelocity = Vector3.right;
            transform.localScale = new Vector3(-1, 1, 1);
        }

        transform.position += moveVelocity * movePower * Time.deltaTime;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
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
    void Update()
    {
        if (hp <= 0)
        {
            Destroy(gameObject);
        }
    }
}