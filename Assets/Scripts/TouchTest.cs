using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TouchTest : MonoBehaviour
{

    public TextMeshProUGUI TouchCountText;
    public FingerIcon FingerIconPrefab;
    public Transform CanvasTransform;
    public List<FingerIcon> FingerIconsList = new List<FingerIcon>();

    private void Start() {
        for (int  i = 0;  i < 10;  i++) {
            FingerIcon newIcon = Instantiate(FingerIconPrefab, CanvasTransform); 
            FingerIconsList.Add(newIcon);
        }
    }

    void Update() 
    {
        // Кол-во пальцев на экране (в данный момент) 
        TouchCountText.text = Input.touchCount.ToString(); 

        for (int i = 0; i < Input.touchCount; i++) {
            Touch touch = Input.GetTouch(i);
            FingerIconsList[i].Show();
            FingerIconsList[i].transform.position = touch.position;
            FingerIconsList[i].Text.text = touch.fingerId + " " + touch.tapCount;  //ToString(); 
            // touch.tapCount - ко-во нажатий текущим пальцем (можно ослеживать двойные нажатия) 
        }

        for (int i = Input.touchCount; i < 10; i++) {
            FingerIconsList[i].Hide();
        }
        
    }
}
