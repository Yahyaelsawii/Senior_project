# Git: Move old main → branch `old`, put this build on `main`

Your current project is committed on **main** (this build). Follow these steps to connect GitHub and get:

- **Branch `old`** = everything that was on GitHub main (previous content)
- **Branch `main`** = this build (current project)

---

## 1. Add your GitHub remote (once)

Replace `YOUR_USERNAME` and `YOUR_REPO` with your GitHub repo:

```bash
cd /Users/yahyael-sawi/VR_Lab_2026
git remote add origin https://github.com/YOUR_USERNAME/YOUR_REPO.git
```

If you already have `origin`, skip this or use `git remote set-url origin https://github.com/...`.

---

## 2. Fetch GitHub and create `old` from current GitHub main

This makes local branch **old** = whatever is currently on GitHub main:

```bash
git fetch origin
git branch old origin/main
```

If your default branch on GitHub is named differently (e.g. `master`), use that instead of `origin/main`.

---

## 3. Push both branches

- Push **main** (this build) and overwrite GitHub main:
  ```bash
  git push -u origin main --force
  ```
- Push **old** (previous main from GitHub):
  ```bash
  git push -u origin old
  ```

---

## Result

- **origin/main** = this build (current VR Lab 2026 project)
- **origin/old** = previous content that was on main

---

## One-liner (after remote is set)

After `git remote add origin ...`:

```bash
git fetch origin && git branch old origin/main && git push -u origin main --force && git push -u origin old
```
