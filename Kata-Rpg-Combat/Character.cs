using System.Drawing;

namespace Kata_Rpg_Combat
{
    public abstract class Character
    {
        private const uint MaxHealth = 1000;
        private const int MaxLevelDifference = 5;

        public void DealDamage(Character other, uint damages)
        {
            if (MathUtilility.GetDistance(WorldPosition, other.WorldPosition) > _maxRange ||
                ReferenceEquals(this, other)) return;
            var isLevelAboveByMaxLevelDifference = (int) Level - (int) other.Level >= MaxLevelDifference;
            var isLevelBehindByMaxLevelDifference = (int) other.Level - (int) Level >= MaxLevelDifference;
            if (isLevelAboveByMaxLevelDifference) damages += damages / 2;
            else if (isLevelBehindByMaxLevelDifference) damages -= damages / 2;
            var isDamagesExceedOtherHealth = damages >= other.Health;

            if (isDamagesExceedOtherHealth)
            {
                other.Health = 0;
                other.State = CharacterState.Dead;
            }
            else
                other.Health -= damages;
        }

        public void Heal(uint heal)
        {
            if (State == CharacterState.Dead)
                return;
            if (Health + heal > MaxHealth)
            {
                Health = MaxHealth;
            }
            else
            {
                Health += heal;
            }
        }

        private readonly uint _maxRange;
        public Point WorldPosition { get; set; } = Point.Empty;

        protected Character(uint maxRange)
        {
            _maxRange = maxRange;
        }

        public uint Health { get; set; } = MaxHealth;
        public uint Level { get; set; } = 1;

        private bool Equals(Character other)
        {
            return Health == other.Health && Level == other.Level && State == other.State;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((Character) obj);
        }

        public CharacterState State { get; set; } = CharacterState.Alive;
    }
}