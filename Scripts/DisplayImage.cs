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
            if (value == 4)
                currentWall = 0;
            else if (value == -1)
                currentWall = 3;
            else 
                currentWall = value;
        }
    }

    private int currentWall;
    private int previousWall;

    void Start()
    {
        previousWall = -1;
        currentWall = 0;
    }

    void Update()
    {
        // change wall when pressing button
        if(currentWall != previousWall)
        {
            GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/wall" + currentWall.ToString());
            GameObject.Find("shadow").GetComponent<ShadowAnimation>().state = ShadowAnimation.State.to_idle;
        }

        previousWall = currentWall;
    }
}
