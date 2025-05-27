using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Touch = UnityEngine.InputSystem.EnhancedTouch.Touch;
[RequireComponent(typeof(TouchSimulation))]


public class ButtonHeightConfig : MonoBehaviour
{
    [Header("Buttons")]
    [SerializeField] public GameObject[] Buttons;
	private int buttonsIndex = 0;
	
	void Awake()
    {
        EnhancedTouchSupport.Enable();   
    }

    // Start is called before the first frame update
    void Start()
    {
		//for every button, tap on the screen where you want them
        //foreach (GameObject button in Buttons)
        //{
		//	//if you touch with one finger, it gets the position of where you touch on the screen and moves the button to that location
		//	if (Touch.activeFingers.Count == 1)
		//	{
		//		Vector3 newPos = Camera.main.ScreenToWorldPoint(Touch.activeTouches[0].screenPosition);
        //        newPos = new Vector3(newPos.x, newPos.y, 0f);
		//		button.transform.position = newPos;
		//	}
        //}
    }

    // Update is called once per frame
    void Update()
    {
		//if there are still buttons to go in the list then move the button position of this button
		if(buttonsPos != (Buttons.Length - 1))
		//if you touch with one finger, it gets the position of where you touch on the screen and moves the button to that location
        if (Touch.activeFingers.Count == 1)
			{
				Vector3 newPos = Camera.main.ScreenToWorldPoint(Touch.activeTouches[0].screenPosition);
                newPos = new Vector3(newPos.x, newPos.y, 0f);
				buttons[buttonsIndex].transform.position = newPos;
				buttonsIndex += 1;
			}
		else
		{
			buttonsIndex = 0
		}
    }
}
