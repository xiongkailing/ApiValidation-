@model $rootnamespace$.Models.Token

@{
    ViewBag.Title = "编辑" + this.Model.ThirdPartName;
    Layout = "~/Views/Shared/TokenLayout.cshtml";
}

<h2>Token 编辑页</h2>

@using (Html.BeginForm("Edit","TokenManage", FormMethod.Post))
{
    @Html.AntiForgeryToken()
   
    <fieldset>
        <div id="legend" class="">
            <legend class="">编辑</legend>
        </div>
        @Html.ValidationSummary(true)
        @Html.HiddenFor(model => model.Id)
        <div class="control-group">
            @Html.LabelFor(model => model.SecretToken, new { @class = "control-label col-md-2" })
            <div class="controls">
                <input type="text" class="form-control" title="点我更新" data-val-required="*" name="SecretToken" id="SecretToken" value=@this.Model.SecretToken />
                @Html.ValidationMessageFor(model => model.SecretToken)
            </div>
        </div>

        <div class="control-group">
            @Html.LabelFor(model => model.OpenToken, new { @class = "control-label col-md-2" })
            <div class="controls">
                <input type="text" class="form-control" title="点我更新" data-val-required="*" name="OpenToken" id="OpenToken" value=@this.Model.OpenToken />
                @Html.ValidationMessageFor(model => model.OpenToken)
            </div>
        </div>

        <div class="control-group">
            @Html.LabelFor(model => model.ThirdPartName, new { @class = "control-label col-md-2" })
            <div class="controls">
                <input id="ThirdPartName" class="form-control" type="text" value=@this.Model.ThirdPartName name="ThirdPartName" data-val-required="*" data-val="true" >
                @Html.ValidationMessageFor(model => model.ThirdPartName)
            </div>
        </div>

        <div class="control-group">
            @Html.LabelFor(model => model.Telphone, new { @class = "control-label col-md-2" })
            <div class="controls">
                <input type="text" value=@this.Model.Telphone name="Telphone" id="Telphone" data-val-regex-pattern="^[1][3578]\d{9}$" data-val-regex="字段 联系方式 必须与正则表达式“^[1][3578]\d{9}$”匹配。" data-val="true" class="form-control">
                @Html.ValidationMessageFor(model => model.Telphone)
            </div>
        </div>

        <div class="control-group">
            <div class="col-md-offset-2 col-md-10">
                <input type="submit" value="确定" class="btn btn-success" />
            </div>
        </div>
    </fieldset>
}

<div>
    @Html.ActionLink("返回列表页", "Index")
</div>

@section Scripts {
    @Scripts.Render("~/bundles/jqueryval")
    @Scripts.Render("~/bundles/jquery")
    <script type="text/javascript">
        $("#SecretToken").click(function () {
            $.ajax({
                url: '/TokenManage/CreateSecret',
                type: 'GET',
                datatype: 'text',
                success: function (res) {
                    console.log(res);
                    if (res) {
                        $("#SecretToken").val(res);
                    }
                }
            });
        });
        $("#OpenToken").click(function () {
            $.ajax({
                url: '/TokenManage/CreateSecret',
                type: 'GET',
                datatype: 'text',
                success: function (res) {
                    console.log(res);
                    if (res) {
                        $("#OpenToken").val(res);
                    }
                }
            });
        });
    </script>
}
