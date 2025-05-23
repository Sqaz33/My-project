using UnityEngine; // обязательно

public class Buoy : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            FindObjectOfType<Level1Manager>().OnBuoyCollected();
            Destroy(gameObject);
        }
    }
}
