using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ToolController : MonoBehaviour {

    public Animation animation;
    public GameObject giveItem;

    public void Interact(PlayerController pc) {
        if (giveItem != null) pc.addItem(giveItem);
        if (animation != null) animation.Play();
    }

}
