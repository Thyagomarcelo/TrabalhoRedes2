using ApiLimiteAgua.Model;
using System;
using System.Data;
using System.Data.SqlClient;

public class ApiLimiteAguaRepository
{
    private const string connectionString = "Data Source=DESENV20\\SQL2019;Initial Catalog=TrabalhoRedes2;MultipleActiveResultSets=True;Integrated Security=True";

    public ApiLimiteAguaRepository()
	{
        
    }

    public void IncluirRegistro(DadosLimiteAguaModel dados)
    {

            using (var conexao = new SqlConnection(connectionString))
            {

                var sophiaConnectionStringBuilder = new SqlConnectionStringBuilder(connectionString);

                conexao.Open();

                using (var command = new SqlCommand(string.Format(
                    $@"
                        INSERT INTO INFOLIMITEAGUA(
                           DATAHORACOLETA,
                           LOCAL,
                           ALTURA
                        )
                        VALUES (
                           @DATAHORACOLETA,
                           @LOCAL,
                           @ALTURA
                        )", sophiaConnectionStringBuilder.InitialCatalog), conexao))
                {
                    command.Parameters.Add(new SqlParameter("@DATAHORACOLETA", SqlDbType.DateTime));
                    command.Parameters.Add(new SqlParameter("@LOCAL", SqlDbType.VarChar));
                    command.Parameters.Add(new SqlParameter("@ALTURA", SqlDbType.Float));

                    command.Parameters["@DATAHORACOLETA"].Value = dados.dataColeta;
                    command.Parameters["@LOCAL"].Value = dados.local;
                    command.Parameters["@ALTURA"].Value = dados.altura;

                    command.ExecuteNonQuery();
                    conexao.Close();
                }
            }
    }
    

    public List<DadosLimiteAguaModel> getDadosBaseDia()
    {
        List<DadosLimiteAguaModel> infoLimiteAgua = new List<DadosLimiteAguaModel>();

        using (var conexao = new SqlConnection(connectionString))
        {

            var sophiaConnectionStringBuilder = new SqlConnectionStringBuilder(connectionString);

            conexao.Open();

            using (var command = new SqlCommand(string.Format(
                $@"
                    SELECT DATAHORACOLETA,
                           LOCAL,
                           ALTURA
                      FROM INFOLIMITEAGUA
                     WHERE DATEPART(dd, DATAHORACOLETA) = DATEPART(dd, GETDATE())
                     ORDER BY DATAHORACOLETA", sophiaConnectionStringBuilder.InitialCatalog), conexao))
            {
                using (var dbReader = command.ExecuteReader())
                {
                    while (dbReader.Read())
                    {
                        DadosLimiteAguaModel infoLimiteAguaDado = new DadosLimiteAguaModel();

                        infoLimiteAguaDado.dataColeta = (DateTime)dbReader["DATAHORACOLETA"];
                        infoLimiteAguaDado.local = (string)dbReader["LOCAL"];
                        infoLimiteAguaDado.altura = (double)dbReader["ALTURA"];

                        infoLimiteAgua.Add(infoLimiteAguaDado);
                    }
                }

                command.ExecuteNonQuery();
                conexao.Close();
            }
        }

        return infoLimiteAgua;
    }


    public List<DadosLimiteAguaModel> getDadosBaseSemana()
    {
        List<DadosLimiteAguaModel> infoLimiteAgua = new List<DadosLimiteAguaModel>();

        using (var conexao = new SqlConnection(connectionString))
        {

            var sophiaConnectionStringBuilder = new SqlConnectionStringBuilder(connectionString);

            conexao.Open();

            using (var command = new SqlCommand(string.Format(
                $@"
                    SELECT DATAHORACOLETA,
                           LOCAL,
                           ALTURA
                      FROM INFOLIMITEAGUA
                     WHERE DATEPART(wk, DATAHORACOLETA) = DATEPART(wk, GETDATE())
                     ORDER BY DATAHORACOLETA", sophiaConnectionStringBuilder.InitialCatalog), conexao))
            {
                using (var dbReader = command.ExecuteReader())
                {
                    while (dbReader.Read())
                    {
                        DadosLimiteAguaModel infoLimiteAguaDado = new DadosLimiteAguaModel();

                        infoLimiteAguaDado.dataColeta = (DateTime)dbReader["DATAHORACOLETA"];
                        infoLimiteAguaDado.local = (string)dbReader["LOCAL"];
                        infoLimiteAguaDado.altura = (double)dbReader["ALTURA"];

                        infoLimiteAgua.Add(infoLimiteAguaDado);
                    }
                }

                command.ExecuteNonQuery();
                conexao.Close();
            }
        }

        return infoLimiteAgua;
    }
}

