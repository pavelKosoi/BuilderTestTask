using BuilderGame.Infrastructure.Services.Ads;
using BuilderGame.Infrastructure.Services.Ads.Fake;
using System;
using System.Threading.Tasks;
using UnityEngine;

public class AdsManager : MonoBehaviour
{
    [SerializeField] FakeAdsSettings fakeAdsSettings;
    static FakeAdvertiser fakeAdvertiser;
    private void Awake()
    {
        fakeAdvertiser = new FakeAdvertiser(fakeAdsSettings);
    }

    public static async Task<AdWatchResult> ShowRewardedAsync()
    {
        AdWatchResult result = await fakeAdvertiser.ShowRewardedAd("123");
        return result;
    }
}
