namespace TallerApi.Models
{
	public class LoginRequestDto
	{
		public required string NombreUsuario { get; set; }
		public required string Password { get; set; }
	}

	public class LoginResponseDto
	{
		public int IdUsuario { get; set; }
		public string NombreUsuario { get; set; } = string.Empty;
		public string NombreCompleto { get; set; } = string.Empty;
		public string Rol { get; set; } = string.Empty;
		public string Token { get; set; } = string.Empty;
	}
}