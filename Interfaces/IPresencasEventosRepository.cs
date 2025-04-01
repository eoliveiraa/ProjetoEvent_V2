using webapi.event_.Domains;

namespace webapi.event_.Interfaces
{
    public interface IPresencasEventosRepository
    {
        void Deletar(Guid id);
        List<PresencasEventos> Listar();
        PresencasEventos BuscarPorId(Guid id);
        void Atualizar(Guid id, PresencasEventos presencaEvento);
        List<PresencasEventos> ListarMinhas(Guid id);
        void Inscrever(PresencasEventos inscricao);
    }
}
