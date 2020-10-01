using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

namespace Core.UI.Fight
{
    public struct ATBIconDescriptor
    {
        public Transform playableUnit;
        public Core.Common.UnitStats unitStats;
        public Texture icon;

        public override string ToString()
        {
            return $"{{{playableUnit},{unitStats},{icon}}}";
        }

        public override int GetHashCode()
        {
            return ToString().GetHashCode();
        }

        public override bool Equals(object obj)
        {
            if (obj is ATBIconDescriptor d)
                return playableUnit == d.playableUnit;
            else
                return false;
        }
    }

    public class ATBScale : MonoBehaviour
    {
        private List<ATBIconDescriptor> activeIconDescriptors = new List<ATBIconDescriptor>();
        private List<ATBIconDescriptor> currentIconDescriptors = new List<ATBIconDescriptor>();

        // Иконка для дебага.
        public Texture defaultDescriptorIcon;

        public List<GameObject> icons = new List<GameObject>();

        public void UpdateIcons(Core.Fight.ATBScale scale)
        {
            currentIconDescriptors.Clear();
            foreach (GameObject icon in icons)
                Destroy(icon);

            List<Core.Common.UnitStats> queueStats = new List<Common.UnitStats>(scale.unitsQueue);
            for (int i = 0; i < Core.Fight.ATBScale.size; i++)
            {
                // find corresponding descriptor
                ATBIconDescriptor descriptor = activeIconDescriptors.Where(d => d.unitStats.Equals(queueStats[i])).First();
                currentIconDescriptors.Add(descriptor);

                // add icon to screen
                GameObject image = new GameObject();

                RawImage rawImage = image.AddComponent<RawImage>();
                rawImage.texture = descriptor.icon;

                RectTransform rect = image.GetComponent<RectTransform>();
                rect.SetParent(transform);
                rect.pivot = new Vector2(0.0f, 1.0f);
                rect.anchorMin = new Vector2(0.0f, 1.0f);
                rect.anchorMax = new Vector2(0.0f, 1.0f);
                rect.anchoredPosition = new Vector2(128.0f * i, 0.0f);
                rect.sizeDelta = new Vector2(128.0f, 128.0f);

                image.SetActive(true);
                icons.Add(image);
            }
        }

        public void InitIconDescriptors(List<Transform> playableUnits)
        {
            foreach (Transform playableUnit in playableUnits)
            {
                ATBIconDescriptor descriptor = new ATBIconDescriptor();

                descriptor.playableUnit = playableUnit;
                descriptor.unitStats = playableUnit.GetComponent<Core.Fight.FightUnitController>().stats;
                descriptor.icon = defaultDescriptorIcon; // TODO: init icon too

                activeIconDescriptors.Add(descriptor);
                Debug.Log("UI ATBScale: added icon descriptor");
            }
        }
    }
}
