# com.karabaev.utilities.unity

General-purpose Unity utilities: component and child lookup, transform/GameObject helpers, layer and
gizmo helpers, deterministic random, math and 2D segment types, and animation event plumbing.

## Installation

Add the package from a Git URL:

```
https://github.com/Karabaev/utilities-unity.git?path=Assets
```

It depends on `com.karabaev.utilities`, which Unity's Package Manager will not pull in automatically —
add that one too:

```
https://github.com/Karabaev/utilities.git?path=Assets
```

## 4.0.0 — breaking changes

Two concerns were split out into their own packages:

| Was | Now |
| --- | --- |
| `com.karabaev.utilities.unity.GameKit` (`GameKitComponent`, `Required*` attributes, the source generator) | [UniRef](https://github.com/Karabaev/uniref) — `com.karabaev.uniref`, base type renamed `UniRefComponent` |
| `com.karabaev.test-utilities.unity` (`TestGameObjectBuilder`, `GameObjectTestUtils`, …) | [UniTest](https://github.com/Karabaev/unitest) — `com.karabaev.unitest` |

The component lookup helpers stayed here but moved up out of the `GameKit` sub-namespace:

```diff
- using com.karabaev.utilities.unity.GameKit.Utils;
+ using com.karabaev.utilities.unity.Utils;
```

`RequireComponent`, `RequireChild`, `GetChild`, `RequireComponentFromChild`,
`RequireComponentFromChildren`, `RequireComponentFromParents` and their `Get*` counterparts are
otherwise unchanged.
