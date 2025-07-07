using Importacao.Application.Interfaces;
using Importacao.Domain.Entities.Access;
using Importacao.Domain.Interfaces;

namespace Importacao.Application.Services;

public class MidiaAcessoService : IMidiaAcessoService {

	private readonly IMidiaAcessoRepository _midiaAcessoRepository;
	public MidiaAcessoService(IMidiaAcessoRepository midiaAcessoRepository) {
		_midiaAcessoRepository = midiaAcessoRepository;
	}
	
	public async Task<MidiaAcesso> HandleCreateMidiaAcesso(long empresa) {
		return await _midiaAcessoRepository.CreateMidiaAcesso(empresa);
	}
	
}