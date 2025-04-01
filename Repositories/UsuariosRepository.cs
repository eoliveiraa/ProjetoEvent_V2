using webapi.event_.Contexts;
using webapi.event_.Domains;
using webapi.event_.Interfaces;
using webapi.event_.Utils;

namespace webapi.event_.Repositories
{
    public class UsuariosRepository : IUsuarioRepository
    {
        private readonly Context _context;

        public UsuariosRepository(Context context)
        {
            _context = context;
        }

        public Usuarios BuscarPorEmailESenha(string email, string senha)
        {
            try
            {
                Usuarios usuarioBuscado = _context.Usuarios
                    .Select(u => new Usuarios
                    {
                        IdUsuario = u.IdUsuario,
                        NomeUsuario = u.NomeUsuario,
                        Email = u.Email,
                        Senha = u.Senha,

                        TipoUsuario = new TiposUsuarios
                        {
                            IdTipoUsuario = u.IdTipoUsuario,
                            TituloTipoUsuario = u.TipoUsuario!.TituloTipoUsuario
                        }
                    }).FirstOrDefault(u => u.Email == email)!;

                if (usuarioBuscado != null)
                {
                    bool confere = Criptografia.CompararHash(senha, usuarioBuscado.Senha!);

                    if (confere)
                    {
                        return usuarioBuscado!;
                    }
                }
                return null!;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Usuarios BuscarPorId(Guid id)
        {
            try
            {
                Usuarios usuarioBuscado = _context.Usuarios
                    .Select(u => new Usuarios
                    {
                        IdUsuario = u.IdUsuario,
                        NomeUsuario = u.NomeUsuario,
                        Email = u.Email,
                        Senha = u.Senha,

                        TipoUsuario = new TiposUsuarios
                        {
                            IdTipoUsuario = u.TipoUsuario!.IdTipoUsuario,
                            TituloTipoUsuario = u.TipoUsuario!.TituloTipoUsuario
                        }

                    }).FirstOrDefault(u => u.IdUsuario == id)!;

                if (usuarioBuscado != null)
                {
                    return usuarioBuscado;

                }
                return null!;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void Cadastrar(Usuarios usuario)
        {
            try
            {
                usuario.IdUsuario = Guid.NewGuid();

                usuario.Senha = Criptografia.GerarHash(usuario.Senha!);


                _context.Usuarios.Add(usuario);


                _context.SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}