using System.Numerics;
using Components;
using UnityEngine.Events;
using Vector3 = UnityEngine.Vector3;

namespace Events
{
    public class InputEvents
    {
        public static UnityAction<Tile, Vector3> MouseDownGrid;
        public static UnityAction<UnityEngine.Vector3> MouseUpGrid;
    }
}