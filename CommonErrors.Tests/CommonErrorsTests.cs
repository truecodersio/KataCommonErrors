using CommonErrors.Shared;
using Xunit;

namespace CommonErrors.Tests
{
    public class CommonErrorsTests
    {
        [Fact]
        public void ShouldOnlyAllowTenAnswers()
        {
            //Arrange
            const int size = 10;
            var queue = new AnswerQueue<TrueFalseAnswer>(size);

            //Act
            for (var i = 0; i < size + 1; i++)
            {
                queue.Enqueue(new TrueFalseAnswer(true));
            }

            //Assert
            Assert.True(queue.Count <= 10);
        }

        [Fact]
        public void ShouldForgetAtCapacity()
        {
            //Arrange
            const int size = 10;
            var queue = new AnswerQueue<TrueFalseAnswer>(size);
            queue.Enqueue(new TrueFalseAnswer(false));

            for (var i = 0; i < 10; i++)
            {
                queue.Enqueue(new TrueFalseAnswer(true));
            }

            //Act
            var grade = queue.Grade;

            //Assert
            Assert.Equal(100, grade);
        }

        [Fact]
        public void ShouldReturnExpectedAverage()
        {
            //Arrange
            const int size = 10;
            var queue = new AnswerQueue<TrueFalseAnswer>(size);

            queue.Enqueue(new TrueFalseAnswer(false));
            queue.Enqueue(new TrueFalseAnswer(true));
            queue.Enqueue(new TrueFalseAnswer(true));
            queue.Enqueue(new TrueFalseAnswer(false));

            //Act
            var grade = queue.Grade;

            //Assert
            Assert.Equal(50, grade);
        }
    }
}
