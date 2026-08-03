---
theme: default
title: Semantic Search & MongoDB
info: |
  ## Semantic Search & MongoDB, Lightning Talk
  Search by meaning, not by keywords. Embeddings, MongoDB Atlas Vector Search
  and automated embeddings with Voyage AI, with a local C# + Ollama demo.
class: text-center
highlighter: shiki
lineNumbers: false
drawings:
  persist: false
transition: slide-left
mdc: true
fonts:
  sans: Inter
  mono: JetBrains Mono
layout: cover
---

<div class="flex items-center justify-between gap-10">
  <div class="text-left">
    <span class="mdb-kicker">🔍 Lightning Talk</span>
    <h1 class="!text-6xl !leading-none mt-5">Semantic Search<br/><span style="color:#00ED64">& MongoDB</span></h1>
    <p class="mt-6 text-xl opacity-80">Search by <strong style="color:#00ED64">meaning</strong>, not by keywords.</p>
  </div>
  <div class="flex flex-col items-center shrink-0">
    <img src="/avatar.png" class="w-40 h-40 rounded-full" alt="Kevin Smith" />
    <div class="mt-3 font-semibold text-lg">Kevin Smith</div>
    <div class="opacity-70 text-sm">Software Engineer</div>
  </div>
</div>

<!--
Welcome everyone, and thanks for coming along. This is a lightning talk on semantic search with MongoDB.

Let me start with the hook. For years, search has meant keyword matching. You type a word, the database looks for that exact word, and if you happen to use a different word than the author did, you get nothing back.

Today I want to show you a different approach, where search understands the meaning behind your words, not just the letters on the screen. This is called semantic search.

It is powered by two ideas we will unpack: embeddings, and vector search. And the good news is that MongoDB and Voyage AI make it genuinely easy to add to an application you are already building.

There is a live demo later on. It is a small C# app that runs an embedding model locally and searches a movie database in MongoDB, all on my laptop, so no cloud account is needed.

A quick word about me. My name is Kevin Smith, I am a software engineer, and I spend a lot of my time working with MongoDB.
-->

---
layout: default
---

# Agenda

- What is semantic search?
- Embeddings & vectors
- MongoDB Vector Search
- Live demo: local model + Atlas
- Automated embeddings with Voyage AI
- Where else you can use it

<!--
Here is the plan for the next fifteen minutes or so, so you know where we are heading.

First we build some intuition. What is semantic search, and why does plain keyword search fall short.

Then the two core concepts. Embeddings, which turn text into numbers, and vectors, which let us measure how similar two meanings are.

Next we look at where MongoDB fits in, with its built in vector search.

Then the fun part, a live demo using a local model and MongoDB running right here on my machine.

After that, how Atlas can create the embeddings for you automatically using Voyage AI, so you write even less code.

And finally, other real world places you can use this same technique.

I am happy to take questions at the end, but if something is not clear as we go, just put your hand up.
-->

---
layout: section
---

# What is semantic search?

<!--
This is our first section. Before we touch any code, I really want the core idea to land.

Let me ask the room a question to get everyone thinking. Put your hand up if you have ever searched for something, you knew the result was there, but you could not find it because you did not type the exact right word.

Keep that moment of frustration in mind, because that is exactly the problem semantic search is designed to solve.
-->

---
layout: statement
---

# Search by meaning,<br/>not by keywords.

Semantic search matches intent and concepts, not just the exact words a user typed.

<!--
If you remember one thing from this whole talk, please make it this sentence. Search by meaning, not by keywords.

Keyword search asks a very literal question. Do these exact words appear in the document, yes or no.

Semantic search asks a smarter question. Is this document actually about the same thing the user is asking for.

Here is a concrete example I will keep coming back to. Imagine you search for a film about a young wizard who must defeat a dark lord. The perfect answers are the Harry Potter films.

