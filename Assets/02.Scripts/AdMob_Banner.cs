//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using GoogleMobileAds.Api;

//public class AdMob_Banner : MonoBehaviour
//{
//    //private readonly string unitID = "ca-app-pub-8964021498469985/6835918587";
//    private readonly string test_unitID = "	ca-app-pub-3940256099942544/6300978111";

//    private BannerView banner;
//    public AdPosition position;
//    private void Start()
//    {
//        InitAd();
//    }
//    private void InitAd()
//    {
//        //id는 디버그에 따라서 테스트나 실제를 사용할 지를 선택하는 것.
//        //string id = Debug.isDebugBuild ? test_unitID : unitID;
//        string id = test_unitID;

//        banner = new BannerView(id, AdSize.SmartBanner, position);
//        //빌더와 빌드 사이에 많은 옵션을 할 수 있음.
//        AdRequest request = new AdRequest.Builder().Build();

//        banner.LoadAd(request);

//    }
//    //토글 할 때에 나오도록 하는 것.
//    public void ToggleAd(bool active)
//    {
//        if (active)
//        {
//            banner.Show();
//        }
//        else
//        {
//            banner.Hide();
//        }
//    }
//    public void DestroyAd()
//    {
//        banner.Destroy();
//    }
//}
