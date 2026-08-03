using MongoDB.Driver;

const string OllamaUrl = "http://127.0.0.1:11434";
const string EmbeddingModel = "nomic-embed-text";
const string MongoUrl = "mongodb://127.0.0.1:27017/?directConnection=true&serverSelectionTimeoutMS=2000";

var movies = new Movie[]
{
    // Sci-Fi
    new() { Title = "Interstellar", Genre = "Sci-Fi",
        Description = "A team of astronauts travels through a wormhole near Saturn searching for a new home for humanity as Earth faces extinction. Themes of love, time dilation, and sacrifice drive this cosmic odyssey." },
    new() { Title = "Dune", Genre = "Sci-Fi",
        Description = "A young nobleman is thrust into conflict over the desert planet Arrakis, the sole source of the universe's most valuable substance. Epic political intrigue meets ecological allegory." },
    new() { Title = "2001: A Space Odyssey", Genre = "Sci-Fi",
        Description = "Astronauts battle a malfunctioning AI aboard a mission to Jupiter guided by a mysterious monolith. A visionary meditation on evolution, technology, and consciousness." },
    new() { Title = "The Martian", Genre = "Sci-Fi",
        Description = "An astronaut stranded alone on Mars uses ingenuity and science to survive until rescue is possible. A celebration of problem-solving, resilience, and the human will to endure." },
    new() { Title = "Arrival", Genre = "Sci-Fi",
        Description = "A linguist is recruited to communicate with alien spacecraft, uncovering a mind-bending truth about language and time. An intimate and emotional first-contact story." },
    new() { Title = "Moon", Genre = "Sci-Fi",
        Description = "A lone astronaut nearing the end of a three-year lunar mining contract makes a shocking discovery about his own identity. A quiet, philosophical exploration of solitude and cloning." },
    // Thriller
    new() { Title = "The Silence of the Lambs", Genre = "Thriller",
        Description = "A young FBI trainee enlists imprisoned cannibal Hannibal Lecter to help catch a serial killer known as Buffalo Bill. Psychological terror built through chilling dialogue and dread." },
    new() { Title = "Gone Girl", Genre = "Thriller",
        Description = "When a man's wife mysteriously disappears on their anniversary, a media circus and police scrutiny reveal deeply disturbing secrets in their marriage. A sharp dissection of perception and deception." },
    new() { Title = "Parasite", Genre = "Thriller",
        Description = "A destitute Korean family schemes their way into employment with a wealthy household, unleashing a dark chain of events exposing stark class divisions. A darkly comedic social thriller." },
    new() { Title = "Memento", Genre = "Thriller",
        Description = "A man with short-term memory loss pieces together the events surrounding his wife's murder using notes and tattoos on his body. A labyrinthine puzzle about identity and unreliable memory." },
    new() { Title = "Se7en", Genre = "Thriller",
        Description = "Two detectives hunt a meticulous serial killer whose gruesome murders are inspired by the seven deadly sins. A relentlessly dark descent into the worst of human nature." },
    // Drama
    new() { Title = "Whiplash", Genre = "Drama",
        Description = "An obsessive jazz drumming student is pushed to the breaking point by his ruthless music conservatory instructor. An electrifying battle of wills about the true cost of greatness." },
    new() { Title = "La La Land", Genre = "Drama",
        Description = "An aspiring actress and a jazz musician fall in love in Los Angeles while chasing their artistic dreams. A bittersweet musical about ambition, passion, and roads not taken." },
    new() { Title = "The Shawshank Redemption", Genre = "Drama",
        Description = "A banker wrongfully convicted of murder befriends a fellow prisoner and slowly shapes his own redemption over decades in a brutal penitentiary. An enduring testament to hope and friendship." },
    new() { Title = "Good Will Hunting", Genre = "Drama",
        Description = "A janitor at MIT with genius-level intellect struggles with his troubled past until a compassionate therapist helps him find direction. A moving exploration of potential and emotional healing." },
    // Action / Adventure
    new() { Title = "Mad Max: Fury Road", Genre = "Action",
        Description = "In a post-apocalyptic wasteland, a drifter joins a warrior fleeing a tyrannical warlord in a relentless high-speed chase across a burning desert. Kinetic, visually stunning survival action." },
    new() { Title = "The Dark Knight", Genre = "Action",
        Description = "Batman faces the Joker, a chaotic criminal mastermind who pushes Gotham City to the edge of anarchy with unrelenting psychological warfare. A morally complex superhero epic." },
    new() { Title = "Inception", Genre = "Action",
        Description = "A thief who steals secrets through dream infiltration is given the impossible task of planting an idea in a target's subconscious. A dizzying labyrinth of nested realities." },
    // Animation
    new() { Title = "Spirited Away", Genre = "Animation",
        Description = "A young girl becomes trapped in a mysterious spirit world and must work in a bathhouse for supernatural beings to rescue her parents. A richly imaginative coming-of-age fairy tale." },
    new() { Title = "WALL-E", Genre = "Animation",
        Description = "A small waste-collecting robot left alone on a ruined Earth falls in love and embarks on a galaxy-spanning adventure. A tender fable about loneliness, consumerism, and the resilience of life." },
    // Fantasy — the Harry Potter series
    new() { Title = "Harry Potter and the Philosopher's Stone", Genre = "Fantasy",
        Description = "An orphaned boy learns on his eleventh birthday that he is a wizard and is whisked away to Hogwarts School of Witchcraft and Wizardry. There he makes loyal friends, learns to fly a broomstick, and uncovers the truth about the night his parents died." },
    new() { Title = "Harry Potter and the Chamber of Secrets", Genre = "Fantasy",
        Description = "In his second year at Hogwarts, Harry hears sinister whispers within the castle walls as students are mysteriously turned to stone. He must uncover the secret of a hidden chamber and confront the monstrous creature lurking inside it." },
    new() { Title = "Harry Potter and the Prisoner of Azkaban", Genre = "Fantasy",
        Description = "A dangerous escaped prisoner is believed to be hunting Harry, while soul-draining guards patrol the school grounds. Harry learns painful truths about his family and the friends who betrayed his parents." },
    new() { Title = "Harry Potter and the Goblet of Fire", Genre = "Fantasy",
        Description = "Harry is mysteriously chosen to compete in a perilous magical tournament between rival schools. The deadly contest ends with the terrifying rebirth of Lord Voldemort and the dawn of a darker age." },
    new() { Title = "Harry Potter and the Order of the Phoenix", Genre = "Fantasy",
        Description = "As the authorities deny that Voldemort has returned and seize control of Hogwarts, Harry secretly trains a band of students to defend themselves, forming a rebellion against a cruel new regime." },
    new() { Title = "Harry Potter and the Half-Blood Prince", Genre = "Fantasy",
        Description = "Harry explores hidden memories to discover the secret of Voldemort's immortality, while first love blossoms among his friends and a trusted mentor is lost to treachery." },
    new() { Title = "Harry Potter and the Deathly Hallows: Part 1", Genre = "Fantasy",
        Description = "With the school no longer safe, Harry, Ron and Hermione abandon their studies and go on the run to seek out and destroy the hidden fragments of Voldemort's shattered soul." },
    new() { Title = "Harry Potter and the Deathly Hallows: Part 2", Genre = "Fantasy",
        Description = "The final battle for the wizarding world erupts at Hogwarts, where Harry faces Voldemort in a last confrontation that will decide the fate of good and evil." },
};

