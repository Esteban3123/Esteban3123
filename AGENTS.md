# AGENTS.md

## Cursor Cloud specific instructions

This repository is a **GitHub profile README** (`Esteban3123/Esteban3123`). It contains only `README.md` — there is no application code, no dependency manifest (`package.json`, `requirements.txt`, etc.), no build system, and no automated tests.

- **There is nothing to install, build, lint, or test.** The startup update script is intentionally a no-op.
- **The "product" is the rendered profile page.** GitHub renders `README.md` on the user's profile. To preview it locally the way GitHub does, you can run a Markdown preview server such as `grip` (`pip install grip` then `grip README.md 0.0.0.0:6419`) and open the served URL in a browser. `grip` is a preview convenience only and is not a project dependency.
- **External images may not load in the cloud VM.** The README embeds images from `github-readme-stats.vercel.app` and a generated `snake.svg`. These can fail (5xx) behind the sandbox network proxy; that is an egress limitation, not a repo problem, and they render fine on GitHub itself.
- The `snake.svg` animation is produced by a GitHub Action and lives on a separate `output` branch, not in `main`.
