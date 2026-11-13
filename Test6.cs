using NUnit.Framework;
using Moq;

namespace nunittest1
{
    public class TestsTask6
    {
        [Test]
        public void PublicMethod_VerifiesValPropertyWasRead()
        {
            // Arrange
            var mockAff = new Mock<IAffectingClass>();
            mockAff.Setup(m => m.Val).Returns(10);

            var sut = new ClassUnderTest(mockAff.Object);

            // Act
            int result = sut.PublicMethod(5);

            // Assert (Меняем Times.Once на Times.Exactly(2), так как свойство запрашивается дважды)
            mockAff.Verify(m => m.Val, Times.Exactly(2));
        }
    }
}