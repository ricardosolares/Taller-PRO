using Microsoft.EntityFrameworkCore;
using TallerApi.Data;
using TallerApi.Entities;
using TallerApi.Models;

namespace TallerApi.Services
{
	public interface IAuthService
	{
		Task<(bool Ok, string Mensaje)> RegisterAsync(LoginRequestDto request);
		Task<LoginResponseDto?> LoginAsync(LoginRequestDto request);
	}

	public class AuthService : IAuthService
	{
		private readonly TallerDbContext _context;

		public AuthService(TallerDbContext context)
		{
			_context = context;
		}

		public async Task<(bool Ok, string Mensaje)> RegisterAsync(LoginRequestDto request)
		{
			var existe = await _context.Usuarios
				.AnyAsync(u => u.NombreUsuario == request.NombreUsuario);

			if (existe)
				return (false, "El nombre de usuario ya existe.");

			var usuario = new UsuarioEntities
			{
				NombreCompleto = request.NombreUsuario,
				NombreUsuario = request.NombreUsuario,
				PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Password),
				Rol = "Admin",
				Activo = true,
				FechaCreacion = DateTime.Now
			};

			_context.Usuarios.Add(usuario);
			await _context.SaveChangesAsync();

			return (true, "Usuario creado correctamente.");
		}

		public async Task<LoginResponseDto?> LoginAsync(LoginRequestDto request)
		{
			var usuario = await _context.Usuarios
				.FirstOrDefaultAsync(u => u.NombreUsuario == request.NombreUsuario && u.Activo);

			if (usuario == null || !BCrypt.Net.BCrypt.Verify(request.Password, usuario.PasswordHash))
				return null;

			return new LoginResponseDto
			{
				IdUsuario = usuario.IdUsuario,
				NombreUsuario = usuario.NombreUsuario,
				NombreCompleto = usuario.NombreCompleto,
				Rol = usuario.Rol,
				Token = "sesion-valida"
			};
		}
	}
}