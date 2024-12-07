using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState : MonoBehaviour
{
    public PlayerCurrentState currentPlayerState;
    public InteractionState currentInteractionState;


}
public enum PlayerCurrentState
{
    InAir, Bounced, Boosted, Spinned
}
public enum InteractionState
{
    Interacting, NotInteracting, CanInteract
}

