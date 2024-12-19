$().ready(function () {

    // 엔터키 이벤트 (ent 요소 유무 확인)
    $("input[ent]").keypress(function (e) {
        if (e.keyCode == 13) {
            eval($(this).attr("ent") + "();");
            return false;
        }
    });

    // 드랍다운 이벤트 (pgcnt 요소 유무 확인)
    //$("select[pgcnt]").on("change", function () {

    //    var p = $(this).find(":selected").val()

    //    $("#frmPage > #frmPage").val(1)
    //    $("#frmPage > #frm" + $(this).attr("id")).val(p);

    //    $("#frmPage").submit();

    //})


    // 페이지 이동 이벤트 (role = button 인지 체크)
    $("[role=button]").on('click', function () {
        var page = $(this).attr('href')

        if (page == '' || page == null) {
            return;
        }

        location.href = page;

    })

    String.format = function () {
        let args = arguments;

        return args[0].replace(/{(\d+)}/g, function (match, num) {
            num = Number(num) + 1;
            return typeof (args[num]) != undefined ? args[num] : match;
        });
    }


})

function decodeHTMLEntities(str) {
    if (str !== undefined && str !== null && str !== '') {
        str = String(str);

        str = str.replace(/<script[^>]*>([\S\s]*?)<\/script>/gmi, '');
        str = str.replace(/<\/?\w(?:[^"'>]|"[^"]*"|'[^']*')*>/gmi, '');
        var element = document.createElement('div');
        element.innerHTML = str;
        str = element.textContent;
        element.textContent = '';
    }

    return str;
}

function fnReadContent(str) {

    if (str !== undefined && str !== null && str !== '') {

        str = str.replaceAll("<br>", "\n");

        str = str.replaceAll("&gt;", ">");

        str = str.replaceAll("&lt;", "<");

        str = str.replaceAll("&quot;", "");

        str = str.replaceAll("&nbsp;", " ");

        str = str.replaceAll("&amp;", "&");

        return str;

    }

}