﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum Combat_State
{
    Init,
    Begin_Turn,
    Player_Choose,
    Card_Preview,
    Paying,
    Sacrifising,
    Turn_Resolution,
    End_Turn,
    End_Combat
}

public class CombatManager : MonoBehaviour
{
 
    public MonsterController monster;
    public Animator monster_anim;
    public PlayerController player;
    public ActiveCard activeCard;
    public PreviewCardDisplay preview_Display;
    public TitlesController titlesController;

    public Combat_State current_state;
    public GameObject endTurn_Buttons;

    private GameObject selectedCard;

    private bool monster_initialized;
    private bool player_initialized;
    private bool activeCard_initialized;

    private bool monster_beginTurnReady;
    private bool player_beginTurnReady;
    private bool activeCard_beginTurnReady;

    private bool monster_endTurnReady;
    private bool player_endTurnReady;
    private bool activeCard_endTurnReady;

    private bool resolution_finished;
    private bool endTurnChoiceMade;

    private int number_turn;

    // Start is called before the first frame update
    void Start()
    {
        number_turn = 1;
        ChangeState(Combat_State.Init);
    }

    // Update is called once per frame
    void Update()
    {
        TestChangeState();
    }

    #region PUBLIC FUNCTION

    /// <summary>
    /// Functions to be called to tell the CombatController that the a process is finished.
    /// </summary>
    /// <param name="go">The gameobject that finished a process.</param>
    /// <param name="finished_state">The state process finished.</param>
    public void StateFinish(GameObject go, Combat_State finished_state)
    {
        ActiveCard temp_activeCard = go.GetComponent<ActiveCard>();
        if (temp_activeCard != null)
        {
            switch (finished_state)
            {
                case Combat_State.Init:
                    activeCard_initialized = true;
                    break;
                case Combat_State.Begin_Turn:
                    activeCard_beginTurnReady = true;
                    break;
                case Combat_State.End_Turn:
                    activeCard_endTurnReady = true;
                    break;
            }
            return;
        }

    

        PlayerController temp_player = go.GetComponent<PlayerController>();
        if (temp_player != null)
        {
            switch (finished_state)
            {
                case Combat_State.Init:
                    player_initialized = true;
                    break;
                case Combat_State.Begin_Turn:
                    player_beginTurnReady = true;
                    break;
                case Combat_State.End_Turn:
                    player_endTurnReady = true;
                    break;
            }
            return;
        }

        MonsterController temp_monster = go.GetComponent<MonsterController>();
        if (temp_monster != null)
        {
            switch (finished_state)
            {
                case Combat_State.Init:
                    monster_initialized = true;
                    break;
                case Combat_State.Begin_Turn:
                    monster_beginTurnReady = true;
                    break;
                case Combat_State.End_Turn:
                    monster_endTurnReady = true;
                    break;
            }
            return;
        }

        // TODO with the others.
    }


    public void Win()
    {
        SceneManager.LoadScene("VictoryScene");
    }

    public void Loose()
    {
        SceneManager.LoadScene("GameOverScene");
    }
    /// <summary>
    /// Gives the CombatController the card choosen by the player.
    /// </summary>
    /// <param name="card">The card to be played.</param>
    public void Choosen_Card(Card card)
    {
        selectedCard = card.gameObject;
    }

    /// <summary>
    /// Validates the choosen card to be played.
    /// </summary>
    public void Valid_Card()
    {
        if (current_state != Combat_State.Card_Preview)
        {
            Debug.LogWarning("Bug here ! Wrong current state");
            return;
        }
        player.PlayedCard(selectedCard.GetComponent<Card>());
        ChangeState(Combat_State.Paying);
    }

    /// <summary>
    /// Cancels the choosen card.
    /// </summary>
    public void Cancel_Card()
    {
        if (current_state != Combat_State.Card_Preview)
        {
            Debug.LogWarning("Bug here ! Wrong current state");
            return;
        }
        selectedCard = null;
        ChangeState(Combat_State.Player_Choose);
    }

    /// <summary>
    /// Tells the CombatController that a card in the hand of the player has been sacrificed to pay the price of invocation.
    /// </summary>
    /// <param name="value">The amount of life given by the sacrifice.</param>
    public void Sacrificed_Card(int value)
    {
        activeCard.Pay(value);
    }

    #endregion