But their descriptions talk about Harry, Hogwarts, and Voldemort. They might never use the phrase young wizard defeats a dark lord. So keyword search struggles, while semantic search finds them instantly, because it understands the concept behind the words.
-->

---
layout: default
---

# Keyword search falls short

- Matches literal words, not meaning
- Misses synonyms: <span class="font-mono">"wizard" ≠ "sorcerer"</span>
- No sense of intent or context
- "young wizard fights evil" becomes keyword soup

<!--
Let me be fair to keyword search first. It is fast, it is simple, and it is perfect when you know the exact term you want, like a product code, an order number, or a username.

But it breaks down in a few very common ways, and I want to name three of them.

One. It only matches literal words. If a film description says sorcerer and you search for wizard, you can get nothing back, even though the two words mean the same thing.

Two. It has no real sense of intent. A long, natural question gets chopped into separate words and ranked mostly by how often those words appear, not by what you actually meant.

Three. To paper over this, you end up hand maintaining synonym lists and clever matching rules, and they never quite cover every case your users come up with.

So keyword search is a genuinely useful tool, but at the end of the day it is matching letters, not meaning.
-->

---
layout: default
---

# Semantic search understands meaning

- Ranks results by conceptual similarity
- Handles synonyms & paraphrases naturally
- Resilient to typos, phrasing & even different languages
- Powered by **embeddings + vector search**

<!--
Semantic search flips the model around. Instead of comparing words, we compare meaning.

The way we do that is to convert both the query and every document into a numerical form that captures what they are about. We will see exactly how in the next couple of slides.

Because we are now comparing meaning, synonyms simply work out of the box. Wizard, sorcerer, and mage all end up in a similar place, with no synonym list required.

It also copes well with paraphrases, with small typos, and even with different languages, because all of those still carry the same underlying meaning.

And results come back ranked by how conceptually similar they are, so the strongest match tends to sit right at the top of the list.

The two ingredients that make all of this possible are embeddings and vector search, which are the very next two slides.
-->

---
layout: default
---

# Embeddings 101

- A model turns text into a **vector**: a list of numbers
- Similar meaning → vectors that sit **close together**
- Different meaning → vectors **far apart**
- Demo uses <span class="font-mono">nomic-embed-text</span> (768 dimensions) via Ollama

<!--
Let me define embeddings clearly, because they are the heart of everything we are doing today.

An embedding is simply a list of numbers that represents a piece of text. You feed text into a model, and it hands you back a fixed length list of numbers. That list is called a vector.

In our demo, every piece of text becomes a list of 768 numbers.

The clever part is what those numbers mean. The model has been trained so that texts with similar meaning produce similar numbers, and texts with very different meaning produce very different numbers.

So you can think of an embedding as a kind of fingerprint for the meaning of the text.

In the demo, this runs entirely on my laptop using a tool called Ollama, with a model called nomic embed text. There are no API keys, and nothing leaves the machine, which is great for privacy and very handy on conference wifi.
-->

---
layout: default
---

# Vectors & similarity

- Each item is a point in high-dimensional space
- Similarity = cosine of the angle between vectors
- Closer vectors = more similar meaning
- "Nearest neighbours" become your top results

<!--
So we have turned every document into a vector, which is really just a point in space. The question now is how we actually search through them.

Picture each document as a single dot. If a vector only had two or three numbers, we could plot it on a simple graph. Our vectors have 768 numbers, so it is a 768 dimensional space. We cannot picture that in our heads, but the computer handles it very easily.

To find matches, we measure how close two points are to each other. The most common measure for text is cosine similarity, which looks at the angle between two vectors.

A small angle means the two meanings are pointing in almost the same direction, so they are very similar. A large angle means they are pretty much unrelated.

So a search boils down to this. Embed the query into the same space, then find the nearest points to it. Those nearest neighbours are your best results.

The next slide shows exactly this idea as a picture, which makes it much easier to grasp.
-->

---
layout: default
---

# Searching the vector space

