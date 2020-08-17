using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.ComTypes;
using UnityEngine;

public class ToolController : MonoBehaviour
{
    public Animation animation;
    public GameObject giveItem;

    public void Interact() {
        if(animation != null)
            animation.Play();
        Destroy(gameObject);
    }
}
