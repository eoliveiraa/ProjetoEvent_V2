using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace webapi.event_.Domains
{
    [Table("PresencasEventos")]
    public class PresencasEventos
    {
        [Key]
        public Guid IdPresencaEvento { get; set; }

        [Column(TypeName = "BIT")]
        [Required(ErrorMessage = "Situação obrigatório!")]
        public bool Situacao { get; set; }

        //ref.tabela Usuario
        [Required(ErrorMessage = "Usuário obrigatório!")]
        public Guid IdUsuario { get; set; }

        [ForeignKey("IdUsuario")]
        public Usuarios? Usuario { get; set; }


        //ref.tabela Evento
        [Required(ErrorMessage = "Evento obrigatório!")]
        public Guid IdEvento { get; set; }

        [ForeignKey("IdEvento")]
        public Eventos? Evento { get; set; }
    }
}
