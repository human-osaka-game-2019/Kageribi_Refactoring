using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class hp_manager : MonoBehaviour
{
    [SerializeField] private GameObject gold;
    [SerializeField] private GameObject silver;
    private GameObject[] gold_hp = new GameObject[20];
    private GameObject[] silver_hp = new GameObject[20];
    public GameObject Player;
    bothHP script;
    public int hp;
    int count = 0;
    int Acount = 0;
    public int damage;
    public float night_timeout;
    public float morning_timeout;
    public float time;
    public int Pdm;

    // Start is called before the first frame update
    void Start()
    {

        foreach (Transform gold_child in gold.transform)
        {
            if (count == gold_hp.Length)
            {
                break;
            }
            gold_hp[count] = gold_child.gameObject;
            count += 1;
        }

        count = 0;

        foreach (Transform silver_child in silver.transform)
        {
            if (count == silver_hp.Length)
            {
                break;
            }
            silver_hp[count] = silver_child.gameObject;
            count += 1;
        }
        count = 0;
        for (int i = 0; i < silver_hp.Length; i++) 
        {
            silver_hp[i].SetActive(false);
        }
        script = Player.GetComponent<bothHP>();
    }

            // Update is called once per frame
    void Update()
    {
        //int Pdm = Player.hp;
        time += Time.deltaTime;

        if (time > morning_timeout)
        {
            count = 19;
            for (int i = 0; i < hp; i++)
            {               
                gold_hp[count].SetActive(true);
                count--;
            }
            for (int i = 0; i < silver_hp.Length; i++)
            {
                silver_hp[i].SetActive(false);
            }
            time = 0;
        }
        else if (time > night_timeout)
        {
            count = 19;
            for (int i = 0; i < hp; i++)
            {                
                silver_hp[count].SetActive(true);
                count--;
            }
            for(int i = 0; i < gold_hp.Length; i++)
            { 
                gold_hp[i].SetActive(false);
            }


        }
        

    }
    public void Controle()
    {
        Pdm = script.HP;
        for (hp = hp; hp > Pdm; hp--)
        {
            gold_hp[Acount].SetActive(false);
            silver_hp[Acount].SetActive(false);
            Acount++;
        }       
    }

}
