using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Reflection;
using System.Linq;
[Serializable]
public class StrategyMap
{
    // Initializes dictionary with all types inherited from Strategy by relevant enum value keys in AdaptiveGrid settings
    private static Dictionary<System.Enum, Strategy> _map { get; set; }
    
    static StrategyMap() {
        _map = new Dictionary<System.Enum, Strategy>();
        IEnumerable<Type> inheritedStrategyTypes =
            Assembly.GetAssembly(typeof(Strategy)).GetTypes().Where(type => type.IsSubclassOf(typeof(Strategy)));

        foreach (Type t in inheritedStrategyTypes) {
            Strategy instance = (Strategy)Activator.CreateInstance(t);
            _map.Add(instance.SelectorInInspector, instance);
        }
    }
    public static void SetStrategy(ref Strategy strategy, System.Enum e) {
        if (strategy != null) if (strategy?.SelectorInInspector == e) return;
        Debug.Log($"New strategy: {e}");
        strategy = GetStrategy(e);
    }
    public static Strategy GetStrategy(System.Enum e) {
        return _map.FirstOrDefault(p => p.Value.SelectorInInspector.Equals(e)).Value;
    }
}

public abstract class Strategy
{
    public abstract System.Enum SelectorInInspector { get; }
    public abstract void Apply(List<RectTransform> elements, RectTransform grid);
}