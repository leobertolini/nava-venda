using Moq;
using Nava.Venda.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Nava.Venda.Application.Tests
{
    public class VendaServiceTests
    {
        private readonly IVendaService vendaService;
        private readonly Mock<IVendaSqlAdapter> sqlAdapterMock;

        public VendaServiceTests()
        {
            sqlAdapterMock = new Mock<IVendaSqlAdapter>();

            vendaService = new VendaService(sqlAdapterMock.Object);
        }

        [Fact]
        [Trait(nameof(IVendaService.ObterPorIdAsync), "Sucesso")]
        public async Task When_ObterPorId_Expected_Sucesso()
        {
            var identificador = Guid.Parse("c32ca4c4-66b3-4884-afb1-be6f473f8189");

            var vendaMock = ObterVendaMock();

            sqlAdapterMock
                .Setup(s => s.ObterPorIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync(vendaMock);

            var vendaAtual = await vendaService.ObterPorIdAsync(identificador);

            sqlAdapterMock
                .Verify(s => s.ObterPorIdAsync(It.IsAny<Guid>()), Times.Once);

            Assert.Equal(identificador, vendaAtual.VendaId);
            Assert.Equal("Leonardo Bertolini", vendaAtual.Vendedor.Nome);
            Assert.Single(vendaAtual.Itens);
        }

        [Fact]
        [Trait(nameof(IVendaService.ObterPorIdAsync), "Erro")]
        public async Task When_ObterPorId_ComIdVendaInexistente_Expected_VendaNaoEncontradaException()
        {
            var identificador = Guid.Parse("c32ca4c4-66b3-4884-afb1-be6f473f8189");

            sqlAdapterMock
                .Setup(s => s.ObterPorIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync((Domain.Venda)null);

            var exception = await Assert.ThrowsAsync<VendaNaoEncontradaException>(async () =>
                await vendaService.ObterPorIdAsync(identificador));

            sqlAdapterMock
                .Verify(s => s.ObterPorIdAsync(It.IsAny<Guid>()), Times.Once);

            Assert.Equal($"Venda não encontrada. Id: {identificador}", exception.Message);
        }

        [Fact]
        [Trait(nameof(IVendaService.AtualizarStatusAsync), "Sucesso")]
        public async Task When_AtualizarStatus_Expected_Sucesso()
        {
            var identificador = Guid.Parse("c32ca4c4-66b3-4884-afb1-be6f473f8189");

            var vendaMock = ObterVendaMock();

            sqlAdapterMock
                .Setup(s => s.ObterPorIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync(vendaMock);

            sqlAdapterMock
                .Setup(s => s.AtualizarStatusAsync(vendaMock))
                .ReturnsAsync(true);

            var sucessoAtualizacao = await vendaService
                .AtualizarStatusAsync(identificador, StatusVenda.EnviadoTransportadora);

            sqlAdapterMock
                .Verify(s => s.ObterPorIdAsync(It.IsAny<Guid>()), Times.Once);

            sqlAdapterMock
                .Verify(s => s.AtualizarStatusAsync(It.IsAny<Domain.Venda>()), Times.Once);

            Assert.True(sucessoAtualizacao);
        }

        [Fact]
        [Trait(nameof(IVendaService.AtualizarStatusAsync), "Erro")]
        public async Task When_AtualizarStatus_ComTransicaoStatusInvalida_Expected_TransicaoStatusVendaInvalidaException()
        {
            var identificador = Guid.Parse("c32ca4c4-66b3-4884-afb1-be6f473f8189");

            var vendaMock = ObterVendaMock();

            sqlAdapterMock
                .Setup(s => s.ObterPorIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync(vendaMock);

            var exception = await Assert.ThrowsAsync<TransicaoStatusVendaInvalidaException>(async () =>
                await vendaService.AtualizarStatusAsync(identificador, StatusVenda.AguardandoPagamento));

            sqlAdapterMock
                .Verify(s => s.ObterPorIdAsync(It.IsAny<Guid>()), Times.Once);

            Assert.Equal($"Transição de status inválida de {vendaMock.Status} para {StatusVenda.AguardandoPagamento}.", 
                exception.Message);
        }

        [Fact]
        [Trait(nameof(IVendaService.RegistrarAsync), "Sucesso")]
        public async Task When_Registrar_Expected_Sucesso()
        {
            var vendaMock = ObterVendaMock();
            
            sqlAdapterMock
                .Setup(s => s.RegistrarAsync(It.IsAny<Domain.Venda>()))
                .ReturnsAsync(true);

            var sucessoOperacao = await vendaService.RegistrarAsync(vendaMock);

            sqlAdapterMock
                .Verify(s => s.RegistrarAsync(It.IsAny<Domain.Venda>()), Times.Once);
            
            Assert.True(sucessoOperacao);
        }

        [Fact]
        [Trait(nameof(IVendaService.RegistrarAsync), "Erro")]
        public async Task When_Registrar_ComItemNulo_Expected_VendaNaoPossuiItemException()
        {
            var vendaSemItemMock = new Domain.Venda()
            {
                VendaId = Guid.Parse("c32ca4c4-66b3-4884-afb1-be6f473f8189"),
                Data = new DateTime(2020, 11, 2, 14, 0, 0),
                Status = StatusVenda.PagamentoAprovado,
                Vendedor = new Funcionario()
                {
                    FuncionarioId = Guid.Parse("f6efb92b-2bf0-4136-919d-1557f5c8d0ea"),
                    Nome = "Leonardo Bertolini",
                    Cpf = "07820255552",
                    Email = "leonardo@gmail.com",
                    Cargo = Cargo.Vendedor
                }
            };

            var exception = await Assert.ThrowsAsync<VendaNaoPossuiItemException>(async () =>
                await vendaService.RegistrarAsync(vendaSemItemMock));

            sqlAdapterMock
                .Verify(s => s.RegistrarAsync(It.IsAny<Domain.Venda>()), Times.Never);

            Assert.Equal($"Venda não possui nenhum item relacionado. VendaId: {vendaSemItemMock.VendaId}",
                exception.Message);
        }

        [Fact]
        [Trait(nameof(IVendaService.RegistrarAsync), "Erro")]
        public async Task When_Registrar_ComItemVazio_Expected_VendaNaoPossuiItemException()
        {
            var vendaSemItemMock = new Domain.Venda()
            {
                VendaId = Guid.Parse("c32ca4c4-66b3-4884-afb1-be6f473f8189"),
                Data = new DateTime(2020, 11, 2, 14, 0, 0),
                Status = StatusVenda.PagamentoAprovado,
                Vendedor = new Funcionario()
                {
                    FuncionarioId = Guid.Parse("f6efb92b-2bf0-4136-919d-1557f5c8d0ea"),
                    Nome = "Leonardo Bertolini",
                    Cpf = "07820255552",
                    Email = "leonardo@gmail.com",
                    Cargo = Cargo.Vendedor
                },
                Itens = new List<Item>()
            };

            var exception = await Assert.ThrowsAsync<VendaNaoPossuiItemException>(async () =>
                await vendaService.RegistrarAsync(vendaSemItemMock));

            sqlAdapterMock
                .Verify(s => s.RegistrarAsync(It.IsAny<Domain.Venda>()), Times.Never);

            Assert.Equal($"Venda não possui nenhum item relacionado. VendaId: {vendaSemItemMock.VendaId}",
                exception.Message);
        }

        private static Domain.Venda ObterVendaMock()
        {
            return new Domain.Venda()
            {
                VendaId = Guid.Parse("c32ca4c4-66b3-4884-afb1-be6f473f8189"),
                Data = new DateTime(2020, 11, 2, 14, 0, 0),
                Status = StatusVenda.PagamentoAprovado,
                Vendedor = new Funcionario()
                {
                    FuncionarioId = Guid.Parse("f6efb92b-2bf0-4136-919d-1557f5c8d0ea"),
                    Nome = "Leonardo Bertolini",
                    Cpf = "07820255552",
                    Email = "leonardo@gmail.com",
                    Cargo = Cargo.Vendedor
                },
                Itens = new List<Item>()
                {
                    new Item()
                    {
                        ItemId = Guid.Parse("800144b7-ae41-48ec-9c7c-63e252deff90"),
                        Nome = "Produto 1",
                        Valor = 99.90M
                    }
                }
            };
        }
    }
}
