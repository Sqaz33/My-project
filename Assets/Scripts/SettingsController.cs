using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SettingsController : MonoBehaviour
{
    [Header("Color Selection")]
    [SerializeField] public Image _colorPreview;
    [SerializeField] public Slider _redSlider;
    [SerializeField] public Slider _greenSlider;
    [SerializeField] public Slider _blueSlider;
    
    [Header("Buttons")]
    [SerializeField] public Button _confirmButton;
    [SerializeField] public Button _cancelButton;
    
    private Color _currentColor;
    private Color _originalColor;
    
    private void Start()
    {
        // Загружаем сохранённый цвет
        _originalColor = SettingsManager.LoadBuoyColor();
        _currentColor = _originalColor;
        
        // Инициализируем UI
        InitializeColorSliders();
        UpdateColorPreview();
        
        // Назначаем обработчики кнопок
        _confirmButton.onClick.AddListener(OnConfirmClicked);
        _cancelButton.onClick.AddListener(OnCancelClicked);
    }
    
    private void InitializeColorSliders()
    {
        _redSlider.value = _currentColor.r;
        _greenSlider.value = _currentColor.g;
        _blueSlider.value = _currentColor.b;
        
        _redSlider.onValueChanged.AddListener(OnRedChanged);
        _greenSlider.onValueChanged.AddListener(OnGreenChanged);
        _blueSlider.onValueChanged.AddListener(OnBlueChanged);
    }
    
    private void OnRedChanged(float value)
    {
        _currentColor.r = value;
        UpdateColorPreview();
        ApplyTemporaryColor();
    }
    
    private void OnGreenChanged(float value)
    {
        _currentColor.g = value;
        UpdateColorPreview();
        ApplyTemporaryColor();
    }
    
    private void OnBlueChanged(float value)
    {
        _currentColor.b = value;
        UpdateColorPreview();
        ApplyTemporaryColor();
    }
    
    private void UpdateColorPreview()
    {
        if (_colorPreview != null)
            _colorPreview.color = _currentColor;
    }
    
    private void ApplyTemporaryColor()
    {
        SettingsManager.ApplyBuoyColor(_currentColor);
    }
    
    private void OnConfirmClicked()
    {
        // Сохраняем новый цвет
        SettingsManager.SaveBuoyColor(_currentColor);
        ReturnToMainMenu();
    }
    
    private void OnCancelClicked()
    {
        // Восстанавливаем оригинальный цвет
        SettingsManager.ApplyBuoyColor(_originalColor);
        ReturnToMainMenu();
    }
    
    private void ReturnToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}