

function toast(e, f) {
    toastr.options.timeOut = 5000;
    toastr.options.extendedTimeOut = 1000;
    toastr.options.positionClass = 'toast-top-center'

    switch (f) {
        case "Success":
            toastr.success(e);
            break;
        case "Error":
            toastr.error(e);
            break;
        case "Info":
            toastr.info(e);
            break;
        case "Warning":
            toastr.warning(e);
            break;
    }
};

function ilovemyself() {
    alert("Hello Updated");
};

function toastMsg(msg,type)
{
    var Toast = Swal.mixin({
        toast: true,
        position: 'top',
        showConfirmButton: false,
        timer: 5000
    });

    Toast.fire({
        icon: type,
        title: ' ' + msg
    })
 
}



