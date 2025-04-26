using Unity.Cinemachine;
using UnityEngine;

public class CinemachineShake : MonoBehaviour
{
    public static CinemachineShake Instance { get; private set; }

    private CinemachineCamera cinemachineCamera;
    private float shakeDuration;

    private void Awake()
    {
        Instance = this;
        cinemachineCamera = GetComponent<CinemachineCamera>();
    }

    public void ShakeCamera(float intensity, float duration)
    {
        var perlin = cinemachineCamera.GetCinemachineComponent(CinemachineCore.Stage.Noise) as CinemachineBasicMultiChannelPerlin;

        if (perlin != null)
        {
            perlin.AmplitudeGain = intensity;
            shakeDuration = duration;
        }
    }

    private void Update()
    {
        if (shakeDuration > 0)
        {
            shakeDuration -= Time.deltaTime;
            if (shakeDuration <= 0)
            {
                var perlin = cinemachineCamera.GetCinemachineComponent(CinemachineCore.Stage.Noise) as CinemachineBasicMultiChannelPerlin;

                if (perlin != null)
                {
                    perlin.AmplitudeGain = 0f;
                }
            }
        }
    }
}
