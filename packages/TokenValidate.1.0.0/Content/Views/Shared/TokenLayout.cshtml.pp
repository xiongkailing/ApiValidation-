<!DOCTYPE html>
<html lang="zh-CN">
<head>
    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <title>@ViewBag.Title</title>
    <link href="~/Content/bootstrap.min.css" rel="stylesheet" />
    <link href="~/Content/sticky-footer.css" rel="stylesheet" />
</head>

<body>

    <!-- Begin page content -->
    <div class="container">
        @RenderBody()
    </div>
    
    <footer class="footer">
        <div class="container">
            <p class="text-muted">&copy;@DateTime.Now.Year  Powered By 小熊有话说</p>
        </div>
    </footer>
    @RenderSection("scripts", required: false)
</body>
</html>
