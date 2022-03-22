using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This class manage which picture is showing on the screen. 
// e.g: we are looking at which wall, which object
public class DisplayImage : MonoBehaviour
{

    public enum State
    {
        normal, zoom, changedview // so it can only zoom in once
    };

    // Other class can access the class member CurrentState.
    public State CurrentState { get; set; }

    // Other class can access the class member CurrentState.
    public int CurrentWall
    {
        get { return currentWall; }
        set 
        {
            if (value == 5)
                currentWall = 1;
            else if (value == 0)
                currentWall = 4;
            else 
                currentWall = value;
        }
    }

    private int currentWall;
    private int previousWall;

    void Start()
    {
        previousWall = 0;
        currentWall = 1;
    }

    void Update()
    {
        // change wall when pressing button
        if(currentWall != previousWall)
        {
            GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/wall" + currentWall.ToString());
        }

        previousWall = currentWall;
    }
}
