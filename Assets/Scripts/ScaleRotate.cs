using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleRotate : MonoBehaviour {


    public Vector2 StartCenter;
    public float StartSummDistance;

    public float StartScale;
    public Vector3 StartPosition;

    public float MoveMultiplier = 0.005f;
    public float RotationMultiplier = 0.05f;

    Vector2[] _toCenterOldArray = new Vector2[5];

    void Start() {

    }

    void Recalculate() {

        StartScale = transform.localScale.x;
        StartPosition = transform.position;

        Vector2 summ = Vector2.zero;
        int numberOfActiveFingers = 0;

        for (int i = 0; i < Input.touchCount; i++) {
            if (Input.GetTouch(i).phase != TouchPhase.Ended) {
                summ += Input.GetTouch(numberOfActiveFingers).position;
                numberOfActiveFingers++;
            }
        }

        StartCenter = summ / numberOfActiveFingers;

        float summDistance = 0f;
        int index = 0;
        for (int i = 0; i < Input.touchCount; i++) {
            if (Input.GetTouch(i).phase != TouchPhase.Ended) {

                Vector2 toCenter = StartCenter - Input.GetTouch(i).position;
                float distanceFromCenter = toCenter.magnitude;

                _toCenterOldArray[index] = toCenter;
                summDistance += distanceFromCenter;
                index++;
            }
        }

        StartSummDistance = summDistance / numberOfActiveFingers;

    }


    void Update() {

        if (Input.touchCount < 2) return;

        Vector2 summ = Vector2.zero;
        for (int i = 0; i < Input.touchCount; i++) {

            if (Input.GetTouch(i).phase == TouchPhase.Ended || Input.GetTouch(i).phase == TouchPhase.Began) {
                Recalculate();
                return;
            }

            summ += Input.GetTouch(i).position;
        }
        Vector2 center = summ / Input.touchCount;

        float summDistance = 0f;
        for (int i = 0; i < Input.touchCount; i++) {
            float distanceFormCenter = Vector2.Distance(center, Input.GetTouch(i).position);
            summDistance += distanceFormCenter;
        }

        summDistance = summDistance / Input.touchCount;
        float deltaPercent = summDistance / StartSummDistance;
        float scale = Mathf.Clamp(StartScale * deltaPercent, 0.8f, 6f);

        transform.localScale = new Vector3(scale, scale, scale);

        // MOVE
        Vector2 centerOffset = center - StartCenter;
        transform.position = StartPosition + (Vector3)centerOffset * MoveMultiplier;

        // rotation
        float deltaAngle = 0f;
        for (int i = 0; i < Input.touches.Length; i++) {
            Touch item = Input.touches[i];
            Vector2 toCenter = center - item.position;
            Vector2 toCenterOld = _toCenterOldArray[i];
            float angle = Vector2.SignedAngle(toCenterOld, toCenter);
            deltaAngle += angle;
            _toCenterOldArray[i] = toCenter;
        }

        transform.Rotate(0, 0, deltaAngle * RotationMultiplier);


    }

}
