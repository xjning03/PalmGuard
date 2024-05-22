using System.Collections;
using UnityEngine;

public class Controller : MonoBehaviour
{
    // Start is called before the first frame update
    public float moveSpeed;
    private bool isMoving;
    public Vector2 input;
    private Animator animator;
    public LayerMask soldObjectsLayer;
    public LayerMask interactableLayer;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    public void HandleUpdate()
    {
        if (!isMoving)
        {
            input.x = Input.GetAxisRaw("Horizontal");
            input.y = Input.GetAxisRaw("Vertical");

            if(input.x!=0) input.y = 0;

            if(input!=Vector2.zero)
            {

                animator.SetFloat("moveX", input.x);
                animator.SetFloat("moveY", input.y);

                var targetPos = transform.position;
                targetPos.x += input.x;
                targetPos.y += input.y;

                if (IsWalkable(targetPos))
                    StartCoroutine(Move(targetPos));
            }
        }

        animator.SetBool("isMoving", isMoving);

        if (Input.GetKeyDown(KeyCode.Z))
            Interact();

    }

    void Interact()
    {
        var facingDir = new Vector3(animator.GetFloat("moveX"), animator.GetFloat("moveY"));
        var interactPos = transform.position + facingDir;
    
        var collider = Physics2D.OverlapCircle(interactPos, 0.2f, interactableLayer);
        if (collider != null)
        {
            collider.GetComponent<Interactable>()?.Interact();
        }
    }

    IEnumerator Move(Vector3 targetPos)
    {
        isMoving = true;

        while ((targetPos - transform.position).sqrMagnitude > Mathf.Epsilon)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPos, moveSpeed * Time.deltaTime);
            yield return null;
        }
        transform.position = targetPos;

        isMoving = false;
    }

    private bool IsWalkable(Vector3 targetpos)
    {
        if(Physics2D.OverlapCircle(targetpos, 0.2f, soldObjectsLayer | interactableLayer) != null)
        {
            return false;
        }
        return true;
    }

}
