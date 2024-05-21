using UnityEngine;

public class PickupObject : MonoBehaviour
{
    public float pickupRange = 3f; // Jarak maksimum untuk mengambil objek
    public LayerMask pickupLayer; // Layer yang berisi objek yang bisa diambil

    private GameObject heldObject; // Objek yang sedang dipegang

    void Update()
    {
        // Jika tombol Interaksi ditekan, coba ambil objek
        if (Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("pres");
            if (heldObject == null)
            {
                AttemptPickup();
            }
            else
            {
                DropObject();
            }
        }
    }

    void AttemptPickup()
    {
        RaycastHit hit;
        // Raycast ke depan dari posisi pemain
        if (Physics.Raycast(transform.position, transform.forward, out hit, pickupRange, pickupLayer))
        {
            // Cek apakah objek yang bisa diambil
            if (hit.collider.CompareTag("Pickupable"))
            {
                // Ambil objek
                heldObject = hit.collider.gameObject;
                heldObject.transform.SetParent(transform); // Set parent menjadi pemain
                heldObject.transform.localPosition = Vector3.up; // Posisi relatif terhadap pemain
                heldObject.GetComponent<Rigidbody>().isKinematic = true; // Nonaktifkan physics
            }
        }
    }

    void DropObject()
    {
        // Lepaskan objek
        heldObject.transform.SetParent(null); // Reset parent
        heldObject.GetComponent<Rigidbody>().isKinematic = false; // Aktifkan physics
        heldObject = null;
    }
}