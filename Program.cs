using System;
using System.Net.Http;
using System.Reflection.Metadata;
using System.Text.Json;
using System.Threading.Tasks;


namespace Program
{
    class Program
    {
        static async Task Main(string[] args)
        {
            if (args.Length == 0)
            {
                return;
            }

            string username = args[0];
            string url = $"https://api.github.com/users/{username}/events";

            using HttpClient client = new HttpClient();

            client.DefaultRequestHeaders.Add("User-Agent", "GithubActivityCLI");

            try
            {
                HttpResponseMessage response = await client.GetAsync(url);

                if (!response.IsSuccessStatusCode)
                {
                    if ((int)response.StatusCode == 404)
                    {
                        Console.WriteLine("Lỗi không tìm thấy tài khoản Github");
                    }
                    else if ((int)response.StatusCode == 403)
                    {
                        Console.WriteLine("Lỗi vược quá giới hạn gọi API");
                    }
                    else
                    {
                        Console.WriteLine("Lỗi không thể tải dữ liệu");
                    }
                    return;
                }

                string jsonResponse = await response.Content.ReadAsStringAsync();
                using JsonDocument doc = JsonDocument.Parse(jsonResponse);

                JsonElement root = doc.RootElement;
                if (root.GetArrayLength() == 0)
                {
                    Console.WriteLine($"Không tìm thấy hoạt động gần đây nào cho tài khoản {username}");
                    return;
                }

                Console.WriteLine($"\nHoạt động gần đây của {username}:");



                foreach (JsonElement element in root.EnumerateArray())
                {
                    string type = element.GetProperty("type").GetString() ?? "";
                    string repoName = element.GetProperty("repo").GetProperty("name").GetString() ?? "";
                    switch (type)
                    {
                        case "PushEvent":
                            int commitCount = 0;
                            if (element.TryGetProperty("payload", out JsonElement payload) &&
                                payload.TryGetProperty("commits", out JsonElement commits))
                            {
                                commitCount = commits.GetArrayLength();
                            }
                            Console.WriteLine($"- Pushed {commitCount} commit(s) to {repoName}");
                            break;

                        case "IssuesEvent":
                            string action = element.GetProperty("payload").GetProperty("action").GetString() ?? "";
                            if (action == "opened")
                            {
                                Console.WriteLine($"- Opened a new issue in {repoName}");
                            }
                            else
                            {
                                Console.WriteLine($"- {action} an issue in {repoName}");
                            }
                            break;

                        case "WatchEvent":
                            Console.WriteLine($"- Starred {repoName}");
                            break;

                        case "CreateEvent":
                            string refType = element.GetProperty("payload").GetProperty("ref_type").GetString() ?? "";
                            Console.WriteLine($"- Created {refType} in {repoName}");
                            break;

                        case "PullRequestEvent":
                            string prAction = element.GetProperty("payload").GetProperty("action").GetString() ?? "";
                            Console.WriteLine($"- {prAction} a pull request in {repoName}");
                            break;

                        default:
                            // Các loại sự kiện khác
                            Console.WriteLine($"- {type} in {repoName}");
                            break;
                    }

                }
            }
            catch (HttpRequestException)
            {
                Console.WriteLine("Lỗi: Không thể kết nối đến máy chủ GitHub. Vui lòng kiểm tra lại kết nối internet.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Đã xảy ra lỗi không mong muốn: {ex.Message}");
            }
        }
    }
}