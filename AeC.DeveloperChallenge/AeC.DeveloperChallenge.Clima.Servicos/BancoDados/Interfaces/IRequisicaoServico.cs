﻿using AeC.DeveloperChallenge.Clima.Modelos.Entidades;

namespace AeC.DeveloperChallenge.Clima.Repositorios.BancoDados.Interfaces
{
    /// <summary>
    /// Contém os contratos referentes ao serviço de requisições
    /// </summary>
    public interface IRequisicaoServico
    {
        #region Metodos

        /// <summary>
        /// Adiciona uma nova requisição
        /// </summary>
        /// <param name="requisicao">Informações da requisição</param>
        /// <returns>Código da requisição criada</returns>
        Task<long> AdicionaRequisicaoAsync(Requisicao requisicao);

        #endregion
    }
}