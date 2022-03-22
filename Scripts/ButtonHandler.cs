using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 
public class ButtonHandler : MonoBehaviour
{
    private DisplayImage currentDisplay;
    
    // private float initialCameraSize;
    // private Vector3 initialCameraPosition;

    void Start()
    {
        currentDisplay = GameObject.Find("displayImage").GetComponent<DisplayImage>();
        // initialCameraSize = Camera.main.orthographicSize;
        // initialCameraPosition = Camera.main.transform.position;
    }

    public void OnRightClickArrow()
    {
        currentDisplay.CurrentWall = currentDisplay.CurrentWall + 1;
    }

    public void OnLeftClickArrow()
    {
        currentDisplay.CurrentWall = currentDisplay.CurrentWall - 1;
    }

    // public void OnClickReturn()
    // {
    //     // return from zoom
    //     if(currentDisplay.CurrentState == DisplayImage.State.zoom)
    //     {
    //         GameObject.Find("displayImage").GetComponent<DisplayImage>().CurrentState = DisplayImage.State.normal;
    //         var zoomInObjects = FindObjectsOfType<ZoomInObject>();
    //         foreach(var zoomInObject in zoomInObjects)
    //         {
    //             zoomInObject.gameObject.layer = 0;
    //         }

    //         Camera.main.orthographicSize = initialCameraSize;
    //         Camera.main.transform.position = initialCameraPosition;
    //     }
    //     else // return from change view
    //     {
    //         currentDisplay.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/wall" + currentDisplay.CurrentWall);
    //         GameObject.Find("displayImage").GetComponent<DisplayImage>().CurrentState = DisplayImage.State.normal;
    //     }
    // }
}
