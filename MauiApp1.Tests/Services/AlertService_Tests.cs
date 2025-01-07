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
    }
}
