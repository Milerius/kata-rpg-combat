namespace Kata_Rpg_Combat
{
    public class Character
    {
        private const uint MaxHealth = 1000;
        private const int MaxLevelDifference = 5;
        public uint Health { get; set; } = MaxHealth;
        public uint Level { get; set; } = 1;

        public CharacterState State { get; set; } = CharacterState.Alive;

        public void DealDamage(ref Character other, uint damages)
        {
            if (ReferenceEquals(this, other)) return;
            if ((int) Level - (int) other.Level >= MaxLevelDifference) damages += damages / 2;
            else if ((int) other.Level - (int) Level >= MaxLevelDifference) damages -= damages / 2;
            if (damages >= other.Health)
            {
                other.Health = 0;
                other.State = CharacterState.Dead;
            }
            else
                other.Health -= damages;
        }

        public void Heal(uint heal)
        {
            if (this.State == CharacterState.Dead)
                return;
            if (this.Health + heal > MaxHealth)
            {
                this.Health = MaxHealth;
            }
            else
            {
                this.Health += heal;
            }
        }

        private bool Equals(Character other)
        {
            return Health == other.Health && Level == other.Level && State == other.State;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj.GetType() == this.GetType() && Equals((Character) obj);
        }
    }
}