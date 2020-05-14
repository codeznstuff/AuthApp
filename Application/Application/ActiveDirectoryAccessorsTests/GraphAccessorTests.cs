using ActiveDirectoryAccessors;
using ActiveDirectoryAccessors.Interfaces;
using Xunit;

namespace ActiveDirectoryAccessorsTests
{
    public class GraphAccessorTests
    {
        [Fact]
        public void GetUserByEmail_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var accessorFactory = new AccessorFactory();
            var graphAccessor = accessorFactory.CreateAccessor<IGraphAccessor>();
            string email = "Fake.User@dummy.com";

            // Act
            var result = graphAccessor.GetUserByEmail(email);

            // Assert
            Assert.Equal(expected: email, actual: result.EmailAddress);
        }

        [Fact]
        public void GetUserById_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var accessorFactory = new AccessorFactory();
            var graphAccessor = accessorFactory.CreateAccessor<IGraphAccessor>();
            string userId = "be64b27b-59b6-43bb-a824-51c16d328ffb";

            // Act
            var result = graphAccessor.GetUserById(userId);

            // Assert
            Assert.Equal(expected: userId, actual: result.Id.ToString());
        }

        [Fact]
        public void GetUserByDisplayName_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var accessorFactory = new AccessorFactory();
            var graphAccessor = accessorFactory.CreateAccessor<IGraphAccessor>();
            string displayName = "Fake.User";

            // Act
            var result = graphAccessor.GetUserByDisplayName(displayName);

            // Assert
            Assert.Equal(expected: displayName, actual: result.DisplayName);
        }
    }
}