using Moq;
using RealTimeOrderEngine.Application.Interfaces.Repositories;
using RealTimeOrderEngine.Application.Interfaces.Services;
using RealTimeOrderEngine.Application.Services;
using RealTimeOrderEngine.Domain.Entities;
using RealTimeOrderEngine.Shared.DTOs.Auth;
using Xunit;

namespace RealTimeOrderEngine.Application.Tests;

public class AuthServiceTests
{
    [Fact]
    public async Task LoginAsync_ReturnsNull_WhenStaffMemberDoesNotExist()
    {
        var staffRepository = new Mock<IStaffRepository>();
        var tokenService = new Mock<ITokenService>();
        staffRepository.Setup(x => x.GetByPinAsync("1234")).ReturnsAsync((Staff?)null);

        var sut = new AuthService(staffRepository.Object, tokenService.Object);

        var result = await sut.LoginAsync(new LoginDto { PinCode = "1234" });

        Assert.Null(result);
        tokenService.Verify(x => x.GenerateToken(It.IsAny<Staff>()), Times.Never);
    }

    [Fact]
    public async Task LoginAsync_ReturnsNull_WhenStaffMemberIsInactive()
    {
        var staffRepository = new Mock<IStaffRepository>();
        var tokenService = new Mock<ITokenService>();
        staffRepository.Setup(x => x.GetByPinAsync("1234")).ReturnsAsync(new Staff
        {
            Name = "Kitchen User",
            PinCode = "1234",
            Role = "Kitchen",
            IsActive = false
        });

        var sut = new AuthService(staffRepository.Object, tokenService.Object);

        var result = await sut.LoginAsync(new LoginDto { PinCode = "1234" });

        Assert.Null(result);
        tokenService.Verify(x => x.GenerateToken(It.IsAny<Staff>()), Times.Never);
    }

    [Fact]
    public async Task LoginAsync_ReturnsNull_WhenStaffMemberIsSoftDeleted()
    {
        var staffRepository = new Mock<IStaffRepository>();
        var tokenService = new Mock<ITokenService>();
        staffRepository.Setup(x => x.GetByPinAsync("1234")).ReturnsAsync(new Staff
        {
            Name = "Deleted User",
            PinCode = "1234",
            Role = "Admin",
            IsActive = true,
            IsDeleted = true
        });

        var sut = new AuthService(staffRepository.Object, tokenService.Object);

        var result = await sut.LoginAsync(new LoginDto { PinCode = "1234" });

        Assert.Null(result);
        tokenService.Verify(x => x.GenerateToken(It.IsAny<Staff>()), Times.Never);
    }

    [Fact]
    public async Task LoginAsync_ReturnsTokenAndIdentity_WhenStaffMemberIsValid()
    {
        var staff = new Staff
        {
            Name = "Admin User",
            PinCode = "9876",
            Role = "Admin",
            IsActive = true
        };

        var staffRepository = new Mock<IStaffRepository>();
        var tokenService = new Mock<ITokenService>();
        staffRepository.Setup(x => x.GetByPinAsync("9876")).ReturnsAsync(staff);
        tokenService.Setup(x => x.GenerateToken(staff)).Returns("jwt-token");

        var sut = new AuthService(staffRepository.Object, tokenService.Object);

        var result = await sut.LoginAsync(new LoginDto { PinCode = "9876" });

        Assert.NotNull(result);
        Assert.Equal("jwt-token", result!.Token);
        Assert.Equal("Admin User", result.StaffName);
        Assert.Equal("Admin", result.Role);
        tokenService.Verify(x => x.GenerateToken(staff), Times.Once);
    }
}
