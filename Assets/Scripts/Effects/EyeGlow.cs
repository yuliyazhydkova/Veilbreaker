using UnityEngine;

public class EyeGlow : MonoBehaviour
{
    private Light eyeLight;
    private float timer;

    void Start()
    {
        eyeLight = GetComponent<Light>();
    }

    void Update()
    {
        timer += Time.deltaTime * 5f;
        eyeLight.intensity = Mathf.PingPong(timer, 2f) + 3f;
    }
}
