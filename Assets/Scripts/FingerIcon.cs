using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FingerIcon : MonoBehaviour
{
    public TextMeshProUGUI Text;

    public void Show() {
        gameObject.SetActive(true);
    }

    public void Hide() {
        gameObject.SetActive(false);
    }
}
