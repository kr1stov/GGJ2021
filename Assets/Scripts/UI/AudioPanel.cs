using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class AudioPanel : MonoBehaviour
    {
        public GameSettings gameSettings;
        public Slider mainVol;

        private void Start()
        {
            mainVol.value = gameSettings.GetMainVol();
        }
    }
}