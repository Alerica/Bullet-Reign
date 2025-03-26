using UnityEngine;
using Unity.Cinemachine;

public class CameraShake : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private CinemachineCamera cinemachineCamera;  
    private CinemachineBasicMultiChannelPerlin noise;
    private float shakeTimer;

    void Start()
    {
        noise = cinemachineCamera.GetComponent<CinemachineBasicMultiChannelPerlin>();
    }

    public void ShakeCamera(float intensity, float duration)
    {
        noise.AmplitudeGain = intensity;
        shakeTimer = duration;
    }

    void Update()
    {
        if (shakeTimer > 0)
        {
            shakeTimer -= Time.deltaTime;
            if (shakeTimer <= 0)
            {
                noise.AmplitudeGain = 0f; 
            }
        }
    }
}
