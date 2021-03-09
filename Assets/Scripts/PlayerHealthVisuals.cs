using System.Collections;
using System.Collections.Generic;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine;

public class PlayerHealthVisuals : MonoBehaviour
{
    private Vignette _vignette;
    private PostProcessVolume volume;
    private float _minIntensity = 0.4f;
    private float _maxIntensity = 0.65f;
    /*private float _minSmoothness = 0.1f;
    private float _maxSmoothness = 0.55f;*/

    // Start is called before the first frame update
    void Start()
    {
        volume = GetComponent<PostProcessVolume>();
        volume.profile.TryGetSettings(out _vignette);
    }

    public void setHealth(float percent)
    {
        if (percent < 1f)
        {
            _vignette.intensity.value = (1f - percent) * (_maxIntensity - _minIntensity) + _minIntensity;
           // _vignette.smoothness.value = (1f - percent) * (_maxSmoothness - _minSmoothness) + _minSmoothness;
        }
        else
        {
            _vignette.intensity.value = 0;
           // _vignette.smoothness.value = 0;
        }
    }
}
