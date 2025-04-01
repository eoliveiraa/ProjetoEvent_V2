using Microsoft.EntityFrameworkCore;
using webapi.event_.Contexts;
using webapi.event_.Domains;
using webapi.event_.Interfaces;

namespace webapi.event_.Repositories
{
    public class EventosRepository : IEventosRepository
    {
        private readonly Context _context;

        public EventosRepository(Context context)
        {
            _context = context;
        }
        public void Atualizar(Guid id, Eventos evento)
        {
            try
            {
                Eventos eventoBuscado = _context.Eventos.Find(id)!;

                if (eventoBuscado != null)
                {
                    eventoBuscado.DataEvento = evento.DataEvento;
                    eventoBuscado.NomeEvento = evento.NomeEvento;
                    eventoBuscado.Descricao = evento.Descricao;
                    eventoBuscado.IdTipoEvento = evento.IdTipoEvento;
                }

                _context.Eventos.Update(eventoBuscado!);

                _context.SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Eventos BuscarPorId(Guid id)
        {
            try
            {
                return _context.Eventos
                    .Select(e => new Eventos
                    {
                        IdEvento = e.IdEvento,
                        NomeEvento = e.NomeEvento,
                        Descricao = e.Descricao,
                        DataEvento = e.DataEvento,
                        TiposEvento = new TiposEventos
                        {
                            TituloTipoEvento = e.TiposEvento!.TituloTipoEvento
                        },
                        Instituicao = new Instituicoes
                        {
                            NomeFantasia = e.Instituicao!.NomeFantasia
                        }
                    }).FirstOrDefault(e => e.IdEvento == id)!;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void Cadastrar(Eventos evento)
        {
            try
            {
                // Verifica se a data do evento é maior que a data atual
                if (evento.DataEvento < DateTime.Now)
                {
                    throw new ArgumentException("A data do evento deve ser maior ou igual a data atual.");
                }

                evento.IdEvento = Guid.NewGuid();

                _context.Eventos.Add(evento);

                _context.SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void Deletar(Guid id)
        {
            try
            {
                Eventos eventoBuscado = _context.Eventos.Find(id)!;

                if (eventoBuscado != null)
                {
                    _context.Eventos.Remove(eventoBuscado);
                }

                _context.SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<Eventos> Listar()
        {
            try
            {
                return _context.Eventos
                    .Select(e => new Eventos
                    {
                        IdEvento = e.IdEvento,
                        NomeEvento = e.NomeEvento,
                        Descricao = e.Descricao,
                        DataEvento = e.DataEvento,
                        IdTipoEvento = e.IdTipoEvento,
                        TiposEvento = new TiposEventos
                        {
                            IdTipoEvento = e.IdTipoEvento,
                            TituloTipoEvento = e.TiposEvento!.TituloTipoEvento
                        },
                        IdInstituicao = e.IdInstituicao,
                        Instituicao = new Instituicoes
                        {
                            IdInstituicao = e.IdInstituicao,
                            NomeFantasia = e.Instituicao!.NomeFantasia
                        }
                    }).ToList();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<Eventos> ListarPorId(Guid id)
        {
            try
            {
                return _context.Eventos
                    .Include(e => e.PresencasEventos)
                    .Select(e => new Eventos
                    {
                        IdEvento = e.IdEvento,
                        NomeEvento = e.NomeEvento,
                        Descricao = e.Descricao,
                        DataEvento = e.DataEvento,
                        IdTipoEvento = e.IdTipoEvento,
                        TiposEvento = new TiposEventos
                        {
                            IdTipoEvento = e.IdTipoEvento,
                            TituloTipoEvento = e.TiposEvento!.TituloTipoEvento
                        },
                        IdInstituicao = e.IdInstituicao,
                        Instituicao = new Instituicoes
                        {
                            IdInstituicao = e.IdInstituicao,
                            NomeFantasia = e.Instituicao!.NomeFantasia
                        },
                        PresencasEventos = new PresencasEventos
                        {
                            IdUsuario = e.PresencasEventos!.IdUsuario,
                            Situacao = e.PresencasEventos!.Situacao
                        }
                    })
                    .Where(e => e.PresencasEventos!.Situacao == true && e.PresencasEventos.IdUsuario == id)
                    .ToList();
            }
            catch (Exception)
            {
                throw;
            }
        }


        public List<Eventos> ProximosEventos()
        {
            try
            {
                return _context.Eventos
                    .Select(e => new Eventos
                    {
                        IdEvento = e.IdEvento,
                        NomeEvento = e.NomeEvento,
                        Descricao = e.Descricao,
                        DataEvento = e.DataEvento,
                        IdTipoEvento = e.IdTipoEvento,
                        TiposEvento = new TiposEventos
                        {
                            IdTipoEvento = e.IdTipoEvento,
                            TituloTipoEvento = e.TiposEvento!.TituloTipoEvento
                        },
                        IdInstituicao = e.IdInstituicao,
                        Instituicao = new Instituicoes
                        {
                            IdInstituicao = e.IdInstituicao,
                            NomeFantasia = e.Instituicao!.NomeFantasia
                        }

                    })
                    .Where(e => e.DataEvento >= DateTime.Now)
                    .ToList();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}