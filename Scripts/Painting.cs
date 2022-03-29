using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class Painting : MonoBehaviour, IInteractable
{
    // parameter of swinging affect
    public float initial_vel=100;
    public float damping=2;
    public int frame_num=16; 
    public float time_interval=0.3f;

    public enum State
    {
        no_shadow, locked, unlocked // behave differently based on state
    };
    public State state ;//{ get; set; }

    // this is set by ShadowAnimation, at the frame of bumping the painting in the animation
    // enter the swing_state to swing
    public bool swing_state { get; set; } 
    
    //Start is called before the first frame update
    void Start()
    {
        swing_state = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (swing_state){
            swing();
            swing_state = false; // finish the swing effect
        }
            
    }

    public void Interact(DisplayImage currentDisplay)
    {
        if (state == Painting.State.no_shadow)
            swing_state = true;
        else 
        {
            GameObject.Find("shadow").GetComponent<ShadowAnimation>().painting_pos = this.transform.position.x;
            GameObject.Find("shadow").GetComponent<ShadowAnimation>().current_painting = this.name;
            GameObject.Find("shadow").GetComponent<ShadowAnimation>().state = ShadowAnimation.State.to_painting;
            GameObject.Find("shadow").GetComponent<ShadowAnimation>().painting_state = state;
        }
    }

    // The painting swings, reacting to click or bumping.
    public void swing()
    {
        StartCoroutine(waiter());
    }

    IEnumerator waiter()
    {
        //Rotate 90 deg
        for(int i=0; i<frame_num; i++){
            transform.eulerAngles = new Vector3(0, 0, (float)angle(i));
            yield return new WaitForSeconds(time_interval);
        }
    }

    // return the angle of the swing at the given time.
    private double angle(int t){
        return (initial_vel*Math.Sin(t)*Math.Exp(-damping*t));
    }
}


