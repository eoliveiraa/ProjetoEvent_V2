using webapi.event_.Domains;

namespace webapi.event_.Interfaces
{
    public interface ITiposUsuariosRepository
    {
        void Cadastrar(TiposUsuarios tipoUsuario);
        void Deletar(Guid id);
        List<TiposUsuarios> Listar();
        TiposUsuarios BuscarPorId(Guid id);
        void Atualizar(Guid id, TiposUsuarios tipoUsuario);
    }
}
