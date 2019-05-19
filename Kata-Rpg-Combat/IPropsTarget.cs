namespace Kata_Rpg_Combat
{
    public interface IPropsTarget
    {
        uint Health { get; set; }
        PropsTargetState State { get; set; }
    }
}