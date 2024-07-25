using AeC.DeveloperChallenge.Clima.Repositorios.APIsExternas;
using AeC.DeveloperChallenge.Clima.Repositorios.APIsExternas.Interfaces;
using AeC.DeveloperChallenge.Clima.Repositorios.BancoDados;
using AeC.DeveloperChallenge.Clima.Repositorios.BancoDados.Interfaces;
using AeC.DeveloperChallenge.Clima.Repositorios.ExecutorBancoDados.Interfaces;
using AeC.DeveloperChallenge.Clima.Repositorios.ExecutorBancoDados;
using AeC.DeveloperChallenge.Clima.Servicos.APIsExternas;
using AeC.DeveloperChallenge.Clima.Servicos.APIsExternas.Interfaces;
using System.Data.SqlClient;
using System.Data;

namespace AeC.DeveloperChallenge.Clima.API
{
    /// <summary>
    /// Contém a configiração de injeção de dependências
    /// </summary>
    public static class InjecaoDependencia
    {
        #region Metodos

        /// <summary>
        /// Configura as injeções de depêndencia
        /// </summary>
        /// <param name="servicos">Coleção de serviços</param>
        /// <returns>Coleção de serviços atualizada</returns>
        public static IServiceCollection Configura(this IServiceCollection servicos, IConfiguration configuracao)
        {
            var urlApi = configuracao["UrlBrasiApi"];
            var connectionString = configuracao.GetConnectionString("SqlServerPadrao");

            // Banco de dados
            servicos.AddTransient<IDbConnection>(provedor => new SqlConnection(connectionString));
            servicos.AddTransient<IExecutorBancoDados, ExecutorBancoDados>();

            // Repositórios
            servicos.AddScoped<IClimaRepositorio, ClimaRepositorio>();
            servicos.AddScoped<ICidadeRepositorio, CidadeRepositorio>();
            servicos.AddScoped<IRequisicaoRepositorio>(provedor =>
                new RequisicaoRepositorio(provedor.GetRequiredService<IExecutorBancoDados>()));
            servicos.AddScoped<ILogRequisicaoRepositorio>(provedor =>
                new LogRequisicaoRepositorio(provedor.GetRequiredService<IExecutorBancoDados>()));


            servicos.AddHttpClient<ICidadeRepositorio, CidadeRepositorio>(httpClient =>
            {
                httpClient.BaseAddress = new Uri(urlApi);
            });

            servicos.AddHttpClient<IClimaRepositorio, ClimaRepositorio>(httpClient =>
            {
                httpClient.BaseAddress = new Uri(urlApi);
            });

            // Serviços
            servicos.AddScoped<IClimaServico, ClimaServico>();
            servicos.AddScoped<ICidadeServico, CidadeServico>();
            servicos.AddScoped<IRequisicaoServico, RequisicaoServico>();
            servicos.AddScoped<ILogRequisicaoServico, LogRequisicaoServico>();

            return servicos;
        }

        #endregion
    }
}