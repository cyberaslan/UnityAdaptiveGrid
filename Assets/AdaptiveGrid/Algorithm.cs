using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Reflection;
using System.Linq;
[Serializable]
public abstract class Algorithm : ICloneable 
{
    public abstract System.Enum SelectorInInspector { get; }
    public abstract void Apply(List<RectTransform> elements, RectTransform grid);
    public object Clone() { Debug.Log("Clone"); return this.MemberwiseClone(); }
}

public static class AlgorithmMap
{
    // Initializes dictionary with all types inherited from Algorithm by relevant enum value keys in AdaptiveGrid settings
    private static Dictionary<System.Enum, Algorithm> _map { get; set; }

    static AlgorithmMap() {
        _map = new Dictionary<System.Enum, Algorithm>();
        IEnumerable<Type> inheritedAlgorithmTypes =
            Assembly.GetAssembly(typeof(Algorithm)).GetTypes().Where(type => type.IsSubclassOf(typeof(Algorithm)));

        foreach (Type t in inheritedAlgorithmTypes) {
            Algorithm instance = (Algorithm)Activator.CreateInstance(t);
            _map.Add(instance.SelectorInInspector, instance);
        }
    }
    public static void SetAlgorithm(ref Algorithm Algorithm, System.Enum e) {
        if (Algorithm == null) {
            Algorithm = GetAlgorithm(e);
            return;
        }
        if (Algorithm.SelectorInInspector.Equals(e)) return;
        Algorithm = GetAlgorithm(e);
    }
    public static Algorithm GetAlgorithm(System.Enum e) {
        return (Algorithm)_map[e].Clone();
    }
}