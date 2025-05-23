using UnityEngine; // обязательно

[RequireComponent(typeof(Renderer))]
public class Buoy : MonoBehaviour
{
    private Renderer _renderer;
    private Material _material;
    
    private void Awake()
    {
        _renderer = GetComponent<Renderer>();
        _material = _renderer.material;
        
        // Применяем сохранённый цвет при старте
        ApplyColor(SettingsManager.LoadBuoyColor());
    }
    
    public void ApplyColor(Color color)
    {
        if (_material != null)
        _material.color = color;
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            FindObjectOfType<Level1Manager>().OnBuoyCollected();
            Destroy(gameObject);
        }
    }
}
