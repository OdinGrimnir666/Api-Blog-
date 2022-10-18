public class PostApi : IApi
{
    public void Register(WebApplication app)
    {
        app.MapGet("/post", Get)
            .Produces<List<Post>>(StatusCodes.Status200OK)
            .WithName("GetAllPosts")
            .WithTags("Getters");

        app.MapGet("/post/{id}", GetById)
        .Produces<Post>(StatusCodes.Status200OK)
        .WithName("GetPosts")
        .WithTags("Getters");

        app.MapPost("/post", Post)
            .Accepts<Post>("application/json")
            .Produces<Post>(StatusCodes.Status201Created)
            .WithName("Create")
            .WithTags("Creators");

        app.MapPut("/post", Put)
            .Accepts<Post>("application/json")
            .WithName("UpdatesPost")
            .WithTags("Updaters");

        app.MapDelete("post/{id}", Delete)
            .WithName("DeleteHotel")
            .WithTags("Deleters");
    }

    private IResult Get(IPostRepository repository, IMapPost mappost)
    {
        var answer = mappost.GetPostsDTO(repository.GetPostsAsync().Result);
        return Results.Json(answer);
    }

    private IResult GetById(int id, IPostRepository repository, IMapPost mappost)
    {
        return  mappost.GetPostDTO(repository.GetPostAsync(id).Result) is PostDTO postDTO ?
        Results.Ok(postDTO)
        : Results.NotFound();
    }


    [Authorize]
    private async Task<IResult> Post([FromBody] Post post, IPostRepository repository)
    {
        post.EditDate = post.CreateDate;
        await repository.InsertPostAsync(post);
        await repository.SaveAsync();
        return Results.Created($"/hotels/{post.Id}", post);
    }

    [Authorize]
    private async Task<IResult> Put([FromBody] Post post, IPostRepository repository)
    {
        await repository.UpdatePostAsync(post);
        await repository.SaveAsync();
        return Results.NoContent();
    }

    private async Task<IResult> Delete(int id, IPostRepository repository)
    {
        await repository.DeletePostAsync(id);
        await repository.SaveAsync();
        return Results.NoContent();

    }

}