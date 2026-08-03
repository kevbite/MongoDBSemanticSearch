# MongoDB Semantic Search

A small .NET 9 console demo showing semantic (vector) search over MongoDB using
local embeddings. It seeds a handful of movies, generates embeddings with a local
[Ollama](https://ollama.com/) model, stores them in MongoDB Atlas Local, and runs
[Atlas Vector Search](https://www.mongodb.com/docs/atlas/atlas-vector-search/) to
find results by meaning rather than keywords.

This repo also contains the accompanying lightning-talk deck built with
[Slidev](https://sli.dev/) (see [`presentation/slidev`](presentation/slidev)).

## Prerequisites

- [.NET 9 SDK](https://dotnet.microsoft.com/download)
- [Docker](https://www.docker.com/) (for MongoDB Atlas Local + Ollama)

## Running the demo

1. Start the infrastructure (MongoDB Atlas Local + Ollama):

   ```bash
   docker compose up -d
   ```

2. Pull the embedding model into Ollama (only needed once per machine):

   ```bash
   docker exec semantic_ollama ollama pull nomic-embed-text
   ```

   > The app calls Ollama's `POST /api/embeddings` with the `nomic-embed-text`
   > model. If the model isn't pulled you'll get a **404** from the embeddings
   > endpoint. Pulling the model fixes it.

3. (Optional) Verify the model is available:

   ```bash
   docker exec semantic_ollama ollama list
   ```

4. Run the app:

   ```bash
   dotnet run
   ```

The app creates the `vector_index` (768 dimensions, cosine) automatically, seeds
the sample data, embeds it, and prints semantic search results with scores.

## Services

`docker compose up` starts:

| Service   | Image                          | Port    | Purpose                          |
| --------- | ------------------------------ | ------- | -------------------------------- |
| `mongo`   | `mongodb/mongodb-atlas-local`  | `27017` | MongoDB with Atlas Vector Search |
| `ollama`  | `ollama/ollama`                | `11434` | Local embedding model host       |

## Presentation

The lightning-talk slides live in [`presentation/slidev`](presentation/slidev)
and run locally with Slidev:

```bash
cd presentation/slidev
npm install
npm run dev
```

Then open http://localhost:3030 (presenter view at `/presenter`).
