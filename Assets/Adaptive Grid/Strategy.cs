using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Reflection;
using System.Linq;
public abstract class Strategy<K, V> : IMapKeyable where K : System.Enum
{
    protected static Dictionary<K, V> _map { get; set; }
    public static void SetStrategy (ref V strategy, K key) {
        strategy = _map[key];
    }
    public static V GetStrategy(K key) {
        return _map[key];
    }
    protected static void InitializeMap() {
        _map = new Dictionary<K, V>();
        IEnumerable<Type> inheritedStrategyTypes =
            Assembly.GetAssembly(typeof(V)).GetTypes().Where(type => type.IsSubclassOf(typeof(V)));

        foreach (Type t in inheritedStrategyTypes) {
            V instance = (V)Activator.CreateInstance(t);
            _map.Add((K)(instance as IMapKeyable).GetKey(), instance);
        }
    }
    public abstract System.Enum GetKey();
}

interface IMapKeyable
{
    public System.Enum GetKey();

}