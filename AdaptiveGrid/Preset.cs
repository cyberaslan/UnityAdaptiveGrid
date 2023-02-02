using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;



namespace AdaptiveGrid
{
    [Serializable]
    public abstract class AdaptivePreset : ICloneable
    {
        static AdaptivePreset()
        {
            _presetPototypeDict = new Dictionary<System.Enum, AdaptivePreset>();
            IEnumerable<Type> inheritedAdaptivePresetTypes =
                Assembly.GetAssembly(typeof(AdaptivePreset)).GetTypes().Where(type => type.IsSubclassOf(typeof(AdaptivePreset)));

            foreach (Type t in inheritedAdaptivePresetTypes)
            {
                AdaptivePreset instance = (AdaptivePreset)Activator.CreateInstance(t);
                _presetPototypeDict.Add(instance.SelectorInInspector, instance);
            }
        }

        public static event Action PresetChanged;

        //Instantiates preset instance for concrete AdaptiveGrip component
        private static AdaptivePreset InstantiateAdaptivePreset(System.Enum e)
        {
            return (AdaptivePreset)_presetPototypeDict[e].Clone();
        }

        // Initialize dictionary with each AdaptivePreset prototype instance
        private static Dictionary<System.Enum, AdaptivePreset> _presetPototypeDict { get; set; }

        //Realizes preset algorithm
        public abstract void Apply(List<RectTransform> elements, RectTransform grid, Offset gridMargin, Offset cellPadding);

        //Changes preset value with callback
        public static void ChangeValue(ref AdaptivePreset AdaptivePreset, System.Enum e)
        {
            if (AdaptivePreset != null) if (AdaptivePreset.SelectorInInspector.Equals(e)) return;
            AdaptivePreset = InstantiateAdaptivePreset(e);
            PresetChanged?.Invoke();
        }

        //Clones preset instance 
        public object Clone() { return this.MemberwiseClone(); }

        //Relevant enum in inspector
        public abstract System.Enum SelectorInInspector { get; }
    }
}