using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressUI : MonoBehaviour
{
    [SerializeField] private StonePile stonePile;
    [SerializeField] private Image progressImage;
    [SerializeField] private Image ButtonUnpressedImage;

    private void Start() {
        stonePile.OnStonePileChanged += stonePile_OnStonePileChanged;

        progressImage.fillAmount = 0f;

        Hide();
    }

    private void stonePile_OnStonePileChanged(object sender, StonePile.OnProgressChangedEventArgs e) {
        progressImage.fillAmount = e.progressNormalised;

        if (e.progressNormalised == 0f || e.progressNormalised == 1f) {
            Hide();
        } else {
            ButtonUnpressedImage.gameObject.SetActive(false);
            Show();
        }
    }

    private void Show() {
        gameObject.SetActive(true);
    }

    private void Hide() {
        gameObject.SetActive(false);
    }


}
