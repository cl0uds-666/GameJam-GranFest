using System.Collections.Generic;
using UnityEngine;

public class DeathandRespawn : MonoBehaviour
{
    public List<GameObject> AliveCars;
    //could try finding some way to detect if players are outside of camera area through a list
    //could also try and use trigger colliders on the bounds of the camera, but i feel like that's more risky

    //use oninvisible to trigger here then if that car which is invisible stays offcamera for 3 secs it dies
    private Vector2 screenSize;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //adding all cars to a list
        for (int i = 0; i < 4; i++)
        {
            AliveCars.Add(GameObject.Find("Car(" + i.ToString() + ")"));
        }
        screenSize = new Vector2(Screen.width, Screen.height);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