<div class="flex justify-center mt-2">
  <img src="/vector-space.png" class="mdb-diagram w-[86%]" alt="A 2-D projection of a 768-dimensional embedding space, showing thematic clusters and a query with its nearest neighbours." />
</div>

<!--
Here is the whole idea in a single picture, so let me give everyone a moment to take it in.

Every document, in this case every film, has been embedded and placed as a point in this space. Notice how films about the same theme naturally group together into clusters. The green group is the magic and wizardry films, the blue group is space and science fiction, and so on.

Now we take the user query, a young wizard faces a dark lord, and we embed it in exactly the same way. That is the red star. It lands right next to the wizardry cluster, even though it shares no keywords at all with those film descriptions.

To answer the search, we simply grab the nearest points to that star. That is the dashed ring. Here the three closest are the Philosopher's Stone, the Goblet of Fire, and the Deathly Hallows, and we return them ranked by how close they are.

One honest caveat. Real embeddings are 768 dimensional. This is a flattened, two dimensional version so that it fits on a slide. But the underlying idea, that closest points means closest meaning, is exactly what the database is doing for us.
-->

---
layout: section
---

# MongoDB Vector Search

<!--
So far, everything I have said has been generic. It applies to any embedding model and any database. Now let me make it concrete with MongoDB.

The key message for this whole section is that you do not need to bolt on a separate, specialised vector database just to do this.

MongoDB can store your normal data and the embedding vectors together, in the same document, and then search across them together. One database, not two.
-->

---
layout: default
---

# Atlas Vector Search

- Store your documents and their embeddings **together**
- Native <span class="font-mono">$vectorSearch</span> aggregation stage
- Approximate nearest-neighbour (ANN) index
- <span class="font-mono">cosine · dotProduct · euclidean</span> similarity

<!--
MongoDB Atlas Vector Search adds two things to the database you may already know and use.

The first is a new kind of index that understands vectors. This is what lets it find nearest neighbours quickly, even across millions of documents.

The second is a new aggregation stage, called vector search, that you drop straight into a normal aggregation pipeline.

Because the embedding lives on the very same document as the rest of your data, you can combine meaning based search with everything else MongoDB already does. Filtering, sorting, joins, grouping, all in the same query.

You can also choose the similarity measure that suits your model, such as cosine, dot product, or euclidean. For most text embeddings, cosine is a safe default.

Under the hood it uses approximate nearest neighbour search. That trades a tiny bit of accuracy for a huge amount of speed, which is what keeps it fast as your data grows.
-->

---
layout: default
---

# One document: data + vector together

```json
{
  "_id": ObjectId("6520f1a3c3a4b2e1d8f90a12"),
  "Title": "Harry Potter and the Philosopher's Stone",
  "Genre": "Fantasy",
  "Description": "An orphaned boy learns on his eleventh birthday
    that he is a wizard and is whisked away to Hogwarts School of
    Witchcraft and Wizardry...",
  "Embedding": [0.021, -0.044, 0.118, -0.007, /* ...764 more */ ]
}
```

<div class="mt-3 text-sm opacity-70">
  The <span class="font-mono">Embedding</span> field is just a 768-number array on the same document, with no separate vector store.
</div>

<!--
Before we look at the code, here is what actually lands in the database, so the idea is concrete.

This is a single MongoDB document for one film, Harry Potter and the Philosopher's Stone. Notice it has the everyday fields you would expect: a title, a genre, and a description.

The important part is the last field, Embedding. That is the 768 number vector we generated from the description. I have only shown the first few numbers here, but in reality the array has 768 entries.

The key thing to take away is that the vector sits right alongside the normal data, on the very same document. There is no separate vector database and no second system to keep in sync. Your data and its meaning live together, and that is exactly what lets MongoDB search across both at once.
-->

---
layout: statement
---

# The demo:<br/>a local model + Atlas

A C# console app · Ollama for embeddings · MongoDB Atlas Local in Docker · a small movies dataset.

