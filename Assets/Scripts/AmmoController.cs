using UnityEngine;

public class AmmoController : MonoBehaviour
{
    private Rigidbody rb;

    void Start()
    {
        Destroy(gameObject, 3f);
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // 速度が一定以上ある場合、その方向を向く
        if (rb.linearVelocity.sqrMagnitude > 0.1f)
        {
            transform.rotation = Quaternion.LookRotation(rb.linearVelocity);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("ヒットしました");
        Destroy(gameObject);
    }

    
}
