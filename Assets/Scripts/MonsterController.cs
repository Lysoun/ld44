using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//Prochain coup, combien tu tape...

public class MonsterController : MonoBehaviour
{
	
	public Sprite targetCard_sprite;
	public Sprite targetPlayer_sprite;
	
	private string Pattern;
	private int maxHealth;
	private int speed;
	private int armor;
    private int attack;
	private string monsterName;
	private int health;
	private int patternIndex;
    private GameObject healthBar;
    private GameObject actionTarget;
    private Action currentTarget;

    enum Action {C, P};
    // Start is called before the first frame update
    void Start()
    {
        Init();	
    }
    
    void Init() {
		//Stats
		Pattern = "CP";
		maxHealth = 20;
		speed = 10;
		armor = 0;
        attack = 5;
		monsterName = "Vaporeon";
		health = maxHealth;
		patternIndex = 0;//Random?
        healthBar = gameObject.transform.Find("Life").gameObject;
        actionTarget = gameObject.transform.Find("Action").gameObject;

        //Update UI
        healthBar.GetComponent<Slider>().maxValue = maxHealth;
        healthBar.GetComponent<Slider>().value = health;
        actionTarget.SetActive(false);

        //Send finish signal
        //CombatManager.InitFinish(this);
    }

    void BeginTurn() {
        currentTarget = (Action) System.Enum.Parse(typeof(Action), Pattern[patternIndex].ToString());
        switch(currentTarget)
        {
            case Action.C:
                actionTarget.GetComponent<Image>().sprite = targetCard_sprite;
                break;
            case Action.P:
                actionTarget.GetComponent<Image>().sprite = targetPlayer_sprite;
                break;
            default:
                Debug.Log("Error ><");
                break;
        }
		patternIndex = (patternIndex + 1) % Pattern.Length;
        actionTarget.SetActive(true);
		//CombatManager.BeginTurnFinish(this);
	}

    void TakeDamage(int damage) {
        health = health - damage;
        healthBar.GetComponent<Slider>().value = health;
    }

    // Update is called once per frame
    void Update()
    {
        /*if (Input.GetKeyDown("a")) {
			BeginTurn();
		}
        if (Input.GetKeyDown("b"))
        {
            TakeDamage(1);
        }
        if (Input.GetKeyDown("c"))
        {
            Debug.Log("Monster " + GetName() + " with " + GetAttack().ToString() + " strength attacked target n° " + TypeAttack().ToString());
        }
        if (Input.GetKeyDown("d"))
        {
            EndCombat();
        }*/
    }

    void EndCombat()
    {
        Destroy(gameObject);
    }

    int TypeAttack()
    {
        actionTarget.SetActive(false);
        return (int)currentTarget;
    }

    int GetAttack()
    {
        return attack;
    }

    int GetArmor()
    {
        return armor;
    }

    int GetSpeed()
    {
        return speed;
    }

    int GetHealth()
    {
        return health;
    }

    string GetName()
    {
        return monsterName;
    }
}
