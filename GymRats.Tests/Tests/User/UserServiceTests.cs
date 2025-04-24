using GymRats.Business.Interfaces;
using Moq;
using Xunit;

namespace GymRats.Tests.Tests.User;

public class UserServiceTests
{
    private readonly Mock<IUserServices> _userServicesMock;

    public UserServiceTests()
    {
        _userServicesMock = new Mock<IUserServices>();
    }

    [Fact]
    public async Task UserLogin_WithValidCredentials_ShouldReturnUser()
    {
        var email = "mmajek@example.com";
        var password = "test1";
        var expectedUser = new Data.Entities.Uzytkownik
        {
            IdUzytkownik = 12,
            Email = email
        };

        _userServicesMock
            .Setup(x => x.LoginAsync(
                email,
                password,
                It.IsAny<CancellationToken>()))
            .ReturnsAsync((true, "mock_token", expectedUser));

        var result = await _userServicesMock.Object.LoginAsync(
            email, password,
            CancellationToken.None);

        Assert.True(result.success);
        Assert.NotNull(result.token);
        Assert.Equal(expectedUser.Email, result.user.Email);
    }
 
    [Fact]
    public async Task UserLogin_WithInvalidPassword_ShouldReturnFailure()
    {
        _userServicesMock
            .Setup(x => x.LoginAsync(
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync((false, null, null));

        var result =
            await _userServicesMock.Object.LoginAsync(
                "wrong@example.com",
                "wrong_pass",
                CancellationToken.None);

        Assert.False(result.success);
        Assert.Null(result.token);
        Assert.Null(result.user);
    }

    [Fact]
    public async Task UserRegister_WithValidCredentials_ShouldReturnTrue()
    {
        _userServicesMock
            .Setup(x => x.RegisterAsync(
                "existing@example.com",
                "strongPassword",
                "Jan",
                "Kowalski",
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(true);

        var result = await _userServicesMock.Object.RegisterAsync(
            "existing@example.com", "strongPassword", "Jan", "Kowalski", CancellationToken.None);

        Assert.True(result);
    }
    
    
    [Fact]
    public async Task UserRegister_WithInvalidCredentials_ShouldReturnFalse()
    {
        _userServicesMock
            .Setup(x => x.RegisterAsync(
                "existing@example.com",
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(false);

        var result = await _userServicesMock.Object.RegisterAsync(
            "existing@example.com", "strongPassword", "Jan", "Kowalski", CancellationToken.None);

        Assert.False(result);
    }
}