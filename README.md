# GitHubAutoUpdater

Personal Windows utility, not maintained (last updated 2023). A C# console app that
automatically commits/pushes and pulls every git repository found under a list of
root directories, so machines used in multiple locations stay in sync without manual
git housekeeping.

## How it works

- `GitHubUpdate/` (Visual Studio solution, .NET Framework console app) reads
  `Paths.txt` — one root directory per line, e.g. `C:\Users\<user>\Modular_Projects` —
  and recursively finds every subdirectory containing a `.git` folder.
- On an "evening" pass it runs `git add . && git commit -m 'evening_update' && git push`
  in each repo; on a "morning" pass it runs `git pull` in each. It then sleeps ~55
  minutes and repeats, running indefinitely.
- Evening/morning trigger hours are configurable in `Program.cs` (defaults 23 and 1),
  though the checked-in code currently bypasses the hour check, so the passes simply
  alternate each cycle.
- Git commands are executed by piping into `cmd.exe`, so `git` must be on the PATH and
  credentials already configured (e.g. a credential manager) — there is no auth handling
  in the app.

`main.py` is an unfinished Python sketch of the same directory-walking idea and is not
functional.

## Requirements

- Windows (hard-coded `C:\Windows\System32\cmd.exe`)
- .NET Framework, git installed and authenticated for the target remotes

## Usage

Put one root directory per line in `Paths.txt` next to the executable, then build and
run the `GitHubUpdate` solution. Note the blanket `git add .` commits everything in
each repo on every evening pass.
