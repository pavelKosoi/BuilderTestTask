using System.Threading.Tasks;
using TMPro;
using UnityEngine;

namespace BuilderGame.Infrastructure.Services.Ads.Fake
{
    public class FakeAdsWindow : MonoBehaviour
    {
        [SerializeField]
        private TMP_Text placement;

        [SerializeField]
        private Transform enableWhenRewarded;

        public string Placement { get; set; }
        public bool IsRewarded { get; set; }

        public TaskCompletionSource<AdWatchResult> Tcs { get; set; }
        
        private void Start()
        {
            placement.text = Placement;
            enableWhenRewarded.gameObject.SetActive(IsRewarded);
        }

        public void Watch()
        {
            Tcs.SetResult(AdWatchResult.Watched);
            Close();
        }

        public void Cancel()
        {
            Tcs.SetResult(AdWatchResult.Closed);
            Close();
        }

        private void Close()
        {
            Destroy(gameObject);
        }
    }
}