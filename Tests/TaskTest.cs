using Xunit;
using System.Collections.Generic;
using System;
using System.Data;
using System.Data.SqlClient;

namespace ToDoList
{
    public class ToDoTest : IDisposable
    {
        public ToDoTest()
        {
            DBConfiguration.ConnectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=todolist;Integrated Security=SSPI;";
        }
        public void Dispose()
        {
            Task.DeleteAll();
        }

        [Fact]
        public void Test_DatabaseEmptyAtFirst()
        {
            //ARRANGE, ACT
            int result = Task.GetAll().Count;

            //ASSERT
            Assert.Equal(0, result);
        }
        [Fact]
        public void Test_Equal_ReturnsTrueIfDescriptionsAreTheSame()
        {
            Task firstTask = new Task("Sort laundry");
            Task secondTask = new Task("Sort laundry");

            Assert.Equal(firstTask, secondTask);
        }

        [Fact]
        public void Test_Save_SavesToDatabase()
        {
          //Arrange
          Task testTask = new Task("Mow the lawn");

          //Act
          testTask.Save();
          List<Task> result = Task.GetAll();
          List<Task> testList = new List<Task>{testTask};

          //Assert
          Assert.Equal(testList, result);
        }
        [Fact]
        public void Test_Save_AssignsIdToObject()
        {
            //Arrange
            Task testTask = new Task("Mow the lawn");

            //Act
            testTask.Save();
            Task savedTask = Task.GetAll()[0];

            int result = savedTask.GetId();
            int testId = testTask.GetId();

            //Assert
            Assert.Equal(testId, result);
        }
        [Fact]
        public void Test_Find_FindsTaskInDatabase()
        {
            //Arrange
            Task testTask = new Task("Mow the lawn");
            testTask.Save();

            //Act
            Task foundTask = Task.Find(testTask.GetId());

            //Assert
            Assert.Equal(testTask, foundTask);
        }
    }
}
