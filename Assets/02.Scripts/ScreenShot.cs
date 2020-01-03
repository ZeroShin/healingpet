using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 안드로이드 플랫폼 사용
#if PLATFORM_ANDROID
using UnityEngine.Android;
#endif

public class ScreenShot : MonoBehaviour
{
    public AudioSource _audioSource;

    void Start()
    {
        //규정에 의해서 퍼미션을 받아야 저장이 가능하다.
#if PLATFORM_ANDROID
        // 외부 저장 퍼미션이 설정되어 있지 않다면
        if (!Permission.HasUserAuthorizedPermission(Permission.ExternalStorageWrite))
        {
            // 외부 저장 퍼미션을 설정함
            Permission.RequestUserPermission(Permission.ExternalStorageWrite);
        }
#endif
    }

    // 스크린샷 수행
    public void SaveScreenshot()
    {
        //찰칵소리
        _audioSource.Play();
        // NativeToolkit을 이용해 스크린샷을 저장함
        NativeToolkit.SaveScreenshot("Screenshot_" + System.DateTime.Now.ToString(),
            "/storage/emulated/0/DCIM/Screenshots", "PNG");
        //NativeToolkit.SaveScreenshot("Screenshot_" + System.DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss"),
        //    "/storage/emulated/0/DCIM/Screenshots", "PNG");
    }

}
