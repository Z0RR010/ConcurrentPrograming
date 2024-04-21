using Data;

namespace DataUnitTest
{
    public class DataTest
    {
        [SetUp]
        public void Setup()
        {
        }

        [TestFixture]
        public class DataTests
        {
            [Test]
            public void GetRepository_Returns_Correct_Repository()
            {
                // Arrange
                var dataApi = new DataApi();

                // Act
                var repository = dataApi.GetRepository<object>();

                // Assert
                Assert.IsNotNull(repository);
                Assert.IsInstanceOf<Repository<object>>(repository);
            }
        }
        [TestFixture]
        public class RepositoryTests
        {
            [Test]
            public void Add_Adds_Item_To_Collection()
            {
                // Arrange
                var repository = new Repository<object>();
                var item = new object();

                // Act
                repository.Add(item);

                // Assert
                Assert.IsTrue(repository.Contains(item));
            }

            [Test]
            public void Remove_Removes_Item_From_Collection()
            {
                // Arrange
                var repository = new Repository<object>();
                var item = new object();
                repository.Add(item);

                // Act
                repository.Remove(item);

                // Assert
                Assert.IsFalse(repository.Contains(item));
            }

            [Test]
            public void Clear_Removes_All_Items_From_Collection()
            {
                // Arrange
                var repository = new Repository<object>();
                repository.Add(new object());
                repository.Add(new object());
                repository.Add(new object());

                // Act
                repository.Clear();

                // Assert
                Assert.AreEqual(0, repository.Count);
            }

            [Test]
            public void Count_Returns_Correct_Number_Of_Items_In_Collection()
            {
                // Arrange
                var repository = new Repository<object>();
                repository.Add(new object());
                repository.Add(new object());
                repository.Add(new object());

                // Act & Assert
                Assert.AreEqual(3, repository.Count);
            }
        }
    }
}