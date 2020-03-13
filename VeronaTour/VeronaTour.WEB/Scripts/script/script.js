$(document).ready(function () {
    //Details
    $('#horizontalTab').easyResponsiveTabs({
        type: 'default', //Types: default, vertical, accordion
        width: 'auto', //auto or any width like 600px
        fit: true, // 100% fit in a container
        closed: 'accordion', // Start closed if in accordion view
        activate: function (event) { // Callback function if tab is switched
            var $tab = $(this);
            var $info = $('#tabInfo');
            var $name = $('span', $info);
            $name.text($tab.text());
            $info.show();
        }
    });

    //IndexCarousel
    $('.owl-carousel').owlCarousel({
        loop: true,
        margin: 10,
        responsiveClass: true,
        responsive: {
            0: {
                items: 1,
                nav: true
            },
            600: {
                items: 2,
                nav: false
            },
            900: {
                items: 2,
                nav: false
            },
            1000: {
                items: 3,
                nav: true,
                loop: false,
                margin: 20
            }
        }
    })
});


//Slider
var slider = document.getElementById("myRange");
var output = document.getElementById("demo");
if (slider) {
    output.innerHTML = slider.value;

    slider.oninput = function () {
        output.innerHTML = this.value;
    }
}

//Alert
function showSuccessAlert() {
    swal("You ordered a tour", "Go to the orders page to place an order!", "success")
        .then((result) => {
            if (result) {
                $("#add-form").submit();
            }
        });
}
function showRegistrationAlert() {
    swal({
        title: "In order to book a tour you need to register!",
        type: "info",
        showCancelButton: true,
        confirmButtonText: "Registration",
        confirmButtonColor: "#ff0055",
        cancelButtonColor: "#999999",
        reverseButtons: true,
        focusConfirm: false,
        focusCancel: true

    }).then((result) => {
        if (result.value === true) {

            window.location.href = "/Account/Register";
        }
    });
}
function ResetFilters() {

    window.location.href = "/Home/Tours";
}

//Paging
function ChangePage(number) {
    document.getElementById("SelectedPage").value = number;
    document.getElementById("FindATour").submit();
}

//Phonemask
jQuery(function ($) {
    $("#phone").mask("+99999999999");
});

function Latin(obj) {
    if (/^[a-zA-Z0-9\_,.\-:"()]*?$/.test(obj.value))
        obj.defaultValue = obj.value;
    else
        obj.value = obj.defaultValue;
}