using NUnit.Framework;
using Moq;
using System;

namespace nunittest1
{
    [TestFixture]
    public class TestsTask2
    {
        // Вспомогательный класс-обертка для доступа к protected методу
        private class ClassUnderTestWrapper : ClassUnderTest
        {
            public ClassUnderTestWrapper(IAffectingClass pAff) : base(pAff) { }

            public int CallProtectedMethod(int arg)
            {
                return ProtectedMethod(arg);
            }
        }

        [Test]
        [TestCase(5, 10)]   // arg не равен 0 -> должен вернуть свойство Val (10)
        [TestCase(-1, 42)]  // arg не равен 0 -> должен вернуть свойство Val (42)
        public void ProtectedMethod_WhenArgNotZero_ReturnsVal(int arg, int expectedVal)
        {
            // Arrange
            var mockAff = new Mock<IAffectingClass>();
            mockAff.Setup(m => m.Val).Returns(expectedVal);

            var sut = new ClassUnderTestWrapper(mockAff.Object);

            // Act
            int result = sut.CallProtectedMethod(arg);

            // Assert
            Assert.AreEqual(expectedVal, result);
        }

        [Test]
        public void ProtectedMethod_WhenArgIsZero_ThrowsArgumentException()
        {
            // Arrange
            var mockAff = new Mock<IAffectingClass>();
            var sut = new ClassUnderTestWrapper(mockAff.Object);

            // Act & Assert (проверяем, что выбрасывается ArgumentException при arg = 0)
            var ex = Assert.Throws<ArgumentException>(() => sut.CallProtectedMethod(0));

            // Дополнительно проверяем имя параметра в исключении
            Assert.AreEqual("arg", ex.ParamName);
        }
    }
}