using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour{

    private const string IS_WALKING = "IsWalking";
    private const string MINING_ORDER = "MiningOrder";

    [SerializeField] private Player player;

    private Animator animator;

    private void Awake() {
        animator = GetComponent<Animator>();
    }


    private void Update() {
        animator.SetBool(IS_WALKING, player.IsWalking());
        if (player.IsMining())
            animator.SetTrigger(MINING_ORDER);
        player.isMining = false;
    }
}


