using webapi.event_.Domains;

namespace webapi.event_.Interfaces
{
    public interface IEventosRepository
    {
        void Cadastrar(Eventos evento);
        void Deletar(Guid id);
        List<Eventos> Listar();
        List<Eventos> ListarPorId(Guid id);
        List<Eventos> ProximosEventos();
        Eventos BuscarPorId(Guid id);
        void Atualizar(Guid id, Eventos evento);
    }
}