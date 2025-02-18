// Version: 1.0
// required: bootstrap5, jquery

var actionToast = {
    alert: function (msg) {

        if ($("#alertToast").length) {

            $("#alertToast").remove()

        }

        const alertTitle = "오류"

        const alert_html = `<div class="toast-container position-absolute top-0 start-50 translate-middle-x pt-5" id="alertToast">
                                    <div id="alert_toast" class="toast w-100 rounded-bottom">
                                        <div class="toast-header">
                                            <span id="alert_title" class="text-danger">
                                                <i class="fa-solid fa-circle-xmark pe-2"></i><b>${alertTitle}</b>
                                            </span>
                                        </div>
                                        <div class="toast-body bg-white text-center rounded-bottom">
                                            <span id="alert_msg" class=""><b>${msg}</b></span>
                                        </div>
                                    </div>
                                </div>`

        $('body').append(alert_html);

        const toast = document.getElementById("alert_toast");

        $("#alert_toast").toast("show");

        toast.addEventListener('hide.bs.toast', () => {

            $("#alert_toast").addClass("fadeSlow");

        })

        toast.addEventListener('show.bs.toast', () => {

            $("#alert_toast").removeClass("fadeSlow");

        })
    },
    confirm: function (msg, callback) {
        const confirmTitle = "알림"

        const confirm_html = `<div class="toast-container position-absolute top-0 start-50 translate-middle-x pt-5">
                                    <div id="confirm_toast" class="toast w-100 rounded-bottom">
                                        <div class="toast-header">
                                            <span id="confirm_title" class="text-danger">
                                                <i class="fa-solid fa-exclamation pe-2"></i><b>${confirmTitle}</b>
                                            </span>
                                        </div>
                                        <div class="toast-body bg-white text-center rounded-bottom">
                                            <span id="confirm_msg" class=""><b>${msg}</b></span>
                                        </div>
                                      
                                        <hr class="bg-white m-0" />
        
                                        <div class="bg-white p-2 btn-group col-12">
                                            <button id="toast_ok" type="button" class="btn btn-border text-success"><i class="fa-solid fa-check"></i></button>
                                            <button id="toast_deny" type="button" class="btn btn-border text-danger"><i class="fa-solid fa-xmark"></i></button>
                                        </div>
                                    </div>
                                </div>`;

        $('body').append(confirm_html);

        var toastElem = document.getElementById("confirm_toast")

        var toast = new bootstrap.Toast(toastElem, {
            autohide: false
        });

        toast.show();

        if (msg == null || msg.trim() == "") {
            console.warn("confirm message is empty.");
            return;
        } else if (callback == null || typeof callback != 'function') {
            console.warn("callback is null or not function.");
            return;
        } else {
            $("#toast_ok").on("click", function () {
                $(this).unbind("click");
                callback(true)
            })

            $("#toast_deny").on("click", function () {
                $(this).unbind("click");
                toast.dispose();
            })
        }
    }
}