
namespace VozAmiga.Core.DTO.Commands;


public record ChangePasswordCmd(
    string token,
    string Password,
    string NewPassword
);
