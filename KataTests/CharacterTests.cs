using System;
using System.Drawing;
using Kata_Rpg_Combat;
using NUnit.Framework;

namespace KataTests
{
    [TestFixture]
    public class CharacterTests
    {
        private Character _chr;
        private Character _chrTwo;
        private Character _chrRange;

        [SetUp]
        public void Setup()
        {
            _chr = new MeleeCharacter();
            _chrTwo = new MeleeCharacter();
            _chrRange = new RangeCharacter();
        }

        [Test]
        public void DefaultValueAreOk()
        {
            Assert.AreEqual(1, _chr.Level);
            Assert.AreEqual(1000, _chr.Health);
            Assert.AreEqual(CharacterState.Alive, _chr.State);
            Assert.AreEqual(Point.Empty, _chr.WorldPosition);
            Assert.IsTrue(_chr.Equals(_chrTwo));
            Assert.IsFalse(_chr.Equals(null));
            Assert.IsFalse(_chr.Equals(new String("")));
            Assert.IsTrue(_chr.Equals(_chr));
        }


        [TestCase(TestName = "DealDamageToAlly")]
        public void DealDamageToAlly()
        {
            _chr.JoinFaction(Faction.Anthrax);
            _chrTwo.JoinFaction(Faction.Anthrax);
            _chr.DealDamage(_chrTwo, 100);
            Assert.AreEqual(1000, _chrTwo.Health);
            Assert.True(_chrTwo.State == CharacterState.Alive);
        }

        [TestCase(TestName = "DealDamageStandard")]
        public void DealDamageStandard()
        {
            _chr.DealDamage(_chrTwo, 100);
            Assert.AreEqual(900, _chrTwo.Health);
            Assert.True(_chrTwo.State == CharacterState.Alive);
        }

        [TestCase(TestName = "DealDamageExceedHealth")]
        public void DealDamageExceedHealth()
        {
            _chr.DealDamage(_chrTwo, 1100);
            Assert.AreEqual(0, _chrTwo.Health);
            Assert.True(_chrTwo.State == CharacterState.Dead);
        }

        [TestCase(TestName = "DealDamageToMySelf")]
        public void DealDamageToMySelf()
        {
            _chr.DealDamage(_chr, 100);
            Assert.AreEqual(1000, _chr.Health);
            Assert.True(_chr.State == CharacterState.Alive);
        }

        [TestCase(TestName = "DealDamageWithFiveLevelAbove")]
        public void DealDamageWithFiveLevelAbove()
        {
            _chr.Level = 6;
            _chr.DealDamage(_chrTwo, 50);
            Assert.AreEqual(925, _chrTwo.Health);
        }

        [TestCase(TestName = "DealDamageWithFiveLevelBehind")]
        public void DealDamageWithFiveLevelBehind()
        {
            _chrTwo.Level = 6;
            _chr.DealDamage(_chrTwo, 50);
            Assert.AreEqual(975, _chrTwo.Health);
        }

        [TestCase(TestName = "DealDamageOutOfRange")]
        public void DealDamageOutOfRange()
        {
            _chrTwo.WorldPosition = new Point(15, 15);
            _chr.DealDamage(_chrTwo, 100);
            Assert.AreEqual(1000, _chrTwo.Health);

            _chrTwo.WorldPosition = new Point(22, 33);
            _chrTwo.DealDamage(_chr, 100);
            Assert.AreEqual(1000, _chr.Health);
        }

        [TestCase(TestName = "HealMoreThanMaxHealth")]
        public void HealMoreThanMaxHealth()
        {
            _chrTwo.DealDamage(_chr, 100);
            _chr.Heal(200);
            Assert.AreEqual(1000, _chr.Health);
        }

        [TestCase(TestName = "HealRegular")]
        public void HealRegular()
        {
            _chrTwo.DealDamage(_chr, 300);
            _chr.Heal(100);
            Assert.AreEqual(800, _chr.Health);
        }

        [TestCase(TestName = "HealAlly")]
        public void HealAlly()
        {
            _chr.JoinFaction(Faction.Anthrax);
            _chrRange.JoinFaction(Faction.Anthrax);
            _chrTwo.DealDamage(_chr, 300);
            _chrRange.Heal(_chr, 100);
            Assert.AreEqual(800, _chr.Health);

            _chr.LeaveFaction(Faction.Anthrax);
            _chrRange.Heal(_chr, 100);
            Assert.AreEqual(800, _chr.Health);
        }

        [TestCase(TestName = "HealDeathCharacter")]
        public void HealDeathCharacter()
        {
            _chrTwo.DealDamage(_chr, 1100);
            _chr.Heal(100);
            Assert.True(_chr.State == CharacterState.Dead);
            Assert.AreEqual(0, _chr.Health);
        }

        [TestCase(TestName = "AddOneFaction")]
        public void AddOneFaction()
        {
            Assert.AreEqual(1, _chr.JoinFaction(Faction.Anthrax));
        }

        [TestCase(TestName = "AddMultipleFactions")]
        public void AddMultipleFactions()
        {
            Assert.AreEqual(3, _chr.JoinFaction(Faction.Anthrax, Faction.Iron, Faction.Vortex));
        }

        [TestCase(TestName = "AddMultipleSameFactions")]
        public void AddMultipleSameFactions()
        {
            Assert.AreEqual(2, _chr.JoinFaction(Faction.Anthrax, Faction.Anthrax, Faction.Vortex));
        }

        [TestCase(TestName = "RemoveOneFaction")]
        public void RemoveOneFaction()
        {
            _chr.JoinFaction(Faction.Anthrax);
            Assert.AreEqual(0, _chr.LeaveFaction(Faction.Anthrax));
        }

        [TestCase(TestName = "RemoveMultipleFactions")]
        public void RemoveMultipleFactions()
        {
            _chr.JoinFaction(Faction.Anthrax, Faction.Iron);
            Assert.AreEqual(0, _chr.LeaveFaction(Faction.Anthrax, Faction.Iron));
        }

        [TestCase(TestName = "AreWeAlly")]
        public void AreWeAlly()
        {
            _chr.JoinFaction(Faction.Anthrax, Faction.Iron);
            _chrTwo.JoinFaction(Faction.Anthrax, Faction.Vortex);
            Assert.IsTrue(_chr.AreWeAlly(_chrTwo));

            _chrTwo.LeaveFaction(Faction.Anthrax);
            Assert.IsFalse(_chr.AreWeAlly(_chrTwo));
        }
    }
}