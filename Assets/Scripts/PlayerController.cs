using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 5f;
    public Transform holdPoint;
    public Animator animator;
    bool isPicking = false;
    Rigidbody rb;
    PickableObject heldObject;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }


    void Update()
    {
        HandlePickDrop();
    }

    void FixedUpdate()
    {
        Move();
    }


    void Move()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        Vector3 moveDir = new Vector3(h, 0, v);

        if (moveDir.magnitude > 0.1f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(moveDir);
            transform.rotation = targetRotation;

            if (!isPicking)
                animator.SetBool("isWalking", true);
        }
        else
        {
            if (!isPicking)
                animator.SetBool("isWalking", false);
        }

        Vector3 newPos = rb.position + moveDir.normalized * speed * Time.fixedDeltaTime;
        rb.MovePosition(newPos);
    }




    void HandlePickDrop()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (heldObject == null)
                TryPick();
            else
                Drop();
        }
    }

    void TryPick()
    {
        if (isPicking) return;

        Collider[] hits = Physics.OverlapSphere(transform.position, 1.5f);

        foreach (Collider hit in hits)
        {
            PickableObject obj = hit.GetComponent<PickableObject>();
            if (obj)
            {
                isPicking = true;

                heldObject = obj;
                Rigidbody objRb = obj.GetComponent<Rigidbody>();
                if (objRb != null)
                {
                    objRb.isKinematic = true;
                    objRb.useGravity = false;
                    objRb.velocity = Vector3.zero;
                    objRb.angularVelocity = Vector3.zero;
                }

                Collider objCol = obj.GetComponent<Collider>();
                if (objCol != null)
                    objCol.enabled = false;

                obj.transform.SetParent(holdPoint);
                obj.transform.localPosition = Vector3.zero;
                obj.transform.localRotation = Quaternion.identity;

                animator.SetTrigger("Pickup");
                Invoke(nameof(EndPick), 0.6f);
                return;
            }
        }
    }


    void EndPick()
    {
        isPicking = false;
    }


    void Drop()
    {
        Transform objTransform = heldObject.transform;
        objTransform.SetParent(null, true);

        Rigidbody objRb = heldObject.GetComponent<Rigidbody>();
        if (objRb != null)
        {
            objRb.isKinematic = false;
            objRb.useGravity = true;
            objRb.velocity = Vector3.zero;
            objRb.angularVelocity = Vector3.zero;
        }

        Collider objCol = heldObject.GetComponent<Collider>();
        if (objCol != null)
            objCol.enabled = true;

        RaycastHit hit;
        Vector3 rayStart = objTransform.position + Vector3.up * 2f;

        if (Physics.Raycast(rayStart, Vector3.down, out hit, 5f))
        {
            objTransform.position = hit.point;
        }

        heldObject = null;
    }



    public PickableObject GetHeldObject()
    {
        return heldObject;
    }

    public void ClearHeldObject()
    {
        heldObject = null;
    }

}
