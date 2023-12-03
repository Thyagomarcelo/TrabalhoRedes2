using System;

public class ApiLimiteAguaRepository
{
    const connectionString = "Data Source=localhost;Initial Catalog=SOPHIA_TrabalhoRedes2;MultipleActiveResultSets=True;User Id=sophia;password=samurai;";


    public ApiLimiteAguaRepository()
	{
	}

    public void IncluirRegistro(DateTime dataColeta, string local, float altura)
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

                    command.Parameters["@DATAHORACOLETA"].Value = dataColeta;
                    command.Parameters["@LOCAL"].Value = local;
                    command.Parameters["@ALTURA"].Value = altura;

                    command.ExecuteNonQuery();
                    conexao.Close();
                }
            }
    }
    

    public void getDadosBase()
    {

        using (var conexao = new SqlConnection(connectionString))
        {

            var sophiaConnectionStringBuilder = new SqlConnectionStringBuilder(connectionString);

            conexao.Open();

            using (var command = new SqlCommand(string.Format(
                $@"
                            SELECT DATAHORACOLETA,
                                   LOCAL,
                                   ALTURA
                              FROM INFOLIMITEAGUA", sophiaConnectionStringBuilder.InitialCatalog), conexao))
            {


                command.ExecuteNonQuery();
                conexao.Close();
            }
        }
    }
    }
}
