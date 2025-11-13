using System;
using System.Linq;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Repo.RepoEventos;

var builder = WebApplication.CreateBuilder(args);

// Registrar repositorios en DI
builder.Services.AddSingleton<RepoPersonas>();
builder.Services.AddSingleton<RepoEventos>();
builder.Services.AddSingleton<RepoReservas>();

// Registrar reportes (transient)
builder.Services.AddTransient<ReporteEventosMasAsistidos>();

var app = builder.Build();

app.MapGet("/", () => Results.Ok("FoodieEvents API - Proyecto listo"));

app.MapPost("/chef", (RepoPersonas repo, ChefDto dto) =>
{
    try
    {
        var chef = new Chef(dto.Nombre, dto.Email, dto.Telefono, dto.Especialidad, dto.Nacionalidad, dto.AnosExperiencia);
        repo.Agregar(chef);
        return Results.Created($"/chef/{chef.Id}", chef);
    }
    catch (ErrorValidacion e) { return Results.BadRequest(new { error = e.Message }); }
});

app.MapPost("/participante", (RepoPersonas repo, ParticipanteDto dto) =>
{
    try
    {
        var p = new Participante(dto.Nombre, dto.Email, dto.Telefono, dto.Documento, dto.Restricciones);
        repo.Agregar(p);
        return Results.Created($"/participante/{p.Id}", p);
    }
    catch (ErrorValidacion e) { return Results.BadRequest(new { error = e.Message }); }
});

app.MapPost("/invitado", (RepoPersonas repo, InvitadoDto dto) =>
{
    try
    {
        var i = new InvitadoEspecial(dto.Nombre, dto.Email, dto.Telefono, dto.TipoInvitado, dto.AccesoGratuito);
        repo.Agregar(i);
        return Results.Created($"/invitado/{i.Id}", i);
    }
    catch (ErrorValidacion e) { return Results.BadRequest(new { error = e.Message }); }
});

app.MapPost("/evento/presencial", (RepoEventos repoEventos, RepoPersonas repoPersonas, EventoPresencialDto dto) =>
{
    try
    {
        var organizador = repoPersonas.BuscarPorEmail(dto.OrganizadorEmail) as Chef;
        if (organizador == null) return Results.BadRequest(new { error = "Chef organizador no encontrado." });

        var evento = new EventoPresencial(dto.Nombre, dto.Descripcion, dto.Tipo, dto.FechaInicio, dto.FechaFin, dto.Capacidad, dto.Precio, organizador, dto.Ubicacion);
        repoEventos.Agregar(evento);
        return Results.Created($"/evento/{evento.Id}", evento);
    }
    catch (ErrorValidacion e) { return Results.BadRequest(new { error = e.Message }); }
});

app.MapPost("/evento/virtual", (RepoEventos repoEventos, RepoPersonas repoPersonas, EventoVirtualDto dto) =>
{
    try
    {
        var organizador = repoPersonas.BuscarPorEmail(dto.OrganizadorEmail) as Chef;
        if (organizador == null) return Results.BadRequest(new { error = "Chef organizador no encontrado." });

        var evento = new EventoVirtual(dto.Nombre, dto.Descripcion, dto.Tipo, dto.FechaInicio, dto.FechaFin, dto.Capacidad, dto.Precio, organizador, dto.Enlace);
        repoEventos.Agregar(evento);
        return Results.Created($"/evento/{evento.Id}", evento);
    }
    catch (ErrorValidacion e) { return Results.BadRequest(new { error = e.Message }); }
});

app.MapDelete("/evento/{id:guid}", (Guid id, RepoEventos repoEventos, RepoReservas repoReservas) =>
{
    var evento = repoEventos.Obtener(id);
    if (evento == null) return Results.NotFound();

    // eliminar reservas del repositorio
    foreach (var r in evento.Reservas) repoReservas.Eliminar(r.Id);
    repoEventos.Eliminar(id);
    return Results.NoContent();
});

app.MapPost("/reserva", (RepoPersonas repoPersonas, RepoEventos repoEventos, RepoReservas repoReservas, ReservaDto dto) =>
{
    try
    {
        var persona = repoPersonas.BuscarPorEmail(dto.EmailParticipante);
        if (persona == null) return Results.BadRequest(new { error = "Persona no encontrada." });

        var evento = repoEventos.Obtener(dto.EventoId);
        if (evento == null) return Results.BadRequest(new { error = "Evento no encontrado." });

        // Si la persona es invitado especial con acceso gratuito, forzamos confirmación
        bool confirmarPago = dto.ConfirmarPago;
        if (persona is InvitadoEspecial inv && inv.AccesoGratuito)
            confirmarPago = true;

        var reserva = new Reserva(persona, dto.FechaReserva, dto.MetodoPago, confirmarPago);
        evento.AgregarReserva(reserva);
        repoReservas.Agregar(reserva);
        return Results.Created($"/reserva/{reserva.Id}", reserva);
    }
    catch (ErrorValidacion e) { return Results.BadRequest(new { error = e.Message }); }
});

app.MapPost("/reserva/{id:guid}/cancelar", (Guid id, RepoReservas repoReservas) =>
{
    var r = repoReservas.Obtener(id);
    if (r == null) return Results.NotFound();
    r.Cancelar();
    return Results.Ok();
});

// Reporte: eventos con más asistentes confirmados (via DI)
app.MapGet("/reportes/eventos-mas-asistidos", (ReporteEventosMasAsistidos reporte) =>
{
    var data = reporte.Generar().Select(t => new { EventoId = t.EventoId, Nombre = t.NombreEvento, Confirmadas = t.Asistentes });
    return Results.Ok(data);
});

app.Run();

#region DTOs
record ChefDto(string Nombre, string Email, string Telefono, string Especialidad, string Nacionalidad, int AnosExperiencia);
record ParticipanteDto(string Nombre, string Email, string Telefono, string Documento, string Restricciones);
record InvitadoDto(string Nombre, string Email, string Telefono, string TipoInvitado, bool AccesoGratuito);

record EventoPresencialDto(string Nombre, string Descripcion, TipoEvento Tipo, DateTime FechaInicio, DateTime FechaFin, int Capacidad, decimal Precio, string OrganizadorEmail, string Ubicacion);
record EventoVirtualDto(string Nombre, string Descripcion, TipoEvento Tipo, DateTime FechaInicio, DateTime FechaFin, int Capacidad, decimal Precio, string OrganizadorEmail, string Enlace);

record ReservaDto(Guid EventoId, string EmailParticipante, DateTime FechaReserva, MetodoPago MetodoPago, bool ConfirmarPago);
#endregion
