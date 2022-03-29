using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowAnimation : MonoBehaviour
{
    private Animator anim;
    public float speed = 10f;     // the speed of moving toward the painting.
    public float target_x = -1f;  // the destination of moving toward the painting.
    public bool move_state { get; set; }            // set true by clicking the painting (Painting-Interact).
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        move_state = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (move_state)
            move_to_painting();
    }

    private void move_to_painting()
    {
        Vector3 targetPosition = new Vector3(target_x, this.transform.position.y, this.transform.position.z);
        this.transform.position = Vector3.MoveTowards(this.transform.position, targetPosition, speed * Time.deltaTime);
        // bump into the painting after the shadow approach the painting.
        anim.SetBool("bump", targetPosition == this.transform.position); 
        // stop moving when reach the spot, so "bump" will only be set to true the first time it reach the painting
        if (targetPosition == this.transform.position) {
            move_state = false;
        }
    }

    public void after_bump()
    {
        // this function is called on the last frame of the bumping animation
        // set bump false after the bumping movement
        anim.SetBool("bump", false); 
    }

    public void painting_swing()
    {
        // this function is called in the middle of bumping animation, bumping to the painting
        GameObject.Find("painting0").GetComponent<Painting>().swing_state = true;
    }
}

