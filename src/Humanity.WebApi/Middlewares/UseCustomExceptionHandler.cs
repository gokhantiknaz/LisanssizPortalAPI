using Humanity.Application.Models.DTOs;
using Humanity.Domain.Exceptions;
using Humanity.WebApi.Controllers;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.EntityFrameworkCore;
using System;
using System.Text.Json;

namespace Humanity.WebApi.Middlewares
{
    public static class UseCustomExceptionHandler
    {
        public static void UseCustomException(this IApplicationBuilder app)
        {
            app.UseExceptionHandler(config =>
            {

                var _logger = config.ApplicationServices.GetService<ILogger<CustomBaseController>>();
                config.Run(async context =>
                {
                    context.Response.ContentType = "application/json";

                    var exceptionFeature = context.Features.Get<IExceptionHandlerFeature>();

                    var statusCode = exceptionFeature.Error switch
                    {
                        ClientSideException => 400,
                        NotFoundException => 404,
                        DbUpdateConcurrencyException => 501,
                        _ => 500
                    };

                    // 500 durumları için hatayı loglayıp eğer 500 ise kendi ortak hata mesajımızı dönmemiz lazım. Yapılacak.
                    context.Response.StatusCode = statusCode;

                    //Status kodlar ve hata mesajları düzenlenecek!!!
                    string errorMessage = exceptionFeature.Error.Message;
                    if (statusCode == 501)
                    {
                        errorMessage = "İşlem yapılmak istenilen kayıt üzerinde başka bir işlem yapılmıştır. Lütfen Tekrar Deneyiniz!";
                    }

                    var response = CustomResponseDto<NoContentDto>.Fail(statusCode, errorMessage,"Burada Ek Bilgilendirmeler Ekleyebiliriz!!!");


                    //Düzenlemeler yapılacak
                    _logger.LogTrace(exceptionFeature.Error, "Hata{a}", "aa");

                    await context.Response.WriteAsync(JsonSerializer.Serialize(response));


                });

            }
            );
        }
    }
}
