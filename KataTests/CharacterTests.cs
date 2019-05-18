using Kata_Rpg_Combat;
using NUnit.Framework;

namespace Tests
{
    public class CharacterTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void DefaultValueAreOk()
        {
            var chr = new Character();
            Assert.AreEqual(1, chr.Level);
            Assert.AreEqual(1000, chr.Health);
            Assert.AreEqual(CharacterState.Alive, chr.State);

            var chrToCompare = new Character {Health = 1000, Level = 1, State = CharacterState.Alive};

            Assert.True(chr.Equals(chrToCompare));
        }

        [Test]
        public void DealDamage()
        {
            var chr = new Character();
            var chrToFight = new Character();

            chr.DealDamage(ref chrToFight, 100);
            Assert.AreEqual(900, chrToFight.Health);
            Assert.True(chrToFight.State == CharacterState.Alive);

            chr.DealDamage(ref chrToFight, 1000);
            Assert.AreEqual(0, chrToFight.Health);
            Assert.True(chrToFight.State == CharacterState.Dead);

            chr.DealDamage(ref chr, 100);
            Assert.AreEqual(1000, chr.Health);
            Assert.True(chr.State == CharacterState.Alive);


            chr = new Character();
            chrToFight = new Character();

            chr.Level = 6;

            chr.DealDamage(ref chrToFight, 50);
            Assert.AreEqual(925, chrToFight.Health);

            chr.Level = 1;
            chrToFight.Level = 6;

            chr.DealDamage(ref chrToFight, 50);
            Assert.AreEqual(900, chrToFight.Health);
        }

        [Test]
        public void Heal()
        {
            var chr = new Character();
            var otherChrCannotBeHeal = new Character();


            otherChrCannotBeHeal.DealDamage(ref chr, 100);
            chr.Heal(200);
            Assert.AreEqual(1000, chr.Health);

            otherChrCannotBeHeal.DealDamage(ref chr, 300);
            chr.Heal(100);
            Assert.AreEqual(800, chr.Health);

            otherChrCannotBeHeal.DealDamage(ref chr, 800);


            chr.Heal(100);
            Assert.True(chr.State == CharacterState.Dead);
            Assert.AreEqual(0, chr.Health);
        }
    }
}