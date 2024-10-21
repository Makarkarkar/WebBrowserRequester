using PuppeteerSharp;

namespace PuppeteerSharpProject
{
    class Program
    {
        static async Task Main(string[] args)
        {
            // download the browser executable
            await new BrowserFetcher().DownloadAsync();

            // browser execution configs
            var launchOptions = new LaunchOptions
            {
                Headless = true, // = false for testing
            };
            var pupHtml = "";
            // open a new page in the controlled browser
            using (var browser = await Puppeteer.LaunchAsync(launchOptions))
            using (var page = await browser.NewPageAsync())
            {
                // visit the target page
                await page.GoToAsync("https://www.stuff.co.nz/");
                // retrieve the HTML source code and log it
                pupHtml = await page.GetContentAsync();
                Console.WriteLine(pupHtml);
            }
            
            using (HttpClient client = new HttpClient())
            {
                // Устанавливаем User-Agent (если нужно)
                //client.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/91.0.4472.124 Safari/537.36");

                try
                {
                    // Отправляем GET-запрос
                    HttpResponseMessage response = await client.GetAsync("https://www.stuff.co.nz/");
                    response.EnsureSuccessStatusCode();

                    // Читаем содержимое ответа
                    string responseBody = await response.Content.ReadAsStringAsync();
                    
                    // Выводим HTML-код в консоль
                    Console.WriteLine(responseBody);
                }
                catch (HttpRequestException e)
                {
                    Console.WriteLine($"Ошибка при отправке запроса: {e.Message}");
                }
            }
        }
    }
}