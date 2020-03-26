using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bothHP : MonoBehaviour
{
    public int HP = 20;
    public GameObject Gameover;
    public GameObject Hp_manager;
    public GameObject gold;
    goldfox Goldstop;
    public GameObject silver;
    silverfox Silverstop;
    // Start is called before the first frame update
    void Start()
    {
        Goldstop = gold.GetComponent<goldfox>();
        Silverstop = silver.GetComponent<silverfox>();
    }

    // Update is called once per frame
    void Update()
    {
        if (HP <= 0)
        {
            Gameover.SetActive(true);
            Goldstop.goldStop();
            Silverstop.silverStop();
        }
    }
    public void BigDamage()
    {
        HP = HP - 4;
        Hp_manager.GetComponent<hp_manager>().Controle();
        Debug.Log(HP);
    }
    public void NormalDamage()
    {
        HP = HP - 2;
        Hp_manager.GetComponent<hp_manager>().Controle();
    }
    public void SmallDamage()
    {
        HP = HP - 1;
        Hp_manager.GetComponent<hp_manager>().Controle();
    }
}
