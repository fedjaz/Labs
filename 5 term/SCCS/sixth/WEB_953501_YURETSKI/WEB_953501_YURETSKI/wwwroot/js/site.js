$(document).ready(function () {
    $(document).on("click", "a[class=page-link]", function (e) {
        e.preventDefault();
        var url = this.attributes["href"].value;
        var urlParams = new URLSearchParams(url.split('?')[1]);
        for (let p of urlParams) {
            console.log(p);
        }
        page = parseInt(urlParams.get("pageNo"));
        $("#list").load(url);
        $("#product-pager").load('/Product/GetPager?pageNo=' + page + '&pages=' + pages + '&category=' + category);
        history.pushState(null, null, url);
    })
})