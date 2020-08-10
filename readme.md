# Bot Template II

Slightly less stupid Discord Bot Template.

## Features
* Ping command in `Modules/` dir
* Basic Error handling in [EventHandlers.cs](https://github.com/lordDashh/BotTemplateII/blob/master/EventHandlers.cs#L34)
* Exit command and send error message one liner: `throw new UserException("You're dumb")`

## Prerequisites
* [.NET Core SDK 3.1](https://dotnet.microsoft.com/download/dotnet-core/3.1)
* [DSharpPlus nightly feed](https://dsharpplus.github.io/articles/advanced/nightly_builds.html) (tested with 4.0.0-nightly-00711)

## Usage
* Create the `Resources` directory
* Create a config.json file:
```json
{
    "token": "bot token here",
    "prefixes": ["prefix1", "prefix2"]
}
```
* `dotnet run`
