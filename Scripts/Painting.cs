using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class Painting : MonoBehaviour, IInteractable
{
    // Start is called before the first frame update
    // void Start()
    // {
    //     angle = this.transform.position.z;
    // }

    public void Interact(DisplayImage currentDisplay)
    {
        
    }

    // private void swing()
    // {
    //     this.transform.eulerAngles = new Vector3(this.transform.eulerAngles.x, this.transform.eulerAngles.y, this.transform.eulerAngles.z+30);
    // }

    void Start()
    {
        StartCoroutine(waiter());
    }

    IEnumerator waiter()
    {
        //Rotate 90 deg
        transform.Rotate(new Vector3(90, 0, 0), Space.World);

        //Wait for 4 seconds
        yield return new WaitForSeconds(4);

        //Rotate 40 deg
        transform.Rotate(new Vector3(40, 0, 0), Space.World);

        //Wait for 2 seconds
        yield return new WaitForSeconds(2);

        //Rotate 20 deg
        transform.Rotate(new Vector3(20, 0, 0), Space.World);
        
    }
}

