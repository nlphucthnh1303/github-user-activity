
# GitHub User Activity CLI

A simple Command Line Interface (CLI) application built with **C#** and **.NET** to fetch and display the recent activity of a specified GitHub user using the official GitHub REST API.

This project is inspired by the [GitHub User Activity Idea on Roadmap.sh](https://roadmap.sh/projects/github-user-activity).

---

## Features

- **Fetch Recent Activity:** Retrieves user events like commits, opened issues, starred repositories, pull requests, and created branches.
- **Pure Native Solution:** Uses built-in .NET libraries (`HttpClient` and `System.Text.Json`) without any external dependencies.
- **Graceful Error Handling:** Handles network failures, rate limiting (HTTP 403), and non-existent users (HTTP 404).

---

## Prerequisites

- [.NET 8.0 SDK](https://dotnet.microsoft.com/download) or higher installed.

---

## How to Run

1. **Clone the repository:**
   ```bash
   git clone [https://github.com/your-username/github-user-activity.git](https://github.com/your-username/github-user-activity.git)
   cd github-user-activity

```

2. **Run the application using `dotnet run`:**
Pass the GitHub username as a command-line argument:
```bash
dotnet run -- <username>

```


**Example:**
```bash
dotnet run -- kamranahmedse

```



---

## Example Output

```text
Hoạt động gần đây của kamranahmedse:
- Pushed 3 commit(s) to kamranahmedse/developer-roadmap
- Opened a new issue in kamranahmedse/developer-roadmap
- Starred kamranahmedse/developer-roadmap
- Created branch in kamranahmedse/developer-roadmap

```

---

## Error Examples

* **User Not Found:**
```text
Lỗi: Không tìm thấy tài khoản GitHub 'invalid_user_name_123'.

```


* **Missing Argument:**
```text
Sử dụng: github-activity <username>

```



```

---

<FollowUp label="Bạn có muốn tạo thêm file .gitignore cho project .NET này không?" query="Tạo file .gitignore chuẩn cho dự án C# .NET"/>

```
