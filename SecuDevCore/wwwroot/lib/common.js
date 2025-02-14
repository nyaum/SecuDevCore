$().ready(function () {

    // autocomplete - off
    $("input").attr("autocomplete", "off");
   
    // 엔터키 이벤트 (ent 요소 유무 확인)
    $("input[ent]").keypress(function (e) {
        if (e.keyCode == 13) {

            // 보안 이슈 eval 대체 함수
            new Function($(this).attr("ent") + "();")();
            return false;
        }
    });

    // 실시간 input 데이터 검사 (propertychange에 함수 이름 입력)
    $("input[propertychange]").on("propertychange change paste input", function (e) {

        var fnName = $(this).attr("propertychange");

        if (fnName != "") {

            new Function(fnName + '()')();

        }

    })

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


    // string.format evt
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

function fnValidation(e) {
    e.value = e.value.replace(/[^a-z0-9_]/ig, '')
}

function dateFormat(date, format) {

    var yyyy = date.getFullYear();
    var mm = date.getMonth() + 1;
    mm = mm >= 10 ? mm : '0' + mm;	// 10 보다 작으면 0을 앞에 붙여주기 ex) 3 > 03
    var dd = date.getDate();
    dd = dd >= 10 ? dd : '0' + dd;	// 10 보다 작으면 9을 앞에 붙여주기 ex) 9 > 09

    if (format == "yyyy-mm-dd") {
        return yyyy + '-' + mm + '-' + dd;
    }
    else if (format == "yyyymmdd") {
        return yyyy + mm + dd;
    }

}