    #region STATE START FUNCTIONS
    private void Init()
    {
        current_state = Combat_State.Init;

        monster_initialized = false;
        player_initialized = false;
        activeCard_initialized = false;

        monster_beginTurnReady = false;
        player_beginTurnReady = false;
        activeCard_beginTurnReady = false;

        preview_Display.Hide();
        endTurn_Buttons.SetActive(false);

        monster.Init();
        player.Init();
        activeCard.Init();
    }

    private void Begin_Turn()
    {
        titlesController.New_Turn(number_turn++, "Beginning of new turn");

        monster_beginTurnReady = false;
        player_beginTurnReady = false;
        activeCard_beginTurnReady = false;

        monster.BeginTurn();
        player.BeginTurn();
        activeCard.BeginTurn();

        resolution_finished = false;
        endTurnChoiceMade = false;
    }

    private void Player_Choose()
    {
        titlesController.New_Phase("Choose a card");
        preview_Display.Hide();
        selectedCard = null;
        player.Play();
    }

    private void Card_Preview()
    {
        preview_Display.card_to_preview = Instantiate(selectedCard).GetComponent<Card>();
        preview_Display.original_card = selectedCard.GetComponent<Card>();
        preview_Display.Display();
        // Start preview of selected card
        // Activation of the button pay what is left to pay.
    }

    private void Paying()
    {
        titlesController.New_Phase("Invocation");
        preview_Display.Hide();
        activeCard.NewCard(selectedCard.GetComponent<Card>());
        player.Sacrifice();
        // Display buttons.
        // Display cost text etc.
    }

    private void Turn_Resolution()
    {
        titlesController.New_Phase("Fight !");
        StartCoroutine(Combat_Coroutine());
    }

    IEnumerator Combat_Coroutine()
    {
        int speed_card = activeCard.card.getSpeedValue();
        int speed_monster = monster.Speed;

        if (speed_card > speed_monster)
        {
            // Player hits first
            int damage = activeCard.card.getAttackValue() - monster.Armor;
            monster.TakeDamage(damage);
            Debug.Log("Attack first : card fait " + damage.ToString() + " dégats au monstre");
            activeCard.card.GetComponent<Animator>().SetTrigger("Attack");
            monster_anim.SetTrigger("Hurt");
            yield return new WaitForSeconds(1);

            if (monster.Health > 0)
            {
                damage = monster.Attack - activeCard.card.getArmorValue();
                if (monster.TypeAttack() == MonsterController.Action.C)
                {
                    Debug.Log("Attack second : monstre fait " + damage.ToString() + " dégats à la carte");
                    activeCard.TakeDamage(damage);
                    monster_anim.SetTrigger("Attack");
                    activeCard.card.GetComponent<Animator>().SetTrigger("Hurt");
                    yield return new WaitForSeconds(1);
                }
                else
                {
                    Debug.Log("Attack second : monstre fait " + damage.ToString() + " dégats au joueur");
                    player.TakeDamage(damage);
                    monster_anim.SetTrigger("AttackPlayer");
                    //activeCard.card.GetComponent<Animator>().SetTrigger("Hurt");
                    // Anim Dégat PV
                    yield return new WaitForSeconds(1);
                }

            }
        }
        else
        {
            // Monster hits first
            int damage = monster.Attack - activeCard.card.getArmorValue();
            Debug.Log(damage);
            if (monster.TypeAttack() == MonsterController.Action.C)
            {
                Debug.Log("Attack first : monstre fait " + damage.ToString() + " dégats à la carte");
                activeCard.TakeDamage(damage);
                monster_anim.SetTrigger("Attack");
                activeCard.card.GetComponent<Animator>().SetTrigger("Hurt");
                yield return new WaitForSeconds(1);
            }
            else
            {
                Debug.Log("Attack first : monstre fait " + damage.ToString() + " dégats au joueur");
                player.TakeDamage(damage);
                monster_anim.SetTrigger("AttackPlayer");
                //activeCard.card.GetComponent<Animator>().SetTrigger("Hurt");
                // Anim Dégat PV
                yield return new WaitForSeconds(1);
            }

            if (activeCard.card.getHealthValue() > 0)
            {
                damage = activeCard.card.getAttackValue() - monster.Armor;
                Debug.Log("Attack second : card fait " + damage.ToString() + " dégats au monstre");
                monster.TakeDamage(damage);
                activeCard.card.GetComponent<Animator>().SetTrigger("Attack");
                monster_anim.SetTrigger("Hurt");
                yield return new WaitForSeconds(1);
            }
            
        }

        resolution_finished = true;
    }

