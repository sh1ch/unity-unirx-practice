using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

public class PlayerTextureChanger : MonoBehaviour
{
    [SerializeField]
    private GameResourceProvider _GameResourceProvider = null;

    [SerializeField]
    private RawImage _RawImage = null;

    // Start is called before the first frame update
    void Start()
    {
        _GameResourceProvider._PlayerTextureAsync
            .Subscribe(SetMyTexture)
            .AddTo(this);
    }

    // Update is called once per frame
    private void SetMyTexture(Texture newTexture)
    {
        if (_RawImage != null)
        {
            _RawImage.texture = newTexture;
        }
    }
}
