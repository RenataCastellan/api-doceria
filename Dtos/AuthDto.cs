namespace api_doceria.Dtos;

public class LoginClienteDto
{
    public string Email { get; set; } = string.Empty;
    public string Senha { get; set; } = string.Empty;
}

public class LoginAdminDto
{
    public string Login { get; set; } = string.Empty;
    public string Senha { get; set; } = string.Empty;
}

public class LoginResponseDto
{
    public int Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public string Perfil { get; set; } = string.Empty;
    public string Token { get; set; } = string.Empty;
}

