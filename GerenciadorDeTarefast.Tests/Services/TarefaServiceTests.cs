using AutoMapper;
using GerenciadorDeTarefasApi.DTOs;
using GerenciadorDeTarefasApi.Models;
using GerenciadorDeTarefasApi.Repositories;
using GerenciadorDeTarefasApi.Services;
using GerenciadorDeTarefasApi.Services.Exceptions;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace GerenciadorDeTarefas.Tests.Services;

public class TarefaServiceTests
{
    [Fact]
    public async Task CreateTarefaAsync_TituloExistente_DeveLancarTituloJaExistenteException()
    {
        // Arrange
        var tituloExistente = "Título Existente";
        var descricao = "Descriçao Teste";
        var finalizada = false;
        var dataParaEntrega = "2024-11-09";

        var novaTarefa = new TarefaCreateDTO(tituloExistente, descricao, finalizada, dataParaEntrega);

        var tarefaRepositoryMock = new Mock<ITarefaRepository>();
        var mapperMock = new Mock<IMapper>();
        
        tarefaRepositoryMock.Setup(repo => repo.NomeTarefaExistente(tituloExistente)).ReturnsAsync(true);

        var tarefaService = new TarefaService(tarefaRepositoryMock.Object, mapperMock.Object);

        //Act
        var exception = await Assert.ThrowsAsync<TituloJaExistenteException>(() => tarefaService.CreateTarefaAsync(novaTarefa));

        //Assert
        Assert.Equal("O Nome da tarefa já existe em outra tarefa.", exception.Message);
    }
}
