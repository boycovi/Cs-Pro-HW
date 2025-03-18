using AngleSharp.Html.Parser;
using RefactoringGuruParser;
using Npgsql;

var connectionString = "Server=localhost;Port=5432;User Id=postgres;Password=postgres;Database=Refactoring Guru Articles;";

using var httpClient = new HttpClient();

var response = await httpClient.GetAsync("https://refactoring.guru/refactoring/smells");
if (!response.IsSuccessStatusCode)
{
    Console.WriteLine("Error");
    return;
}


var content = await response.Content.ReadAsStringAsync();

var parser = new HtmlParser();
var document = parser.ParseDocument(content);

var articles = document.QuerySelectorAll("div.cell-text").ToArray();

var articlesList = new List<Article>();
foreach (var article in articles)
{
    var articleName = article.QuerySelector("a").TextContent;
    var articleLink = article.QuerySelector("a").GetAttribute("href");

    var subArticles = article.QuerySelectorAll("div.catalog-list").SelectMany(sub => sub.QuerySelectorAll("li")).ToList();
    var subArticleNameLink = new Dictionary<string, string>();

    foreach (var subArticle in subArticles)
    {
        var subArticleName = subArticle.QuerySelector("a").TextContent;
        var subArticleLink = $"https://refactoring.guru{subArticle.QuerySelector("a").GetAttribute("href")}";
        subArticleNameLink.Add(subArticleName, subArticleLink);
    }

    articlesList.Add(new Article
    {
        Name = articleName,
        Link = articleLink,
        SubArticles = subArticleNameLink,
    });
}

foreach (var article in articlesList)
{
    Console.WriteLine($"{article.Name}");

    foreach (var subArticle in article.SubArticles)
    {
        Console.WriteLine($"- {subArticle.Key}: {subArticle.Value}");
    }
}

foreach (var article in articlesList)
{
    using var connection = new NpgsqlConnection(connectionString);
    connection.Open();

    using var cmdArticle = new NpgsqlCommand("INSERT INTO articles (name, link) VALUES (@Name, @Link) RETURNING id", connection);
    cmdArticle.Parameters.AddWithValue("Name", article.Name);
    cmdArticle.Parameters.AddWithValue("Link", article.Link);

    var articleId = (int)cmdArticle.ExecuteScalar();
    
    foreach (var subArticle in article.SubArticles)
    {
        using var cmdSubArticle = new NpgsqlCommand("INSERT INTO sub_articles (article_id, name, link) VALUES (@ArticleId, @Name, @Link)", connection);
        cmdSubArticle.Parameters.AddWithValue("ArticleId", articleId);
        cmdSubArticle.Parameters.AddWithValue("Name", subArticle.Key);
        cmdSubArticle.Parameters.AddWithValue("Link", subArticle.Value);
        cmdSubArticle.ExecuteNonQuery();
    }
}