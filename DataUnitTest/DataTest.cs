using NUnit.Framework;
using Data;
namespace DataUnitTest
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Add_WhenCalled_AddsItemToList()
        {
            // Arrange
            var repository = new DataApi<string>();
            string itemToAdd = "TestItem";

            // Act
            repository.Add(itemToAdd);

            // Assert
            Assert.Contains(itemToAdd, repository.GetAll());
        }

        [Test]
        public void Remove_WhenCalled_RemovesItemFromList()
        {
            // Arrange
            var repository = new DataApi<int>();
            int itemToRemove = 10;
            repository.Add(itemToRemove);

            // Act
            repository.Remove(itemToRemove);

            // Assert
            Assert.IsFalse(repository.GetAll().Contains(itemToRemove));
        }

        [Test]
        public void GetAll_WithItemsInRepository_ReturnsAllItems()
        {
            // Arrange
            var repository = new DataApi<double>();
            var itemsToAdd = new List<double> { 1.1, 2.2, 3.3 };
            foreach (var item in itemsToAdd)
            {
                repository.Add(item);
            }

            // Act
            var result = repository.GetAll();

            // Assert
            Assert.AreEqual(itemsToAdd.Count, result.Count);
            foreach (var item in itemsToAdd)
            {
                Assert.Contains(item, result);
            }
        }
    }
}