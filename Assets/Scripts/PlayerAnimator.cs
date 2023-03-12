using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour{

    private const string IS_WALKING = "IsWalking";
    private const string MINING_ORDER = "MiningOrder";
    private const string PICKING_UP = "PickingUp";

    [SerializeField] private Player player;

    private Animator animator;

    private void Awake() {
        animator = GetComponent<Animator>();
    }


    private void Update() {
        animator.SetBool(IS_WALKING, player.IsWalking());
        if (player.IsMining()) {
            player.canMove = false;
            StartMining();
        }
        if (player.IsPicking()) {
            player.canMove = false;
            StartPicking();
        }
    }

    private void StartMining() {
        animator.SetBool(IS_WALKING, false);
        animator.SetTrigger(MINING_ORDER);
        StartCoroutine(DelayMining(2.750f));
    }

    private void StartPicking() {
        animator.SetBool(IS_WALKING, false);
        animator.SetTrigger(PICKING_UP);
        StartCoroutine(DelayPicking(2.533f));
    }

    IEnumerator DelayMining(float _delay) {
        yield return new WaitForSeconds(_delay);
        player.isMining = false;
        animator.SetBool(IS_WALKING, player.IsWalking());
        animator.ResetTrigger(MINING_ORDER);
    }

    IEnumerator DelayPicking(float _delay) {
        yield return new WaitForSeconds(_delay);
        player.isPicking = false;
        animator.SetBool(IS_WALKING, player.IsWalking());
        animator.ResetTrigger(PICKING_UP);
    }
}


