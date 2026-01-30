using UnityEngine;

public class Pickup : MonoBehaviour
{
    // Rotation des Pickups
    void Update()
    {
        transform.Rotate(new Vector3(15, 30, 45) * Time.deltaTime);
    }
}
