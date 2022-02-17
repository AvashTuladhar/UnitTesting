using NUnit.Framework;
using NUnitNSubsitute;

namespace NUnitNSubsituteTests
{
    [TestFixture]

    public class SimpleClassTests
    {
        private SimpleClass _simpleCLass;

        [SetUp]
        public void Setup()
        {
            _simpleCLass = new SimpleClass();
            _simpleCLass.Amount = 100;
        }
        
        [Test]
        public void AddCount_TakeNumber_ReturnsAmount101()
        {
            var result = _simpleCLass.AddCount(1);

            Assert.That(result,Is.EqualTo(101));
        }

        [Test]
        public void LessCount_TakesNumber_ReturnAmount99()
        {
            var result= _simpleCLass.LessCount(1);
            Assert.That(result,Is.EqualTo(99));
        }

        [Test]
        public void GetAmount_NoInput_ReturnsAmount100()
        {
            var result = _simpleCLass.GetAmount();
            Assert.That(result, Is.EqualTo(100));
        }
    }
}