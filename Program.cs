var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

var FilmList = new List<Film>
{
    new Film() { Judul = "Inception", Sutradara = "Christopher Nolan", Tahun = "2010", Genre = "Sci-Fi", Rating = "9.0" },
    new Film() { Judul = "Interstellar", Sutradara = "Christopher Nolan", Tahun = "2014", Genre = "Sci-Fi", Rating = "8.7" },
    new Film() { Judul = "Parasite", Sutradara = "Bong Joon Ho", Tahun = "2019", Genre = "Thriller", Rating = "8.6" },
    new Film() { Judul = "12 Angry Men", Sutradara = "Sidney Lumet", Tahun = "1957", Genre = "Crime, Drama", Rating = "9.0" },
};

app.MapGet("/films", () =>
{
    return FilmList;
});

app.MapPost("/films", (Film film) =>
{
    FilmList.Add(film);
    return Results.Created($"/films/{film.Judul}", film);
});

app.MapGet("/films/{id}", (string id) =>
{
    return FilmList[int.Parse(id)];
});

app.MapDelete("/films/{id}", (string id) =>
{
    var film = FilmList[int.Parse(id)];
    if (film != null)
    {
        FilmList.Remove(film);
        return Results.Ok();
    }
    return Results.NotFound();
});

app.Run();
