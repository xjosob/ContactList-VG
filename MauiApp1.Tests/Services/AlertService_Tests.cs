using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MauiApp1.Interfaces;
using MauiApp1.Services;

namespace MauiApp1.Tests.Services
{
    public class AlertService_Tests
    {
        [Fact]
        public async Task DisplayAlert_WithValidParameters_ShouldExecuteSuccessfully()
        {
            // Arrange
            var alertService = new AlertService();

            // Act & Assert
            await alertService.DisplayAlert("Title", "Message", "OK");
        }

        [Fact]
        public async Task DisplayAlert_WithNullTitle_ShouldNotThrowException()
        {
            // Arrange
            var alertService = new AlertService();

            // Act & Assert
            await alertService.DisplayAlert(null!, "Message", "OK");
        }
    }
}
