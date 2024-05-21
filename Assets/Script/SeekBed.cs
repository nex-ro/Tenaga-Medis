using UnityEngine;

public class SeekBed : MonoBehaviour
{
    public float speed = 5.0f;
    private Bed targetBed;
    private bool hasCollided = false;

    void Update()
    {
        if (hasCollided) return; // Jika sudah ber-collider, hentikan pencarian

        if (targetBed == null)
        {
            FindTargetBed();
        }
        else
        {
            MoveTowardsTarget();
        }
    }

    void FindTargetBed()
    {
        Bed[] beds = GameObject.FindObjectsOfType<Bed>();
        foreach (Bed bed in beds)
        {
            if (!bed.status)
            {
                targetBed = bed;
                break;
            }
        }
    }

    void MoveTowardsTarget()
    {
        if (targetBed != null)
        {
            Vector3 direction = (targetBed.transform.position - transform.position).normalized;
            transform.position += direction * speed * Time.deltaTime;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        Bed bed = collision.gameObject.GetComponent<Bed>();
        if (bed != null && !bed.status)
        {
            Debug.Log("Collided with bed");
            bed.status = true;
            targetBed = null;
            hasCollided = true; // Set hasCollided to true to stop further searching

            // Adjust position and rotation
            Vector3 newPosition = bed.transform.position;
            newPosition.y += 0.5f;
            transform.position = newPosition;

            Quaternion newRotation = Quaternion.Euler(90, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z);
            transform.rotation = newRotation;
        }
    }
}
