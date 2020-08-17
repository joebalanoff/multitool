using System.Collections;
using System.Collections.Generic;
using System.Resources;
using UnityEngine;
using UnityEngine.U2D;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour {
    #region variables

    public enum STATES { MOVE, WAIT };
    [Header("State-Machine")]
    public STATES state;

    public bool developerMode;

    private float MAX_SPEED = 0.1f;
    private float ACCELERATION = .5f;
    private float FRICTION = .5f;

    private KeyCode switchState = KeyCode.Space;
    private KeyCode switchTool = KeyCode.Tab;
    private KeyCode[] useKey = { KeyCode.K, KeyCode.V };

    private Rigidbody2D rb;
    private Vector2 velocity;

    [Header("Tools")]
    public GameObject pickaxe;
    public GameObject axe;
    public GameObject blade;

    private List<GameObject> tools = new List<GameObject>();

    public GameObject currentInteraction;

    public Transform toolHolder;
    public GameObject curTool;
    private int curIndex = 0;

    public List<GameObject> inventory;

    #endregion

    #region predefined voids

    void Start() {
        rb = GetComponent<Rigidbody2D>();
        velocity = Vector2.zero;

        tools.Clear();

        tools.Add(pickaxe);
        tools.Add(axe);
        tools.Add(blade);
    }

    public void Update() {
        switch (state) {
            case STATES.MOVE:
                moveState(Time.fixedDeltaTime);
                break;
            case STATES.WAIT:
                waitState();
                break;
        }
    }

    #endregion

    #region moveState

    private void moveState(float delta) {
        Vector2 moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;
        if (moveInput != Vector2.zero) {
            velocity = Vector2.MoveTowards(velocity, moveInput, ACCELERATION * delta);
        } else {
            velocity = Vector2.MoveTowards(velocity, Vector2.zero, FRICTION * delta);
        }
        velocity = Vector2.ClampMagnitude(velocity, MAX_SPEED);


        rb.MovePosition(rb.position + velocity);

        if (KeyPressed(switchTool)) {
            curIndex++;
            if (curIndex >= tools.Count) {
                curIndex = 0;
            }
            setTool(curIndex);
        }

        if (curTool != null) {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            float angle = Mathf.Atan2(mousePos.x - curTool.transform.position.x, mousePos.y - curTool.transform.position.y) * Mathf.Rad2Deg;
            toolHolder.localRotation = Quaternion.Euler(new Vector3(0f, 0f, -angle));

            if (currentInteraction != null) {
                string tag = currentInteraction.tag.ToLower();

                ToolController tc = currentInteraction.GetComponent<ToolController>();

                if (tc != null) {
                    if (curTool == pickaxe) {
                        if (tag == "pickaxe") {
                            tc.Interact();
                        }
                    } else if (curTool == axe) {
                        if (tag == "axe") {
                            tc.Interact();
                        }
                    } else if (curTool == blade) {
                        if (tag == "blade") {
                            tc.Interact();  
                        }
                    }
                }
            }

        }

        if (developerMode) {
            if (KeyPressed(switchState)) {
                setState(STATES.WAIT);
            }
        }
    }

    #endregion

    #region waitState

    private void waitState() {
        if (developerMode) {
            if (KeyPressed(switchState)) {
                setState(STATES.MOVE);
            }
        }
    }

    #endregion

    #region KeyPressed

    private bool KeyPressed(KeyCode key) {
        if (Input.GetKeyDown(key)) return true;
        return false;
    }
    private bool KeyPressed(KeyCode[] keys) {
        foreach (KeyCode key in keys)
            if (Input.GetKeyDown(key)) return true;
        return false;
    }

    #endregion

    #region Setters

    private void setState(STATES state) {
        this.state = state;
        velocity = Vector2.zero;
    }
    private void setTool(int index) {
        if (index >= 0 && index < tools.Count) {
            if (tools[index] != null) {
                curTool = tools[index];
                foreach (Transform child in toolHolder.transform) {
                    GameObject.Destroy(child.gameObject);
                }
                GameObject go = Instantiate(curTool, Vector3.zero, toolHolder.rotation, toolHolder);
                go.transform.localPosition = new Vector3(0f, 0.5f, 0f);
            }
        }
    }

    #endregion

    #region Inventory

    public void addItem(GameObject go) {
        inventory.Add(go);
    }

    public void removeItem(GameObject go) {
        inventory.Remove(go);
    }

    public bool hasItem(GameObject go) {
        return inventory.Contains(go);
    }

    public void addItems(GameObject[] go) {
        foreach (GameObject g in go) {
            inventory.Add(g);
        }
    }

    public void removeItems(GameObject[] go) {
        foreach (GameObject g in go) {
            inventory.Remove(g);
        }
    }

    public bool hasItems(GameObject[] go) {
        foreach (GameObject g in go) {
            if (!inventory.Contains(g)) return false;
        }
        return true;
    }

    #endregion

    void OnTriggerEnter2D(Collider2D other) {
        currentInteraction = other.gameObject;
    }
}
