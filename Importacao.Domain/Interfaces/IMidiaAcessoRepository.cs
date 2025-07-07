using Importacao.Domain.Entities.Access;

namespace Importacao.Domain.Interfaces;

public interface IMidiaAcessoRepository {
	Task<MidiaAcesso> CreateMidiaAcesso(long empresaId);
}