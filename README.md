# ImageAICaptioner

Welcome to `ImageAICaptioner`. A simple C# console app that uses `LLaVA` (via `Ollama`) to generate captions for images! 

Built with `.NET 8` and powered by `Microsoft.Extensions.AI` (in preview), this project preloads images from a folder, lets you pick one, and spits out a caption. 

All with dependency injection for a clean, modern twist. 

Perfect for C# devs curious about local AI tools.

## What It Does

Scans an images folder in the project’s output directory.

Lists all `.jpg` and `.png` files for you to choose from.

Sends your selected image to `LLaVA` (running locally) and prints a one-sentence caption.

## Prerequisites
* `.NET 8 SDK` installed.
* `Ollama` set up on your machine.
* A C# IDE (Visual Studio, Rider, VSCode. Your pick!).

## Getting Started

Follow these steps to get the app running on your system.

### 1. Install `Ollama` and `LLaVA`

Download `Ollama` from [ollama.com](https://ollama.com) and install it (Windows/Mac installer or Linux script: `curl -fsSL https://ollama.com/install.sh | sh`).

Open a terminal and pull the lightweight `LLaVA` model:

```bash
ollama pull llava:7b
```

Start `LLaVA`:

```bash
ollama run llava:7b
```

Keep this terminal open, it hosts `LLaVA` at `http://localhost:11434`.

**Note:** Want better captions? Try `ollama pull llava` for the full model (needs more RAM/GPU power).

### 2. Clone and Set Up the Project

Clone this repo:

```bash
git clone https://github.com/ronnythedev/ImageAICaptioner.git
cd ImageAICaptioner
```

Add the required NuGet packages (version 9.3.0-preview.1.25161.3 or later):

`Microsoft.Extensions.AI`
`Microsoft.Extensions.AI.Ollama`

Use your IDE’s package manager or run:

```bash
dotnet add package Microsoft.Extensions.AI --version 9.3.0-preview.1.25161.3
dotnet add package Microsoft.Extensions.AI.Ollama --version 9.3.0-preview.1.25161.3
```

Check the images folder in the repo. 

It’s got sample images (e.g., dog.jpg, cat.png). 

Add your own if you like. Ensure they’re `.jpg` or `.png`. 

In your IDE, set their “Copy to Output Directory” to “Copy if newer” so they land in `bin/Debug/net8.0/images`.

### 3. Run the App

Build and run the project.

You’ll see something like:

```text
Welcome to the Image AI Caption Generator!

Choose an image to caption:
1. dog.jpg
2. rabbit.jpg
3. car.jpg
4. llama.jpg
5. cat.png

Enter the number of your choice:
```

Type a number (e.g., `4`), and get a caption like:

```text
Caption: A large, woolly alpaca standing on a grass field under a blue sky with clouds. 
```

## How It Works

The app uses dependency injection via `Microsoft.Extensions.Hosting` to wire up an `IChatClient` for Ollama’s `LLaVA` model. 

It preloads images from the images folder, lets you pick one, and sends it as raw bytes to `LLaVA` with a prompt. 

The response comes back, and we grab the caption from the first message. 

Check out `Program.cs` for the full code!

## Troubleshooting
* “No images”: Ensure the images folder has `.jpg` or `.png` files and they’re copied to the output directory (`bin/Debug/net8.0/images`).
* Connection errors: Verify `ollama run llava:7b` is running and `http://localhost:11434` is up (test in a browser).
* Slow performance: If it lags, stick with `llava:7b` or upgrade your hardware for `llava`.

## Preview Warning
Heads-up: This uses `Microsoft.Extensions.AI` version `9.3.0-preview.1.25161.3`, a preview release. 

The code works as-is with this version, but the API might change in future updates. 

Keep an eye on the official docs if you’re using a newer release.

## Contributing
Got ideas? Fork this repo, tweak it, and send a pull request! I’d love to see GUI versions or new features.

## License
This project is MIT-licensed—use it, share it, have fun with it!