<!--
Time to make this real. Let me quickly explain how the demo is wired together before I actually run it.

There is a small C# console application. Its only job is to embed some text and to talk to MongoDB.

For the embeddings, it calls Ollama, which is running the local model on my machine.

For storage and search, it uses the MongoDB Atlas Local image. That is the full Atlas Vector Search experience running inside a Docker container, with no cloud account required.

The dataset is a small collection of films, each one with a title and a short description, including the whole Harry Potter series.

So the flow is straightforward. For each movie, embed its description, and store the movie together with its vector. Then at search time, embed whatever the user typed, and ask MongoDB for the closest movies to it.
-->

---
layout: default
---

# Generating embeddings (C#)

```csharp
var http = new HttpClient {
    BaseAddress = new Uri("http://127.0.0.1:11434") };

var resp = await http.PostAsJsonAsync("/api/embeddings",
    new { model = "nomic-embed-text", prompt = text });

float[] embedding =
    (await resp.Content
        .ReadFromJsonAsync<EmbedResponse>()).Embedding;
```

<!--
This is the entire embedding step, and it is deliberately tiny.

First we create an HTTP client pointed at the local Ollama endpoint, on port 11434.

Then we send it just two things. The name of the model we want to use, and the text we want to embed.

It returns an array of floating point numbers. That array is our 768 dimensional embedding, and that is all it takes to turn text into a vector.

We call this once for every movie while we are seeding the database, and then once more for each query at search time.

The takeaway here is that if you have ever called a REST API, you already know how to generate embeddings. There is nothing exotic going on.
-->

---
layout: default
---

# Indexing & querying vectors

```csharp
// Create the Atlas Vector Search index (768 dims, cosine)
await collection.SearchIndexes.CreateOneAsync(
    new CreateSearchIndexModel("vector_index",
        SearchIndexType.VectorSearch, indexDef));

// Find the nearest movies to the query vector (type-safe pipeline)
var searchResults = await collection.Aggregate()
    .VectorSearch(
        field: x => x.Embedding,   // The vector field in your document
        queryVector: queryVector,  // Your query vector coordinates
        limit: 5,                  // Number of top matches to return
        options: new() { IndexName = "vector_index" })
    .ToListAsync();
```

<!--
Now the MongoDB side, which is also just a little bit of setup plus one query.

At the top, we create the vector search index. We tell MongoDB which field holds the vector, that it has 768 dimensions, and that we want to compare vectors using cosine similarity. We only do this once.

At the bottom is the actual search. It is a normal aggregation pipeline, but using the driver's strongly typed VectorSearch stage, so there is no hand assembled BSON.

We give it the field that holds the embedding, expressed as a simple lambda, the query vector, which is just the embedding of whatever the user typed, a limit for how many results we want back, and the options telling it which index to use.

Because it is type safe against our Movie class, the compiler checks the field name for us, and it reads almost like plain English.
-->

---
layout: default
---

# Live demo

- "a young wizard faces a dark lord" → the Harry Potter films
- "a hidden school for magic" → Philosopher's Stone, Chamber of Secrets
- "friends fight a rising evil" → the later Potter films
- None of those exact words appear, yet **meaning** still matched

<!--
Let us switch over to the terminal and run this for real.

Before this moment, I have already started MongoDB and Ollama with a single docker compose up, and I have pulled the model in advance, so we are ready to go.

When the app starts for the first time, it seeds the films, generates all of their embeddings, and builds the vector index. After that, it drops into an interactive prompt where I can just type queries.

I will try the queries on the slide. Watch the descriptions of the results as they come back. The words I type, like a young wizard faces a dark lord, do not actually appear in the film descriptions. It is matching on meaning, not on keywords.

Then I will try a hidden school for magic, which should surface the earlier Potter films where Hogwarts is first discovered. And friends fight a rising evil, which should bring back the darker, later films.

If the demo gods are unkind and something refuses to run, do not worry. I have screenshots, and I will simply walk through these same queries and the titles and scores they return.
-->

