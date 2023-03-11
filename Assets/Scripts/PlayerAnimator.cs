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
        if (player.IsMining()) {
            player.canMove = false;
            StartMining();
        }
    }

    private void StartMining() {
        animator.SetBool(IS_WALKING, false);
        animator.SetTrigger(MINING_ORDER);
        StartCoroutine(DelayMining(2.750f));
    }

    IEnumerator DelayMining(float _delay) {
        yield return new WaitForSeconds(_delay);
        player.isMining = false;
        animator.SetBool(IS_WALKING, player.IsWalking());
        animator.ResetTrigger(MINING_ORDER);
    }
}


