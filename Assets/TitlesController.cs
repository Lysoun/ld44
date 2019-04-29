using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TitlesController : MonoBehaviour
{
    public Animator animator;

    public TextMeshProUGUI text_turn;
    public TextMeshProUGUI text_phase;

    public void New_Turn(int nbr_turn, string phase_name)
    {
        text_turn.text = nbr_turn.ToString();
        text_phase.text = phase_name;
        animator.SetTrigger("BeginTurn");
    }

    public void New_Phase(string phase_name)
    {
        text_phase.text = phase_name;
        animator.SetTrigger("NewState");
    }
}
