namespace VozAmiga.Api.DTO.ViewModel;

/// <summary>
/// A record of the aplication control information
/// </summary>
/// <param name="Token"></param>
/// <param name="IsPatient"></param>
/// <param name="UpdateCredential"></param>
/// <returns></returns>
public record ApiCredentials(string Token, bool IsPatient = false, bool UpdateCredential = false, string Name ="");
