using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace Kata_Rpg_Combat
{
    public abstract class Character
    {
        //! Constant
        private const uint MaxHealth = 1000;
        private const int MaxLevelDifference = 5;

        //! Private Member functions
        private static bool IsDamagesExceedOtherHealth(uint damagesAmount, uint health) => damagesAmount >= health;

        //! Public Member functions
        public void DealDamage(Character other, uint damagesAmount)
        {
            if (MathUtilility.GetDistance(WorldPosition, other.WorldPosition) > _maxRange ||
                ReferenceEquals(this, other) || AreWeAlly(other)) return;
            bool IsLevelBehindByMaxLevelDifference() => (int) other.Level - (int) Level >= MaxLevelDifference;
            bool IsLevelAboveByMaxLevelDifference() => (int) Level - (int) other.Level >= MaxLevelDifference;
            if (IsLevelAboveByMaxLevelDifference()) damagesAmount += damagesAmount / 2;
            else if (IsLevelBehindByMaxLevelDifference()) damagesAmount -= damagesAmount / 2;
            if (IsDamagesExceedOtherHealth(damagesAmount, other.Health))
            {
                other.Health = 0;
                other.State = CharacterState.Dead;
            }
            else
                other.Health -= damagesAmount;
        }

        public void DealDamage(IPropsTarget target, uint damagesAmount)
        {
            if (MathUtilility.GetDistance(WorldPosition, target.WorldPosition) > _maxRange)
                return;
            if (IsDamagesExceedOtherHealth(damagesAmount, target.Health))
            {
                target.Health = 0;
                target.State = PropsTargetState.Destroyed;
            }
            else
                target.Health -= damagesAmount;
        }

        public void Heal(uint heal)
        {
            Heal(this, heal);
        }

        public void Heal(Character other, uint heal)
        {
            bool HealthConditionsAreNotFullFilled() =>
                State == CharacterState.Dead || !ReferenceEquals(this, other) && !AreWeAlly(other);

            if (HealthConditionsAreNotFullFilled()) return;
            bool HealExceedMaxHealth() => other.Health + heal > MaxHealth;
            if (HealExceedMaxHealth()) other.Health = MaxHealth;
            else other.Health += heal;
        }

        public int JoinFaction(params Faction[] factions)
        {
            Factions.UnionWith(factions);
            return Factions.Count;
        }

        public int LeaveFaction(params Faction[] factions)
        {
            foreach (var factionToRemove in factions)
            {
                Factions.Remove(factionToRemove);
            }

            return Factions.Count;
        }

        public bool AreWeAlly(Character other)
        {
            return Factions.Any(other.Factions.Contains);
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

        public HashSet<Faction> Factions { get; } = new HashSet<Faction>();

        //! Properties
        public Point WorldPosition { get; set; } = Point.Empty;
        public uint Health { get; private set; } = MaxHealth;
        public uint Level { get; set; } = 1;
        public CharacterState State { get; private set; } = CharacterState.Alive;
    }
}