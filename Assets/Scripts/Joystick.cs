using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Joystick : MonoBehaviour {
    public RectTransform Background;
    public RectTransform Stick;

    public Vector2 Value;

    private void Update() {
        if (Input.touchCount > 0) {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began) {
                ActivateStick();
                Background.position = touch.position;
            }

            Stick.position = touch.position; 

            Vector2 toStick = Stick.position - Background.position;
            Vector2 toStickNormalized = toStick.normalized;
            float clampedDistance = Mathf.Clamp(toStick.magnitude, 0, Background.sizeDelta.x * 0.5f);
            Vector2 toStickClamped = toStickNormalized * clampedDistance;

            Stick.position = Background.position + (Vector3)toStickClamped;

            Value = toStickClamped / (Background.sizeDelta.x * 0.5f);

            if (touch.phase == TouchPhase.Ended) {
                DeactivateStick();
                Value = Vector2.zero;
            }
        }
    }

    private void ActivateStick() {
        Background.gameObject.SetActive(true);
        Stick.gameObject.SetActive(true);
    }

    private void DeactivateStick() {
        Background.gameObject.SetActive(false);
        Stick.gameObject.SetActive(false);
    }

}