    private void End_Turn()
    {
        if (activeCard.card.getHealthValue() > 0)
        {
            endTurn_Buttons.SetActive(true);
        }
        else
        {
            endTurn_Buttons.SetActive(false);
            endTurnChoiceMade = true;
        }

        activeCard.EndTurn();
        monster.EndTurn();
        player.EndTurn();
    }

    private void End_Combat()
    {
        if (monster.Health > 0)
        {
            Debug.Log("You lose !");
            Loose();
        }
        else
        {
            Debug.Log("Yoou win !");
            Win();
        }
        activeCard.EndCombat();
        monster.EndCombat();
        player.EndCombat();
    }
    #endregion

    #region STATE UPDATE FUNCTIONS

    #endregion

    #region STATE CONDITIONS CHANGE
    private void TestChangeState()
    {
        switch (current_state)
        {
            case Combat_State.Init:
                if (activeCard_initialized &&
                    monster_initialized &&
                    player_initialized)
                {
                    ChangeState(Combat_State.Begin_Turn);
                }
                break;

            case Combat_State.Begin_Turn:
                if (activeCard_beginTurnReady &&
                    monster_beginTurnReady &&
                    player_beginTurnReady)
                {
                    if (activeCard.IsEmpty())
                    {
                        ChangeState(Combat_State.Player_Choose);
                    }
                    else
                    {
                        ChangeState(Combat_State.Turn_Resolution);
                    }
                }
                break;

            case Combat_State.Player_Choose:
                if (selectedCard != null && 
                    selectedCard.GetComponent<Card>() != null)
                {
                    ChangeState(Combat_State.Card_Preview);
                }
                break;

            case Combat_State.Card_Preview: 
                break;
                // No need because of the functions Valid() and Cancel() which do the change
                
            // SacrificeCard() redo the change to paying.
            // The button call the function PayWithLife() does the change too.
            case Combat_State.Paying:
                if (activeCard.RemainingCost() <= 0)
                {
                    player.EndSacrifice();
                    ChangeState(Combat_State.Turn_Resolution);
                }
                break;

            case Combat_State.Turn_Resolution:
                if (resolution_finished)
                {
                    ChangeState(Combat_State.End_Turn);
                }
                break;

            case Combat_State.End_Turn:
                if (activeCard_endTurnReady &&
                    monster_endTurnReady &&
                    player_endTurnReady &&
                    endTurnChoiceMade)
                {
                    if (player.GetLifePoints() <= 0 || monster.Health <= 0) // Ajouter pour le joueur aussi !
                    {
                        ChangeState(Combat_State.End_Combat);
                    }
                    else
                    {
                        ChangeState(Combat_State.Begin_Turn);
                    }
                }
                    break;
            case Combat_State.End_Combat:
                // Cant leave this state.
                break;

            default:
                Debug.Log("A good bug here !");
                break;
        }
    }

    private void ChangeState(Combat_State new_state)
    {
        current_state = new_state;
        // Debug.Log(current_state);
        Debug.Log(Time.time + "/// New state : " + current_state);
        switch (current_state)
        {
            case Combat_State.Init:
                Init();
                break;
            case Combat_State.Begin_Turn:
                Begin_Turn();
                break;
            case Combat_State.Player_Choose:
                Player_Choose();
                break;
            case Combat_State.Card_Preview:
                Card_Preview();
                break;
            case Combat_State.Paying:
                Paying();
                break;
            case Combat_State.Turn_Resolution:
                Turn_Resolution();
                break;
            case Combat_State.End_Turn:
                End_Turn();
                break;
            case Combat_State.End_Combat:
                End_Combat();
                break;

            default:
                Debug.Log("A good bug here !");
                break;
        }

    }

    public void DiscardInvoc()
    {
        activeCard.DiscardCard();
        endTurn_Buttons.SetActive(false);
        endTurnChoiceMade = true;
    }

    public void KeepInvoc()
    {
        endTurn_Buttons.SetActive(false);
        endTurnChoiceMade = true;
    }

    public void PayWithHealth()
    {
        if (current_state == Combat_State.Paying)
        {
            player.EndSacrifice();
            int remaining_cost = activeCard.RemainingCost();
            player.TakeDamage(remaining_cost);
            activeCard.Pay(remaining_cost);
            ChangeState(Combat_State.Turn_Resolution);
        }
        else
        {
            Debug.Log("Bug ! Should not be able to push that button !");
        }
    }

    #endregion

}
