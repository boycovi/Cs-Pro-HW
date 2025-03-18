using AngleSharp.Html.Parser;

using var httpClient = new HttpClient();
var response = await httpClient.GetAsync("https://rozetka.com.ua/notebooks/c80004/");
if (!response.IsSuccessStatusCode)
{
    Console.WriteLine("Error");
    return;
}

var content = await response.Content.ReadAsStringAsync();

var parser = new HtmlParser();
var document = parser.ParseDocument(content);

var goods = document.QuerySelectorAll("div.goods-tile__inner").ToArray();

var goodsList = new List<Good>();
foreach (var good in goods)
{
    var name = good.QuerySelector(".goods-tile__title").TextContent;
    var price = good.QuerySelector("span.goods-tile__price-value").TextContent;
    var link = good.QuerySelector(".goods-tile__picture").GetAttribute("href");
    var image = good.QuerySelector(".goods-tile__picture img").GetAttribute("src");

    goodsList.Add(new Good
    {
        Name = name,
        Price = price,
        Link = link,
        Image = image
    });
}
Console.WriteLine(goodsList.Count());

Console.WriteLine(goodsList[15].Price);