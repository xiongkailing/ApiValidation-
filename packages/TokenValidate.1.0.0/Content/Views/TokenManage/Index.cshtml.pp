﻿@model IEnumerable<$rootnamespace$.Models.Token>

@{
    ViewBag.Title = "Token管理页";
    Layout = "~/Views/Shared/TokenLayout.cshtml";
}

<h2>Token管理页</h2>

<p>
    @Html.ActionLink("新增", "Create")
</p>
<table class="table">
    <tr>
        <th>
            @Html.DisplayNameFor(model => model.SecretToken)
        </th>
        <th>
            @Html.DisplayNameFor(model => model.OpenToken)
        </th>
        <th>
            @Html.DisplayNameFor(model => model.ThirdPartName)
        </th>
        <th>
            @Html.DisplayNameFor(model => model.Telphone)
        </th>
        <th></th>
    </tr>

@foreach (var item in Model) {
    <tr>
        <td>
            @Html.DisplayFor(modelItem => item.SecretToken)
        </td>
        <td>
            @Html.DisplayFor(modelItem => item.OpenToken)
        </td>
        <td>
            @Html.DisplayFor(modelItem => item.ThirdPartName)
        </td>
        <td>
            @Html.DisplayFor(modelItem => item.Telphone)
        </td>
        <td>
            @Html.ActionLink("编辑", "Edit", new { id=item.Id }) |
            @Html.ActionLink("详细", "Details", new { id=item.Id }) |
            @Html.ActionLink("删除", "Delete", new { id=item.Id })
        </td>
    </tr>
}

</table>
