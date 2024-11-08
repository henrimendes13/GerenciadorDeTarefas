using Microsoft.DotNet.Scaffolding.Shared.Messaging;

namespace GerenciadorDeTarefasApi.Services.Exceptions;

public class TituloJaExistenteException : Exception
{
    public TituloJaExistenteException(string message) : base(message) { }
}
