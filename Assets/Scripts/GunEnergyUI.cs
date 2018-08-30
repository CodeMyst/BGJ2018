using UnityEngine;
using UnityEngine.UI;

namespace BGJ2018
{
    public class GunEnergyUI : MonoBehaviour
    {
        [SerializeField]
        private Slider slider;
        [SerializeField]
        private LightGun gun;

        private void Update ()
        {
            slider.value = gun.Energy / gun.MaxEnergy;
        }
    }
}