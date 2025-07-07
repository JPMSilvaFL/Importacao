using Importacao.Domain.Entities.Access;

namespace Importacao.Application.Interfaces;

public interface IMidiaAcessoService {
	Task<MidiaAcesso> HandleCreateMidiaAcesso(long empresa);
}