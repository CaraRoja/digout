using UnityEditor.Rendering.Universal;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class EnemyDistort : Enemy
{
    public Volume volume;
    private VolumeProfile profile;
    private ChromaticAberration chromaticAberration;
    private LensDistortion lensDistortion;
    private EnemyGauge gauge;
    public float frequency = 1.0f;
    private float timer;
    private float originalPitch;

    protected override void Awake()
    {
        volume = GameObject.Find("PostProcessingVolume").GetComponent<Volume>();
        //profile = volume.sharedProfile;
        base.Awake();

        speed = 1.3f; // Ajustado
        coinLoss = 5; // Ajustado

        if (volume == null)
        {
            Debug.LogError("EnemyDistort: PostProcessVolume is not assigned!");
            return;
        }
        /*
        if (!profile.TryGet<ChromaticAberration> (out var chromaticAberration) )
        {
            Debug.LogError("EnemyDistort: ChromaticAberration is not found in the PostProcessVolume profile!");
        }

        if (!volume.profile.TryGetSettings(out lensDistortion))
        {
            Debug.LogError("EnemyDistort: LensDistortion is not found in the PostProcessVolume profile!");
        }
        */
        /*

        foreach (VolumeComponent volume in profile.components)
        {
            if (volume.name.Equals("Chromatic Aberration"))
            {
                chromaticAberration = volume as UnityEngine.Rendering.Universal.ChromaticAberration;
            }
            if (volume.name.Equals("Lens Distortion"))
            {
                chromaticAberration = volume as UnityEngine.Rendering.Universal.LensDistortion;
            }
        }
        */

       

        
    }

    private void Start()
    {


        if (!volume.profile.TryGet(out  chromaticAberration))
        {
            Debug.LogError("EnemyDistort: ChromaticAberration is not found in the PostProcessVolume profile!");
        }

        if (!volume.profile.TryGet(out lensDistortion))
        {
            Debug.LogError("EnemyDistort: LensDistortion is not found in the PostProcessVolume profile!");
        }

        //if (chromaticAberration != null) chromaticAberration.active = false;
        //if (lensDistortion != null) lensDistortion.active = false;

        gauge = GetComponent<EnemyGauge>();

        if (MusicManager.Instance != null)
        {
            Debug.Log("EnemyDistort: SoundManager instance is set");

            if (MusicManager.Instance.music != null)
            {
                originalPitch = MusicManager.Instance.music.pitch;  // Salva o pitch original
            }
            else
            {
                Debug.LogError("MusicManager: musicSource is not assigned!");
            }
        }
        else
        {
            Debug.LogError("MusicManager instance is not set.");
        }
    }

    void Update()
    {
        if (chromaticAberration != null && chromaticAberration.active)
        {
            /*
            float chromaticValue = (Mathf.Sin(timer * frequency) + 1) / 2 * 1.0f;
            float lensDistortionValue = (Mathf.Sin(timer * frequency) + 1) / 2 * 100.0f - 50;

            chromaticAberration.intensity.value = chromaticValue * 100;
            lensDistortion.intensity.value = lensDistortionValue;

            timer += Time.deltaTime;
            */
        }

        DeactivateEnemy();
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
            Debug.Log("ENTROU");
            //chromaticAberration.active = isActive;
            chromaticAberration.intensity.value = isActive ? 1f : 0;
        }

        if (lensDistortion != null)
        {
            //lensDistortion.active = isActive;
            lensDistortion.intensity.value = isActive ? 0.75f : 0;
        }

        if (MusicManager.Instance != null)
        {
            if (isActive)
            {
                MusicManager.Instance.SetMusicPitch(0.7071f);  // Aproximadamente 5 semitons abaixo
            }
            else
            {
                MusicManager.Instance.SetMusicPitch(originalPitch);
            }
        }
    }

    private void DeactivateEnemy()
    {
        if (!gauge.GaugeIsWorking() && !GetEnemySolved())
        {
            Deactivate();
            SetEnemySolved();
            ChangeTagToSolvedProblem();
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

        if (MusicManager.Instance != null)
        {
            MusicManager.Instance.SetMusicPitch(originalPitch);  // Restaura o pitch original
        }

        base.Deactivate();
    }

    public override void SetEnemySolved()
    {
        solved = true;
    }

    public override bool GetEnemySolved()
    {
        return solved;
    }

    public override void ChangeTagToSolvedProblem()
    {
        this.gameObject.tag = "EnemySolved";
    }
}
