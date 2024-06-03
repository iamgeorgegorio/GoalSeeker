using Moq;
using GoalSeek.Server.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GoalSeek.Server.Helpers;
using GoalSeek.Server.Controllers;
using Microsoft.Extensions.Logging;
using GoalSeek.Server.Models;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;

namespace GoalSeek.Test.ControllerTest
{

    public class GoalSeekControllerTests
    {
        private readonly Mock<IValidation> _validate;
        private readonly Mock<IGoalSeekProcessor> _seekprocessor;
        private readonly Mock<ILogger<GoalSeekController>> _logger;
        private GoalSeekController _controller;
        public GoalSeekControllerTests()
        {
            _validate = new Mock<IValidation>();
            _seekprocessor = new Mock<IGoalSeekProcessor>();
            _logger = new Mock<ILogger<GoalSeekController>>();
            _controller = new GoalSeekController(_validate.Object, _seekprocessor.Object, _logger.Object);
        }

        [Fact]
        public void Post_ValidData_Return200()
        {
            //Arrange
            var data = new GoalSeekData()
            {
                formula = "2.5 * input",
                input = "100",
                maximumIterations = "10",
                targetResult = "2500"
            };

            var response = new GoalSeekResult() { 
              targetInput = "1000",
              iteration = "10"
            };

            _validate.Setup(x => x.CheckExpression(data)).Returns("");
            _seekprocessor.Setup(x => x.Process(data)).Returns(response);

            var sut = new GoalSeekController(_validate.Object, _seekprocessor.Object, _logger.Object);

            //Act
            var result = sut.Post(data);

            //Assert
            result.GetType().Should().Be(typeof(OkObjectResult));
        }

        [Fact]
        public void Post_InvalidData_Return400()
        {
            //Arrange
            var data = new GoalSeekData()
            {
                formula = "2.5 * input * p",
                input = "100",
                maximumIterations = "10",
                targetResult = "2500"
            };

            var response = new GoalSeekResult()
            {
                targetInput = "1000",
                iteration = "10"
            };

            _validate.Setup(x => x.CheckExpression(data)).Returns("Invalid formula!");
            _seekprocessor.Setup(x => x.Process(data)).Returns(response);
            //_logger.Setup(x => x.LogInformation(""));

            var sut = new GoalSeekController(_validate.Object, _seekprocessor.Object, _logger.Object);

            //Act
            var result = sut.Post(data);

            //Assert
            result.GetType().Should().Be(typeof(BadRequestObjectResult));

        }
    }
}
