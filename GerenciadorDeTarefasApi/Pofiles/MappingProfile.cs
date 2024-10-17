using AutoMapper;
using GerenciadorDeTarefasApi.DTOs;
using GerenciadorDeTarefasApi.Models;

namespace GerenciadorDeTarefasApi.Pofiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Tarefa, TarefaReadDTO>();
            CreateMap<TarefaCreateDTO, Tarefa>();
        }
    }
}
