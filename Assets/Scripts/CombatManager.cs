using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    public PlayerController player;
    public ActiveCard activeCard;

    public Combat_State current_state;


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

    private bool combat_finished;


    // Start is called before the first frame update
    void Start()
    {
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

        // TODO with the others.
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

        monster.Init();
        player.Init();
        activeCard.Init();
    }

    private void Begin_Turn()
    {
        monster_beginTurnReady = false;
        player_beginTurnReady = false;
        activeCard_beginTurnReady = false;

        monster.BeginTurn();
        player.BeginTurn();
        activeCard.BeginTurn();
    }

    private void Player_Choose()
    {
        selectedCard = null;
        player.Play();
    }

    private void Card_Preview()
    {
        // Start preview of selected card
        // Activation of the button pay what is left to pay.
    }

    private void Paying()
    {
        player.Sacrifice();
        // Display buttons.
        // Display cost text etc.
    }

    private void Turn_Resolution()
    {
        // check first to hit.
        // Then hit each others
    }

    private void End_Turn()
    {
        activeCard.EndTurn();
        monster.EndTurn();
        player.EndTurn();
    }

    private void End_Combat()
    {
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
                    ChangeState(Combat_State.Player_Choose);
                }
                break;

            case Combat_State.Player_Choose:
                if (selectedCard != null && 
                    selectedCard.GetComponent<Card>() != null)
                {
                    ChangeState(Combat_State.Card_Preview);
                }
                break;

            //case Combat_State.Card_Preview: 
                // break;
                // No need because of the functions Valid() and Cancel() which do the change
                
            // SacrificeCard() redo the change to paying.
            // The button call the function PayWithLife() does the change too.
            case Combat_State.Paying:
                if (activeCard.RemainingCost() < 0)
                {
                    ChangeState(Combat_State.Turn_Resolution);
                }
                break;

            case Combat_State.Turn_Resolution:
                if (activeCard_endTurnReady &&
                    monster_endTurnReady &&
                    player_endTurnReady)
                {
                    ChangeState(Combat_State.End_Turn);
                }
                break;

            case Combat_State.End_Turn:
                if (activeCard_endTurnReady &&
                    monster_endTurnReady &&
                    player_endTurnReady)
                {
                    if (player.health < 0 || monster.health < 0)
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

    public void SubmitCard()
    {
        if (current_state == Combat_State.Card_Preview)
        {
            ChangeState(Combat_State.Paying);
        }
        else
        {
            Debug.Log("Bug ! Should not be able to push that button !");
        }
    }

    public void CancelCard()
    {
        if (current_state == Combat_State.Card_Preview)
        {
            ChangeState(Combat_State.Player_Choose);
        }
        else
        {
            Debug.Log("Bug ! Should not be able to push that button !");
        }
    }

    public void PayWithHealth()
    {
        if (current_state == Combat_State.Paying)
        {
            ChangeState(Combat_State.Turn_Resolution);
        }
        else
        {
            Debug.Log("Bug ! Should not be able to push that button !");
        }
    }

    #endregion

}
