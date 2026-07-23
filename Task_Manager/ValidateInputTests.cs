using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Task__Manager;
using Task_Manager;

namespace Task_Manager
{
    [TestClass]
    public sealed class ValidateInputTests
    {
        // TestMethod] attribute indicates that this method is a test method
        [TestMethod]
        // Test method to check if the title is valid
        public void IsTitleValid_ValidTitle_ReturnsTrue()
        {
            //Arrange
            bool result = ValidateInput.IsTitleValid("Title Is Valid");
            //Assert
            Assert.IsTrue(result);
        }
        [TestMethod]
        // Test method to check if the title is empty
        public void IsTitleValid_EmptyTitle_ReturnsFalse()
        {
            //Act
            bool result = ValidateInput.IsTitleValid("");
            //Assert
            Assert.IsFalse(result);
        }
        [TestMethod]
        // Test method to check if the Date is valid
        public void IsDueDateValid_FutureDate_ReturnsTrue()
        {
            //Arrange
            bool result = ValidateInput.IsDueDateValid(DateTime.Today.AddDays(1));
            //Assert
            Assert.IsTrue(result);
        }
        [TestMethod]
        // Test method to check if the Date is in the past
        public void IsDueDateValid_PastDate_ReturnsFalse()
        {
            //Arrange
            bool result = ValidateInput.IsDueDateValid(DateTime.Today.AddDays(-1));
            Assert.IsFalse(result);
        }
    }
}
