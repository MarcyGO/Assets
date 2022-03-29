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

    // this is set by ShadowAnimation, at the frame of bumping the painting in the animation
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
        //swing();
        //GameObject.Find("shadow").GetComponent<Animator>().Play("shadowBumping");
        GameObject.Find("shadow").GetComponent<ShadowAnimation>().move_state = true;
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


