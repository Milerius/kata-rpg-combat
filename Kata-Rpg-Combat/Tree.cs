using System.Drawing;

namespace Kata_Rpg_Combat
{
    public class Tree : IPropsTarget
    {
        public uint Health { get; set; } = 2000;
        public PropsTargetState State { get; set; } = PropsTargetState.Alive;
        public Point WorldPosition { get; set; } = Point.Empty;
    }
}