using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))] public class TestProjectile : MonoBehaviour
{
    [SerializeField] private float speed = 20;
    [SerializeField] private float lifespan = 10;

    private Rigidbody2D physics = null;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        physics = GetComponent<Rigidbody2D>();
        physics.linearVelocity = transform.rotation * new Vector3(speed,0,0);
    }

    // Update is called once per frame
    void Update()
    {
        lifespan -= Time.deltaTime;
        if (lifespan <= 0)
        {
            GameObject.Destroy(gameObject);
        }
    }

}
