using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class EnemyDistort : Enemy
{
    public PostProcessVolume volume;
    private ChromaticAberration chromaticAberration;
    private LensDistortion lensDistortion;
    public float frequency = 1.0f;
    private float timer;
    private float originalPitch;

    protected override void Awake()
    {
        base.Awake();

        speed = 1.3f; // Ajustado
        coinLoss = 5; // Ajustado

        if (volume == null)
        {
            Debug.LogError("EnemyDistort: PostProcessVolume is not assigned!");
            return;
        }

        if (!volume.profile.TryGetSettings(out chromaticAberration))
        {
            Debug.LogError("EnemyDistort: ChromaticAberration is not found in the PostProcessVolume profile!");
        }

        if (!volume.profile.TryGetSettings(out lensDistortion))
        {
            Debug.LogError("EnemyDistort: LensDistortion is not found in the PostProcessVolume profile!");
        }

        if (chromaticAberration != null) chromaticAberration.active = false;
        if (lensDistortion != null) lensDistortion.active = false;

        if (SoundManager.Instance != null)
        {
            Debug.Log("EnemyDistort: SoundManager instance is set");

            if (SoundManager.Instance.musicSource != null)
            {
                originalPitch = SoundManager.Instance.musicSource.pitch;  // Salva o pitch original
            }
            else
            {
                Debug.LogError("SoundManager: musicSource is not assigned!");
            }
        }
        else
        {
            Debug.LogError("SoundManager instance is not set.");
        }
    }

    void Update()
    {
        if (chromaticAberration != null && chromaticAberration.active)
        {
            float chromaticValue = (Mathf.Sin(timer * frequency) + 1) / 2 * 1.0f;
            float lensDistortionValue = (Mathf.Sin(timer * frequency) + 1) / 2 * 100.0f - 50;

            chromaticAberration.intensity.value = chromaticValue * 100;
            lensDistortion.intensity.value = lensDistortionValue;

            timer += Time.deltaTime;
        }
    }

    public override void ApplyEffect(bool entering)
    {
        if (!solved)
        {
            ActivateEffects(entering);
        }
    }

    private void ActivateEffects(bool isActive)
    {
        if (chromaticAberration != null)
        {
            chromaticAberration.active = isActive;
            chromaticAberration.intensity.value = isActive ? 1f : 0;
        }

        if (lensDistortion != null)
        {
            lensDistortion.active = isActive;
            lensDistortion.intensity.value = isActive ? 60f : 0;
        }

        if (SoundManager.Instance != null)
        {
            if (isActive)
            {
                SoundManager.Instance.SetMusicPitch(0.7071f);  // Aproximadamente 5 semitons abaixo
            }
            else
            {
                SoundManager.Instance.SetMusicPitch(originalPitch);
            }
        }
    }

    protected override void Deactivate()
    {
        Renderer renderer = GetComponent<Renderer>();
        if (renderer != null)
        {
            renderer.material.SetColor("_CurrentColor", new Color(0.0f, 0.898f, 0.525f, 1.0f));
        }

        if (chromaticAberration != null) chromaticAberration.active = false;
        if (lensDistortion != null) lensDistortion.active = false;

        if (SoundManager.Instance != null)
        {
            SoundManager.Instance.SetMusicPitch(originalPitch);  // Restaura o pitch original
        }

        base.Deactivate();
    }
}
