using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour {

    #region Variables

    public const float MAX_SPEED = .1f;
    public const float ACCELERATION = 1f;
    public const float FRICTION = 1f;

    public enum STATES { MOVE, WAIT };
    public STATES state = STATES.MOVE;

    Rigidbody2D rb;
    Vector2 velocity = Vector2.zero;

    GameObject trigger;

    [Header("Tools")]
    public GameObject pickaxe;
    public GameObject axe;
    public GameObject blade;

    private List<GameObject> tools = new List<GameObject>();
    private int curToolIndex = 0;
    public GameObject curTool;

    [HideInInspector]
    public List<GameObject> inventory;

    #endregion

    #region Functions

    void Start() {
        rb = GetComponent<Rigidbody2D>();

        tools.Clear();

        tools.Add(blade);
    }

    void FixedUpdate() {
        switch (state) {
            case STATES.MOVE:
                move_state();
                break;
            case STATES.WAIT:
                break;
        }
    }

    #endregion

    #region Move State / Tool State

    private void move_state() {
        Vector2 moveInput = Vector2.zero;
        moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;
        if (moveInput != Vector2.zero) {
            velocity = Vector2.MoveTowards(velocity, moveInput * MAX_SPEED, ACCELERATION * Time.fixedDeltaTime);
        } else {
            velocity = Vector2.MoveTowards(velocity, Vector2.zero, FRICTION * Time.fixedDeltaTime);
        }
        velocity.x = Mathf.Clamp(velocity.x, -MAX_SPEED, MAX_SPEED);
        velocity.y = Mathf.Clamp(velocity.y, -MAX_SPEED, MAX_SPEED);
        rb.MovePosition(rb.position + velocity);

        useTools();
    }

    private void useTools() {
        if (Input.GetKeyDown(KeyCode.Tab)) {
            curToolIndex++;
            if (curToolIndex > tools.Count-1) curToolIndex = 0;
            curTool = tools[curToolIndex];
        }
        if (curTool != null && trigger != null) {
            string tag = trigger.tag.ToLower();

            if (Input.GetMouseButtonDown(0)) {
                if (curTool == pickaxe) {
                    if (tag == "pickaxe") {
                        trigger.GetComponent<ToolController>().Interact(this);
                    }
                } else if (curTool == axe) {
                    if (tag == "axe") {
                        trigger.GetComponent<ToolController>().Interact(this);
                    }
                } else if (curTool == blade) {
                    if (tag == "blade") {
                        trigger.GetComponent<ToolController>().Interact(this);
                    }
                }
            }
        }
    }

    #endregion

    #region Triggers

    private void OnTriggerEnter2D(Collider2D other) {
        trigger = other.gameObject;
    }

    #endregion

    #region Inventory

    public void addItem(GameObject go) {
        inventory.Add(go);
    }

    public void addItems(GameObject[] go) {
        for (int i = 0; i < go.Length; i++) {
            inventory.Add(go[i]);
        }
    }

    public void removeItem(GameObject go) {
        inventory.Remove(go);
    }

    public void removeItems(GameObject[] go) {
        for (int i = 0; i < go.Length; i++) {
            inventory.Remove(go[i]);
        }
    }

    public bool hasItem(GameObject go) {
        return inventory.Contains(go);
    }

    public bool hasItems(GameObject[] go) {
        for (int x = 0; x < go.Length; x++) {
            if (!inventory.Contains(go[x])) return false;
        }
        return true;
    }

    #endregion

}
