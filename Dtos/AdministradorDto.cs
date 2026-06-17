namespace api_doceria.Dtos;

public class AdministradorCreateDto
{
    public string Login { get; set; } = string.Empty;
    public string Senha { get; set; } = string.Empty;
}

public class AdministradorReadDto
{
    public int IdAdmin { get; set; }
    public string Login { get; set; } = string.Empty;
}