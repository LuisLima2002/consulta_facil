
namespace VozAmiga.Core.DTO.Commands;


public record SignInCmd(
    string UserName,
    string? Password
);
