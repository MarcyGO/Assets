using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowAnimation : MonoBehaviour
{
    /*-----------------------------public variable-----------------------------*/
    public float speed = 10f;     // the speed of 
    public float target_distance = -3.4f;  // the distance btw the destination and the painting.
    
    // transform.x of the idle position of each wall
    public float[] idle_position = new float[4];

    // set by clicking the painting (Painting-Interact).
    public float painting_pos { get; set; }            
    public string current_painting { get; set; } // we have to call its swing method.
    public Painting.State painting_state { get; set; }

    public enum State
    {
        to_painting, to_idle, idle // movement state
    };

    // Other class can access the class member state.
    public State state { get; set; }
    
    /* -----------------------------private variable-----------------------------*/
    private Animator anim;
    private DisplayImage currentDisplay;


    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        currentDisplay = GameObject.Find("displayImage").GetComponent<DisplayImage>();
        // default value that will be set by clicked painting
        painting_pos = -100;
        current_painting = "painting1";
        state = ShadowAnimation.State.idle;
    }

    // Update is called once per frame
    void Update()
    {
        if (state == ShadowAnimation.State.to_painting)
            move_to_painting(painting_pos, current_painting);
        else if (state == ShadowAnimation.State.to_idle)
            move_to_idle_position(idle_position[currentDisplay.CurrentWall]);
    }

    /*-------------------------------MOVING FUNCTION-------------------------------*/
    
    private void move_to_painting(float position, string current_painting)
    {
        Vector3 targetPosition = new Vector3(position+target_distance, this.transform.position.y, this.transform.position.z);
        this.transform.position = Vector3.MoveTowards(this.transform.position, targetPosition, speed * Time.deltaTime);
        if(painting_state == Painting.State.locked)
            // bump into the painting after the shadow approach the painting.
            anim.SetBool("bump", targetPosition == this.transform.position); 
        else if(painting_state == Painting.State.unlocked)
            anim.SetBool("enter", targetPosition == this.transform.position); 
        // stop moving when reach the spot, so "bump" will only be set to true the first time it reach the painting
        if (targetPosition == this.transform.position) {
            state = ShadowAnimation.State.idle;
        }
    }

    private void move_to_idle_position(float position)
    {
        Vector3 targetPosition = new Vector3(position, this.transform.position.y, this.transform.position.z);
        this.transform.position = Vector3.MoveTowards(this.transform.position, targetPosition, speed * Time.deltaTime);
        // stop moving when reach the spot
        if (targetPosition == this.transform.position) {
            state = ShadowAnimation.State.idle;
        }
    }

    /*-----------------------------ANIMATION EVENT FUNCTION-----------------------------*/

    // this function is called on the last frame of the bumping animation
    // set bump false after the bumping movement
    public void after_bump()
    {
        anim.SetBool("bump", false); 
        state = ShadowAnimation.State.to_idle;
    }

    // this function is called in the middle of bumping animation, bumping to the painting
    public void painting_swing()
    {
        GameObject.Find(current_painting).GetComponent<Painting>().swing_state = true;
    }

    // this function is called on the last frame of the entering animation
    // change view to inside the painting.
    public void after_enter()
    {
        anim.SetBool("enter", false); 
        // when change the sprite of currentDisplay, sceneManager will show related object and set other inactive
        currentDisplay.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/" + current_painting + "View");
        currentDisplay.CurrentState = DisplayImage.State.painting;
    }

    // on click return
    public void return_from_painting()
    {
        if(painting_state == Painting.State.solved)
            anim.SetBool("exit", true); 
        else
            anim.SetBool("return", true); 
    }

    public void after_return()
    {
        anim.SetBool("return", false); 
        state = ShadowAnimation.State.to_idle;
    }

    public void after_exit()
    {
        anim.SetBool("exit", false); 
        state = ShadowAnimation.State.to_idle;
    }
}

