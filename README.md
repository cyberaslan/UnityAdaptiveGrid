# AdaptiveGrid
Adaptive UIBehaviour with auto-layout and flexible settings.

1. No need to count or control cell size manually (in units) and ajust to parent size changes.
2. Elements dont freeze like in LayoutGroup (it transforms not driven by object). Means it could be animated or used any other way.
3. Rebuilds only in regular cases (OnTransformChildrenChanged, OnTransformChildrenChanged, OnCanvasHierarchyChanged, OnValidate).
4. Easy to extend: just create class inherited from Preset and its enum-value for inspector (ArrangeLayout or ScaleMethod).

 OnTransorm

### Component automatically arrange elements in container (fits content if it is Image)
![](http://korneev.spb.ru/adaptivegrid/promo2.gif)
### Reacting to children order and composition changes
![](http://korneev.spb.ru/adaptivegrid/promo1.gif)
### Flex padding settings
![](http://korneev.spb.ru/adaptivegrid/promo3.gif)
### Fixed NxM grid and MaxRects bin pack by Image content
![](http://korneev.spb.ru/adaptivegrid/promo4.gif)
