$(document).ready(function () {
    $(document).on("click", "a[class=page-link]", function (e) {
        e.preventDefault();
        let url = this.attributes["href"].value;
        let sub_url = url.split('/')[2];
        page = parseInt(sub_url.split('_')[1].split('?')[0]);
        $("#list").load(url);
        $("#product-pager").load('/Product/GetPager?pageNo=' + page + '&pages=' + pages + '&category=' + category);
        history.pushState(null, null, url);
    })
})