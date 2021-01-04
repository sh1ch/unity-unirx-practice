using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

public class GameResourceProvider : MonoBehaviour
{
    private readonly AsyncSubject<Texture> _PlayerTextureAsyncSubject = new AsyncSubject<Texture>();

    public IObservable<Texture> _PlayerTextureAsync => _PlayerTextureAsyncSubject;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(LoadTexture());
    }

    // Update is called once per frame
    private IEnumerator LoadTexture()
    {
        var resource = Resources.LoadAsync<Texture>("Textures/sample");

        yield return resource;

        _PlayerTextureAsyncSubject.OnNext(resource.asset as Texture);
        _PlayerTextureAsyncSubject.OnCompleted();
    }
}
