using FluentAssertions;
using GoalSeek.Server.Helpers;
using GoalSeek.Server.Interfaces;
using GoalSeek.Server.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoalSeek.Test.HelpersTest
{
    public class ValidationTests
    {
 
        [Fact]
        public void CheckExpression_InvalidFormula()
        {
            //Arrange
            var data = new GoalSeekData()
            {
                formula = "2.5 * input * p",
                input = "100",
                maximumIterations = "10",
                targetResult = "2500"
            };

            var validate = new Validation();

            //Act
            var sut = validate.CheckExpression(data);

            //Assert
            var result = sut.ToString();

            result.Should().Be("Invalid formula");
        }

        [Fact]
        public void CheckExpression_InputValue_IsEmpty()
        {
            //Arrange
            var data = new GoalSeekData()
            {
                formula = "2.5 * input * p",
                input = "",
                maximumIterations = "10",
                targetResult = "2500"
            };

            var validate = new Validation();

            //Act
            var sut = validate.CheckExpression(data);

            //Assert
            var result = sut.ToString();

            result.Should().Be("Input value is empty.");
        }

        [Fact]
        public void CheckExpression_InputValue_Invalid()
        {
            //Arrange
            var data = new GoalSeekData()
            {
                formula = "2.5 * input * p",
                input = "notnumber",
                maximumIterations = "10",
                targetResult = "2500"
            };

            var validate = new Validation();

            //Act
            var sut = validate.CheckExpression(data);

            //Assert
            var result = sut.ToString();

            result.Should().Be("Input value is invalid.");
        }

        [Fact]
        public void CheckExpression_TargetResult_IsEmpty()
        {
            //Arrange
            var data = new GoalSeekData()
            {
                formula = "2.5 * input",
                input = "100",
                maximumIterations = "10",
                targetResult = ""
            };

            var validate = new Validation();

            //Act
            var sut = validate.CheckExpression(data);

            //Assert
            var result = sut.ToString();

            result.Should().Be("Target result value is empty.");
        }

        [Fact]
        public void CheckExpression_TargetResult_Invalid()
        {
            //Arrange
            var data = new GoalSeekData()
            {
                formula = "2.5 * input",
                input = "100",
                maximumIterations = "10",
                targetResult = "notnumr"
            };

            var validate = new Validation();

            //Act
            var sut = validate.CheckExpression(data);

            //Assert
            var result = sut.ToString();

            result.Should().Be("Target result value is invalid.");
        }

        [Fact]
        public void CheckExpression_MaximumIterations_IsEmpty()
        {
            //Arrange
            var data = new GoalSeekData()
            {
                formula = "2.5 * input",
                input = "100",
                maximumIterations = "",
                targetResult = "2500"
            };

            var validate = new Validation();

            //Act
            var sut = validate.CheckExpression(data);

            //Assert
            var result = sut.ToString();

            result.Should().Be("Maximum iterations value is empty.");
        }

        [Fact]
        public void CheckExpression_MaximumIterations_Invalid()
        {
            //Arrange
            var data = new GoalSeekData()
            {
                formula = "2.5 * input",
                input = "100",
                maximumIterations = "adad",
                targetResult = "2500"
            };

            var validate = new Validation();

            //Act
            var sut = validate.CheckExpression(data);

            //Assert
            var result = sut.ToString();

            result.Should().Be("Maximum iterations value is invalid.");
        }
    }
}
