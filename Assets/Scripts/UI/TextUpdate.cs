using UnityEngine;
using UnityEngine.UI;

namespace TowerDefense
{
    public class TextUpdate : MonoBehaviour
    {
        public enum UpdateSource
        {
            Gold,
            Lives
        }

        public UpdateSource source = UpdateSource.Gold;

        private Text m_Text;

        private void Start()
        {
            m_Text = GetComponent<Text>();

            switch (source)
            {
                case UpdateSource.Gold: 
                    TDPlayer.GoldUpdateSubscribe(UpdateText);
                    break;

                case UpdateSource.Lives:
                    TDPlayer.LivesUpdateSubscribe(UpdateText);
                    break;
            }
        }

        private void OnDestroy()
        {
            TDPlayer.RemoveGoldUpdateSubscribe(UpdateText);
            TDPlayer.RemoveLivesUpdateSubscribe(UpdateText);
        }

        private void UpdateText(int num)
        {
            m_Text.text = num.ToString(); 
        }
    }
}