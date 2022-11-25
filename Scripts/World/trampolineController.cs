using System.Collections;
using UnityEngine;


// Controls the interaction between player and trampoline
// Infinite force addition should be removed
public class trampolineController : MonoBehaviour
{
    [SerializeField] float impulse;
    Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.transform.CompareTag("Player"))
        {
            StartCoroutine(trampoline(other.rigidbody));
        }
    }

    IEnumerator trampoline(Rigidbody2D other)
    {
        anim.Play("trampoline");
        yield return new WaitForSeconds(.1f);
        other.AddForce(Vector2.up * impulse, ForceMode2D.Impulse);
        yield return new WaitForSeconds(animationInfo.animLength(anim, "trampoline"));
        anim.Play("trampoline-IDLE");
    }

}
