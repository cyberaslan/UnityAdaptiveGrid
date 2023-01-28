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
    private static Dictionary<System.Enum, Strategy> _map { get; set;  }
    
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
        if (strategy == null) {
            strategy = GetStrategy(e);
            return;     
        }
        if (strategy.SelectorInInspector.Equals(e)) return;
        strategy = GetStrategy(e);
    }
    public static Strategy GetStrategy(System.Enum e) {
        return (Strategy) _map[e].Clone();
    }
}

public abstract class Strategy : ICloneable 
{
    public abstract System.Enum SelectorInInspector { get; }
    public abstract void Apply(List<RectTransform> elements, RectTransform grid);
    public object Clone() { Debug.Log("Clone"); return this.MemberwiseClone(); }
}