Console.WriteLine("=== MongoDB Semantic Search Demo ===");
Console.WriteLine($"  Embedding model : {EmbeddingModel}");
Console.WriteLine($"  MongoDB         : {MongoUrl}");
Console.WriteLine($"  Ollama          : {OllamaUrl}");
Console.WriteLine();

var embedder = new EmbeddingService(OllamaUrl, EmbeddingModel);
var mongoClient = new MongoClient(MongoUrl);
var db = mongoClient.GetDatabase("semantic_search");
var repo = new MovieRepository(db);

if (!await repo.IsSeededAsync())
{
    Console.WriteLine($"First run — generating embeddings for {movies.Length} movies...");
    for (int i = 0; i < movies.Length; i++)
    {
        Console.Write($"  [{i + 1:D2}/{movies.Length}] {movies[i].Title}...");
        movies[i].Embedding = await embedder.GenerateAsync($"{movies[i].Title}. {movies[i].Description}");
        Console.WriteLine(" done");
    }
    await repo.SeedAsync(movies);
}
else
{
    Console.WriteLine($"Database already seeded with {movies.Length} movies.");
}

await repo.EnsureVectorIndexAsync(numDimensions: 768);

Console.WriteLine();
Console.WriteLine("Type a query to find semantically similar movies, or 'exit' to quit.");
Console.WriteLine();

while (true)
{
    Console.Write("Query: ");
    var query = Console.ReadLine()?.Trim();

    if (string.IsNullOrEmpty(query) || query.Equals("exit", StringComparison.OrdinalIgnoreCase))
        break;

    var queryEmbedding = await embedder.GenerateAsync(query);
    var results = await repo.SearchAsync(queryEmbedding);

    Console.WriteLine();
    for (int i = 0; i < results.Count; i++)
    {
        var r = results[i];
        Console.WriteLine($"  {i + 1}. [{r.Score:F4}] {r.Title}  ({r.Genre})");
        Console.WriteLine($"     {r.Description[..Math.Min(90, r.Description.Length)]}...");
    }
    Console.WriteLine();
}

Console.WriteLine("Bye!");
