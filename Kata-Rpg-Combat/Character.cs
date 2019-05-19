using System.Drawing;

namespace Kata_Rpg_Combat
{
    public abstract class Character
    {
        //! Constant
        private const uint MaxHealth = 1000;
        private const int MaxLevelDifference = 5;


        //! Public Member functions
        public void DealDamage(Character other, uint damages)
        {
            if (MathUtilility.GetDistance(WorldPosition, other.WorldPosition) > _maxRange ||
                ReferenceEquals(this, other)) return;
            bool IsLevelBehindByMaxLevelDifference() => (int) other.Level - (int) Level >= MaxLevelDifference;
            bool IsLevelAboveByMaxLevelDifference() => (int) Level - (int) other.Level >= MaxLevelDifference;
            bool IsDamagesExceedOtherHealth() => damages >= other.Health;
            if (IsLevelAboveByMaxLevelDifference()) damages += damages / 2;
            else if (IsLevelBehindByMaxLevelDifference()) damages -= damages / 2;
            if (IsDamagesExceedOtherHealth())
            {
                other.Health = 0;
                other.State = CharacterState.Dead;
            }
            else
                other.Health -= damages;
        }

        public void Heal(uint heal)
        {
            if (State == CharacterState.Dead) return;
            bool HealExceedMaxHealth() => Health + heal > MaxHealth;
            if (HealExceedMaxHealth()) Health = MaxHealth;
            else Health += heal;
        }

        //! Constructor
        protected Character(uint maxRange)
        {
            _maxRange = maxRange;
        }


        //! Relational Operations
        private bool Equals(Character other) =>
            Health == other.Health && Level == other.Level && State == other.State &&
            _maxRange == other._maxRange && WorldPosition == other.WorldPosition;

        public override bool Equals(object obj) =>
            !ReferenceEquals(null, obj) &&
            (ReferenceEquals(this, obj) || obj.GetType() == GetType() && Equals((Character) obj));


        //! Private members
        private readonly uint _maxRange;

        //! Properties
        public Point WorldPosition { get; set; } = Point.Empty;
        public uint Health { get; private set; } = MaxHealth;
        public uint Level { get; set; } = 1;
        public CharacterState State { get; private set; } = CharacterState.Alive;
    }
}