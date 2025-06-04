using UnityEngine;

public class SettingsManager : MonoBehaviour
{
    public static SettingsManager Instance { get; private set; }

    [Header("Impact Settings")]
    [SerializeField] private ImpactSettingsSO impactSettings;
    public ImpactSettingsSO ImpactSettings => impactSettings;

    // Here you can add other settings for different systems
    // [Header("Other Settings")]
    // [SerializeField] private OtherSettingsSO otherSettings;
    // public OtherSettingsSO OtherSettings => otherSettings;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(this.gameObject);

        // Automatically load default ImpactSettingsSO from Resources if not assigned
        if (impactSettings == null)
        {
            impactSettings = Resources.Load<ImpactSettingsSO>("ImpactSettings");
            if (impactSettings == null)
            {
                Debug.LogWarning("Default ImpactSettingsSO not found in Resources folder.");
            }
        }
    }
}