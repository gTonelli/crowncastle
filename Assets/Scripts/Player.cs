using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    public event EventHandler<OnSelectedBannerChangedEventArgs> OnSelectedBannerChanged;
    public class OnSelectedBannerChangedEventArgs : EventArgs {
        public SpawnBanner selectedBanner;
    }

    public event EventHandler<OnSelectedPostChangedEventArgs> OnSelectedPostChanged;
    public class OnSelectedPostChangedEventArgs : EventArgs {
        public KeepUpgrade selectedPost;
    }

    public event EventHandler<OnSelectedCatChangedEventArgs> OnSelectedCatChanged;
    public class OnSelectedCatChangedEventArgs : EventArgs {
        public CatapultGuy selectedCat;
    }

    [SerializeField] private float moveSpeed = 7f;
    [SerializeField] private GameInput gameInput;

    [SerializeField] private LayerMask stoneLayerMask;
    [SerializeField] private LayerMask collectableMask;
    [SerializeField] private LayerMask spawnerMask;
    [SerializeField] private LayerMask postMask;
    [SerializeField] private LayerMask catapultsControllerMask;

    [SerializeField] private AudioSource footstepSound;
    [SerializeField] private AudioSource mineOrderSound;
    [SerializeField] private AudioSource firstGoldPick;
    [SerializeField] private AudioSource firstStonePick;
    [SerializeField] private AudioSource gameStartSound;

    public bool isWalking;
    public bool isMining;
    public bool isPicking;

    private Vector3 lastInteractDir;

    private StonePile selectedPile;
    private Collectable selectedCollectable;
    private SpawnBanner selectedBanner;
    private KeepUpgrade selectedPost;
    private CatapultGuy selectedCat;

    public int Gold = 2;
    public int Stone = 12;

    public Text GoldCount;
    public Text StoneCount;

    public bool canMove;
    public bool initialised;

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

        if (selectedCollectable != null) {
            selectedCollectable.Interact();
            isPicking = true;
        }

        if (selectedBanner != null) {
            selectedBanner.Interact();
        }

        if (selectedPost != null) {
            selectedPost.Interact();
        }

        if (selectedCat != null) {
            selectedCat.Interact();
        }

        FirstPickSound();
    }

    private void Update() {
        if (initialised) {
            gameStartSound.enabled = true;
        }
        MoveIfNotMining();
        PlayerInteractions();
        WalkingSound();
        SetUIText();
    }

    private void MoveIfNotMining() {
        if (!isMining && !isPicking) {
            PlayerMovement();
            mineOrderSound.enabled = false;
        } else if (isMining){
            mineOrderSound.enabled = true;
        }
    }

    public bool IsWalking() {
        return isWalking;
    }

    public bool IsMining() {
        return isMining;
    }

    public bool IsPicking() {
        return isPicking;
    }

    private void PlayerInteractions() {
        Vector2 inputVector = gameInput.GetMovementVectorNormalized();

        Vector3 moveDir = new Vector3(inputVector.x, 0f, inputVector.y);

        if (moveDir != Vector3.zero) {
            lastInteractDir = moveDir;
        }

        float interactDistance = 2f;

        if (Physics.Raycast(transform.position, lastInteractDir, out RaycastHit raycastHit, interactDistance, stoneLayerMask)) {
            if (raycastHit.transform.TryGetComponent(out StonePile stonePile)) {
                if (stonePile != selectedPile) {
                    SetSelectedPile(stonePile);
                }
            } else {
                SetSelectedPile(null);
            }
        } else {
            SetSelectedPile(null);

        }

        if (Physics.Raycast(transform.position, lastInteractDir, out raycastHit, interactDistance, collectableMask)) {
            if (raycastHit.transform.TryGetComponent(out Collectable collectable)) {
                if (collectable != selectedCollectable) {
                    SetSelectedCollectable(collectable);
                }
            } else {
                SetSelectedCollectable(null);
            }
        } else {
            SetSelectedCollectable(null);

        }

        if (Physics.Raycast(transform.position, lastInteractDir, out raycastHit, interactDistance, spawnerMask)) {
            if (raycastHit.transform.TryGetComponent(out SpawnBanner spawnBanner)) {
                if (spawnBanner != selectedBanner) {
                    SetSelectedBanner(spawnBanner);
                }
            } else {
                SetSelectedBanner(null);
            }
        } else {
            SetSelectedBanner(null);

        }

        if (Physics.Raycast(transform.position, lastInteractDir, out raycastHit, interactDistance, postMask)) {
            if (raycastHit.transform.TryGetComponent(out KeepUpgrade upgradePost)) {
                if (upgradePost != selectedPost) {
                    SetSelectedPost(upgradePost);
                }
            } else {
                SetSelectedPost(null);
            }
        } else {
            SetSelectedPost(null);

        }

        if (Physics.Raycast(transform.position, lastInteractDir, out raycastHit, interactDistance, catapultsControllerMask)) {
            if (raycastHit.transform.TryGetComponent(out CatapultGuy catapultGuy)) {
                if (catapultGuy != selectedCat) {
                    SetSelectedCat(catapultGuy);
                }
            } else {
                SetSelectedCat(null);
            }
        } else {
            SetSelectedCat(null);

        }

        Debug.Log(raycastHit.transform);

        /*Debug.Log(selectedPile);
        Debug.Log(selectedCollectable);*/
        //Debug.Log(selectedBanner);
    }

    private void PlayerMovement() {
        Vector2 inputVector = gameInput.GetMovementVectorNormalized();

        Vector3 moveDir = new Vector3(inputVector.x, 0f, inputVector.y);

        float moveDistance = moveSpeed * Time.deltaTime;
        float playerRadius = 0.3f;
        float playerHeight = 0.3f;
        if (isMining == false && isPicking == false && initialised == true) {
            //canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDir, moveDistance);

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

    private void SetSelectedBanner(SpawnBanner selectedBanner) {
        this.selectedBanner = selectedBanner;
        
        OnSelectedBannerChanged?.Invoke(this, new OnSelectedBannerChangedEventArgs {
            selectedBanner = selectedBanner
        });
    }

    private void SetSelectedPost(KeepUpgrade selectedPost) {
        this.selectedPost = selectedPost;

        OnSelectedPostChanged?.Invoke(this, new OnSelectedPostChangedEventArgs {
            selectedPost = selectedPost
        });
    }

    private void SetSelectedCat(CatapultGuy selectedCat) {
        this.selectedCat = selectedCat;

        OnSelectedCatChanged?.Invoke(this, new OnSelectedCatChangedEventArgs {
            selectedCat = selectedCat
        });
    }

    private void SetUIText() {
        if (GoldCount != null) {
            GoldCount.text = Gold.ToString();
        }
        if (StoneCount != null) {
            StoneCount.text = Stone.ToString();
        }
    }

    private void WalkingSound() {
        if (isWalking) {
            footstepSound.enabled = true;
        } else {
            footstepSound.enabled = false;
        }
    }

    private void FirstPickSound() {
        if (Gold > 0) {
            firstGoldPick.enabled = true;
        }

        if (Stone > 0) {
            firstStonePick.enabled = true;
        }
    }
}
