using UnityEngine;

// I used this to test some things on the editor, not much useful I think
[ExecuteInEditMode]
public class addForce : MonoBehaviour
{
    [SerializeField]Rigidbody2D rb;
    [SerializeField]Vector2 direction;
    [SerializeField]float force;

    public void push()
    {
        rb.AddForce(force*direction);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
