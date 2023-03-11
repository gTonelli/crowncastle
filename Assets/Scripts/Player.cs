using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    public static Player Instance { get; set; }

    public event EventHandler<OnSelectedPileChangedEventArgs> OnSelectedPileChanged;
    public class OnSelectedPileChangedEventArgs : EventArgs {
        public StonePile selectedPile;
    }

    public event EventHandler<OnSelectedCollectableChangedEventArgs> OnSelectedCollectableChanged;
    public class OnSelectedCollectableChangedEventArgs : EventArgs {
        public Collectable selectedCollectable;
    }

    [SerializeField] private float moveSpeed = 7f;
    [SerializeField] private GameInput gameInput;
    [SerializeField] private LayerMask stoneLayerMask;
    [SerializeField] private LayerMask CollectableMask;
    [SerializeField] private AudioSource footstepSound;
    [SerializeField] private AudioSource mineOrderSound;

    public bool isWalking;
    public bool isMining;
    private Vector3 lastInteractDir;
    private StonePile selectedPile;
    private Collectable selectedCollectable;

    public bool canMove;

    private void Awake() {
        if (Instance != null) {
            Debug.LogError("More than one Player in scene!");
        }
        Instance = this;
    }

    private void Start() {
        gameInput.OnInteractionAction += GameInput_OnInteractionAction;
    }

    private void GameInput_OnInteractionAction(object sender, System.EventArgs e) {
        if (selectedPile != null) {
            selectedPile.Interact();
            isMining = true;
        }
    }

    private void Update() {
        Debug.Log("isMining: " + isMining);
        if (!isMining) {
            PlayerMovement();
            Debug.Log("Not Mining");
            mineOrderSound.enabled = false;
        } else {
            Debug.Log("Mining");
            mineOrderSound.enabled = true;
        }
        PlayerInteractions();
        if (isWalking) {
            footstepSound.enabled = true;
        } else {
            footstepSound.enabled = false;
        }

    }

    public bool IsWalking() {
        return isWalking;
    }

    public bool IsMining() {
        return isMining;
    }


    private void PlayerInteractions() {
        Vector2 inputVector = gameInput.GetMovementVectorNormalized();

        Vector3 moveDir = new Vector3(inputVector.x, 0f, inputVector.y);

        if (moveDir != Vector3.zero) {
            lastInteractDir = moveDir;
        }

        float interactDistance = 2f;

        if (Physics.Raycast(transform.position, lastInteractDir, out RaycastHit raycastHitPile, interactDistance, stoneLayerMask)) {
            if (raycastHitPile.transform.TryGetComponent(out StonePile stonePile)) {
                if (stonePile != selectedPile) {
                    SetSelectedPile(stonePile);
                }
            } else {
                SetSelectedPile(null);
            }
        } else {
            SetSelectedPile(null);

        }

        if (Physics.Raycast(transform.position, lastInteractDir, out RaycastHit raycastHitCollectable, interactDistance, CollectableMask)) {
            if (raycastHitCollectable.transform.TryGetComponent(out Collectable collectable)) {
                if (collectable != selectedCollectable) {
                    SetSelectedCollectable(selectedCollectable);
                }
            } else {
                SetSelectedCollectable(null);
            }
        } else {
            SetSelectedCollectable(null);

        }

        Debug.Log(selectedPile);
    }

    private void PlayerMovement() {
        Vector2 inputVector = gameInput.GetMovementVectorNormalized();

        Vector3 moveDir = new Vector3(inputVector.x, 0f, inputVector.y);

        float moveDistance = moveSpeed * Time.deltaTime;
        float playerRadius = 0.7f;
        float playerHeight = 0.3f;
        if (isMining == false) {
            canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDir, moveDistance);

            if (!canMove) {
                //Cannot move towards moveDir

                //Attempt only X movement
                Vector3 moveDirX = new Vector3(moveDir.x, 0, 0).normalized;
                canMove = moveDir.x != 0 && !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDirX, moveDistance);

                if (canMove) {
                    //Can move towards moveDirX
                    moveDir = moveDirX;
                } else {
                    //Cannot move towards moveDirX

                    //Attempt only Z movement
                    Vector3 moveDirZ = new Vector3(0, 0, moveDir.z).normalized;
                    canMove = moveDir.z != 0 && !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDirZ, moveDistance);

                    if (canMove) {
                        //Can move towards moveDirZ
                        moveDir = moveDirZ;
                    }
                }
            }

            if (canMove) {
                transform.position += moveDir * Time.deltaTime * moveSpeed;
            }

            float rotateSpeed = 6f;
            isWalking = moveDir != Vector3.zero;

            transform.forward = Vector3.Slerp(transform.forward, moveDir, Time.deltaTime * rotateSpeed);
        }
    }

    private void SetSelectedPile(StonePile selectedPile) {
        this.selectedPile = selectedPile;

        OnSelectedPileChanged?.Invoke(this, new OnSelectedPileChangedEventArgs {
            selectedPile = selectedPile
        });
    }

    private void SetSelectedCollectable(Collectable selectedCollectable) {
        this.selectedCollectable = selectedCollectable;

        OnSelectedCollectableChanged?.Invoke(this, new OnSelectedCollectableChangedEventArgs {
            selectedCollectable = selectedCollectable
        });
    }
}
