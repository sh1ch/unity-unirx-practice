using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class DownloadImage : MonoBehaviour
{
    [SerializeField]
    private RawImage _RawImage = null;

    public string _Uri = "";

    // Start is called before the first frame update
    void Start()
    {
        if (string.IsNullOrEmpty(_Uri)) return;

        GetTextureAsync(_Uri)
            .OnErrorRetry(
                onError: (Exception _) => { },
                retryCount: 3
            ).Subscribe(
                result => _RawImage.texture = result,
                error => Debug.LogError(error)
            ).AddTo(this);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IObservable<Texture> GetTextureAsync(string uri)
    {
        var task = Observable.FromCoroutine<Texture>(observer =>
        {
            return GetTextureCoroutine(observer, uri);
        });

        return task;
    }

    private IEnumerator GetTextureCoroutine(IObserver<Texture> observer, string uri)
    {

        using (var webRequest = UnityWebRequestTexture.GetTexture(uri))
        {
            yield return webRequest.SendWebRequest();
            
            if (webRequest.result == UnityWebRequest.Result.ConnectionError || webRequest.result == UnityWebRequest.Result.ProtocolError)
            {
                observer.OnError(new Exception(webRequest.error));

                yield break;
            }

            var result = ((DownloadHandlerTexture)webRequest.downloadHandler).texture;

            // 成功時は、メッセージを発行
            observer.OnNext(result);
            observer.OnCompleted();
        }
    }

}
