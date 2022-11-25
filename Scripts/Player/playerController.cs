using UnityEngine;
using System.Linq;
using System.Collections;

// Main controller for everything related to the player
// Some features need to be moved to different classes and optimized
// It's important to keep this class very SOLID
public class playerController : MonoBehaviour
{
    [SerializeField] string stateName;
    [SerializeField] Vector3 rbVelocity;
    public playerStatesController states;
    public playerAtributes atributes;
    public playerAnimController animationController;
    [HideInInspector] public playerMoveController moveController;

    Collider2D[] colliders;
    public Collider2D[] Colliders
    {
        get { return colliders; }
    }

    cooldownSystem cooldownSystem;
    public cooldownSystem CooldownSystem
    {
        get { return cooldownSystem; }
    }

    Rigidbody2D rb;
    public Rigidbody2D Rb
    {
        get { return rb; }
    }

    HingeJoint2D hj;
    public HingeJoint2D Hj
    {
        get { return hj; }
    }

    Vector2 movement;
    public Vector2 Movement
    {
        get { return movement; }
        set { movement = value; }
    }
    int lastDir = 1;
    public int LastDir
    {
        get { return lastDir; }
        set { lastDir = value; }
    }
    public Vector3 hitPosition;
    [SerializeField] float distanceToWall;
    Vector3 startCastPos;
    public Vector3 StartCastPos
    {
        get { return startCastPos; }
    }
    [SerializeField] cameraOffset camOffset;
    public cameraOffset CamOffset
    {
        get { return camOffset; }
    }
    [HideInInspector] public Vector3 respawnPoint;
    public GameObject dust;
    bool dying;

    // in case is needed
    // void stringInteraction(string parameter, string value)
    // {
    // }

    void boolInteraction(string parameter, bool value)
    {
        if (parameter.Contains("trap"))
        {
            if (value)
                states.transitionToState(states.trappedState);
            else if (states.CurrentState == states.trappedState)
                states.transitionToState(states.idleState);
        }
        if (parameter.Contains("death"))
        {
            if (value)
            {
                transform.position = respawnPoint;
            }
        }
        if (parameter.Contains("hauntDeath"))
        {
            if (value)
            {
                states.transitionToState(states.deathState);
            }
        }
        if(parameter.Contains("endTalk"))
        {
            if(value)
                states.transitionToState(states.idleState);
        }

    }

    void Awake()
    {
        Cursor.visible = false;
        GetComponent<playerInputController>().player = this;
        states = GetComponent<playerStatesController>();
        states.player = this;
        animationController = GetComponent<playerAnimController>();
        rb = GetComponent<Rigidbody2D>();
        hj = GetComponent<HingeJoint2D>();
        moveController = GetComponent<playerMoveController>();
        cooldownSystem = GetComponent<cooldownSystem>();
        colliders = GetComponents<Collider2D>();
        hj.enabled = false;
        startCastPos = transform.position + Vector3.down * transform.localScale.y / 3.25f;
        dust.SetActive(false);
    }

    // Start is called before the first frame update
    void Start()
    {
        states.transitionToState(states.idleState);
        // gameEvents.current.onPassValue += stringInteraction; does nothing for now
        gameEvents.current.onPassBool += boolInteraction;
        gameEvents.current.onPassPosition += delegate (string parameter, Vector3 pos)
        {
            if (parameter.Contains("setRespawn"))
                respawnPoint = pos;
        };

    }

    void FixedUpdate()
    {
        moveController.Move(movement.x * atributes.speed * Time.fixedDeltaTime, atributes.xMoveMul);

        if (states.CurrentState != states.talkState)
            moveController.haunt();

        moveController.clampVelocity(atributes.minY_Velocity, atributes.maxY_Velocity);

        states.CurrentState.Update(this);

        foreach (RaycastHit2D r in Physics2D.RaycastAll(transform.position, Vector2.left * lastDir, 1).ToList().FindAll(item => item.transform.gameObject != null))
        {
            if (r.transform.gameObject.layer == LayerMask.NameToLayer("Walls"))
            {
                hitPosition = r.point;
                continue;
            }
        }
        startCastPos = transform.position + Vector3.down * transform.localScale.y / 3.25f;
        distanceToWall = Vector2.Distance(transform.position, hitPosition);
        // if (!called && Mathf.Abs(rb.velocity.magnitude) > 0.01f)
        // {
        //     gameEvents.current.passFloat("haunt", 0);
        //     called = true;
        // }
        // if (called && Mathf.Abs(rb.velocity.magnitude) < 0.01f)
        // {
        //     gameEvents.current.passFloat("haunt", idleTime);
        //     called = false;
        // }

    }

    // Update is called once per frame
    void Update()
    {
        stateName = states.CurrentState.stateName(this);
        rbVelocity = rb.velocity;
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        states.CurrentState.OnCollisionEnter(this, other);
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        states.CurrentState.OnCollisionExit(this, other);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + Vector3.down * 12);
    }
}
