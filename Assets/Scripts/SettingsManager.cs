using System;
using System.Net.Http.Headers;
using Unity.VisualScripting;
using UnityEditor.VersionControl;
using UnityEngine;

public static class SettingsManager
{
    private const string BuoyColorKey = "BuoyColor";

    // Цвет по умолчанию
    public static Color DefaultBuoyColor = new Color(1f, 0.5f, 0f, 1f); // Оранжевый

    // Сохраняем цвет буя
    public static void SaveBuoyColor(Color color)
    {
        PlayerPrefs.SetFloat(BuoyColorKey + "_R", color.r);
        PlayerPrefs.SetFloat(BuoyColorKey + "_G", color.g);
        PlayerPrefs.SetFloat(BuoyColorKey + "_B", color.b);
        PlayerPrefs.SetFloat(BuoyColorKey + "_A", color.a);
        PlayerPrefs.Save();
    }

    public static void SaveBoatSensitivity(float sens)
    {
        PlayerPrefs.SetFloat("sens", sens);
        PlayerPrefs.Save();
    }

    // Загружаем цвет буя
    public static Color LoadBuoyColor()
    {
        if (PlayerPrefs.HasKey(BuoyColorKey + "_R"))
        {
            float r = PlayerPrefs.GetFloat(BuoyColorKey + "_R");
            float g = PlayerPrefs.GetFloat(BuoyColorKey + "_G");
            float b = PlayerPrefs.GetFloat(BuoyColorKey + "_B");
            float a = PlayerPrefs.GetFloat(BuoyColorKey + "_A");
            return new Color(r, g, b, a);
        }
        return DefaultBuoyColor;
    }

    public static float LoadBoatSensivity()
    {
        if (PlayerPrefs.HasKey("sens"))
        {
            return PlayerPrefs.GetFloat("sens");
        }
        return 70f;
    }

    // Применяем цвет ко всем буям в сцене
    public static void ApplyBuoyColor(Color color)
    {
        Buoy[] buoys = GameObject.FindObjectsOfType<Buoy>();
        foreach (Buoy buoy in buoys)
        {
            buoy.ApplyColor(color);
        }
    }

    public static void ApplyBoatSensivity(float sens)
    {
        BoatController[] controllers
            = GameObject.FindObjectsOfType<BoatController>();
        // Debug.Log(sens);
        foreach (BoatController contr in controllers)
        {
            Debug.Log(sens);
            contr.ApplySensivity(sens);
        }
    }
}