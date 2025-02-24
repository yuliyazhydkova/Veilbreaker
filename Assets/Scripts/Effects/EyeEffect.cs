using UnityEngine;

public class EyeEffect : MonoBehaviour
{
    [SerializeField] private Light eyeLight;
    [SerializeField] private ParticleSystem eyeParticles;
    [SerializeField] private TrailRenderer eyeTrail;

    private void Start()
    {
        if (eyeLight != null)
        {
            eyeLight.enabled = true;
        }
        if (eyeParticles != null)
        {
            var emission = eyeParticles.emission;
            emission.enabled = true;
            eyeParticles.Play();
        }
        if (eyeTrail != null)
        {
            eyeTrail.emitting = true;
        }
    }
}