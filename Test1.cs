using NUnit.Framework;
using Moq;


namespace nunittest1
{
    public class TestsTask1
    {
        [Test]
        [TestCase(5, 10, 10)] // Val = 5, arg = 10 -> результат arg (10)
        [TestCase(15, 10, 15)] // Val = 15, arg = 10 -> результат Val (15)
        [TestCase(10, 10, 10)] // Val = 10, arg = 10 -> результат arg (10), так как не строго больше
        public void PublicMethod_ReturnsExpectedValue(int val, int arg, int expectedResult)
        {
            // Arrange
            var mockAff = new Mock<IAffectingClass>();
            mockAff.Setup(m => m.Val).Returns(val);

            var sut = new ClassUnderTest(mockAff.Object);

            // Act
            int result = sut.PublicMethod(arg);

            // Assert
            Assert.AreEqual(expectedResult, result);
        }

        [Test]
        [TestCase(1, 1)] // Обычный AffectingClass возвращает 1
        [TestCase(42, 42)] // Если бы метод возвращал другое значение (мокаем через наследника или переопределение)
        public void CallAffectingMethod_ReturnsExpectedValue(int methodResult, int expectedResult)
        {
            // Arrange
            // Поскольку ClassUnderTest2 принимает конкретный класс AffectingClass, 
            // мы можем передать его мок-версию через наследника (или Mock от Moq, если Method виртуальный)
            var mockAff = new Mock<AffectingClass>();
            mockAff.Setup(m => m.Method()).Returns(methodResult);

            var sut = new ClassUnderTest2(mockAff.Object);

            // Act
            int result = sut.CallAffectingMethod();

            // Assert
            Assert.AreEqual(expectedResult, result);
        }
    }
}