---
layout: section
---

# Automated embeddings<br/>with Voyage AI

<!--
In the demo you just saw, notice that I had to run and manage an embedding model myself. That is perfectly fine, but it is one more moving part to look after.

MongoDB now offers a way to remove that part entirely.

MongoDB acquired a company called Voyage AI, who build high quality embedding models. Those models are now available directly inside Atlas, which leads us to automated embeddings.
-->

---
layout: default
---

# Let Atlas do the embedding

- **Voyage AI** = MongoDB's built-in embedding models
- Atlas auto-embeds your text on write and on query
- Store text, search with text, with **no glue code**
- No separate embedding service to run or scale

<!--
Here is the idea in a nutshell. Instead of embedding text yourself, you let Atlas do it for you.

You mark a field as automatically embedded. From then on, whenever you insert or update a document, Atlas quietly sends that text off to Voyage AI and stores the resulting vector for you.

At search time, you do the same thing in reverse. You pass in plain text, and Atlas embeds your query automatically before it runs the vector search.

So the pattern becomes wonderfully simple. You store text, and you search with text. The whole embedding step disappears from your own code.

That means there is no separate embedding service for you to run, secure, and scale. In our demo, this would remove Ollama from the picture completely.

If you want the full details, the documentation link is here on the slide, under automated embedding.
-->

---
layout: default
---

# How automated embedding works

<div class="flex justify-center mt-2">
  <img src="/auto-embed.png" class="mdb-diagram w-[88%]" alt="Flow diagram: on insert/update and on query, Atlas sends text to Voyage AI, which embeds it; vectors are stored, searched and results returned." />
</div>

<!--
Let me walk through what Atlas is actually doing for you, in two flows.

The top flow happens on write. Whenever your app inserts or updates a document, Atlas takes the text from the field you marked for automated embedding, sends it to the Voyage AI model, gets back a vector, and stores that vector in an internal collection and keeps the index up to date. It stays in sync automatically as your data changes.

The bottom flow happens on query. Your app sends a plain text query. Atlas sends that text to the very same Voyage AI model, turns it into a query vector, runs the nearest neighbour search, and returns the ranked results.

The key thing to notice is that Voyage AI sits inside Atlas in both flows. Your application only ever deals in text, going in and coming out.

Compare this to the demo earlier, where our own C# code had to call Ollama for every embedding. Here, that entire step is handled for you inside the database.
-->

---
layout: default
---

# Set it up in MongoDB (C#), part 1: the index

```csharp
// Index the text field with AUTOMATED embedding.
//   type:"text" + model  ->  Atlas + Voyage AI embed for you.
var definition = new BsonDocument("fields", new BsonArray {
    new BsonDocument {
        { "type", "text" },
        { "path", "Description" },
        { "model", "voyage-3-large" },
        { "similarity", "cosine" } } });

await collection.SearchIndexes.CreateOneAsync(
    new CreateSearchIndexModel("auto_index",
        SearchIndexType.VectorSearch, definition));
```

<!--
This first slide is the index, and it is the only place the magic lives.

It looks almost identical to a normal vector index, with one important difference. Instead of describing a pre-computed vector field, we set the field type to text and give it a model, here voyage-3-large. That single change is what tells Atlas to embed this field for us with Voyage AI.

You point path at the text field you want searchable, in our case the film Description, and you pick a similarity function like cosine. Atlas then does the initial embedding of every existing document and keeps it in sync from then on.

So creating the index is a one time call. From this moment on, every insert and update is embedded automatically, and we never have to think about vectors again.

One note for the talk. Automated embedding is a newer capability, so mention that names and exact syntax may evolve, and point people at the documentation link from the earlier slide.
-->

---
layout: default
---

# Set it up in MongoDB (C#), part 2: the query

