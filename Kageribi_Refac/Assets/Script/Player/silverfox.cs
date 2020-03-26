using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class silverfox : MonoBehaviour
{
    public GameObject HP;
    bothHP golsilHP;

    public GameObject[] AttackRange;

    public float speed = 0.1f;

    public RuntimeAnimatorController Fire;
    public RuntimeAnimatorController Water;
    public RuntimeAnimatorController Grass;
    public GameObject Kin;
    public GameObject Checkpoint;

    private Animator animator;

    Rigidbody2D rigid2D;
    public float jumpForce;
    public float jumpForce2;
    int jumpCounts = 0;
    int attackCounts = 0;

    [SerializeField ]
    private bool Attack_Flag;
    private bool Land_Flag;
    private bool Shadow_Flag;
    private bool Wall_Flag;
    private bool Shadow_Wall_Flag;
    int Run_Flag;

    // Start is called before the first frame update
    void Start() 
    {
        animator =  GetComponent<Animator>();
        rigid2D = GetComponent<Rigidbody2D>();
        Attack_Flag = false;
        Run_Flag = 1;
        golsilHP = HP.GetComponent<bothHP>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            golsilHP.BigDamage();
            animator.SetBool("Damage", true);
        }

        bool right = Input.GetKey(KeyCode.RightArrow) || Input.GetAxisRaw("HorizontalL") > 0.19;
        bool left = Input.GetKey(KeyCode.LeftArrow) || Input.GetAxisRaw("HorizontalL") < -0.19;
        bool up = Input.GetKeyDown(KeyCode.UpArrow) || Input.GetButtonDown("XBOXA");
        bool wallup = Input.GetKey(KeyCode.UpArrow)|| Input.GetAxisRaw("VerticalL") >0.19;
        bool walldown = Input.GetKeyDown(KeyCode.DownArrow) || Input.GetAxisRaw("VerticalL") < -0.19;
        bool attack = Input.GetKeyDown(KeyCode.S) || Input.GetButtonDown("XBOXB");
        bool special_attack = Input.GetKeyDown(KeyCode.D) || Input.GetButtonDown("XBOXY");

        if (Shadow_Wall_Flag == false)
        {
            if (Attack_Flag == false)
            {
                if (right == true)
                {
                    transform.Translate(speed, 0.0f, 0.0f);
                    animator.SetInteger("Right", 1);
                    animator.SetInteger("Left", 0);
                    Run_Flag = 1;
                }
                else if (left == true)
                {
                    transform.Translate(-speed, 0.0f, 0.0f);
                    animator.SetInteger("Left", 1);
                    animator.SetInteger("Right", 0);
                    Run_Flag = -1;
                }
                else
                {
                    animator.SetInteger("Right", 0);
                    animator.SetInteger("Left", 0);
                }

            }
        }
        if (Shadow_Flag == false)
        {
            if (attack == true)
            {

                Attack_Flag = true;
                animator.SetTrigger("Attack");
                rigid2D.velocity = Vector2.zero;

                if (Run_Flag == 1)
                {
                    if (animator.runtimeAnimatorController == Fire)
                    {
                        AttackRange[0].SetActive(true);
                    }
                    if (animator.runtimeAnimatorController == Water)
                    {
                        AttackRange[1].SetActive(true);
                    }
                    if (animator.runtimeAnimatorController == Grass)
                    {
                        AttackRange[2].SetActive(true);
                    }
                }
                else if (Run_Flag == -1)
                {
                    if (animator.runtimeAnimatorController == Fire)
                    {
                        AttackRange[3].SetActive(true);
                    }
                    if (animator.runtimeAnimatorController == Water)
                    {
                        AttackRange[4].SetActive(true);
                    }
                    if (animator.runtimeAnimatorController == Grass)
                    {
                        AttackRange[5].SetActive(true);
                    }
                }

            }
            if (special_attack == true)
            {
                Attack_Flag = true;
                animator.SetTrigger("SpecialAttack");
                rigid2D.velocity = Vector2.zero;
                if (Run_Flag == 1)
                {
                    if (animator.runtimeAnimatorController == Fire)
                    {
                        AttackRange[6].SetActive(true);
                    }
                    if (animator.runtimeAnimatorController == Water)
                    {
                        AttackRange[7].SetActive(true);
                    }
                    if (animator.runtimeAnimatorController == Grass)
                    {
                        AttackRange[8].SetActive(true);
                    }
                }
                else if (Run_Flag == -1)
                {
                    if (animator.runtimeAnimatorController == Fire)
                    {
                        AttackRange[9].SetActive(true);
                    }
                    if (animator.runtimeAnimatorController == Water)
                    {
                        AttackRange[10].SetActive(true);
                    }
                    if (animator.runtimeAnimatorController == Grass)
                    {
                        AttackRange[11].SetActive(true);
                    }
                }

            }




            if (up == true)
            {
                jumpCounts = jumpCounts + 1;
                if (jumpCounts == 1)
                {
                    animator.SetTrigger("Jump");
                    rigid2D.velocity = Vector2.zero;
                    rigid2D.AddForce(transform.up * jumpForce);

                }
            }

            if (Input.GetKeyDown(KeyCode.A) || Input.GetButtonDown("XBOXRB"))
            {

                if (animator.runtimeAnimatorController == Fire)
                {
                    animator.runtimeAnimatorController = Water;
                }

                else if (animator.runtimeAnimatorController == Water)
                {
                    animator.runtimeAnimatorController = Grass;
                }

                else if (animator.runtimeAnimatorController == Grass)
                {
                    animator.runtimeAnimatorController = Fire;
                }
            }

            if (Input.GetKeyDown(KeyCode.W) || Input.GetButtonDown("XBOXLB"))
            {

                if (animator.runtimeAnimatorController == Fire)
                {
                    animator.runtimeAnimatorController = Grass;
                }

                else if (animator.runtimeAnimatorController == Grass)
                {
                    animator.runtimeAnimatorController = Water;
                }

                else if (animator.runtimeAnimatorController == Water)
                {
                    animator.runtimeAnimatorController = Fire;
                }

            }
        }
        if (Land_Flag == true)
        {
            if (Input.GetKey(KeyCode.Space) == true || Input.GetAxisRaw("Trigger") > 0.19)
            {
                animator.SetBool("Shadow", true);
                GetComponent<CapsuleCollider2D>().enabled = false;
                rigid2D.gravityScale = 0.0f;                
                AttackRange[12].SetActive(true);
                //AttackRange[13].SetActive(true);
                Land_Flag = true;
                Shadow_Flag = true;
            }
            else
            {
                Shadow_Flag = false;
                animator.SetBool("Shadow", false);
                GetComponent<CapsuleCollider2D>().enabled = true;
                rigid2D.gravityScale = 1f;
                AttackRange[12].SetActive(false);
                //AttackRange[13].SetActive(false);
            }

        }
        if (Wall_Flag == true)
        {

            if (Input.GetKey(KeyCode.Space) == true || Input.GetAxisRaw("Trigger") > 0.19)
            {
                GetComponent<CapsuleCollider2D>().enabled = false;
                rigid2D.gravityScale = 0.0f;
                AttackRange[12].SetActive(true);
                if (wallup == true)
                {
                    transform.Translate(0, 0.1f, 0);
                }
                else if (walldown == true)
                {
                    transform.Translate(0, -0.1f, 0);
                }
                Wall_Flag = true;
                Shadow_Wall_Flag = true;
            }
            else
            {
                GetComponent<CapsuleCollider2D>().enabled = true;
                rigid2D.gravityScale = 1f;
                AttackRange[12].SetActive(false);
                Shadow_Wall_Flag = false;
            }

        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "floor")
        {
            jumpCounts = 0;
            animator.SetBool("Jumping", false);
            Land_Flag = true;
        }
        if (collision.gameObject.tag == "MonsterD_Fire")
        {
            if (animator.runtimeAnimatorController == Fire)
            {
                golsilHP.NormalDamage();
            }
            if (animator.runtimeAnimatorController == Water)
            {
                golsilHP.SmallDamage();
            }
            if (animator.runtimeAnimatorController == Grass)
            {
                golsilHP.BigDamage();
            }

        }
        if (collision.gameObject.tag == "MonsterD_Water")
        {
            if (animator.runtimeAnimatorController == Fire)
            {
                golsilHP.BigDamage();
            }
            if (animator.runtimeAnimatorController == Water)
            {
                golsilHP.NormalDamage();
            }
            if (animator.runtimeAnimatorController == Grass)
            {
                golsilHP.SmallDamage();
            }

        }
        if (collision.gameObject.tag == "MonsterD_Grass")
        {
            if (animator.runtimeAnimatorController == Fire)
            {
                golsilHP.SmallDamage();
            }
            if (animator.runtimeAnimatorController == Water)
            {
                golsilHP.BigDamage();
            }
            if (animator.runtimeAnimatorController == Grass)
            {
                golsilHP.NormalDamage();
            }

        }

    }
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "stage")
        {
            Wall_Flag = true;
        }


        if (collider.gameObject.tag == "Bullet_Fire")
        {
            if (animator.runtimeAnimatorController == Fire)
            {
                golsilHP.NormalDamage();
            }
            if (animator.runtimeAnimatorController == Water)
            {
                golsilHP.SmallDamage();
            }
            if (animator.runtimeAnimatorController == Grass)
            {
                golsilHP.BigDamage();
            }

        }
        if (collider.gameObject.tag == "Bullet_Water")
        {
            if (animator.runtimeAnimatorController == Fire)
            {
                golsilHP.BigDamage();
            }
            if (animator.runtimeAnimatorController == Water)
            {
                golsilHP.NormalDamage();
            }
            if (animator.runtimeAnimatorController == Grass)
            {
                golsilHP.SmallDamage();
            }

        }
        if (collider.gameObject.tag == "Bullet_Grass")
        {
            if (animator.runtimeAnimatorController == Fire)
            {
                golsilHP.SmallDamage();
            }
            if (animator.runtimeAnimatorController == Water)
            {
                golsilHP.BigDamage();
            }
            if (animator.runtimeAnimatorController == Grass)
            {
                golsilHP.NormalDamage();
            }

        }


    }
    private void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "stage")
        {
            Wall_Flag = false;
        }

    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "floor")
        {
            animator.SetBool("Jumping",true);
            Land_Flag = false;
        }
        if (collision.gameObject.tag == "wall")
        {
            Wall_Flag = false;
        }
    }

    void FinishJump()
    {
    }
    void FinishAttack()
    {
        Attack_Flag = false;
        AttackRange[0].SetActive(false);
        AttackRange[1].SetActive(false);
        AttackRange[2].SetActive(false);
        AttackRange[3].SetActive(false);
        AttackRange[4].SetActive(false);
        AttackRange[5].SetActive(false);


    }
    void FinishSpecialAttack()
    {
        Attack_Flag = false;
        AttackRange[6].SetActive(false);
        AttackRange[7].SetActive(false);
        AttackRange[8].SetActive(false);
        AttackRange[9].SetActive(false);
        AttackRange[10].SetActive(false);
        AttackRange[11].SetActive(false);

        if (Run_Flag == 1)
        {
            rigid2D.position = new Vector3(transform.position.x + 5, transform.position.y, transform.position.z);
        }
        else if (Run_Flag == -1)
        {
            rigid2D.position = new Vector3(transform.position.x - 5, transform.position.y, transform.position.z);
        }

    }
    void FinishDamage()
    {
        animator.SetBool("Damage", false);
    }
    public void silverStop()
    {
        animator.SetBool("Damage", true);
        enabled = false;
    }

    public void CheckP()
    {
        transform.position = Checkpoint.GetComponent<check>().checkpoint;
    }
}

