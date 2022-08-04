using ChallengeDominio._Util;
using ChallengeDominio.Excecoes;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using System;


namespace ChallengeRepositorioPostgresql._Util
{
    public class TradutorExcecao : ITradutorExcecao
    {
        public Exception Converte(Exception ex)
        {

            switch (ex)
            {
                case DbUpdateConcurrencyException e:
                    return new DesatualizadoException(e.Message, e.InnerException);
                case DbUpdateException e:
                    if (e.InnerException != null && e.InnerException.GetType().Equals(typeof(PostgresException)))
                    {
                        //O nome do constraint sobe no lugar mensagem da exception para que a camada superior possa entender o que foi que falhou. 
                        //O nome do constraint deve ser padronizado, independentemente do banco de dados utilizado.
                        var pgException = (PostgresException)e.InnerException;
                        return pgException.SqlState switch
                        {
                            "23505" => new DuplicadoException(pgException.ConstraintName, pgException),
                            "23514" => new ValidacaoException(pgException.ConstraintName, pgException),
                            "23503" => pgException.MessageText.StartsWith("update or delete") ?
                                new PossuiReferenciaException(pgException.ConstraintName, pgException) :
                                new ReferenciaIncorretaException(pgException.ConstraintName, pgException),
                            _ => ex
                        };
                    }
                    return ex;
                default:
                    return ex;
            }
        }
    }
}