```csharp
// Query with PLAIN TEXT.
//   no queryVector, no call to an embedding model.
var searchResults = await collection.Aggregate()
    .AppendStage<Movie>(new BsonDocument("$vectorSearch", new BsonDocument {
        { "index", "auto_index" },
        { "path", "Description" },
        { "query", "a young wizard faces a dark lord" },
        { "numCandidates", 100 },
        { "limit", 5 } }))
    .ToListAsync();
```

<!--
This second slide is the query, and the interesting part is what is missing.

There is no queryVector and no call to an embedding model. We simply pass our search text in the query field, and Atlas embeds it for us before running the search.

We drop this straight onto the same fluent aggregate we used earlier, with AppendStage. Automated embedding is a newer capability, so there is no strongly typed VectorSearch helper for the text form yet. That is fine, we just express the one stage directly and it still reads cleanly.

You still point path at the same Description field and reference the auto_index we just created.

numCandidates and limit work exactly as before. numCandidates controls how many approximate matches Atlas considers, and limit is how many results we get back.

So the same search we built by hand in the demo becomes this, with the embedding plumbing removed entirely. You store text and you query with text, and Atlas plus Voyage AI handle everything in between.
-->

---
layout: default
---

# Where else semantic search shines

- RAG & chatbots grounded in your own data
- AI agent memory & long-term recall
- Recommendations & related content
- Deduplication & clustering of records
- Image & multimodal search
- Anomaly detection & classification

<!--
Searching movies is really just one shape of this technique. The same nearest neighbour idea powers a lot of the things you are probably being asked to build right now.

The big one today is retrieval augmented generation, often shortened to RAG. You use semantic search to find the most relevant pieces of your own data, and then you feed those pieces to a large language model, so its answers are grounded in your content instead of being made up.

Closely related, and very much the buzzword of the moment, is AI memory. When an agent or chatbot needs to remember past conversations and facts, you embed those memories and store them, then semantically recall the most relevant ones to give the model long term memory. It is the same nearest neighbour trick powering agent memory.

Recommendations and related content are really just find me items that are similar to this one, which is exactly nearest neighbours again.

You can also find duplicate or near duplicate records, even when the wording is different, which is brilliant for cleaning up messy data.

You can embed images instead of text and search by visual similarity, or even mix text and images together in the same search.

And you can spot anomalies. Points that sit far away from every normal cluster often indicate fraud, or a quality problem worth flagging.
-->

---
layout: default
---

# Key takeaways

- **Search by meaning, not keywords**
- Embeddings are the bridge from text to vectors
- MongoDB unifies your data and your vectors
- Voyage AI on Atlas makes it effortless

<!--
Let us bring it all together with four things I would love you to walk away remembering.

One. Search by meaning, not by keywords. That is the whole mindset shift in a single line.

Two. Embeddings are the bridge. They turn text, and other kinds of data, into vectors that capture meaning.

Three. MongoDB lets you keep your data and your vectors in one place, and search across them with the tools you already use every day.

Four. If you are on Atlas, Voyage AI automated embeddings make this almost effortless, because Atlas does the embedding for you.

The main point I want to leave you with is this. You already know MongoDB, so adding AI powered search is a small step, not a whole new system to learn.
-->

---
layout: end
class: text-center
---

# Thank you!

<div class="mt-6 opacity-80 text-lg">
  Questions welcome. Come grab me afterwards.
</div>

<div class="mt-8 text-sm opacity-70 leading-relaxed">
  Atlas Automated Embedding · Atlas Vector Search docs · the C# + Ollama demo project
</div>

<!--
That is everything from me. Thank you all very much for listening, and I am very happy to take any questions.

If you would like to explore this further, here are a few resources to get you going.

The automated embedding documentation, which covers letting Atlas embed your data for you using Voyage AI.

The Atlas Vector Search documentation, which covers the index and the query syntax in full.

And the demo project itself. This is the C# app with Ollama and MongoDB Atlas Local that you saw. You are very welcome to clone it and try it yourself.

Please do come and grab me afterwards if you would like to talk through your own use case.
-->
