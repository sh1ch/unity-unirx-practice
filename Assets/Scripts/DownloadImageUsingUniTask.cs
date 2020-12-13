using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using Cysharp.Threading.Tasks;
using System.Threading;

public class DownloadImageUsingUniTask : MonoBehaviour
{
    [SerializeField]
    private RawImage _RawImage = null;

    public string _Uri = "";

    // Start is called before the first frame update
    void Start()
    {
        var token = this.GetCancellationTokenOnDestroy();

        SetupTextureAsync(token).Forget();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private async UniTaskVoid SetupTextureAsync(CancellationToken token)
    {
        try
        {
            var observable = Observable
                .Defer(() =>
                {
                    return GetTextureAsync(_Uri, token).ToObservable();
                })
                .Retry(3);

            var texture = await observable;

            _RawImage.texture = texture;
        }
        catch (Exception e) when (!(e is OperationCanceledException))
        {
            Debug.LogError(e);
        }
    }

    private async UniTask<Texture> GetTextureAsync(string uri, CancellationToken token)
    {
        using (var webRequest = UnityWebRequestTexture.GetTexture(uri))
        {
            await webRequest.SendWebRequest().WithCancellation(token);

            return ((DownloadHandlerTexture)webRequest.downloadHandler).texture;
        }
    }
}
