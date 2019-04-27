using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Combat_State
{
    Begin_Turn,
    Player_Choose,
    Card_Preview,
    Paying,
    Sacrifising,
    Combat_Resolution,
    End_Turn
}

public class CombatManager : MonoBehaviour
{

    public MonsterController monster;
    public PlayerController player;
    public ActiveCardController activeCard;


    public Combat_State current_state;


    // Start is called before the first frame update
    void Start()
    {
        /**
         * activeCard is set empty
         * player initialize
         * monster initialize
         * 
         * current_state is set to begin turn
        **/
    }

    // Update is called once per frame
    void Update()
    {
        /*
        switch (current_state)
        {
            case Combat_State.Begin_Turn:

                break;
            case Combat_State.Begin_Turn:

                break;
            case Combat_State.Begin_Turn:

                break;
            case Combat_State.Begin_Turn:

                break;
            case Combat_State.Begin_Turn:

                break;
            case Combat_State.Begin_Turn:

                break;
            case Combat_State.Begin_Turn:

                break;
            default:

                break;
        }*/
    }

    

    #region PUBLIC FUNCTION

    /// <summary>
    /// Functions to be called to tell the CombatController that the initialization process is finished.
    /// Has to be call by every GameObject that reveiced an Init() call.
    /// </summary>
    /// <param name="go">The gameobject who has finished his initialization.</param>
    public void Init_Finish(GameObject go)
    {

    }

    /// <summary>
    /// Functions to be called to tell the CombatController that the begin_turn is finished.
    /// Has to be call by every GameObject that reveiced an Begin_Turn() call.
    /// </summary>
    /// <param name="go">The gameobject who has finished his Begin Turn.</param>
    public void Begin_Turn_Finish(GameObject go)
    {

    }

    /// <summary>
    /// Gives the CombatController the card choosen by the player.
    /// </summary>
    /// <param name="card">The card to be played.</param>
    public void Choosen_Card(Card card)
    {
        
    }

    /// <summary>
    /// Validates the choosen card to be played.
    /// </summary>
    public void Valid_Card()
    {

    }

    /// <summary>
    /// Cancels the choosen card.
    /// </summary>
    public void Cancel_Card()
    {

    }

    /// <summary>
    /// Tells the CombatController that a card in the hand of the player has been sacrificed to pay the price of invocation.
    /// </summary>
    /// <param name="value">The amount of life given by the sacrifice.</param>
    public void Sacrificed_Card(int value)
    {

    }

    #endregion

    #region PRIVATE FUNCTION
    private void New_State(Combat_State state)
    {
        current_state = state;
        switch (state)
        {
            case Combat_State.Begin_Turn:
                // Player draw
                // Monster display next move
                break;
            case Combat_State.Player_Choose:
                // Wait to the player to choose a card
                break;
            case Combat_State.Card_Preview:
                // Show the choosen card to preview.
                // Display two buttons : play or cancel
                break;
            case Combat_State.Paying:
                // Active card = choosen card
                // Remove card form player
                // Actualize text, cost and buttons

                break;
            case Combat_State.Sacrifising:
                // 
                break;
            case Combat_State.Begin_Turn:

                break;
            case Combat_State.Begin_Turn:

                break;
            default:

                break;
        }
    }
    #endregion
}
