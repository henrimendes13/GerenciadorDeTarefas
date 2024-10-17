namespace GerenciadorDeTarefasApi.DTOs
{
    public class TarefaReadDTO
    {
        public int Id { get; set; }

        public string Titulo { get; set; }

        public string Descricao { get; set; }

        public bool Finalizada { get; set; }

        public DateTime DataCriacao { get; set; }

        public DateTime DataParaEntrega { get; set; }
    }
}
