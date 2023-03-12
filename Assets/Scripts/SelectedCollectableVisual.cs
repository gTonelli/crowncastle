using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectedCollectableVisual : MonoBehaviour {
    [SerializeField] private Collectable collectable; 
    [SerializeField] private GameObject visualGameObject;

    private void Start() {
        Player.Instance.OnSelectedCollectableChanged += Player_OnSelectedCollectableChanged;
    }

    private void Player_OnSelectedCollectableChanged(object sender, Player.OnSelectedCollectableChangedEventArgs e) {
        if (e.selectedCollectable == collectable) {
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
