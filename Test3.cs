using NUnit.Framework;
using Moq;

namespace nunittest1
{
    [TestFixture]
    public class TestsTask3
    {
        
        //1: С использованием атрибута [SetUp]
        [TestFixture]
        public class WithSetUpTests
        {
            private Mock<IAffectingClass> _mockAff;
            private ClassUnderTest _sut;
            // Этот метод вызывается автоматически ПЕРЕД КАЖДЫМ тестом в этом классе
            [SetUp]
            public void Setup()
            {
                _mockAff = new Mock<IAffectingClass>();
                _sut = new ClassUnderTest(_mockAff.Object);
            }
            [Test]
            [TestCase(5, 10, 10)]
            [TestCase(20, 10, 20)]
            public void PublicMethod_WithSetUp_ReturnsExpectedValue(int val, int arg, int expectedResult)
            {
                // Настраиваем мок, подготовленный в методе Setup
                _mockAff.Setup(m => m.Val).Returns(val);

                // Вызываем тестируемый метод
                int result = _sut.PublicMethod(arg);

                // Проверяем результат
                Assert.AreEqual(expectedResult, result);
            }
        }
        //2: БЕЗ использования атрибута [SetUp]
        [TestFixture]
        public class WithoutSetUpTests
        {
            [Test]
            [TestCase(5, 10, 10)]
            [TestCase(20, 10, 20)]
            public void PublicMethod_WithoutSetUp_ReturnsExpectedValue(int val, int arg, int expectedResult)
            {
                // Всё создание и настройка происходят локально внутри теста (Self-Contained)
                var mockAff = new Mock<IAffectingClass>();
                mockAff.Setup(m => m.Val).Returns(val);
                var sut = new ClassUnderTest(mockAff.Object);
                // Вызываем тестируемый метод
                int result = sut.PublicMethod(arg);
                // Проверяем результат
                Assert.AreEqual(expectedResult, result);
            }
        }
    }
}