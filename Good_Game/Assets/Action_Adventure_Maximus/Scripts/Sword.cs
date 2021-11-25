using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
{

    [SerializeField]
    // [Range(0.5f, 1.5f)]
    private float hitRate = 1;

    [SerializeField]
    // [Range(1, 100)]
    private int damage = 10;

    [SerializeField] private Transform hitPoint;


	private Animator animator;
    void Start()
    {
    }

    // [SerializeField]
    // private Transform hitPoint;
    private float timer;

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= hitRate)
        {
            // if (Input.GetButton("Fire1"))
            // {
            //     timer = 0f;
            //     SwardHit();
            // }
        }
        // OnCollisionEnter();
    }
    private void SwardHit()
    {
        // muzzleParticle.Play();
        // gunFireSource.Play();

        // Ray ray = Camera.main.ViewportPointToRay(Vector3.one * 0.5f);
        // Debug.DrawRay(ray.origin, ray.direction * 100, Color.red, 2f);


        Ray ray = new Ray(hitPoint.position, hitPoint.forward);
        RaycastHit hitInfo;

        if (Physics.Raycast(ray, out hitInfo, 100))
        {
            var health = hitInfo.collider.GetComponent<PlayerHealth>();
            if (health != null)
            {
                // PlayerHealth.TakeDamage(damage);
            }
        }
    }
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Debug.Log("sward collision");
            // PlayerHealth.TakeDamage(damage);
        }
    }
}
