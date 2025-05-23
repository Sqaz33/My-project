using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class SettingsController : MonoBehaviour
{
    [Header("Sensivity selection")]
    [SerializeField] public Slider _sensSlider;

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

    private float _currentSens;
    private float _originalSens;

    private void Start()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        // Загружаем сохранённый цвет
        _originalSens = SettingsManager.LoadBoatSensivity();
        _currentSens = _originalSens;
        InitializeSensivitySliders();

        _originalColor = SettingsManager.LoadBuoyColor();
        _currentColor = _originalColor;

        // Инициализируем UI
        InitializeColorSliders();
        UpdateColorPreview();

        // Назначаем обработчики кнопок
        _confirmButton.onClick.AddListener(OnConfirmClicked);
        _cancelButton.onClick.AddListener(OnCancelClicked);
    }

    private void InitializeSensivitySliders()
    {
        _sensSlider.value = _currentSens;
        _sensSlider.onValueChanged.AddListener(OnSensChanged);
    }

    private void OnSensChanged(float value)
    {
        _currentSens = value;
        ApplyTemporarySens();
    }

    private void ApplyTemporarySens()
    {
        SettingsManager.ApplyBoatSensivity(_currentSens);
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
        SettingsManager.SaveBoatSensitivity(_currentSens);
        ReturnToMainMenu();
    }

    private void OnCancelClicked()
    {
        // Восстанавливаем оригинальный цвет
        SettingsManager.ApplyBuoyColor(_originalColor);
        SettingsManager.ApplyBoatSensivity(_originalSens);
        ReturnToMainMenu();
    }

    private void ReturnToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}