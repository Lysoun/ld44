using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MonsterController : MonoBehaviour
{
    public CombatManager combatManager;
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

    /// <summary>
    /// C is Card, P is Player
    /// </summary>
    public enum Action {C, P};


    /// <summary>
    /// Hide everything, get reference on its children
    /// </summary>
    void Start()
    {
        healthBar = gameObject.transform.Find("Life").gameObject;
        actionTarget = gameObject.transform.Find("Action").gameObject;
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
        healthBar.SetActive(false);
        actionTarget.SetActive(false);
        //Init();	
    }
    
    /// <summary>
    /// Initialize the Monster Stat. If its sprite would be changeable, will be done here
    /// </summary>
    public void Init() {
		//Stats
		Pattern = "CP";
		maxHealth = 20;
		speed = 10;
		armor = 0;
        attack = 5;
		monsterName = "RandoMonster";
		health = maxHealth;
		patternIndex = 0;//Random?
        

        //Update UI
        healthBar.GetComponent<Slider>().maxValue = maxHealth;
        healthBar.GetComponent<Slider>().value = health;

        gameObject.GetComponent<SpriteRenderer>().enabled = true;
        healthBar.SetActive(true);

        //Send finish signal
        combatManager.StateFinish(this.gameObject, Combat_State.Init);
    }

    /// <summary>
    /// Generate next Targer, upload visible target
    /// </summary>
    public void BeginTurn() {
        actionTarget.GetComponent<Image>().sprite = null;
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
		combatManager.StateFinish(this.gameObject, Combat_State.Begin_Turn);
	}

    /// <summary>
    /// Update monster life variable and life bar
    /// </summary>
    /// <param name="damage"> The damage taken</param>
    public void TakeDamage(int damage) {
        health = health - damage;
        healthBar.GetComponent<Slider>().value = health;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown("z")) {
            Init();
          }
          if (Input.GetKeyDown("a")) {
			BeginTurn();
		}
        if (Input.GetKeyDown("b"))
        {
            TakeDamage(1);
        }
        if (Input.GetKeyDown("c"))
        {
            Debug.Log("Monster " + this.MonsterName + " with " + this.Attack + " strength attacked target n° " + TypeAttack().ToString());
        }
        if (Input.GetKeyDown("d"))
        {
            EndCombat();
        }
    }

    /// <summary>
    /// Clean the Monster after battle
    /// </summary>
    public void EndCombat()
    {
        Destroy(gameObject);
    }

    /// <summary>
    /// Get the Attack type/target, and hide the visible symbol
    /// </summary>
    /// <returns>The ID of the attack</returns>
    int TypeAttack()
    {
        actionTarget.SetActive(false);
        return (int)currentTarget;
    }

    public int Health
    {
        get
        {
            return health;
        }
    }

    public int Attack
    {
        get
        {
            return attack;
        }
        set
        {
            attack = value;
        }
    }

    public int Armor
    {
        get
        {
            return armor;
        }
        set
        {
            armor = value;
        }
    }
    
    public int Speed
    {
        get
        {
            return speed;
        }
        set
        {
            speed = value;
        }
    }
    
    public string MonsterName
    {
        get
        {
            return monsterName;
        }
    }
    
}
