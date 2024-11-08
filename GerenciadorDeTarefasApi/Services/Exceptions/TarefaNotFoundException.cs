namespace GerenciadorDeTarefasApi.Services.Exceptions;

public class TarefaNotFoundException : Exception
{
    public TarefaNotFoundException(string message) : base(message) { }
}
