$(function() {
    var area台北市 = ['中山區', '內湖區', '大安區', '士林區', '中正區', '南港區', '北投區', '大同區'];
    var area新北市 = ['板橋區', '土城區', '淡水區', '新莊區', '三重區', '林口區', '新店區', '金山區'];
    var area桃園市 = ['中壢區', '桃園區', '龜山區', '蘆竹區'];
    var area新竹市 = ['北區', '東區'];
    var area新竹縣 = ['竹北市'];
    var area台中市 = ['北屯區', '北區', '西區', '南屯區', '東區', '西屯區', '后里區'];
    var area台南市 = ['安南區', '善化區'];
    var area高雄市 = ['楠梓區', '前鎮區', '仁武區', '鼓山區'];
    var area基隆市 = ['中正區'];
    var area嘉義縣 = ['竹崎鄉'];
    var area彰化縣 = ['彰化市'];         

    $("#city :button").click(function(){   
        var isBtn = $(this).val();
        switch (isBtn) {
            case '臺北市':
                $(".btn-area").remove();
                $.each(area台北市, function() {
                    area = $(`<input type="button" class="btn-area" value="${this}" />`);
                    $("#area-fieldset").append(area);
                });
                break;
            case '新北市':
                $(".btn-area").remove();
                $.each(area新北市, function() {
                    area = $(`<input type="button" class="btn-area" value="${this}" />`);
                    $("#area-fieldset").append(area);
                });
                break;
            case '桃園市':
                $(".btn-area").remove();
                $.each(area桃園市, function() {
                    area = $(`<input type="button" class="btn-area" value="${this}" />`);
                    $("#area-fieldset").append(area);
                });
                break;
            case '新竹市':
                $(".btn-area").remove();
                $.each(area新竹市, function() {
                    area = $(`<input type="button" class="btn-area" value="${this}" />`);
                    $("#area-fieldset").append(area);
                });
                break;
            case '新竹縣':
                $(".btn-area").remove();
                $.each(area新竹縣, function() {
                    area = $(`<input type="button" class="btn-area" value="${this}" />`);
                    $("#area-fieldset").append(area);
                });
                break;
            case '臺中市':
                $(".btn-area").remove();
                $.each(area台中市, function() {
                    area = $(`<input type="button" class="btn-area" value="${this}" />`);
                    $("#area-fieldset").append(area);
                });
                break;
            case '臺南市':
                $(".btn-area").remove();
                $.each(area台南市, function() {
                    area = $(`<input type="button" class="btn-area" value="${this}" />`);
                    $("#area-fieldset").append(area);
                });
                break;
            case '高雄市':
                $(".btn-area").remove();
                $.each(area高雄市, function() {
                    area = $(`<input type="button" class="btn-area" value="${this}" />`);
                    $("#area-fieldset").append(area);
                });
                break;
            case '基隆市':
                $(".btn-area").remove();
                $.each(area基隆市, function() {
                    area = $(`<input type="button" class="btn-area" value="${this}" />`);
                    $("#area-fieldset").append(area);
                });
                break;
            case '嘉義縣':
                $(".btn-area").remove();
                $.each(area嘉義縣, function() {
                    area = $(`<input type="button" class="btn-area" value="${this}" />`);
                    $("#area-fieldset").append(area);
                });
                break;
            case '彰化縣':
                $(".btn-area").remove();
                $.each(area彰化縣, function() {
                    area = $(`<input type="button" class="btn-area" value="${this}" />`);
                    $("#area-fieldset").append(area);
                });
                break;
        }
                                               
         
    });

    function selectedResult (){
        
    }

    

});