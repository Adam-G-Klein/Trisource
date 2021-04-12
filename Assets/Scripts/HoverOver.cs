using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoverOver : MonoBehaviour
{
    public Material baseMaterial;
    public Material hoverMaterial;

    private Renderer _renderer;
    private bool _setMaterial = false;
    // Start is called before the first frame update

    private void Start()
    {
        _renderer = GetComponentInChildren<Renderer>();
    }

    private void LateUpdate()
    {
        if (!_setMaterial)
        {
            _renderer.material = baseMaterial;
        }
        _setMaterial = false;
    }

    public void setHover()
    {
        _renderer.material = hoverMaterial;
        _setMaterial = true;
    }
}
