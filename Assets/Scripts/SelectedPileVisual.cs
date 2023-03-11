using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectedPileVisual : MonoBehaviour
{
    [SerializeField] private StonePile stonePile;
    [SerializeField] private GameObject visualGameObject;

    private void Start() {
        Player.Instance.OnSelectedPileChanged += Player_OnSelectedPileChanged;
    }

    private void Player_OnSelectedPileChanged(object sender, Player.OnSelectedPileChangedEventArgs e) {
        if (e.selectedPile == stonePile) {
            Show();
        } else {
            Hide();
        }
    }

    private void Show() {
        if (visualGameObject != null) {
            visualGameObject.SetActive(true);
        }
    }

    private void Hide() {
        if (visualGameObject != null) {
            visualGameObject.SetActive(false);
        }
    }

}
