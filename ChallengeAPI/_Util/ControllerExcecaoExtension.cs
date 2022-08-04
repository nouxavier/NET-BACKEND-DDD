using ChallengeDominio.Excecoes;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;


namespace ChallengeAPI
{
    public static class ControllerExcecaoExtension
    {

        public static ActionResult CriaResultExcecao(this ControllerBase controller, Exception ex)
        {
            return ex switch
            {
                ArgumentOutOfRangeException e => controller.BadRequest(e.Message),
                ValidacaoException e => controller.BadRequest(e.Message),

                NaoEncontradoException e => controller.NotFound(e.Message),

                DesatualizadoException e => controller.Conflict(e.Message),
                DuplicadoException e => controller.Conflict(e.Message),
                ReferenciaIncorretaException e => controller.Conflict(e.Message),

                _ => controller.StatusCode(StatusCodes.Status500InternalServerError, ex.Message),
            };
        }

    }
}
