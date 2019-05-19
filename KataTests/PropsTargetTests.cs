using Kata_Rpg_Combat;
using NUnit.Framework;

namespace KataTests
{
    [TestFixture]
    public class PropsTargetTests
    {
        private Character _chr;
        private IPropsTarget _things;

        [SetUp]
        public void Setup()
        {
            _chr = new MeleeCharacter();
            _things = new Tree();
        }

        [TestCase(TestName = "DealDamageToProps")]
        public void DealDamageToProps()
        {
            Assert.AreEqual(2000, _things.Health);
            _chr.DealDamage(_things, 200);
            Assert.AreEqual(1800, _things.Health);
            Assert.IsTrue(_things.State == PropsTargetState.Alive);
            _chr.DealDamage(_things, 1800);
            Assert.AreEqual(0, _things.Health);
            Assert.IsTrue(_things.State == PropsTargetState.Destroyed);
        }
    }
}